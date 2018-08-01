using OwinTestServerDemo.Models.DI;
using OwinTestServerDemo.Services.DI.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwinTestServerDemo.Services.DI.Implementations
{
    public class ResourceService : IResourceService
    {
        private IDictionary<string, Resource> _resourceRepository;

        private IPriceService _priceService;

        public ResourceService(IPriceService priceService)
        {
            _resourceRepository = new Dictionary<string, Resource>();
            _priceService = priceService;
        }

        public async Task<Resource> GetAsync(string id)
        {
            try
            {
                var resource = _resourceRepository[id];

                resource.Prices = await _priceService.GetPricesPerGroupNameAsync(resource.Name);

                return resource;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public async Task<Resource> CreateAsync(string name)
        {
            var resource = new Resource
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.Now.AddDays(-15),
                UpdatedAt = DateTime.Now
            };

            _resourceRepository.Add(resource.Id.ToString(), resource);

            return resource;
        }
    }
}
