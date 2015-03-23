using System;
using System.Threading.Tasks;
namespace Crucial.Framework.DesignPatterns.CQRS.Utils
{
    public interface IStateHelper
    {
        Task RestoreState();
    }
}
