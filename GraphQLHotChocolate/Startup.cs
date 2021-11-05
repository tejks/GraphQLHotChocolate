using GraphQL.Server.Ui.Voyager;
using GraphQLHotChocolate.Config;
using GraphQLHotChocolate.Data;
using GraphQLHotChocolate.GraphQL;
using GraphQLHotChocolate.GraphQL.Comments;
using GraphQLHotChocolate.GraphQL.Posts;
using GraphQLHotChocolate.GraphQL.Users;
using GraphQLHotChocolate.Models;
using GraphQLHotChocolate.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace GraphQLHotChocolate
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                return new MongoClient(mongoDbSettings.ConnectionString);
            });

            services.AddSingleton<IPostsDBContext, PostsDBContext>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            BsonClassMap.RegisterClassMap<Post>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(m => m.Comments);
                cm.UnmapMember(m => m.User);
            });

            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(m => m.Comments);
                cm.UnmapMember(m => m.Posts);
            });

            BsonClassMap.RegisterClassMap<Comment>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(m => m.User);
                cm.UnmapMember(m => m.Post);
            });

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddType<PostType>()
                .AddType<UserType>()
                .AddType<CommentType>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql"
            }, "/graphql-voyager");
        }
    }
}
