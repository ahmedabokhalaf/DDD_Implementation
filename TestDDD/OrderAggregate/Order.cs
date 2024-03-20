using TestDDD.OrderAggregate.Input;

namespace TestDDD.OrderAggregate
{
    public class Order
    {
        public Guid Id { get; private set; }
        public decimal TotalPrice { get; private set; }
        public Order() { }
        public Order(OrderInput input) {
            _orderItems = input.OrderItems;
        }

        private readonly List<OrderItem> _orderItems = new();
        public virtual IReadOnlyList<OrderItem> OrderItems => _orderItems.ToList();

        public async Task<decimal> CalculateTotalPrice(OrderInput input)
        {
            if (input == null)
                return 0;
            foreach (var item in input.OrderItems)
            {
                TotalPrice += item.Price;
            }
            return TotalPrice;
        }

        public async Task<decimal> UpdateCalculatedTotalPrice(UpdateOrderInput input)
        {
            if (input == null)
                return 0;
            foreach (var item in input.OrderItemInputs)
            {
                TotalPrice += item.Price;
            }
            return TotalPrice;
        }


    }
}
