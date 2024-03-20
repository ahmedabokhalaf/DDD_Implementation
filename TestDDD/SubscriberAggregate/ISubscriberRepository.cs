using System.Linq.Expressions;
using TestDDD.OrderAggregate;

namespace TestDDD.SubscriberAggregate
{
    public interface ISubscriberRepository
    {
        Task<Subscriber> GetByIdAsync(Guid id);
        Task<Subscriber> GetByIdAsync(Guid id, string[] includes);
        Task CreateAsync(Subscriber subscriber);
        Task UpdateAsync(Subscriber subscriber);
        Task DeleteAsync(Subscriber subscriber);
        Task<IEnumerable<Subscriber>> FindAllAsync(Expression<Func<Subscriber, bool>> criteria, string[] includes = null!);
        Task<Subscriber> FindAsync(Expression<Func<Subscriber, bool>> criteria, string[] includes = null!);
        Task<IEnumerable<Subscriber>> GetAllAsync(string[] includes, bool withNoTracking = true);
        Task<IEnumerable<Subscriber>> GetAllAsync(bool withNoTracking = true);
        Task SoftDeleteAsync(Subscriber subscriber);

    }
}
