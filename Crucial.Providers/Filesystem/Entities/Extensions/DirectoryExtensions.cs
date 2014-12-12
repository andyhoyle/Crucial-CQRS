using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Providers.Filesystem.Entities.Extensions
{
    public static class DirectoryExtensions
    {
        public static string BuildPath(this Directory d)
        {
            string path = String.Join(d.Seperator.ToChar().ToString(), d.PathParts.ToArray());
            return String.Format("{0}:{1}{2}", d.DriveLetter, d.Seperator.ToChar(), path);
        }

        public static char ToChar(this Enums.FilesystemPathSeparator s)
        {
            return s == Enums.FilesystemPathSeparator.BackSlash ? '/' : '\\';
        }
    }
}
