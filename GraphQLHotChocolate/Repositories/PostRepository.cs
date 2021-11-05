﻿using GraphQLHotChocolate.Data;
using GraphQLHotChocolate.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> postsCollection;
        private readonly FilterDefinitionBuilder<Post> filterBuilder = Builders<Post>.Filter;

        public PostRepository(IPostsDBContext context)
        {
            postsCollection = context.Posts;
        }

        public async Task CreatePostAsync(Post post)
        {
            await postsCollection.InsertOneAsync(post);
        }

        public async Task DeletePostAsync(Guid id)
        {
            var filter = filterBuilder.Eq(post => post.Id, id);

            await postsCollection.DeleteOneAsync(filter);
        }

        public async Task<Post> GetPostAsync(Guid id)
        {
            var filter = filterBuilder.Eq(post => post.Id, id);

            return await postsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await postsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            var filter = filterBuilder.Eq(existingPost => existingPost.Id, post.Id);

            await postsCollection.ReplaceOneAsync(filter, post);
        }
    }
}
