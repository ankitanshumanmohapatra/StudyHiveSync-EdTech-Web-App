namespace StudyHiveSync2.Model
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int InstructorId { get; set; }
        public decimal Price { get; set; }
        public List<string> Tag { get; set; }
        public int CategoryId { get; set; }
        //public string Status { get; set; }
    }
}
