using Crucial.Providers.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Providers.Identity.Data
{
    public class IdentityContextProvider : Crucial.Framework.Data.EntityFramework.ContextProvider<IdentityDbContext>, IIdentityContextProvider
    {
    }
}
