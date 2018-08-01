using OwinTestServerDemo.Models.NoDI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwinTestServerDemo.Services.NoDI
{
  public class ResourceService
  {
    private IDictionary<string, Resource> _resourceRepository;

    private static ResourceService _instance;

    private ResourceService()
    {
      _resourceRepository = new Dictionary<string, Resource>();
    }

    public static ResourceService Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new ResourceService();
        }

        return _instance;
      }
    }

    public async Task<Resource> GetAsync(string id)
    {
      try
      {
        return _resourceRepository[id];
      }
      catch (KeyNotFoundException)
      {
        return null;
      }
    }

    public async Task<Resource> CreateAsync(Resource resource)
    {
      resource.Id = Guid.NewGuid();
      resource.CreatedAt = DateTime.Now.AddDays(-15);
      resource.UpdatedAt = DateTime.Now;

      _resourceRepository.Add(resource.Id.ToString(), resource);

      return resource;
    }
  }
}