using System.Collections.Generic;
using Migrations.Tables.Orders;

namespace Migrations.Tables.FreelanceBurses
{
    public class FreelanceBurseEntity
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        
        public List<OrderEntity> Orders { get; set; }
    }
}