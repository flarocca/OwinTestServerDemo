using OwinTestServerDemo.Services.DI.Interfaces;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OwinTestServerDemo.Services.DI.Implementations
{
  public class AuthenticationManager : IAuthenticationManager
  {
    public async Task<bool> IsAuthenticatedAsync(AuthenticationHeaderValue authenticationHeader)
    {
      if (authenticationHeader == null)
      {
        return false;
      }
      else
      {
        string authenticationString = authenticationHeader.Parameter;

        return authenticationString == "net.baires";
      }
    }
  }
}