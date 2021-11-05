using GraphQLHotChocolate.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(Models.User user);
        Task DeleteUserAsync(System.Guid id);
        Task<Models.User> GetUserAsync(System.Guid id);
        Task<IEnumerable<Models.User>> GetUsersAsync();
        Task UpdateUserAsync(Models.User user);
    }
}