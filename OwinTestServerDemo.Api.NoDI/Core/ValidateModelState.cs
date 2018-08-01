using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OwinTestServerDemo.Api.NoDI.Core
{
  [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
  public class ValidateModelState : ActionFilterAttribute
  {
    public override void OnActionExecuting(HttpActionContext actionContext)
    {
      if (actionContext.ModelState.IsValid == false)
      {
        var errors = actionContext.ModelState.Select(x => x.Value.Errors)
                   .Where(y => y.Count > 0)
                   .SelectMany(errorGroup => errorGroup.Select(error => error.ErrorMessage))
                   .ToList();

        var json = JsonConvert.SerializeObject(errors);

        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, json);
      }
    }
  }
}