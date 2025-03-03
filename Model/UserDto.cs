using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudyHiveSync2.Model
{
    public class UserDto
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required]
        [StringLength(100)]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string AccountType { get; set; }
        public required string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        //public int RoleId { get; set; }
        //public Role Role { get; set; }
    }

    //public class UserDto
    //{
    //    public int UserId { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //    public string AccountType { get; set; }
    //    public string Image { get; set; }
    //    public DateTime CreatedAt { get; set; }
    //    public DateTime UpdatedAt { get; set; }
    //}
}