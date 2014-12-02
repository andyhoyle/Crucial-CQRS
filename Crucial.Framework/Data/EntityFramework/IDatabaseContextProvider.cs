using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Crucial.Framework.Data.EntityFramework
{
    public interface IDatabaseContextProvider
    {
        DbContext DbContext { get; }
    }
}
