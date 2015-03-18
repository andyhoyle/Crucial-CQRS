using Crucial.Framework.Entities.Mappers;
using Crucial.Providers.Filesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Providers.Filesystem.Entities.Extensions;

namespace Crucial.Providers.Filesystem.Mappers
{
    public class DirectoryMapper : ProviderEntityMapper<Directory, System.IO.DirectoryInfo>
    {
        public override Directory ToProviderEntity(System.IO.DirectoryInfo source)
        {
            var d = base.ToProviderEntity(source);
            var p = source.FullName.Split(System.IO.Path.DirectorySeparatorChar);
            d.PathParts = p.Take(p.Length - 1).ToList();
            d.IsEmpty = true;
            d.DriveLetter = source.Root.ToString().Take(1).ToString();
            d.Seperator = System.IO.Path.DirectorySeparatorChar == '/' ? Entities.Enums.FilesystemPathSeparator.ForwardSlash : Entities.Enums.FilesystemPathSeparator.BackSlash;
            return d;
        }

        public override System.IO.DirectoryInfo ToAnyEntity(Directory source)
        {
            var d = new System.IO.DirectoryInfo(source.BuildPath());          
            return d;
        }
    }
}
