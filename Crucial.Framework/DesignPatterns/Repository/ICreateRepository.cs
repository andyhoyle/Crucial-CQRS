using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Crucial.Framework.DesignPatterns.Repository
{
    [ServiceContract]
    public interface ICreateRepository<TInput, TKey> where TInput : BaseEntities.ProviderEntityBase
    {
        [OperationContract]
        TKey Create(TInput o);
    }
}
