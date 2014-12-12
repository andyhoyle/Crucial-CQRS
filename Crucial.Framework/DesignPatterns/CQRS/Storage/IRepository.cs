using System;
using Crucial.DesignPatterns.CQRS.Domain;

namespace Crucial.DesignPatterns.CQRS.Storage
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(int id);
    }
}
