namespace NajotEdu.Application.Models
{
    public class UpdateStudentModel
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
