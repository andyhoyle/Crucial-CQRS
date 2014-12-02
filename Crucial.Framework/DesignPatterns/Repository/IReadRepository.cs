using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Crucial.Framework.DesignPatterns.Repository
{
    [ServiceContract]
    public interface IReadRepository<TOutput, TKey> where TOutput : BaseEntities.ProviderEntityBase
    {
        [OperationContract]
        TOutput Get(TKey id);
    }
}
