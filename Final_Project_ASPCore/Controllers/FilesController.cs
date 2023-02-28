using ApplicationCRUD.Data;
using FilesCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace FilesCRUD.Controllers
{
    public class FilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FilesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Files> objCatlist = _context.Files;
            return View(objCatlist);
        }

        public IActionResult Create_File()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_File(Files empobj)
        {
            if (ModelState.IsValid)
            {
                _context.Files.Add(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction("Index");
            }
            return View(empobj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.Files.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }
        
        public IActionResult EditConfirmed(Files empobj)
        {
            if (ModelState.IsValid)
            {
                _context.Files.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "File cannot edit!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var files = await _context.Files
                .FirstOrDefaultAsync(m => m.Id == id);
            if (files == null)
            {
                return NotFound();
            }

            return View(files);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile f)
        {
            try
            {
                var model = new Files();
                string extension;

				if (f != null && f.Length != 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var filePath = Path.Combine(
                        path, Path.GetFileName(f.FileName));

                    using (var stream = new FileStream(
                        filePath, FileMode.Create))
                    {
                        await f.CopyToAsync(stream);
                    }
                    var existingFile = await _context.Files.FirstOrDefaultAsync(x => x.Name == f.FileName);
                    if (existingFile != null)
                    {
                        TempData["Message"] = "File already exists in the database. Please choose another file!";
						return RedirectToAction("Index");
                    }
                    /*using (var ms = new MemoryStream())
                    {
                        Icon icon = Icon.ExtractAssociatedIcon(f.FileName);
                        icon.Save(ms);
                        byte[] iconData = ms.ToArray();
                    }*/
                    model.Name = f.FileName;
					extension = Path.GetExtension(f.FileName).TrimStart('.'); ;
                    model.Extension = extension;
					model.ModifiedAt = DateTime.Now;
                    model.ModifiedBy = User.FindFirst("name").Value;
                    model.type = "file";
                    _context.Files.Add(model);
                    await _context.SaveChangesAsync();
                    TempData["ResultOk"] = "Data Upload Successfully !";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("file", "Please select a file to upload.");
                return View(model);
            }
            catch
            {
                TempData["Message"] = "File cannot upload!";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DownloadFile(int? id)
        {
            try
            {
                var file = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);
                string path;
                if (file != null)
                {
                    path = _webHostEnvironment.WebRootPath + "\\uploads\\" + file.Name;
                    TempData["Message"] = "File downloaded successfully";
                    return File(System.IO.File.ReadAllBytes(path), "image/jpeg", Path.GetFileName(path));
                }
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Message"] = "Cannot find file to download. Please try again later!";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Files
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Files == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }
            _context.Files.Remove(await _context.Files.FindAsync(id));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
