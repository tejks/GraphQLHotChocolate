using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace GraphQLHotChocolate.Models
{
    public record Comment
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public string Text { get; init; }
        public Guid PostId { get; init; }
        public Post Post { get; init; }
        public User User { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
