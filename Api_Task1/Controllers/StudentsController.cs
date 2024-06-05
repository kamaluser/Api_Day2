using Api_Task1.Data;
using Api_Task1.Data.Entities;
using Api_Task1.Dtos.StudentDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Task1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public ActionResult<List<StudentGetDto>> GetAll(int page = 1, int pageSize = 3)
        {
            var students = _context.Students
                .Where(x=>!x.IsDeleted)
                .Skip((page-1)*pageSize).Take(pageSize).Select(x=>new StudentGetDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    BirthDate = x.BirthDate,
                    GroupId = x.GroupId,
                    GroupName = x.Group.No
                }).ToList();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<StudentGetDto> GetById(int id)
        {
            var student = _context.Students.Include(x=>x.Group)
                .FirstOrDefault(x=> x.Id == id && !x.IsDeleted);


            if (student == null)
            {
                return NotFound();
            }

            var dto = new StudentGetDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                BirthDate = student.BirthDate,
                GroupId = student.GroupId,
                GroupName = student.Group.No
            };

            return Ok(dto);
        }

        [HttpPost("")]
        public ActionResult CreateStudent(StudentCreateDto dto)
        {
            if (_context.Students.Any(x => x.Email == dto.Email && !x.IsDeleted))
            {
                return StatusCode(409, "A student with the same email already exists.");
            }


            var student = new Student
            {
                FullName = dto.FullName,
                Email = dto.Email,
                BirthDate = dto.BirthDate,
                GroupId = dto.GroupId,
                CreatedAt = DateTime.Now
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            return StatusCode(201, new { Id = student.Id });
        }

        [HttpPut("{id}")]
        public ActionResult EditStudent(int id, StudentEditDto dto)
        {
            var student = _context.Students.FirstOrDefault(x=>x.Id == id && !x.IsDeleted);
            if (student == null)
            {
                return NotFound();
            }

            if (dto.FullName != null)
            {
                student.FullName = dto.FullName;
            }

            if (dto.Email != null)
            {
                student.Email = dto.Email;
            }

            if (dto.BirthDate != null)
            {
                student.BirthDate = dto.BirthDate.Value;
            }

            if (dto.GroupId != null)
            {
                student.GroupId = dto.GroupId.Value;
            }

            student.ModifiedAt = DateTime.Now;

            _context.SaveChanges();

            return StatusCode(204);
        }



        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (student == null)
            {
                return NotFound();
            }

            student.IsDeleted = true;
            _context.SaveChanges();

            return StatusCode(204);
        }
    }
}
