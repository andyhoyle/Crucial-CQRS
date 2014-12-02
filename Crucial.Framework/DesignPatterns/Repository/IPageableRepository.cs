using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Crucial.Framework.DesignPatterns.Repository
{
    [ServiceContract]
    public interface IPageableRepository<TInput, TOutput>
        where TInput : Framework.BaseEntities.ProviderEntityBase
        //causes error 
        //TODO: Fix this
        //where TOutput : Crucial.Framework.BaseEntities.IPageable<Crucial.Framework.BaseEntities.ProviderEntityBase>
    {
        [OperationContract]
        TOutput GetPage(TInput postPager);
    }
}
