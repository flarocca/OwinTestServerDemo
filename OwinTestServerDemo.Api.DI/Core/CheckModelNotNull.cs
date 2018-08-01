using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OwinTestServerDemo.Api.DI.Core
{
  [AttributeUsage(AttributeTargets.Method, Inherited = true)]
  public class CheckModelNotNull : ActionFilterAttribute
  {
    public string ErrorMessage { get; set; }

    private readonly Func<Dictionary<string, object>, bool> _validate;

    public CheckModelNotNull() 
    {
      _validate = arguments => arguments.ContainsValue(null);
    }

    public override void OnActionExecuting(HttpActionContext actionContext)
    {
      if (_validate(actionContext.ActionArguments))
      {
        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ErrorMessage);
      }
    }
  }
}