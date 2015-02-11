using Crucial.Providers.Identity.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Providers.Identity.Interfaces
{
    public interface IIdentityContextProvider : Crucial.Framework.Data.EntityFramework.IDatabaseContextProvider
    {
        DbContext DbContext { get; }
    }
}
