using Microsoft.EntityFrameworkCore;
using FilesCRUD.Models;
using FolderCRUD.Models;

namespace ApplicationCRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Files> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }
    }
}