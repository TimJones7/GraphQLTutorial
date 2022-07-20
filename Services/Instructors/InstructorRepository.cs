using GraphQLTutorial.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTutorial.Services.Instructors
{
    public class InstructorRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

        public InstructorRepository(IDbContextFactory<SchoolDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        //  [Queries] ----------------------------------------------->
     
        public async Task<InstructorDTO?> GetById(Guid id)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var instructor = await context.Instructors
                    .FirstOrDefaultAsync(c => c.Id == id);
                return instructor;
            }
        }

        public async Task<IEnumerable<InstructorDTO>> GetManyByIds(IReadOnlyList<Guid> keys)
        {
            using (SchoolDbContext context = _contextFactory.CreateDbContext())
            {
                var instructor = await context.Instructors
                    .Where(i => keys.Contains(i.Id))
                    .ToListAsync();
                return instructor;
            }
        }
        //  <-----------------------------------------------[Queries]
    }
}
