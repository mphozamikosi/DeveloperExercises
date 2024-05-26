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
        [StringLength(10, ErrorMessage = "Cell Number must be exactly 10 characters long.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
        [DisplayName("Cellphone Number")]
        public string CellNumber { get; set; }
    }
}
