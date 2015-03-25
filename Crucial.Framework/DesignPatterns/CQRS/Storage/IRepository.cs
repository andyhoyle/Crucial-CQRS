using System;
using Crucial.Framework.DesignPatterns.CQRS.Domain;
using System.Threading.Tasks;

namespace Crucial.Framework.DesignPatterns.CQRS.Storage
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        Task<T> GetById(int id);
    }
}
