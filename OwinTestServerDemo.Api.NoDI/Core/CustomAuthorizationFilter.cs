using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace OwinTestServerDemo.Api.NoDI.Core
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustomAuthorizationFilter : AuthorizationFilterAttribute
    {
        public override void 
            OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization == null)
                    actionContext.Response = 
                        actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                else
                {
                    var authorizationHeader = actionContext.Request.Headers.Authorization;
                    //Http request to another service
                }

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


