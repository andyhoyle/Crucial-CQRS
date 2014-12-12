using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Providers.Filesystem.Exceptions
{
    class GeneralException : Exception
    {
        Object _Object;
        Type _Type;

        public GeneralException(Object o, Type t)
        {
            _Object = o;
        }

        public string Object
        {
            get
            {
                return Object;
            }
        }

    }
}
