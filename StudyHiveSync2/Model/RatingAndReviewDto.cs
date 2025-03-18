namespace StudyHiveSync2.Model
{
    public class RatingAndReviewDto
    {
        public int RatingAndReviewId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public int CourseId { get; set; }
    }
}
