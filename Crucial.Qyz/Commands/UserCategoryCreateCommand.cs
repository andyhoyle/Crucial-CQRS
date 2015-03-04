using Crucial.Framework.DesignPatterns.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Commands
{
    public class UserCategoryCreateCommand : Command
    {
        public string Name { get; set; }

        public UserCategoryCreateCommand(string name) : base (0, -1)
        {
            Name = name;
        }
    }
}
