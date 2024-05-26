using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Test1.Models
{
    public class SimpleStudent
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        [DisplayName("First Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Surname { get; set; }
        [Required]
        [RegularExpression(@"^.{10}$", ErrorMessage = "The field must be exactly 10 characters long.")]
        [Phone]
        [DisplayName("Cellphone Number")]
        public string CellNumber { get; set; }
    }
}
