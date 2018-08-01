using OwinTestServerDemo.Services.DI.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OwinTestServerDemo.Api.DI.Core
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustomAuthorizationFilter : AuthorizationFilterAttribute
    {
        public IAuthenticationManager AuthenticationManager { get; set; }

        public override async Task 
            OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            try
            {
                var isAuthenticated = 
                    await AuthenticationManager
                    .IsAuthenticatedAsync(actionContext.Request.Headers.Authorization);

                if (isAuthenticated == false)
                    actionContext.Response = 
                        actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

                base.OnAuthorization(actionContext);
            }
            catch (Exception)
            {
                actionContext.Response = 
                    actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}

