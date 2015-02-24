using Crucial.Framework.Data.EntityFramework;
using Crucial.Framework.DesignPatterns.Repository;
using Crucial.Providers.Identity.Data;
using Crucial.Providers.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Providers.Identity
{
    public class UserRepository : BaseRepository<IIdentityDbContext, Entities.AspNetUser, Entities.AspNetUser>, Interfaces.IUserRepository
    {
    }
}
