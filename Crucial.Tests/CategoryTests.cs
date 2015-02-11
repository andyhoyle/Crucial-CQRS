using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crucial.Providers.Questions.Data;
using Crucial.Services.Managers.Interfaces;
using Crucial.Tests.Bootstrap;
using Crucial.Providers.Questions;
using StructureMap;
using Crucial.Framework.DesignPatterns.CQRS.Messaging;
using Crucial.Qyz.Commands;
using System.Linq;
using Crucial.Framework.IoC.StructureMapProvider;

namespace Crucial.Tests
{
    [TestClass]
    public class CategoryTests
    {
        public CategoryTests()
        {
            Dependencies.Setup();
        }
        
        ICategoryRepository _categoryRepo;
        ICommandBus _commandBus;

        [TestInitialize]
        public void Setup()
        {
            _categoryRepo = DependencyResolver.Container.GetInstance<ICategoryRepository>();
            _commandBus = DependencyResolver.Container.GetInstance<ICommandBus>();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var name = "Test Category 1";

            //Arrange
            _commandBus.Send(new UserCategoryCreateCommand(1, name));

            //Act
            var cat = _categoryRepo.FindBy(q => q.Id == 1).FirstOrDefault();

            //Act
            Assert.AreEqual(cat.Name, name);
        }
    }
}
