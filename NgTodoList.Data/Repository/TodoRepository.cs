using NgTodoList.Data.Context;
using NgTodoList.Domain;
using NgTodoList.Domain.Repositories;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace NgTodoList.Data.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private NgTodoListDataContext _context;

        public TodoRepository(NgTodoListDataContext context)
        {
            this._context = context;
        }

        public IList<Todo> Get(string email)
        {
            var user = _context
                .Users
                .Include(x => x.Todos)
                .Where(x => x.Email.ToLower() == email.ToLower())
                .FirstOrDefault();

            if (user != null)
                return user.Todos.ToList();

            return new List<Todo>();
        }

        public void Sync(IList<Todo> todos, string email)
        {
            var user = _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["NgTodoListConnectionString"].ConnectionString))
            {
                conn.Open();

                SqlCommand clearTodosCommand = new SqlCommand("DELETE FROM [Todo] WHERE [UserId]=@userId", conn);
                clearTodosCommand.Parameters.Add("@userId", SqlDbType.VarChar);
                clearTodosCommand.Parameters["@userId"].Value = user.Id;
                clearTodosCommand.ExecuteNonQuery();

                foreach (var todo in todos)
                {
                    SqlCommand insertTodosCommand = new SqlCommand("INSERT INTO [Todo] VALUES (@title, @done, @userId)", conn);

                    insertTodosCommand.Parameters.Add("@title", SqlDbType.VarChar);
                    insertTodosCommand.Parameters.Add("@done", SqlDbType.Bit);
                    insertTodosCommand.Parameters.Add("@userId", SqlDbType.Int);

                    insertTodosCommand.Parameters["@title"].Value = todo.Title;
                    insertTodosCommand.Parameters["@done"].Value = todo.Done;
                    insertTodosCommand.Parameters["@userId"].Value = user.Id;

                    insertTodosCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}