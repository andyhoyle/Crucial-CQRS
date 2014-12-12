using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Services.ServiceEntities
{
    public class Supplier : User
    {
        public string Name { get; set; }
        public string TelephoneNo { get; set; }
        public string MobileNo { get; set; }
    }
}
