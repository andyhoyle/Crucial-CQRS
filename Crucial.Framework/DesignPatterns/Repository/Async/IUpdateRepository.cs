using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.Repository.Async
{
    [ServiceContract]
    public interface IUpdateRepositoryAsync<TInput> where TInput : BaseEntities.ProviderEntityBase
    {
        [OperationContract]
        Task<bool> Update(TInput o);
    }
}
