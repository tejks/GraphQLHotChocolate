using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace GraphQLHotChocolate.Models
{
    public record User
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public Address Address { get; init; }
        public IEnumerable<Comment> Comments { get; init; }
        public IEnumerable<Post> Posts { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }

    public record Address
    {
        public string Street { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
        public string HouseNumber { get; init; }
    }
}
