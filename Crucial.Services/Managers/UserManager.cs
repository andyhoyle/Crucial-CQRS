using SE = Crucial.Services.ServiceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Providers.Identity.Interfaces;

namespace Crucial.Services.Managers
{
    public class UserManager : Interfaces.IUserManager
    {
        private IUserRepository _UserRepository;
        private Mappers.UserToUserMapper _UserMapper;

        public UserManager(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
            _UserMapper = new Mappers.UserToUserMapper();
        }

        public SE.User GetUser(int Id)
        {
            return _UserMapper.ToServiceEntity(_UserRepository.FindBy(u => u.Id == Id).FirstOrDefault());
        }

        public int CreateUser(SE.User user)
        {
            return _UserRepository.Create(_UserMapper.ToProviderEntity(user)).Id;
        }
    }
}
