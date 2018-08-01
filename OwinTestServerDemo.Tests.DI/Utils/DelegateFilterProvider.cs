using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OwinTestServerDemo.Tests.DI.Utils
{
  internal class DelegateFilterProvider : IFilterProvider
  {
    private readonly Action<Type, IFilter> filterProvider;

    public DelegateFilterProvider(Action<Type, IFilter> filterProvider)
    {
      this.filterProvider = filterProvider;
    }

    public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
    {
      var controllerFilters = actionDescriptor.ControllerDescriptor.GetFilters().Select(instance =>
      {
        var result = new FilterInfo(instance, FilterScope.Controller);
        return result;
      });

      var actionFilters = actionDescriptor.GetFilters().Select(instance =>
      {
        filterProvider(instance.GetType(), instance);

        var result = new FilterInfo(instance, FilterScope.Action);
        return result;
      });

      return controllerFilters.Concat(actionFilters);
    }
  }
}
