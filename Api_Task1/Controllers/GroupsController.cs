using Api_Task1.Data;
using Api_Task1.Dtos.GroupDtos;
using Microsoft.AspNetCore.Mvc;
using Group = Api_Task1.Data.Entities.Group;

namespace Api_Task1.Controllers
{
    public class GroupsController : Controller
    {
        private readonly AppDbContext _context;

        public GroupsController(AppDbContext context)
        {
            _context = context;
        }
        [Route("")]
        [HttpGet]
        public ActionResult<List<GroupGetDto>> GetAll(int page=1,int pageSize = 3)
        {
            List<GroupGetDto> dtos = _context.Groups.Where(x => !x.IsDeleted).Skip((page - 1) * pageSize).Take(pageSize).Select(x => new GroupGetDto
            {
                Id = x.Id,
                No = x.No,
                Limit = x.Limit
            }).ToList();
            //var paginatedGroups = _context.Groups.Skip((page-1)*pageSize).Take(pageSize).ToList();

            return StatusCode(200, dtos);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<GroupGetDto> GetById(int id)
        {
            var data = _context.Groups.FirstOrDefault(x=>x.Id == id && !x.IsDeleted);

            if (data == null)
            {
                return StatusCode(404);
            }

            GroupGetDto dto = new GroupGetDto
            {
                Id = data.Id,
                No = data.No,
                Limit = data.Limit
            };
            return StatusCode(200, dto);
        }
        [Route("")]
        [HttpPost]
        public ActionResult CreateGroup(GroupCreateDto groupDto)
        {
            if(_context.Groups.Any(x=>x.No == groupDto.No && !x.IsDeleted))
            {
                return StatusCode(409);
            }
            var group = new Group
            {
                No = groupDto.No,
                Limit = groupDto.Limit,
                CreatedAt = DateTime.Now
            };

            _context.Groups.Add(group);
            _context.SaveChanges();

            return StatusCode(201, new {Id=group.Id});
        }
        [Route("{id}")]
        [HttpPut]
        public ActionResult EditGroup(int id, GroupEditDto dto)
        {
            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }
            if (dto.No != null)
            {
                group.No = dto.No;
            }
            if(dto.Limit != null)
            {
                group.Limit = dto.Limit.Value;
            }
            group.ModifiedAt = DateTime.Now;

            _context.SaveChanges();

            return NoContent();
        }


        [Route("{id}")]
        [HttpDelete]
        public ActionResult DeleteGroup(int id)
        {
            var group = _context.Groups.FirstOrDefault(x => x.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            group.IsDeleted = true;
            _context.Groups.Remove(group);
            _context.SaveChanges();

            return StatusCode(204);
        }
    }
}
