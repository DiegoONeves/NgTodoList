using Microsoft.Owin.Security.OAuth;
using NgTodoList.Api.Helpers;
using NgTodoList.Data.Repository;
using NgTodoList.Domain.Repositories;
using NgTodoList.Utils.Helpers;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace NgTodoList.Api.Security
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            try
            {
                using (IUserRepository _repository = new UserRepository(DataContextHelper.CurrentDataContext))
                {
                    var user = _repository.Get(context.UserName);
                    if (user == null)
                        throw new Exception("Usuário ou senha inválidos");

                    user.Authenticate(context.UserName, context.Password);

                    GenericIdentity genericIdentity = new GenericIdentity(user.Email);
                    var identity = new ClaimsIdentity(genericIdentity, null, context.Options.AuthenticationType, null, null);
                    identity.AddClaim(new Claim("sub", context.UserName));
                    identity.AddClaim(new Claim("role", "user"));

                    context.Validated(identity);
                }
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", ex.Message);
                LogErrorHelper.Register(ex);
                return;
            }
        }
    }
}