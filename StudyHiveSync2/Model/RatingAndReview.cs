using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudyHiveSync2.Model;
namespace StudyHiveSync2.Model;
public class RatingAndReview
{
    [Key]
    public int RatingAndReviewId { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public required User User { get; set; }

    [Required]
    public int Rating { get; set; }

    [Required]
    public required string Review { get; set; }

    [ForeignKey("Course")]
    public int CourseId { get; set; }
    public required Course Course { get; set; }
}