using System.Data.Entity;


namespace Crucial.Framework.Data.EntityFramework
{
    public class ContextProvider<T>
    {
        private readonly T _context;

        public ContextProvider()
        {
            _context = Crucial.Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<T>();
        }

        public T DbContext
        {
            get { return _context; }
        }
    }
}
