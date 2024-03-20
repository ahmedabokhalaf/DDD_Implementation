using TestDDD.Database;

namespace TestDDD.Repositories
{
    public class BaseRepository
    {
        protected ApplicationDbContext dbContext {  get; set; }

        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
