using System.Data.Entity;


namespace Crucial.Framework.Data.EntityFramework
{
    public interface IContextProvider<out T>
    {
        T DbContext { get; }
    }

    public class ContextProvider<T> : IContextProvider<T>
    {
        public T DbContext
        {
            get
            {
                var ctx = Crucial.Framework.IoC.StructureMapProvider.DependencyResolver.Container.GetInstance<T>();
                return ctx;
            }
        }
    }
}
