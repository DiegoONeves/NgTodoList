using NgTodoList.Api.Models.Todo;
using NgTodoList.Domain;
using NgTodoList.Utils.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NgTodoList.Domain.Repositories;

namespace NgTodoList.Api.Controllers
{
    [RoutePrefix("api/todos")]
    public class TodoController : ApiController
    {
        private ITodoRepository _todoRepository;
        private IUserRepository _userRepository;

        public TodoController(ITodoRepository todoRepository, IUserRepository userRepository)
        {
            this._todoRepository = todoRepository;
            this._userRepository = userRepository;
        }

        [HttpGet]
        [Authorize]
        [Route("")]
        public Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var todos = _todoRepository.Get(User.Identity.Name);
                response = Request.CreateResponse(HttpStatusCode.OK, todos.Select(x => new
                {
                    id = x.Id,
                    text = x.Title,
                    done = x.Done
                }));
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                LogErrorHelper.Register(ex);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        [Authorize]
        [Route("")]
        public Task<HttpResponseMessage> Sync(IList<SyncTodoViewModel> model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var todos = new List<Todo>();
                foreach (var item in model)
                {
                    var todo = new Todo(item.Text);
                    if (item.Done)
                        todo.MarkAsDone();

                    todos.Add(todo);
                }

                _todoRepository.Sync(todos, User.Identity.Name);
                response = Request.CreateResponse(HttpStatusCode.OK, "Sincronia realizada com sucesso!");
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                LogErrorHelper.Register(ex);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        protected override void Dispose(bool disposing)
        {
            _todoRepository.Dispose();
        }
    }
}
