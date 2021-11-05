using System;

namespace GraphQLHotChocolate.GraphQL.Posts
{
    public record AddPostInput(Guid UserId, string Text, string Title);
}
