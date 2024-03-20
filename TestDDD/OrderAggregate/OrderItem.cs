namespace TestDDD.OrderAggregate
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
    }
}
