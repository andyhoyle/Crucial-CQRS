using Crucial.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Category : CqrsViewModel
    {
        public string Name { get; set; }
    }
}