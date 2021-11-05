using System;

namespace GraphQLHotChocolate.GraphQL.Users
{
    public record AddUserInput(string FirstName, string LastName, string Email, string Street, string City, string Country, string HouseNumber);
}
