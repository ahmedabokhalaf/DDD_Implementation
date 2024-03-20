namespace TestDDD.OrderAggregate.Input
{
    public class UpdateOrderItemInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
