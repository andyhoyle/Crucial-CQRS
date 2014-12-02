using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Crucial.Framework.DesignPatterns.Repository
{
    [ServiceContract]
    public interface IDeleteRepository<TKey>
    {
        [OperationContract]
        bool Delete(TKey id);
    }
}
