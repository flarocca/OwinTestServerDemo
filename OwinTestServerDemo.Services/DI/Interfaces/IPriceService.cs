using OwinTestServerDemo.Models.DI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwinTestServerDemo.Services.DI.Interfaces
{
  public interface IPriceService
  {
    Task<IEnumerable<Price>> GetPricesPerGroupNameAsync(string name);
  }
}