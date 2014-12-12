using Crucial.Framework.DesignPatterns.Repository;
using Crucial.Providers.Filesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Providers.Filesystem.Interfaces
{
    public interface IDirectoryRepository : 
        ICreateRepository<Directory, Directory>, 
        IUpdateRepository<DirectoryUpdate>, 
        IDeleteRepository<Directory>, 
        IReadRepository<Directory, Directory>
    {
    }
}
