using GraphQLTutorial.DTOs;
using GraphQLTutorial.Services.Instructors;

namespace GraphQLTutorial.Loaders
{
    public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDTO>
    {
        private readonly InstructorRepository _instructorRepo;
        public InstructorDataLoader(InstructorRepository instructorRepo,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _instructorRepo = instructorRepo;
        }

        protected override async Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            IEnumerable<InstructorDTO> instructors = await _instructorRepo.GetManyByIds(keys);

            return instructors.ToDictionary(i => i.Id);
        }
    }
}
