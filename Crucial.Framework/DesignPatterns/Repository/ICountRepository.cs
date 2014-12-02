using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Crucial.Framework.DesignPatterns.Repository
{
    [ServiceContract]
    public interface ICountRepository<TKey>
    {
        [OperationContract]
        int Count(TKey o);
    }
}
