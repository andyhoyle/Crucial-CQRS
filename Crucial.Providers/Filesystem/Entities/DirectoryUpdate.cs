using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Providers.Filesystem.Entities
{
    public class DirectoryUpdate : Directory
    {
        public string RenameTo { get; set; }
        public List<string> MoveTo { get; set; }
    }
}
