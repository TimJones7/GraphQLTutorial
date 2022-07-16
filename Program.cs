using GraphQLTutorial.Schema.Mutations;
using GraphQLTutorial.Schema.Queries;

var builder = WebApplication.CreateBuilder(args);


//  Add GraphQL
//  Register query types
//  Add Mutations
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();



var app = builder.Build();



//  This exposes a single GraphQL query as an endpoint
//  You only need to expose one endpoint for all client queries
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});



app.Run();
