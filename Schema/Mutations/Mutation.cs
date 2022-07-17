using GraphQLTutorial.DTOs;
using GraphQLTutorial.Schema.Queries;
using GraphQLTutorial.Schema.Subscriptions;
using GraphQLTutorial.Services.Courses;
using HotChocolate.Subscriptions;

namespace GraphQLTutorial.Schema.Mutations
{
    public class Mutation
    {

        private readonly CourseRepository _courseRepo;

        public Mutation(CourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        
        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEventSender )
        {
            CourseDTO courseDTO = new CourseDTO()
            {
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };
            courseDTO = await _courseRepo.Create(courseDTO);

            CourseResult courseType = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId
            };
            
            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), courseType);
            return courseType;
        }


        
        public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseDTO courseDTO = new CourseDTO()
            {
                Id = id,
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };
            courseDTO = await _courseRepo.Update(courseDTO);
            CourseResult course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId
            };
            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);
            return course;
        }
        
        public async Task<bool> DeleteCourse(Guid id)
        {
            try
            {
                return await _courseRepo.Delete(id);
            }
            catch 
            {

                return false;
            }
        
        }
    }
}
