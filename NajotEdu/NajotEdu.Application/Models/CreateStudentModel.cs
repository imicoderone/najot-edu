using System.ComponentModel.DataAnnotations;

namespace NajotEdu.Application.Models
{
    public class CreateStudentModel
    {
        [Required]
        public string Fullname { get; set; }
        public DateTime BirthDate { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
