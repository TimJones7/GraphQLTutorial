using Bogus;
using GraphQLTutorial.DTOs;
using GraphQLTutorial.Models;
using GraphQLTutorial.Services.Courses;

namespace GraphQLTutorial.Schema.Queries
{
    public class Query
    {

        private readonly CourseRepository _courseRepo;

        public Query(CourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }


        //  Get all of the courses
        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            IEnumerable<CourseDTO> courseDTOs = await _courseRepo.GetAll();
            return courseDTOs.Select(c => new CourseType() 
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                Instructor = new InstructorType()
                {
                    Id = c.Instructor.Id,
                    FirstName = c.Instructor.FirstName,
                    LastName = c.Instructor.LastName,
                    Salary = c.Instructor.Salary
                }
            });
        }

        
        //  Functions nin GraphQL are called resolvers
        public async Task<CourseType> GetCourseByIdAsync(Guid id)
        {
            CourseDTO courseDTO = await _courseRepo.GetCourseById(id);
            return new CourseType()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                Instructor = new InstructorType()
                {
                    Id = courseDTO.Instructor.Id,
                    FirstName = courseDTO.Instructor.FirstName,
                    LastName = courseDTO.Instructor.LastName,
                    Salary = courseDTO.Instructor.Salary
                }
            };
        }








        [GraphQLDeprecated("This query is depreciated")]
        public string Instructions => "Smash that like button";
    }
}
