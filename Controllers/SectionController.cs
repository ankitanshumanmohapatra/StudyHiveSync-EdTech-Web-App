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
    public class SectionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<SectionDto>>> GetSections()
        {
            return await _context.Sections
                .Select(section => new SectionDto
                {
                    SectionId = section.SectionId,
                    Title = section.Title,
                    TimeDuration = section.TimeDuration,
                    Description = section.Description,
                    CourseId = section.CourseId,
                    VideoUrl = section.VideoUrl
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<SectionDto>> GetSection(int id)
        {
            var section = await _context.Sections
                .Select(section => new SectionDto
                {
                    SectionId = section.SectionId,
                    Title = section.Title,
                    TimeDuration = section.TimeDuration,
                    Description = section.Description,
                    CourseId = section.CourseId,
                    VideoUrl = section.VideoUrl
                })
                .FirstOrDefaultAsync(s => s.SectionId == id);

            if (section == null)
            {
                return NotFound();
            }

            return section;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SectionDto>> PostSection(SectionDto sectionDto)
        {
            var section = new Section
            {
                Title = sectionDto.Title,
                TimeDuration = sectionDto.TimeDuration,
                Description = sectionDto.Description,
                CourseId = sectionDto.CourseId,
                VideoUrl = sectionDto.VideoUrl,
                Course = await _context.Courses.FindAsync(sectionDto.CourseId) // Set the required Course property
            };

            _context.Sections.Add(section);
            await _context.SaveChangesAsync();

            sectionDto.SectionId = section.SectionId;

            return CreatedAtAction("GetSection", new { id = sectionDto.SectionId }, sectionDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutSection(int id, SectionDto sectionDto)
        {
            if (id != sectionDto.SectionId)
            {
                return BadRequest();
            }

            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            section.Title = sectionDto.Title;
            section.TimeDuration = sectionDto.TimeDuration;
            section.Description = sectionDto.Description;
            section.CourseId = sectionDto.CourseId;
            section.VideoUrl = sectionDto.VideoUrl;

            _context.Entry(section).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(id))
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
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SectionExists(int id)
        {
            return _context.Sections.Any(e => e.SectionId == id);
        }
    }
}



//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using StudyHiveSync2.Data;
//using StudyHiveSync2.Model;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace StudyHiveSync2.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SectionsController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public SectionsController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Section>>> GetSections()
//        {
//            return await _context.Sections.ToListAsync();
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Section>> GetSection(int id)
//        {
//            var section = await _context.Sections.FindAsync(id);

//            if (section == null)
//            {
//                return NotFound();
//            }

//            return section;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Section>> PostSection(Section section)
//        {
//            _context.Sections.Add(section);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetSection", new { id = section.SectionId }, section);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutSection(int id, Section section)
//        {
//            if (id != section.SectionId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(section).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!SectionExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteSection(int id)
//        {
//            var section = await _context.Sections.FindAsync(id);
//            if (section == null)
//            {
//                return NotFound();
//            }

//            _context.Sections.Remove(section);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool SectionExists(int id)
//        {
//            return _context.Sections.Any(e => e.SectionId == id);
//        }
//    }
//}




//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace StudyHiveSync2.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SectionController : ControllerBase
//    {
//    }
//}
