using TestDDD.OrderAggregate;
using TestDDD.OrderAggregate.Input;

namespace TestDDD.Controllers;

public class CreateOrderRequest
{
    public List<OrderItemInput> OrderItems { get; set; }
}