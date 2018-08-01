using OwinTestServerDemo.Models.NoDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwinTestServerDemo.Services.NoDI
{
  public class PriceService
  {
    private IDictionary<string, Price> _priceRepository;

    private static PriceService _instance;

    private PriceService()
    {
      _priceRepository = new Dictionary<string, Price>();

      CreateSomeTestPrices();
    }

    public static PriceService Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new PriceService();
        }

        return _instance;
      }
    }

    public async Task<Price> GetAsync(string id)
    {
      try
      {
        return _priceRepository[id];
      }
      catch (KeyNotFoundException)
      {
        return null;
      }
    }

    public async Task<Price> CreateAsync(Price price)
    {
      price.Id = Guid.NewGuid();
      price.CreatedAt = DateTime.Now.AddDays(-15);
      price.UpdatedAt = DateTime.Now;

      _priceRepository.Add(price.Id.ToString(), price);

      return price;
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