using System.Data.Entity;


namespace Crucial.Framework.Data.EntityFramework
{
    public interface IContextProvider<out T>
    {
        T DbContext { get; }
    }

    public class ContextProvider<T> : IContextProvider<T>
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
