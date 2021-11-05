using GraphQLHotChocolate.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.Repositories
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(Comment comment);
        Task DeleteCommentAsync(Guid id);
        Task<Comment> GetCommentAsync(Guid id);
        Task<IEnumerable<Comment>> GetCommentsAsync();
        Task UpdateCommentAsync(Comment comment);
    }
}