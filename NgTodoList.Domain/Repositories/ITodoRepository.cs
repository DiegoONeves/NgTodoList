using System;
using System.Collections.Generic;

namespace NgTodoList.Domain.Repositories
{
    public interface ITodoRepository : IDisposable
    {
        IList<Todo> Get(string email);

        void Sync(IList<Todo> todos, string email);
    }
}
