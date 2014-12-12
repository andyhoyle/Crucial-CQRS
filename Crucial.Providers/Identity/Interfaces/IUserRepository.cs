using Crucial.Framework.DesignPatterns.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Providers.Identity.Interfaces
{
    public interface IUserRepository :
        IQueryableRepository<Crucial.Providers.Identity.Entities.AspNetUser>,
        IUpdateRepository<Crucial.Providers.Identity.Entities.AspNetUser>,
        ICreateRepository<Crucial.Providers.Identity.Entities.AspNetUser, Crucial.Providers.Identity.Entities.AspNetUser>,
        Framework.IoC.IAutoRegister
    {
    }
}
