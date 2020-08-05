using System;

using Migrations.Tables.FreelanceBurses;

namespace Migrations.Tables.Orders
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime Publication { get; set; }
        public int FreelanceBurseId { get; set; }
        public ProcessingStatus Status { get; set; }
        public DateTime LastModificationDate { get; set; }

        public FreelanceBurseEntity FreelanceBurse { get; set; }
    }
}