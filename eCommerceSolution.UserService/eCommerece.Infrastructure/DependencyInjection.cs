using eCommerce.Core.RepositoryContracts;
using eCommerece.Infrastructure.DbContext;
using eCommerece.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerece.Infrastructure; // File-Scoped Namespace (C# 10+)
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<DapperDbContext>();
        return services;
    }
}
