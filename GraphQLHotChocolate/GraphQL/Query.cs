

using GraphQLHotChocolate.Models;
using GraphQLHotChocolate.Repositories;
using HotChocolate;
using HotChocolate.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.GraphQL
{
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Gets the queryable post.")]
        public Task<IEnumerable<Post>> GetPostsAsync([Service] IPostRepository context)
        {
            return context.GetPostsAsync();
        }

        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Gets the queryable comment.")]
        public Task<IEnumerable<Comment>> GetCommentAsync([Service] ICommentRepository context)
        {
            return context.GetCommentsAsync();
        }

        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Gets the queryable user.")]
        public Task<IEnumerable<User>> GetUsersAsync([Service] IUserRepository context)
        {
            return context.GetUsersAsync();
        }
    }
}