using GraphQLHotChocolate.Data;
using GraphQLHotChocolate.Models;
using HotChocolate;
using HotChocolate.Types;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLHotChocolate.GraphQL.Posts
{
    public class PostType : ObjectType<Post>
    {
        protected override void Configure(IObjectTypeDescriptor<Post> descriptor)
        {
            descriptor.Field(_ => _.Id).Description("Represents the unique ID for the post.");

            descriptor.Field(_ => _.Title).Description("Represents the title of post.");

            descriptor.Field(_ => _.Text).Description("Represents the text of post.");

            descriptor.Field(_ => _.CreatedDate).Description("Represents the date of create.");

            descriptor.Field(_ => _.User)
                        .ResolveWith<Resolvers>(_ => _.GetUser(default, default))
                        .Description("Represents the user, who created post.");

            descriptor.Field(_ => _.Comments)
                        .ResolveWith<Resolvers>(_ => _.GetComments(default, default))
                        .Description("Represents the list of comments below the post.");
        }

        private class Resolvers
        {
            private readonly FilterDefinitionBuilder<Comment> commentFilterBuilder = Builders<Comment>.Filter;
            private readonly FilterDefinitionBuilder<User> userFilterBuilder = Builders<User>.Filter;

            public IEnumerable<Comment> GetComments([Parent] Post post, [Service] IPostsDBContext context)
            {
                var filter = commentFilterBuilder.Eq(comment => comment.PostId, post.Id);

                return context.Comments.Find(filter).ToList();
            }

            public User GetUser([Parent] Post post, [Service] IPostsDBContext context)
            {
                var filter = userFilterBuilder.Eq(user => user.Id, post.UserId);

                return context.Users.Find(filter).FirstOrDefault();
            }
        }
    }

}
