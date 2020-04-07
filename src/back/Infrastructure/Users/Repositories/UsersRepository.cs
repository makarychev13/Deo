using System;
using System.Threading.Tasks;
using Domain.Orders;
using Domain.Users;

namespace Infrastructure.Users.Repositories
{
    public sealed class UsersRepository
    {
        public async Task<User[]> GetForNotifications(Order order)
        {
            throw new NotImplementedException();
        }
    }
}