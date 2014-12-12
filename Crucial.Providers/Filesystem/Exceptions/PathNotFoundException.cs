using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Providers.Filesystem.Entities.Extensions;

namespace Crucial.Providers.Filesystem.Exceptions
{
    public class PathNotFoundException : Exception
    {
        string _Path;

        public PathNotFoundException(Entities.Directory d)
        {
            _Path = d.BuildPath();
        }

        public string Path
        {
            get
            {
                return _Path;
            }
        }

        //Todo: ctor for files instead of directories
    }
}
