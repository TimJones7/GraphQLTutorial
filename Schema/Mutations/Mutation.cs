using GraphQLTutorial.Schema.Queries;
using GraphQLTutorial.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQLTutorial.Schema.Mutations
{
    public class Mutation
    {

        private readonly List<CourseResult> _courses;

        public Mutation()
        {
            _courses = new List<CourseResult>();
        }

        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEventSender )
        {
            CourseResult courseType = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };
            _courses.Add(courseType);
            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), courseType);
            return courseType;
        }

        public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseResult course = _courses.FirstOrDefault(course => course.Id == id);

            if (course == null)
            {
                throw new GraphQLException("COURSE_NOT_FOUND");
            }
            course.Name = courseInput.Name;
            course.Subject = courseInput.Subject;
            course.InstructorId = courseInput.InstructorId;

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            
            return course;
        }
        
        public bool DeleteCourse(Guid id)
        {
            return _courses.RemoveAll(course => course.Id == id) >=1;
        }



        
        

    }
}
