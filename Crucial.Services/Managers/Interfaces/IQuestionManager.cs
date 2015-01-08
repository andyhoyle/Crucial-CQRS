using System;
namespace Crucial.Services.Managers.Interfaces
{
    public interface IQuestionManager
    {
        Crucial.Services.ServiceEntities.Category CreateCategory(string name);
    }
}
