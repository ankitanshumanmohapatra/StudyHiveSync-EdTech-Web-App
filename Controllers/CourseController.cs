using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyHiveSync2.Data;
using StudyHiveSync2.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyHiveSync2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            

            return await _context.Courses
                .Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    InstructorId = c.InstructorId,
                    Price = c.Price,
                    //Tag = course.Tag != null ? course.Tag.ToList() : new List<string>(),
                    CategoryId = c.CategoryId
                })
                .ToListAsync();
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            

            var course = await _context.Courses
                .Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    InstructorId = c.InstructorId,
                    Price = c.Price,
                    //Tag = c.Tag != null ? c.Tag.ToList() : new List<string>(),
                    CategoryId = c.CategoryId
                })
                .FirstOrDefaultAsync(c => c.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]

        public async Task<ActionResult<CourseDto>> PostCourse(CourseDto courseDto)
        {
            if (_context == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database context is not available.");
            }

            var instructor = await _context.Users.FirstOrDefaultAsync(u => u.UserId == courseDto.InstructorId);
            if (instructor == null)
            {
                return BadRequest("Invalid InstructorId.");
            }
            var category = await _context.Categories.FindAsync(courseDto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid CategoryId.");
            }

            var course = new Course
            {
                CourseName = courseDto.CourseName,
                CourseDescription = courseDto.CourseDescription,
                InstructorId = courseDto.InstructorId,
                Price = courseDto.Price,
                Tag = courseDto.Tag,
                CategoryId = courseDto.CategoryId,
                Instructor = instructor,
                Category = category,
                StudentsEnrolled = new List<User>()
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            courseDto.CourseId = course.CourseId;

            return CreatedAtAction("GetCourse", new { id = courseDto.CourseId }, courseDto);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> PutCourse(int id, CourseDto courseDto)
        {
            if (_context == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database context is not available.");
            }

            if (id != courseDto.CourseId)
            {
                return BadRequest();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            course.CourseName = courseDto.CourseName;
            course.CourseDescription = courseDto.CourseDescription;
            course.InstructorId = courseDto.InstructorId;
            course.Price = courseDto.Price;
            course.Tag = courseDto.Tag;
            course.CategoryId = courseDto.CategoryId;
            course.Instructor = await _context.Users.FindAsync(courseDto.InstructorId);
            course.Category = await _context.Categories.FindAsync(courseDto.CategoryId);
            course.StudentsEnrolled = course.StudentsEnrolled ?? new List<User>();

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database context is not available.");
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }


        //New method to get courses sorted by price
        [HttpGet("GetCoursesByPrice")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByPrice()
        {
            return await _context.Courses
                .OrderBy(c => c.Price)
                .Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    InstructorId = c.InstructorId,
                    Price = c.Price,
                    CategoryId = c.CategoryId
                })
                .ToListAsync();
        }

        // New method to search courses by name
        [HttpGet("SearchCoursesByName")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> SearchCoursesByName(string courseName)
        {
            return await _context.Courses
                .Where(c => c.CourseName.Contains(courseName))
                .Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    InstructorId = c.InstructorId,
                    Price = c.Price,
                    CategoryId = c.CategoryId
                })
                .ToListAsync();
        }
    }
}