using System;

namespace Domain.Notifications
{
    [Flags]
    public enum Subscriptions
    {
        Email = 0,
        Vk = 1
    }
}