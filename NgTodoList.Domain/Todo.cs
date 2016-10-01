using System;
using System.Diagnostics.Contracts;

namespace NgTodoList.Domain
{
    public class Todo
    {
        protected Todo() { }

        public Todo(string title)
            : this(title, 0)
        { }

        public Todo(string title, int userId)
        {
            if(title.Length<3)
                throw new Exception("O título da tarefa deve conter mais que 3 caracteres");

            this.Id = 0;
            this.Title = title;
            this.Done = false;
            this.UserId = userId;
        }

        public int Id { get; protected set; }
        public string Title { get; protected set; }
        public bool Done { get; protected set; }
        public int UserId { get; protected set; }

        public void MarkAsDone()
        {
            this.Done = true;
        }

        public void MarkAsUndone()
        {
            this.Done = false;
        }
    }
}