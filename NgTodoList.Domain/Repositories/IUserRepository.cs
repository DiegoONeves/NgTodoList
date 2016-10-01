using System;

namespace NgTodoList.Domain.Repositories
{
    public interface IUserRepository: IDisposable
    {
        User Get(string email);
        void SaveOrUpdate(User user);
        void Delete(int id);
    }
}
