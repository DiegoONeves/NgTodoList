using NgTodoList.Data.Context;
using NgTodoList.Domain;
using NgTodoList.Domain.Repositories;
using System.Data.Entity;
using System.Linq;

namespace NgTodoList.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private NgTodoListDataContext _context;

        public UserRepository(NgTodoListDataContext context)
        {
            this._context = context;
        }

        public User Get(string email)
        {
            return _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        public void SaveOrUpdate(User user)
        {
            if (user.Id == 0)
                _context.Users.Add(user);
            else
                _context.Entry<User>(user).State = EntityState.Modified;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Find(id));
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}