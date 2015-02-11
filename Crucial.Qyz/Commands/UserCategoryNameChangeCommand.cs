using Crucial.Framework.DesignPatterns.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Commands
{
    public class UserCategoryNameChangeCommand : Command
    {
        public string Name { get; set; }

        public UserCategoryNameChangeCommand(int id, string name, int version)
            : base(id, version)
        {
            Name = name;
        }
    }
}
