using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudyHiveSync2.Model;
namespace StudyHiveSync2.Model;
public class Section
{
    [Key]
    public int SectionId { get; set; }

    public required string Title { get; set; }//title of video
    public required string TimeDuration { get; set; }//duration of section video
    public required string Description { get; set; }//description of section count

    [ForeignKey("Course")]
    public int CourseId { get; set; }
    public required Course Course { get; set; }
    public required string VideoUrl { get; set; } //url of video for the description
}