
using global::GraphQLHotChocolate.Data;
using global::GraphQLHotChocolate.Models;
using HotChocolate;
using HotChocolate.Types;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.GraphQL.Users
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor.Field(_ => _.Id)
                        .Description("Represents the unique ID for the user.");

            descriptor.Field(_ => _.FirstName)
                        .Description("Represents the firstname of post.");

            descriptor.Field(_ => _.LastName)
                        .Description("Represents the lastname of post.");

            descriptor.Field(_ => _.Address.City)
                        .Description("Represents the city where he lives.");

            descriptor.Field(_ => _.Address.Country)
                        .Description("Represents the country where he lives.");

            descriptor.Field(_ => _.Address.Street)
                        .Description("Represents the street where he lives.");

            descriptor.Field(_ => _.Address.HouseNumber)
                        .Description("Represents the house number.");

            descriptor.Field(_ => _.CreatedDate)
                        .Description("Represents the date of create.");

            descriptor.Field(_ => _.Posts)
                        .ResolveWith<Resolvers>(_ => _.GetPostsAsync(default, default));

            descriptor.Field(_ => _.Comments)
                        .ResolveWith<Resolvers>(_ => _.GetCommentsAsync(default, default));

        }

        private class Resolvers
        {
            private readonly FilterDefinitionBuilder<Comment> commentFilterBuilder = Builders<Comment>.Filter;
            private readonly FilterDefinitionBuilder<Post> postFilterBuilder = Builders<Post>.Filter;

            public async Task<IEnumerable<Comment>> GetCommentsAsync([Parent] User user, [Service] IPostsDBContext context)
            {
                var filter = commentFilterBuilder.Eq(comment => comment.UserId, user.Id);

                return await context.Comments.Find(filter).ToListAsync();
            }

            public async Task<IEnumerable<Post>> GetPostsAsync([Parent] User user, [Service] IPostsDBContext context)
            {
                var filter = postFilterBuilder.Eq(post => post.UserId, user.Id);

                return await context.Posts.Find(filter).ToListAsync();
            }
        }
    }
}


