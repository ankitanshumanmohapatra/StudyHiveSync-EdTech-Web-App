using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudyHiveSync2.Model
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public required User User { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public required Course Course { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }

    }

}
