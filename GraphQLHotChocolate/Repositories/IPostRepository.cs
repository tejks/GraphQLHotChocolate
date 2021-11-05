using GraphQLHotChocolate.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.Repositories
{
    public interface IPostRepository
    {
        Task CreatePostAsync(Post post);
        Task DeletePostAsync(Guid id);
        Task<Post> GetPostAsync(Guid id);
        Task<IEnumerable<Post>> GetPostsAsync();
        Task UpdatePostAsync(Post post);
    }
}