using AutoMapper;
using OwinTestServerDemo.Api.DI.Core;
using OwinTestServerDemo.Api.DI.Dtos.Requests;
using OwinTestServerDemo.Api.DI.Dtos.Responses;
using OwinTestServerDemo.Services.DI.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinTestServerDemo.Api.DI.Controllers
{
    public class ResourceController : ApiController
    {
        private readonly IResourceService _resourceService;
        private readonly IMapper _mapper;

        public ResourceController(IResourceService resourceService, IMapper mapper)
        {
            _resourceService = resourceService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/resource/{id}")]
        [CustomAuthorizationFilter]
        public async Task<IHttpActionResult> GetAsync(string id)
        {
            //1- Call to the service layer
            var resource = await _resourceService.GetAsync(id);

            //2- Map the result
            var result = _mapper.Map<ResourceResponse>(resource);

            //3- Return the response
            if (result== null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/resource")]
        [CustomAuthorizationFilter]
        [CheckModelNotNull(ErrorMessage = "Resource cannot be null.")]
        [ValidateModelState]
        public async Task<IHttpActionResult> PostAsync([FromBody]ResourceRequest request)
        {
            var resourceResponse = await _resourceService.CreateAsync(request.Name);

            var result = _mapper.Map<ResourceResponse>(resourceResponse);

            return Ok(result);
        }
    }
}
