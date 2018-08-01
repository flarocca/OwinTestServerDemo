using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OwinTestServerDemo.Services.DI.Interfaces
{
  public interface IAuthenticationManager
  {
    Task<bool> IsAuthenticatedAsync(AuthenticationHeaderValue authenticationHeader);
  }
}
