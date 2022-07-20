using GraphQLTutorial.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTutorial.Services.Courses
{
    public class CourseRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public CourseRepository(IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        //  [Queries] ----------------------------------------------->
        public async Task<IEnumerable<CourseDTO>> GetAll()
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var courses = await context.Courses
                    .ToListAsync();
                return courses;
            }
        }
        public async Task<CourseDTO?> GetCourseById(Guid id)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var course = await context.Courses
                    .FirstOrDefaultAsync(c => c.Id == id);
                return course;
            }
        }
        //  <-----------------------------------------------[Queries]


        //  [Mutations] ----------------------------------------------->
        public async Task<CourseDTO> Create(CourseDTO course)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                await context.Courses.AddAsync(course);
                await context.SaveChangesAsync();
                return course;
            }
        }

        public async Task<CourseDTO> Update(CourseDTO course)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                context.Courses.Update(course);
                await context.SaveChangesAsync();
                return course;
            }
        }
        
        public async Task<bool> Delete(Guid id)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                CourseDTO course = new CourseDTO()
                {
                    Id = id
                };
                context.Courses.Remove(course);
                return await context.SaveChangesAsync() > 0;   
            }
        }
        //  <-----------------------------------------------[Mutations]
    }
}
