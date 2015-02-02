//using Crucial.Framework.DesignPatterns.CQRS.Messaging;
//using Crucial.Framework.DesignPatterns.CQRS.Storage;
//using Crucial.Framework.DesignPatterns.CQRS.Utils;
//using Crucial.Providers.Questions;
//using StructureMap;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Crucial.Qyz.Configuration
//{
//    public sealed class ServiceLocator
//    {
//        private static ICommandBus _commandBus;
//        private static ICategoryRepository _categoryRepo;
//        private static bool _isInitialized;
//        private static readonly object _lockThis = new object();

//        static ServiceLocator()
//        {
//            if (!_isInitialized)
//            {
//                lock (_lockThis)
//                {
//                    _commandBus = ObjectFactory.GetInstance<ICommandBus>();
//                    _categoryRepo = ObjectFactory.GetInstance<ICategoryRepository>();
//                    _isInitialized = true;
//                }
//            }


//        }

//        public static ICommandBus CommandBus
//        {
//            get { return _commandBus; }
//        }

//        public static ICategoryRepository CategoryRepository
//        {
//            get { return _categoryRepo; }
//        }
//    }
//}
