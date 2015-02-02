using System;
using Crucial.Framework.DesignPatterns.CQRS.Domain;

namespace Crucial.Framework.DesignPatterns.CQRS.Storage
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(int id);
    }
}
