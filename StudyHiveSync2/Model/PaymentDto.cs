namespace StudyHiveSync2.Model
{
    public class PaymentDto
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
