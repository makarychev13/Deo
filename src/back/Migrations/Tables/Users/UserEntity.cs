﻿namespace Migrations.Tables.Users
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string TelegramId { get; set; }
        public bool Active { get; set; }
        
    }
}