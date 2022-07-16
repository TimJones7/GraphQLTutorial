using GraphQLTutorial.Schema;

var builder = WebApplication.CreateBuilder(args);


//  Add GraphQL with query types
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>();



var app = builder.Build();



//  This exposes a single GraphQL query as an endpoint
//  You only need to expose one endpoint for all client queries
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});



app.Run();
