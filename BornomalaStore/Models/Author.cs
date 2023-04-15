using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BornomalaStore.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [NotMapped]
        [Display(Name = "Choose Image")]
        public IFormFile ImageFile { get; set; }
    }
}
