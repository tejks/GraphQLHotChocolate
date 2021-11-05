using GraphQLHotChocolate.Data;
using GraphQLHotChocolate.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<Models.User> usersCollection;
        private readonly FilterDefinitionBuilder<Models.User> filterBuilder = Builders<Models.User>.Filter;

        public UserRepository(IPostsDBContext context)
        {
            usersCollection = context.Users;
        }

        public async Task CreateUserAsync(Models.User user)
        {
            await usersCollection.InsertOneAsync(user);
        }

        public async Task DeleteUserAsync(System.Guid id)
        {
            var filter = filterBuilder.Eq(user => user.Id, id);

            await usersCollection.DeleteOneAsync(filter);
        }

        public async Task<Models.User> GetUserAsync(System.Guid id)
        {
            var filter = filterBuilder.Eq(user => user.Id, id);

            return await usersCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Models.User>> GetUsersAsync()
        {
            return await usersCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateUserAsync(Models.User user)
        {
            var filter = filterBuilder.Eq(existingUser => existingUser.Id, user.Id);

            await usersCollection.ReplaceOneAsync(filter, user);
        }
    }
}
