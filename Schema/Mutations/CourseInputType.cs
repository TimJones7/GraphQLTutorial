using GraphQLTutorial.Models;
using GraphQLTutorial.Schema.Queries;

namespace GraphQLTutorial.Schema.Mutations
{
    public class CourseInputType
    {
        public string Name { get; set; }

        public Subject Subject { get; set; }
        
        public Guid InstructorId { get; set; }
    }
}
