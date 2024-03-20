using TestDDD.SubscriberAggregate;

namespace TestDDD.OrderAggregate.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IOrderRepository OrderRepository { get; }
        public ISubscriberRepository SubscribersRepository { get; }
        Task SaveChangesAsync();
    }
}
