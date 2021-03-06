﻿using System.Collections.Generic;

using Domain.Notifications;

using Migrations.Tables.UsersToFreelanceBurses;
using Migrations.Tables.UsersToKeywords;

namespace Migrations.Tables.Users
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string TelegramId { get; set; }
        public string PasswordHash { get; set; }
        public bool Active { get; set; }
        public Subscriptions Subscriptions { get; set; }

        public List<UsersToKeywordsEntity> ToKeywords { get; set; }
        public List<UsersToFreelanceBursesEntity> ToFreelanceBurses { get; set; }
    }
}