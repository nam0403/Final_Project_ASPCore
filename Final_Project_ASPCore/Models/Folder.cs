using FilesCRUD.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FolderCRUD.Models
{
    public class Folder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string ModifiedBy { get; set; } = null!;
        public DateTime? ModifiedAt { get; set; } = null;

        public List<Files> Files { get; set; } = null!;
        public List<Folder> Folders { get; set; } = null!;
        [NotMapped]
        public Folder RootFolder;
        [Required]
        public string type { get; set; } = null!;
    }
}
