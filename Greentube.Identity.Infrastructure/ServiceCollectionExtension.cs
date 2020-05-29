using Greentube.Identity.Domain.Interfaces;
using Greentube.Identity.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Greentube.Identity.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
