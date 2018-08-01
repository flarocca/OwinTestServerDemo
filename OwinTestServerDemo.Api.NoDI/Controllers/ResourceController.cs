using OwinTestServerDemo.Api.NoDI.Core;
using OwinTestServerDemo.Models.NoDI;
using OwinTestServerDemo.Services.NoDI;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;

namespace OwinTestServerDemo.Api.NoDI.Controllers
{
  public class ResourceController : ApiController
  {
    private readonly ResourceService _resourceService;

    private readonly PriceService _priceService;

    public ResourceController()
    {
      _resourceService = ResourceService.Instance;
      _priceService = PriceService.Instance;
    }

    [HttpGet]
    [Route("api/resource/{id}")]
    [CustomAuthorizationFilter]
    public async Task<IHttpActionResult> GetAsync(string id)
    {
      try
      {
        var resource = await _resourceService.GetAsync(id);

        if (resource == null)
        {
          return NotFound();
        }

        var pricesPerGroupName = await _priceService.GetPricesPerGroupNameAsync(resource.Name);
        resource.Prices = pricesPerGroupName.Select(price =>
        {
          price.CreatedAt = null;
          price.UpdatedAt = null;

          return price;
        });

        resource.CreatedAt = null;
        resource.UpdatedAt = null;

        return Ok(resource);
      }
      catch (Exception)
      {
        return InternalServerError();
      }
    }

    [HttpPost]
    [Route("api/resource")]
    [CustomAuthorizationFilter]
    [ValidateModelState]
    public async Task<IHttpActionResult> PostAsync([FromBody]Resource resource)
    {
      try
      {
        var result = await _resourceService.CreateAsync(resource);

        resource.CreatedAt = null;
        resource.UpdatedAt = null;
        resource.Prices = null;

        return Ok(result);
      }
      catch (Exception)
      {
        return InternalServerError();
      }
    }
  }
}