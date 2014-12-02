using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Crucial.Framework.DesignPatterns.Repository
{
    [ServiceContract]
    public interface IUpdateRepository<TInput> where TInput : BaseEntities.ProviderEntityBase
    {
        [OperationContract]
        bool Update(TInput o);
    }
}
