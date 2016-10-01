using NgTodoList.Api.Models.Account;
using NgTodoList.Domain;
using NgTodoList.Domain.Repositories;
using NgTodoList.Utils.Helpers;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NgTodoList.Api.Controllers
{

    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private IUserRepository _repository;

        public AccountController(IUserRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [Authorize]
        [Route("profile")]
        public Task<HttpResponseMessage> Details()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var user = _repository.Get(User.Identity.Name);
                var model = new DetailUserViewModel();

                model.Email = user.Email;
                model.Id = user.Id;
                model.IsActive = user.IsActive;
                model.Name = user.Name;

                response = Request.CreateResponse(HttpStatusCode.OK, model);
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
        [AllowAnonymous]
        [Route("register")]
        public Task<HttpResponseMessage> Register(RegisterUserViewModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                // Registra o usuário
                var user = new User(model.Name, model.Email, model.Password);
                _repository.SaveOrUpdate(user);

                response = Request.CreateResponse(HttpStatusCode.OK, "Usuário registrado com sucesso!");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("IX_USER_EMAIL"))
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, "Este E-mail já está em uso no sistema.");
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
        [Route("changepassword")]
        public Task<HttpResponseMessage> ChangePassword(ChangePasswordViewModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var user = _repository.Get(User.Identity.Name);
                user.ChangePassword(User.Identity.Name, model.Password, model.NewPassword, model.ConfirmNewPassword);

                _repository.SaveOrUpdate(user);

                response = Request.CreateResponse(HttpStatusCode.OK, "Usuário alterado com sucesso!");
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
        [AllowAnonymous]
        [Route("resetpassword")]
        public Task<HttpResponseMessage> ResetPasword(ResetPasswordViewModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var user = _repository.Get(model.Email);
                var password = user.ResetPassword(model.Email);

                _repository.SaveOrUpdate(user);

                response = Request.CreateResponse(HttpStatusCode.OK, String.Format("Sua nova senha é {0}.", password));
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
        [Route("update")]
        public Task<HttpResponseMessage> UpdateInfo(EditUserViewModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var user = _repository.Get(User.Identity.Name);
                user.UpdateInfo(model.Name, model.Email);

                _repository.SaveOrUpdate(user);

                response = Request.CreateResponse(HttpStatusCode.OK, "Informações salvas com sucesso!");
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
            _repository.Dispose();
        }
    }
}
