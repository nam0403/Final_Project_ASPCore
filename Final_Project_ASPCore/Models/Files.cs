using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilesCRUD.Models
{
    public class Files
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "File Name")]
        public string Name { get; set; } = null!;
        public string ModifiedBy { get; set; } = null!;
        [DataType(DataType.MultilineText)]
        public DateTime? ModifiedAt { get; set; }
        [Required]
        public string Extension { get; set; } = null!;
        [Required]
        public string type { get; set; } = null!;
    }
}