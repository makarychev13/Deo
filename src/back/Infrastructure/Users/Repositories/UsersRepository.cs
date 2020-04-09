using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Notifications;
using Domain.Orders;
using Domain.Users;
using Domain.Users.ValueObjects;

namespace Infrastructure.Users.Repositories
{
    public sealed class UsersRepository
    {
        public async Task<Dictionary<Subscriptions, Contact[]>> GetForNotifications(Order order)
        {
            throw new NotImplementedException();
        }
    }
}