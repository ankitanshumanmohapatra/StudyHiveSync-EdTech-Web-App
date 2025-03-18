using System.Collections.Generic; // Using generic collections like List, ICollection, etc.
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static System.Collections.Specialized.BitVector32;
namespace StudyHiveSync2.Model;

public class Course
{
    [Key]
    public int CourseId { get; set; }
    public required string CourseName { get; set; }
    public required string CourseDescription { get; set; }
    
    public int InstructorId { get; set; }
    [ForeignKey("InstructorId")]
    public required User Instructor { get; set; }
    public decimal Price { get; set; }
    public ICollection<string> Tag { get; set; } = new List<string>();
    
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public required Category Category { get; set; }
    public required ICollection<User> StudentsEnrolled { get; set; } = new List<User>();

    [JsonIgnore]
    public ICollection<RatingAndReview> RatingAndReviews { get; set; } = new List<RatingAndReview>();
}


//public class Course // Defining the Course class.
//{
//    [Key]// Specifies that this property is the primary key.
//    public int CourseId { get; set; } // Unique identifier for the course. 

//    public required string CourseName { get; set; } //name of course
//    public required string CourseDescription { get; set; } //description of course

//    [ForeignKey("User")] //specifies that this property is foreign key for User Entity.
//    public int InstructorId { get; set; } // Foreign key to the User entity representing the instructor.
//    public required User Instructor { get; set; } // Navigation property to the User entity representing the instructor.

//    //public required string WhatYouWillLearn { get; set; } // Description of what students will learn in the course.

//    //public required ICollection<Section> CourseContent { get; set; } = new List<Section>(); // Collection of sections that make up the course content, initialized to a new list.
//    public ICollection<RatingAndReview> RatingAndReviews { get; set; } = new List<RatingAndReview>(); // Collection of ratings and reviews for the course, initialized to a new list.
//    public decimal Price { get; set; } //price of course
//    //public required string Thumbnail { get; set; } //url or path to the course thumbnail image.
//    public ICollection<string> Tag { get; set; } = new List<string>(); // Collection of tags associated with the course, initialized to a new list.

//    [ForeignKey("Category")] // Specifies that this property is a foreign key to the Category entity.
//    public int CategoryId { get; set; }//Foreign key to the Category entity.
//    public required Category Category { get; set; }/// Navigation property to the Category entity.

//    public required ICollection<User> StudentsEnrolled { get; set; } = new List<User>(); // Collection of users enrolled in the course, initialized to a new list.
//    public ICollection<string> Instructions { get; set; } = new List<string>();// Collection of instructions for the course, initialized to a new list.
//    //public required string Status { get; set; }// Status of the course (e.g., active, inactive).
//}