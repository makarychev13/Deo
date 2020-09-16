using Domain.Orders.ValueObjects;

namespace Domain.Orders
{
    public sealed class Order
    {
        public readonly OrderBody Body;
        public readonly FreelanceBurse Source;

        public Order(OrderBody body, FreelanceBurse source)
        {
            Source = source;
            Body = body;
        }
    }
}