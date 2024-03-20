using TestDDD.OrderAggregate;

namespace TestDDD.Controllers;

public class OrderResult
{
    public Guid Id { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public DateTime CreatedOn { get; set; }
}
