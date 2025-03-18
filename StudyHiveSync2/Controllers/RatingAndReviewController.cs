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
    public class RatingAndReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RatingAndReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<RatingAndReviewDto>>> GetRatingAndReviews()
        {
            return await _context.RatingAndReviews
                .Select(ratingAndReview => new RatingAndReviewDto
                {
                    RatingAndReviewId = ratingAndReview.RatingAndReviewId,
                    UserId = ratingAndReview.UserId,
                    Rating = ratingAndReview.Rating,
                    Review = ratingAndReview.Review,
                    CourseId = ratingAndReview.CourseId
                })
                .ToListAsync();
        }

        //[Authorize(Roles = "User,Admin")]
        [HttpGet("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<RatingAndReviewDto>> GetRatingAndReview(int id)
        {
            var ratingAndReview = await _context.RatingAndReviews
                .Select(ratingAndReview => new RatingAndReviewDto
                {
                    RatingAndReviewId = ratingAndReview.RatingAndReviewId,
                    UserId = ratingAndReview.UserId,
                    Rating = ratingAndReview.Rating,
                    Review = ratingAndReview.Review,
                    CourseId = ratingAndReview.CourseId
                })
                .FirstOrDefaultAsync(r => r.RatingAndReviewId == id);

            if (ratingAndReview == null)
            {
                return NotFound();
            }

            return Ok(ratingAndReview);
        }

        //[Authorize(Roles = "User")]
        //[HttpPost]
        //public async Task<ActionResult<RatingAndReviewDto>> PostRatingAndReview(RatingAndReviewDto ratingAndReviewDto)
        //{
        //    if (_context == null)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Database context is not available.");
        //    }

        //    var user = await _context.Users.FindAsync(ratingAndReviewDto.UserId);
        //    if (user == null)
        //    {
        //        return BadRequest("Invalid UserId.");
        //    }

        //    var course = await _context.Courses.FindAsync(ratingAndReviewDto.CourseId);
        //    if (course == null)
        //    {
        //        return BadRequest("Invalid CourseId.");
        //    }

        //    var ratingAndReview = new RatingAndReview
        //    {
        //        UserId = ratingAndReviewDto.UserId,
        //        Rating = ratingAndReviewDto.Rating,
        //        Review = ratingAndReviewDto.Review,
        //        CourseId = ratingAndReviewDto.CourseId,
        //        User = user,
        //        Course = course
        //    };

        //    _context.RatingAndReviews.Add(ratingAndReview);
        //    await _context.SaveChangesAsync();

        //    ratingAndReviewDto.RatingAndReviewId = ratingAndReview.RatingAndReviewId;

        //    return CreatedAtAction("GetRatingAndReview", new { id = ratingAndReviewDto.RatingAndReviewId }, ratingAndReviewDto);
        //}


        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<RatingAndReviewDto>> PostRatingAndReview(RatingAndReviewDto ratingAndReviewDto)
        {
            var ratingAndReview = new RatingAndReview
            {
                UserId = ratingAndReviewDto.UserId,
                Rating = ratingAndReviewDto.Rating,
                Review = ratingAndReviewDto.Review,
                CourseId = ratingAndReviewDto.CourseId,
                User = await _context.Users.FindAsync(ratingAndReviewDto.UserId),
                Course = await _context.Courses.FindAsync(ratingAndReviewDto.CourseId)
            };

            _context.RatingAndReviews.Add(ratingAndReview);
            await _context.SaveChangesAsync();

            ratingAndReviewDto.RatingAndReviewId = ratingAndReview.RatingAndReviewId;

            return CreatedAtAction("GetRatingAndReview", new { id = ratingAndReviewDto.RatingAndReviewId }, ratingAndReviewDto);
        }

        //[Authorize(Roles = "User")]
        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PutRatingAndReview(int id, RatingAndReviewDto ratingAndReviewDto)
        {
            if (id != ratingAndReviewDto.RatingAndReviewId)
            {
                return BadRequest();
            }

            var ratingAndReview = await _context.RatingAndReviews.FindAsync(id);
            if (ratingAndReview == null)
            {
                return NotFound();
            }

            ratingAndReview.UserId = ratingAndReviewDto.UserId;
            ratingAndReview.Rating = ratingAndReviewDto.Rating;
            ratingAndReview.Review = ratingAndReviewDto.Review;
            ratingAndReview.CourseId = ratingAndReviewDto.CourseId;

            _context.Entry(ratingAndReview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingAndReviewExists(id))
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

        //[Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteRatingAndReview(int id)
        {
            var ratingAndReview = await _context.RatingAndReviews.FindAsync(id);
            if (ratingAndReview == null)
            {
                return NotFound();
            }

            _context.RatingAndReviews.Remove(ratingAndReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingAndReviewExists(int id)
        {
            return _context.RatingAndReviews.Any(e => e.RatingAndReviewId == id);
        }

        //[Authorize(Roles = "Admin")]
        //[HttpGet("allfeedback/{userId}")]
        //public async Task<ActionResult<IEnumerable<RatingAndReview>>> GetAllFeedback(int userId)
        //{
        //    var feedback = await _context.RatingAndReviews
        //        .Where(r => r.UserId == userId)
        //        .ToListAsync();

        //    if (feedback == null)
        //    {
        //        return NotFound();
        //    }
        //    return feedback;
        //}
    }
}