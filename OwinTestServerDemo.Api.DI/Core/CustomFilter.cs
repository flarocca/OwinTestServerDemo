using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OwinTestServerDemo.Core
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CustomFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Conflict, "sarasa");
        }
    }
}