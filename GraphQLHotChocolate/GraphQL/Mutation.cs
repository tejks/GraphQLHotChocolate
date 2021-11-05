
using GraphQLHotChocolate.GraphQL.Comments;
using GraphQLHotChocolate.GraphQL.Posts;
using GraphQLHotChocolate.GraphQL.Users;
using GraphQLHotChocolate.Models;
using GraphQLHotChocolate.Repositories;
using HotChocolate;
using System;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.GraphQL
{
    public class Mutation
    {
        public async Task<AddPostPayload> AddPostAsync(AddPostInput input,
            [Service] IPostRepository context)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                UserId = input.UserId,
                Title = input.Title,
                Text = input.Text,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await context.CreatePostAsync(post);

            return new AddPostPayload(post);
        }

        public async Task<AddUserPayload> AddUserAsync(AddUserInput input,
             [Service] IUserRepository context)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                Address = new Address
                {
                    City = input.City,
                    Country = input.Country,
                    Street = input.Street,
                    HouseNumber = input.HouseNumber
                },
                CreatedDate = DateTimeOffset.UtcNow
            };

            await context.CreateUserAsync(user);

            return new AddUserPayload(user);
        }

        public async Task<AddCommentPayload> AddCommentAsync(AddCommentInput input,
            [Service] ICommentRepository context)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                UserId = input.UserId,
                PostId = input.PostId,
                Text = input.Title,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await context.CreateCommentAsync(comment);

            return new AddCommentPayload(comment);
        }
    }
}
