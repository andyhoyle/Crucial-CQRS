using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Providers.Filesystem.Entities
{
    public class Directory : Framework.BaseEntities.ProviderEntityBase
    {
        public string Name { get; set; }
        public List<string> PathParts { get; set; }
        public Enums.FilesystemPathSeparator Seperator { get; set; }
        public bool IsEmpty { get; set; }
        public string DriveLetter { get; set; }
    }
}
