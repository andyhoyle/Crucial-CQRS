using System.Data.Entity;


namespace Crucial.Framework.Data.EntityFramework
{
    public class ContextProvider<T> : IDatabaseContextProvider where T : DbContext, new()
    {
        private readonly T _context;

        public ContextProvider()
        {
            _context = new T();
            _context.Database.Initialize(false);
        }

        public DbContext DbContext
        {
            get { return _context; }
        }
    }
}
