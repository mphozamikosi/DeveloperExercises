using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Test1.Models
{
    public class Student
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
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Cell Number must be numbers and exactly 10 characters long.")]
        [DisplayName("Cellphone Number")]
        public string CellNumber { get; set; }
    }
}
