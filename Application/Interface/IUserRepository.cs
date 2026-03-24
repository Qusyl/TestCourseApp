using Domain.Aggregate.User;
using System;


namespace Application.Interface
{
    public interface IUserRepository
    {
        Task AddAsync(User user);           

        Task<User> GetUserByIdAsync(Guid id);

        Task<User> GetUserByEmail(string email);

    }
}
