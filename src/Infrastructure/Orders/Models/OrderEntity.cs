using System;

namespace Infrastructure.Orders.Models
{
    internal sealed class OrderEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime Publication { get; set; }
        public int FreelanceBurseId { get; set; }
        public ProcessingStatusEntity Status { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}