using GraphQLTutorial.Schema.Queries;

namespace GraphQLTutorial.Schema.Mutations
{
    public class Mutation
    {

        private readonly List<CourseResult> _courses;

        public Mutation()
        {
            _courses = new List<CourseResult>();
        }

        public CourseResult CreateCourse(string name, Subject subject, Guid instructorId)
        {
            CourseResult courseType = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Subject = subject,
                InstructorId = instructorId
            };
            _courses.Add(courseType);
            return courseType;
        }

        public CourseResult UpdateCourse(Guid id, string name, Subject subject, Guid instructorId)
        {
            CourseResult course = _courses.FirstOrDefault(course => course.Id == id);

            if (course == null)
            {
                throw new GraphQLException("COURSE_NOT_FOUND");
            }
            course.Name = name;
            course.Subject = subject;
            course.InstructorId = id;
            return course;
        }
        
        public bool DeleteCourse(Guid id)
        {
            return _courses.RemoveAll(course => course.Id == id) >=1;
        }



        
        

    }
}
