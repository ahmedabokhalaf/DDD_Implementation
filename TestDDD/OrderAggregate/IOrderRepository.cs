using System.Reflection.Metadata;

namespace TestDDD.OrderAggregate
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> FindAllAsync(int lastPageId, int NumberofRecord);
        Task<Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
