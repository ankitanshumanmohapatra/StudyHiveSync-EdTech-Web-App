using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudyHiveSync2.Model;
namespace StudyHiveSync2.Model;
public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    public required string Name { get; set; }

    public required string Description { get; set; }

    public required ICollection<Course> Courses { get; set; } = new List<Course>();
}