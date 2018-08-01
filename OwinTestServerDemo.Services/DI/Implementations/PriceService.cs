using OwinTestServerDemo.Models.DI;
using OwinTestServerDemo.Services.DI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwinTestServerDemo.Services.DI.Implementations
{
  public class PriceService : IPriceService
  {
    private IDictionary<string, Price> _priceRepository;

    public PriceService()
    {
      _priceRepository = new Dictionary<string, Price>();

      CreateSomeTestPrices();
    }

    public async Task<IEnumerable<Price>> GetPricesPerGroupNameAsync(string name)
    {
      return _priceRepository.Values.Where(price => price.Group == name);
    }

    private void CreateSomeTestPrices()
    {
      var price1 = new Price
      {
        Id = Guid.NewGuid(),
        Group = "Test",
        Value = 12.5
      };

      var price2 = new Price
      {
        Id = Guid.NewGuid(),
        Group = "Test",
        Value = 20
      };

      var price3 = new Price
      {
        Id = Guid.NewGuid(),
        Group = "Test 1",
        Value = 9
      };

      _priceRepository.Add(price1.Id.ToString(), price1);
      _priceRepository.Add(price2.Id.ToString(), price2);
      _priceRepository.Add(price3.Id.ToString(), price3);
    }
  }
}