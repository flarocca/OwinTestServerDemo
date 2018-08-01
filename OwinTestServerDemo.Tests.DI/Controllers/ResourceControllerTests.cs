using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OwinTestServerDemo.Api.DI.Controllers;
using OwinTestServerDemo.Api.DI.Core;
using OwinTestServerDemo.Api.DI.Dtos.Responses;
using OwinTestServerDemo.Models.DI;
using OwinTestServerDemo.Services.DI.Interfaces;
using OwinTestServerDemo.Tests.DI.Utils;

namespace OwinTestServerDemo.Tests.DI.Controllers
{
  [TestClass]
  public class ResourceControllerTests : BaseTestController
  {
    private static TestServer _testServerAuthorized;

    private static TestServer _testServerUnauthorized;

    private static TestServer _testServerExceptionAuthorizing;

    private static IResourceService _resourceService;

    private static IMapper _mapper;

    [ClassInitialize]
    public static void Initialize(TestContext context)
    {
      _resourceService = A.Fake<IResourceService>();
      _mapper = A.Fake<IMapper>();

      _testServerAuthorized = CreateTestServer(true);
      _testServerUnauthorized = CreateTestServer(false);
      _testServerExceptionAuthorizing = CreateTestServer(false, true);
    }

    [TestMethod]
    public async Task GetAsync_UnauthorizedRequest_ReturnsStatusCodeUnauthorized()
    {
      var actualResponse = await _testServerUnauthorized.HttpClient.GetAsync("some id");

      actualResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [TestMethod]
    public async Task GetAsync_AuthorizedRequest_NonExistentId_ReturnsStatusCodeNotFound()
    {
      A.CallTo(() => _resourceService.GetAsync(A<string>.Ignored)).Returns<Resource>(null);
      A.CallTo(() => _mapper.Map<ResourceResponse>(A<object>.Ignored)).Returns(null);

      var actualResponse = await _testServerAuthorized.HttpClient.GetAsync("non-existent id");

      actualResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [TestMethod]
    public async Task GetAsync_AuthorizedRequest_ExistentId_ReturnsStatusCodeOk()
    {
      var existentId = Guid.NewGuid();
      var prices = new List<Price> { new Price { Id = Guid.NewGuid() }, new Price { Id = Guid.NewGuid() } };
      var existentResource = new Resource { Id = existentId, Name = "expected resource response", Prices = prices };
      var expectedResourceResponse = new ResourceResponse
      {
        Id = existentId,
        Name = "expected resource response",
        Prices = prices.Select(price => new PriceResponse { Id = price.Id, Group = price.Group, Value = price.Value })
      };

      A.CallTo(() => _resourceService.GetAsync(existentId.ToString())).Returns(existentResource);
      A.CallTo(() => _mapper.Map<ResourceResponse>(A<object>.Ignored)).Returns(expectedResourceResponse);

      var actualResponse = await _testServerAuthorized.HttpClient.GetAsync(existentId.ToString());

      actualResponse.StatusCode.Should().Be(HttpStatusCode.OK);

      var content = await actualResponse.Content.ReadAsStringAsync();
      var actualResourceResponse = JsonConvert.DeserializeObject<ResourceResponse>(content);

      actualResourceResponse.Should().BeEquivalentTo(expectedResourceResponse);
    }

    [TestMethod]
    public async Task GetAsync_ThrowsExceptionAuthorizing_ReturnsStatusCodeINternalServerError()
    {
      var actualResponse = await _testServerExceptionAuthorizing.HttpClient.GetAsync("sarasa");

      actualResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

      var content = await actualResponse.Content.ReadAsStringAsync();

      content.Should().BeNullOrWhiteSpace();
    }

    private static TestServer CreateTestServer(bool authorized, bool throwsException = false)
    {
      var assemblieResolver = new TestWebApiResolver(typeof(ResourceController));
      var mockAuthenticationManager = A.Fake<IAuthenticationManager>();

      if (throwsException)
        A.CallTo(() => mockAuthenticationManager.IsAuthenticatedAsync(A<AuthenticationHeaderValue>.That.Matches(value => value.Parameter == ""))).Throws<Exception>();
      else
        A.CallTo(() => mockAuthenticationManager.IsAuthenticatedAsync(A<AuthenticationHeaderValue>.Ignored)).Returns(authorized);

      Action<Type, IFilter> filterProvider = (type, instance) =>
      {
        if (instance.GetType() == typeof(CustomAuthorizationFilter))
        {
          var attribute = ((CustomAuthorizationFilter)instance);
          attribute.AuthenticationManager = mockAuthenticationManager;
        }
      };

      Func<Type, object> dependencyResolver = t =>
      {
        if (t == typeof(ResourceController))
        {
          return new ResourceController(_resourceService, _mapper);
        }

        return null;
      };

      return CreateTestServer(assemblieResolver, filterProvider, dependencyResolver, "http://localhost/api/resource/");
    }
  }
}
