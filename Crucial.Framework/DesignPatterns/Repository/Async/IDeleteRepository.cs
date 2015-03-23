using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.Repository.Async
{
    [ServiceContract]
    public interface IDeleteRepositoryAsync<TKey>
    {
        [OperationContract]
        Task<bool> Delete(TKey id);
    }
}
