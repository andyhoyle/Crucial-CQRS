using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SE = Crucial.Services.ServiceEntities;
using Crucial.Framework.IoC;

namespace Crucial.Services.Managers.Interfaces
{
    public interface IUserManager : IAutoRegister
    {
        SE.User GetUser(int Id);
        int CreateUser(SE.User user);
    }
}
