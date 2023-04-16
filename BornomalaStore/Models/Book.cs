using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BornomalaStore.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BookTitle { get; set; }
        [Required]
        public string Language { get; set; }

        public string Path { get; set; }
        
        [NotMapped]
        [Display(Name ="Choose Image")]
        public IFormFile ImagePath { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string AuthorAddress { get; set; }
        public string AuthorPath { get; set; }
        [Display(Name = "Choose Image")]
        [NotMapped]
        public IFormFile AuthorImage { get; set; }
    }
}
