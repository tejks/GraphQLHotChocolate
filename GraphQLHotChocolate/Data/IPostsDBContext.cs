using GraphQLHotChocolate.Models;
using MongoDB.Driver;

namespace GraphQLHotChocolate.Data
{
    public interface IPostsDBContext
    {
        IMongoCollection<Comment> Comments { get; }
        IMongoCollection<Post> Posts { get; }
        IMongoCollection<User> Users { get; }
    }
}