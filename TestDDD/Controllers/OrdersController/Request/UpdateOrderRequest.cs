using TestDDD.OrderAggregate.Input;

namespace TestDDD.Controllers;

public class UpdateOrderRequest
{
    public Guid Id { get; set; }
    public List<OrderItemInput> OrderItems { get; set; }

}
