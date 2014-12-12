using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Services.ServiceEntities
{
    public class User : Crucial.Framework.BaseEntities.ServiceEntityBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
