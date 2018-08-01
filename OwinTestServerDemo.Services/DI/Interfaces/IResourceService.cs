using OwinTestServerDemo.Models.DI;
using System.Threading.Tasks;

namespace OwinTestServerDemo.Services.DI.Interfaces
{
  public interface IResourceService
  {
    Task<Resource> GetAsync(string id);

    Task<Resource> CreateAsync(string name);
  }
}
