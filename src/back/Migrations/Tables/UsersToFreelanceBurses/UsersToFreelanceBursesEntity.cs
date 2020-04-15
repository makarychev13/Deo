using Migrations.Tables.FreelanceBurses;
using Migrations.Tables.Users;

namespace Migrations.Tables.UsersToFreelanceBurses
{
    public class UsersToFreelanceBursesEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FreelanceBurseId { get; set; }
        
        public UserEntity User { get; set; }
        public FreelanceBurseEntity FreelanceBurse { get; set;}
    }
}