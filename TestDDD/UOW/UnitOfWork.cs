using TestDDD.Database;
using TestDDD.OrderAggregate;
using TestDDD.OrderAggregate.Interfaces;
using TestDDD.Repositories;
using TestDDD.SubscriberAggregate;

namespace TestDDD.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IOrderRepository OrderRepository { get; }
        public ISubscriberRepository SubscribersRepository { get; }
        private bool _disposed = false;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            OrderRepository = new OrderRepository(dbContext);
            SubscribersRepository = new SubscriberRepository(dbContext);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _dbContext.Dispose();
            _disposed = true;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
