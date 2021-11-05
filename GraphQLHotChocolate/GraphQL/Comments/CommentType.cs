using GraphQLHotChocolate.Data;
using GraphQLHotChocolate.Models;
using HotChocolate;
using HotChocolate.Types;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.GraphQL.Comments
{
    public class CommentType : ObjectType<Comment>
    {
        protected override void Configure(IObjectTypeDescriptor<Comment> descriptor)
        {
            descriptor.Field(_ => _.Id).Description("Represents the unique ID for the comment.");

            descriptor.Field(_ => _.UserId).Ignore();

            descriptor.Field(_ => _.PostId).Ignore();

            descriptor.Field(_ => _.User)
                        .ResolveWith<Resolvers>(_ => _.GetUserAsync(default, default))
                        .Description("Represents the user, who created comment.");

            descriptor.Field(_ => _.Post)
                        .ResolveWith<Resolvers>(_ => _.GetPostAsync(default, default))
                        .Description("Represents a post that was created for the comment.");
        }

        private class Resolvers
        {
            private readonly FilterDefinitionBuilder<Post> postFilterBuilder = Builders<Post>.Filter;
            private readonly FilterDefinitionBuilder<User> userFilterBuilder = Builders<User>.Filter;

            public async Task<Post> GetPostAsync([Parent] Comment comment, [Service] IPostsDBContext context)
            {
                var filter = postFilterBuilder.Eq(post => post.Id, comment.PostId);

                return await context.Posts.Find(filter).FirstOrDefaultAsync();
            }

            public async Task<User> GetUserAsync([Parent] Comment comment, [Service] IPostsDBContext context)
            {
                var filter = userFilterBuilder.Eq(user => user.Id, comment.UserId);

                return await context.Users.Find(filter).FirstOrDefaultAsync();
            }
        }
    }
}
