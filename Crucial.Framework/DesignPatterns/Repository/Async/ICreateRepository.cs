using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.Repository.Async
{
    [ServiceContract]
    public interface ICreateRepositoryAsync<TInput, TKey> where TInput : BaseEntities.ProviderEntityBase
    {
        [OperationContract]
        Task<TKey> Create(TInput o);
    }
}
