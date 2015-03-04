using Crucial.Framework.DesignPatterns.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Commands
{
    public class UserCategoryDeleteCommand : Command
    {
        public UserCategoryDeleteCommand(int id, int version) : base(id,version)
        {

        }
    }
}
