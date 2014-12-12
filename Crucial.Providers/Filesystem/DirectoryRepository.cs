using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crucial.Providers.Filesystem.Entities.Extensions;
using Crucial.Providers.Filesystem.Mappers;

namespace Crucial.Providers.Filesystem
{
    public class DirectoryRepository : Interfaces.IDirectoryRepository
    {
        private DirectoryMapper _Mapper;
        
        public DirectoryRepository()
        {
            _Mapper = new DirectoryMapper();
        }

        public Entities.Directory Create(Entities.Directory o)
        {
            var d = System.IO.Directory.CreateDirectory(o.BuildPath());
            return _Mapper.ToProviderEntity(d);
        }

        public bool Update(Entities.DirectoryUpdate o)
        {
            string path = o.BuildPath();
            o.PathParts = o.MoveTo;
            string destPath = o.BuildPath();

            System.IO.Directory.Move(System.IO.Path.Combine(path, o.Name), System.IO.Path.Combine(destPath, o.RenameTo));

            return true;
        }

        public bool Delete(Entities.Directory d)
        {
            string path = d.BuildPath();

            if (System.IO.Directory.GetFiles(path).Count() > 0)
            {
                throw new Exceptions.DirectoryNotEmptyException(d);
            }

            try
            {
                System.IO.Directory.Delete(path);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                throw new Exceptions.PathNotFoundException(d);
            }
            catch (System.IO.DriveNotFoundException)
            {
                throw new Exceptions.PathNotFoundException(d);
            }
            catch (Exception)
            {
                throw new Exceptions.GeneralException(d, d.GetType());
            }

            return true;
        }
        //How to you know that these are the only errors that will be thrown?
        //Do you not need a general exception to handle just in case?
        public Entities.Directory Get(Entities.Directory id)
        {
            string path = id.BuildPath();
            
            System.IO.DirectoryInfo d;

            try
            {
                d = new System.IO.DirectoryInfo(path);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                throw new Exceptions.PathNotFoundException(id);
            }
            catch (System.IO.DriveNotFoundException)
            {
                throw new Exceptions.PathNotFoundException(id);
            }

            return _Mapper.ToProviderEntity(d);
        }
    }
}
