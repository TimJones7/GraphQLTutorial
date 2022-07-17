using GraphQLTutorial.Schema.Mutations;
using GraphQLTutorial.Schema.Queries;
using GraphQLTutorial.Schema.Subscriptions;
using GraphQLTutorial.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


//  Add GraphQL
//  Register query types
//  Add Mutations
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

builder.Services.AddInMemorySubscriptions();

builder.Services.AddPooledDbContextFactory<SchoolDbContext>(options =>
{
    string dbConnection = builder.Configuration.GetConnectionString("default");
    options.UseSqlite(dbConnection);
});


var app = builder.Build();




//  This exposes a single GraphQL query as an endpoint
//  You only need to expose one endpoint for all client queries
app.UseRouting();

app.UseWebSockets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

using (IServiceScope scope = app.Services.CreateScope())
{
    IDbContextFactory<SchoolDbContext> contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();

    using (SchoolDbContext context = contextFactory.CreateDbContext())
    {
        context.Database.Migrate();
    }
}

app.Run();
