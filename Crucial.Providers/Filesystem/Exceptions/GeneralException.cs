using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crucial.Providers.Filesystem.Exceptions
{
    class GeneralException : Exception
    {
        Object _object;
        Type _type;

        public GeneralException(Object o, Type t)
        {
            _object = o;
            _type = t;
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
