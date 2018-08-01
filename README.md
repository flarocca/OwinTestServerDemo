# Unit-Testing ApiController with Owin TestServer

This demo tries to demostrate how we can start writing tests for .Net ApiController by usin Owin TestServer class, even in scenarios where strong dependencies exist and business logic was written in Controller's actions.

This demo is also meant to ilustrate how to test ApiController as they were running in a web server, which enables us to test things such as Filters and responses.

You will see:

* How to start doing **Integration Tests**
  - This is the closest writeable test to QA tests
* How to start using [Dependency Injection](https://en.wikipedia.org/wiki/Dependency_injection)
  - By taking advantage of Autofac
  - By moving the business logic to a "Service" layer  
* How to mock Filters

Moreover, you will find some design recommendations and patterns

Tools and Libraries:

* [Microsoft.Owin](https://www.nuget.org/packages/Microsoft.Owin/)
* [Microsoft.Owin:testing](https://www.nuget.org/packages/Microsoft.Owin.Testing/)
* [Autofac](https://autofac.org/)
* [Automapper](https://automapper.org/)
* [FakeItEasy](https://fakeiteasy.github.io/)
* [FluentAssertions](https://fluentassertions.com/)
