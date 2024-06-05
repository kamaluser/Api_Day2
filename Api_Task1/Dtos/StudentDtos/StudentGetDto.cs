﻿namespace Api_Task1.Dtos.StudentDtos
{
    public class StudentGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
