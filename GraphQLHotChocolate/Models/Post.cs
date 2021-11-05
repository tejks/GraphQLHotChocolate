using System;
using System.Collections.Generic;

namespace GraphQLHotChocolate.Models
{
    public record Post
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public User User { get; init; }
        public string Title { get; init; }
        public string Text { get; init; }
        public IEnumerable<Comment> Comments { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
