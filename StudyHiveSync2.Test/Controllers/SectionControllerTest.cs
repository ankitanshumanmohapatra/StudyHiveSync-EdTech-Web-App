using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyHiveSync2.Controllers;
using StudyHiveSync2.Data;
using StudyHiveSync2.Model;

namespace StudyHiveSync2.Test.Controllers
{
    [TestFixture]                                                                                                    //indicates class contains test
    public class SectionControllerTest : IDisposable                                                            //disposable method to clean up resources
    {
        private ApplicationDbContext _context;
        private SectionsController _controller;

        [SetUp] 
        public void Setup()                                                   //this method runs before each test to clear existing data from the database.
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new ApplicationDbContext(options);
            _controller = new SectionsController(_context);

            ClearDatabase();
            SeedDatabase();                                                   //adds initial data to db for testing
        }

        private void ClearDatabase()                                      //removes all entities from sections, users  categores and courses
        {
            _context.Sections.RemoveRange(_context.Sections);
            _context.Users.RemoveRange(_context.Users);
            _context.Categories.RemoveRange(_context.Categories);
            _context.Courses.RemoveRange(_context.Courses);

            _context.SaveChanges();
        }

        private void SeedDatabase()                                         //add user, category,course,section to in-memory db
        {
            var user = new User()
            {
                UserId = 1,
                Name = "User",
                Email = "test@gmail.com",
                Password = "Test@123",
                AccountType = "Instructer",
                Image = "https://www.google.com",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Courses = []
            };

            var category = new Category()
            {
                Name = "Test Category",
                Description = "Test Description",
                Courses = []
            };

            var course = new Course()
            {
                CourseId = 1,
                CourseName = "Course Test",
                CourseDescription = "Course Description Test",
                InstructorId = 1,
                Price = 100.0M,
                Tag = [],
                CategoryId = 1,
                StudentsEnrolled = [],
                RatingAndReviews = [],
                Instructor = user,
                Category = category
            };
            var section = new Section()
            {
                SectionId = 1,
                Title = "Section Test",
                TimeDuration = "10 Min",
                Description = "Description Test",
                CourseId = 1,
                VideoUrl = "https://www.youtube.com/watch?v=123456",
                Course = course
            };

            category.Courses = new List<Course>() { course };

            _context.Users.Add(user);
            _context.Categories.Add(category);
            _context.Courses.Add(course);
            _context.Sections.Add(section);

            _context.SaveChanges();

        }

        [Test]                                                                   //test runner will execute this
        public async Task GetSection_WhenCalled_ReturnsSection()                //method is asynchronous and returns a task
        {
            var result = await _controller.GetSection(1);                   // Calls the GetSection method of the controller with section ID 1 and awaits the result.
            var section = result.Value;                                     // Extracts the value (section) from the result.
            if (section == null)                                            //checks id section is null
            {
                Assert.Fail("No Section found");                            // Fails the test if no section is found.
            }
            else
            {
                Assert.That(section.SectionId, Is.EqualTo(1));
            }
        }


        [Test]
        public async Task PostSection_WhenCalled_AddsSection()
        {
            var section = new SectionDto()                                   //create section dto object
            {
                Title = "Section Test 2",                                    //set properties
                TimeDuration = "10 Min",
                Description = "Description Test",
                CourseId = 1,
                VideoUrl = "https://www.youtube.com/watch?v=123456"
            };
            var result = await _controller.PostSection(section);              // Calls the PostSection method of the controller with the new section and awaits the result.
            var actionResult = result.Result as CreatedAtActionResult;        // Casts the result to CreatedAtActionResult.
            var createdSection = actionResult.Value as SectionDto;            // Extracts the created section from the action result.
            Assert.That(createdSection.SectionId, Is.EqualTo(2));             // Asserts that the SectionId of the created section is 2.
        }

        [Test]
        public async Task GetSections_WhenCalled_ReturnsAllSections()         // The method is asynchronous and returns a Task.
        {
            var result = await _controller.GetSections();                     // Calls the GetSections method of the controller and awaits the result.
            var sections = result.Value;
            Assert.That(sections.Count, Is.EqualTo(1));                       // Asserts that the count of sections is 1.
        }

        






        //[Test]
        //public async Task GetSection_WhenCalled_DeleteSection()
        //{
        //    var result = await _controller.GetSections();
        //    var sections = result.Value;
        //    Assert.That(sections.Count, Is.EqualTo(1));
        //}

        [TearDown] //Runs after each case to clean up resources
        public void Dispose() //disposes db context to clean up resources.
        {
            _context.Dispose();
        }
    }
}
//Assertions: The Assert class is used to verify the expected outcomes:
//Assert.Fail is used to fail the test if a condition is not met.
//Assert.That is used to check specific conditions, such as the SectionId or the count of sections.