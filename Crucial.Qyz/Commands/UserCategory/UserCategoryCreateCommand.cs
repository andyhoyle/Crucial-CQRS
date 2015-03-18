using Crucial.Framework.DesignPatterns.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Commands.UserCategory
{
    public class UserCategoryCreateCommand : Command
    {
        public string Name { get; private set; }

        public UserCategoryCreateCommand(string name) : base (0, -1)
        {
            Name = name;
        }
    }
}
