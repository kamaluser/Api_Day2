namespace Api_Task1.Dtos.StudentDtos
{
    public class StudentCreateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int GroupId { get; set; }
    }
}
