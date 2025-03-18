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
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            return await _context.Categories
                .Select(category => new CategoryDto
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _context.Categories
                .Select(category => new CategoryDto
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description
                })
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto categoryDto)
        //{
        //    var category = new Category
        //    {
        //        Name = categoryDto.Name,
        //        Description = categoryDto.Description,
        //        Courses = new List<Course>() // Initialize the required Courses property
        //    };

        //    _context.Categories.Add(category);
        //    await _context.SaveChangesAsync();

        //    categoryDto.CategoryId = category.CategoryId;

        //    return CreatedAtAction("GetCategory", new { id = categoryDto.CategoryId }, categoryDto);
        //}

        //[HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> PutCategory(int id, CategoryDto categoryDto)
        //{
        //    if (id != categoryDto.CategoryId)
        //    {
        //        return BadRequest();
        //    }

        //    var category = await _context.Categories.FindAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    category.Name = categoryDto.Name;
        //    category.Description = categoryDto.Description;
        //    category.Courses = category.Courses ?? new List<Course>(); // Ensure Courses is initialized

        //    _context.Entry(category).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CategoryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> DeleteCategory(int id)
        //{
        //    var category = await _context.Categories.FindAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Categories.Remove(category);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}