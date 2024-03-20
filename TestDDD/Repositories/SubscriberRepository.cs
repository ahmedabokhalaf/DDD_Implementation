using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TestDDD.Database;
using TestDDD.SubscriberAggregate;

namespace TestDDD.Repositories
{
    public class SubscriberRepository : BaseRepository, ISubscriberRepository
    {
        public SubscriberRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateAsync(Subscriber subscriber)
        {
            await dbContext.Subscribers.AddAsync(subscriber);
        }

        public async Task DeleteAsync(Subscriber subscriber)
        {
            dbContext.Subscribers.Remove(subscriber);
        }

        public async Task<IEnumerable<Subscriber>> FindAllAsync(Expression<Func<Subscriber, bool>> criteria, string[] includes = null)
        {
            IQueryable<Subscriber> query = dbContext.Set<Subscriber>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            query = query.Where(x => !x.IsDeleted);
            query = query.OrderByDescending(x => x.Name);
            return await query.Where(criteria).ToListAsync();
        }

        public async Task<Subscriber> FindAsync(Expression<Func<Subscriber, bool>> criteria, string[] includes = null)
        {
            IQueryable<Subscriber> query = dbContext.Subscribers;
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<Subscriber>> GetAllAsync(string[] includes, bool withNoTracking = true)
        {
            IQueryable<Subscriber> query = dbContext.Set<Subscriber>();
            if (withNoTracking)
                query = query.AsNoTracking();
            query = query.Where(x => !x.IsDeleted);

            foreach (var include in includes)
                query = query.Include(include);
            query = query.OrderByDescending(x => x.Name);
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Subscriber>> GetAllAsync(bool withNoTracking = true)
        {
            IQueryable<Subscriber> query = dbContext.Set<Subscriber>();
            if (withNoTracking)
                query = query.AsNoTracking();
            query = query.Where(x => !x.IsDeleted);
            query = query.OrderByDescending(x => x.Name);
            return await query.ToListAsync();
        }
        public async Task<Subscriber> GetByIdAsync(Guid id)
        {
            return await dbContext.Set<Subscriber>().FindAsync(id);
        }

        public async Task<Subscriber> GetByIdAsync(Guid id, string[] includes)
        {
            IQueryable<Subscriber> query = dbContext.Set<Subscriber>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SoftDeleteAsync(Subscriber subscriber)
        {
            subscriber.SoftDelete();
            dbContext.Subscribers.Update(subscriber);
        }

        public async Task UpdateAsync(Subscriber subscriber)
        {
            dbContext.Subscribers.Update(subscriber);
        }


    }
}
