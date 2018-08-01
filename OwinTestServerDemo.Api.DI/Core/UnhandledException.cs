using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace OwinTestServerDemo.Api.DI.Core
{
  internal class UnhandledException : ExceptionFilterAttribute
  {
    public override void OnException(HttpActionExecutedContext actionExecutedContext)
    {
      actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception.ToString());
    }
  }
}