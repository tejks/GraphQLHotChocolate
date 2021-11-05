using GraphQLHotChocolate.Models;
using MongoDB.Driver;

namespace GraphQLHotChocolate.Data
{
    public class PostsDBContext : IPostsDBContext
    {
        private const string DatabaseName = "PostsDB";

        private const string PostCollectionName = "Posts";
        private const string CommentCollectionName = "Comments";
        private const string UserCollectionName = "Users";

        public IMongoCollection<Post> Posts { get; }
        public IMongoCollection<Comment> Comments { get; }
        public IMongoCollection<User> Users { get; }

        public PostsDBContext(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(DatabaseName);

            this.Posts = database.GetCollection<Post>(PostCollectionName);
            this.Comments = database.GetCollection<Comment>(CommentCollectionName);
            this.Users = database.GetCollection<User>(UserCollectionName);
        }
    }
}
