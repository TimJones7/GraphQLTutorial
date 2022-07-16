using GraphQLTutorial.Schema.Mutations;
using GraphQLTutorial.Schema.Queries;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLTutorial.Schema.Subscriptions
{
    public class Subscription
    {

        
        [Subscribe]
        public CourseResult CourseCreated([EventMessage] CourseResult course) => course;

        
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<CourseResult>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver) 
        {
            string topicName = $"{courseId}_{nameof(Subscription.CourseUpdated)}";
            return await topicEventReceiver.SubscribeAsync<string, CourseResult>(topicName);
            
        }



    }
}
