var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer();



var app = builder.Build();

//  This exposes a single GraphQL query as an endpoint
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});



app.Run();
