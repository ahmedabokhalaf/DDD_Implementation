using Microsoft.EntityFrameworkCore;
using TestDDD.Database;
using TestDDD.OrderAggregate;

namespace TestDDD.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext):base(dbContext)
        {
        }
        public async Task CreateAsync(Order Order)
        {
            await dbContext.Orders.AddAsync(Order);
        }

        public async Task DeleteAsync(Order order)
        {
            dbContext.Orders.Remove(order);
        }

        public async Task<IEnumerable<Order>> FindAllAsync(int lastPageId, int NumberofRecord)
        {
            var nextPage = await dbContext.Orders
                .OrderBy(x => x.Id)
                .Take(NumberofRecord)
                .ToListAsync();
            return nextPage;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await dbContext.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await dbContext.Orders.ToListAsync();
        }

        public async Task UpdateAsync(Order Order)
        {
            dbContext.Update(Order);
        }
    }
}
