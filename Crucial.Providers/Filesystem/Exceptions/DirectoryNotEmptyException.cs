using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Providers.Filesystem.Entities.Extensions;

namespace Crucial.Providers.Filesystem.Exceptions
{
    public class DirectoryNotEmptyException : ArgumentException
    {
        private Crucial.Providers.Filesystem.Entities.Directory _Directory;

        public DirectoryNotEmptyException(Crucial.Providers.Filesystem.Entities.Directory Directory)
        {
            _Directory = Directory;
        }
        
        public override string Source
        {
            get
            {
                return _Directory.BuildPath();
            }
            set
            {
                base.Source = value;
            }
        }
    }
}
