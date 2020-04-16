using System.Collections.Generic;
using Migrations.Tables.Orders;
using Migrations.Tables.UsersToFreelanceBurses;

namespace Migrations.Tables.FreelanceBurses
{
    public class FreelanceBurseEntity
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        
        public List<OrderEntity> Orders { get; set; }
        public List<UsersToFreelanceBursesEntity> ToUsers { get; set; }
    }
}