using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NuanceWebApp;

namespace Domain.Tests
{
    public static class TestSetup
    {
        public static ContainerBuilder GetContainerBuilder()
        {
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();

            IServiceCollection services = new ServiceCollection();
            var containerBuilder = BuildContainer(services);

            var startup = new Startup(mockConfiguration.Object);
            startup.ConfigureServices(services);
            startup.ConfigureContainer(containerBuilder);

            return containerBuilder;
        }

        private static ContainerBuilder BuildContainer(IServiceCollection services)
        {
            var factory = new AutofacServiceProviderFactory();
            var result = factory.CreateBuilder(services);
            return result;
        }
    }
}
