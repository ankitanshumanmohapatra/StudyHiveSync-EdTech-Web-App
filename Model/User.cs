using System; //Importing for namespace
using System.Collections.Generic; //Using generic collections like List, ICollection,etc.
using System.ComponentModel.DataAnnotations; //imports namespace for Data Annotation attribrute.
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;//importing namespace for DB schema-related attributes
using System.Text.Json.Serialization;
namespace StudyHiveSync2.Model;//namespace for model
public class User
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
    public required string AccountType { get; set; } = "User";
    public required string Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    //[JsonIgnore]
    //public ICollection<Course> Courses { get; set; }
    [JsonIgnore]
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    //public int RoleId { get; set; }
    //public Role Role { get; set; }
}




//public class User
//{
//    [Key]
//    public int UserId { get; set; }
//    [Required]
//    [StringLength(100)]
//    public required string Name { get; set; }
//    [Required]
//    [StringLength(100)]
//    public required string Email { get; set; }
//    [Required]
//    public required string Password { get; set; }
//    [Required]
//    public required string AccountType { get; set; }
//    public required string Image { get; set; }
//    public DateTime CreatedAt { get; set; }
//    public DateTime UpdatedAt { get; set; }

//    [JsonIgnore]
//    public ICollection<Course> Courses { get; set; } = new List<Course>();
//}




















//public class User // Defining the User class.
//{
//    [Key] // Specifies that this property is the primary key.
//    public int UserId { get; set; } // Unique identifier for the user.

//    [Required] // Specifies that this property is required.
//    [StringLength(100)] // Sets the maximum length of the string to 100 characters.
//    public required string Name { get; set; }//First name of user

//    [Required]
//    [StringLength(100)]
//    public required string Email { get; set; }//Email address of User

//    [Required]
//    public required string Password { get; set; }//Password for user account

//    [Required]
//    public required string AccountType { get; set; }//Type of account user, instructor,admin
//    public ICollection<Course> Courses { get; set; } = new List<Course>();//Collection of courses associated with user, initialized by a new list
//    public required string Image { get; set; }//url or path to user profile image.
//    public DateTime CreatedAt { get; set; }//TimeStamp to when the user was created.
//    public DateTime UpdatedAt { get; set; }//Timestamp for when the user was last updated.
//}
