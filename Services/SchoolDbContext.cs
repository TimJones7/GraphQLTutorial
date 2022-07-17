using GraphQLTutorial.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTutorial.Services
{
    public class SchoolDbContext : DbContext
    {

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {

        }

        public DbSet<CourseDTO> Courses { get; set; }

        public DbSet<InstructorDTO> Instructors { get; set; }

        public DbSet<StudentDTO> Students { get; set; }
    }
}
