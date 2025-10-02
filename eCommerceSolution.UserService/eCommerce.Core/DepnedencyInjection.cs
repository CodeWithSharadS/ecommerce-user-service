using eCommerce.Core;
using eCommerce.Core.ServiceContracts;
using eCommerce.Core.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerece.Core; // File-Scoped Namespace (C# 10+)
public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();

        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        return services;
    }
}
