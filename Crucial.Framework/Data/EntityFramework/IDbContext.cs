using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Framework.Data.EntityFramework
{
    public interface IDbContext<TEntity> where TEntity : class
    {
        System.Data.Entity.Database Database { get; }

        IDbSet<TEntity> Set<T1>();
    }
}
