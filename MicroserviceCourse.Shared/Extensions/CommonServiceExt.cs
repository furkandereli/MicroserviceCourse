using FluentValidation;
using MicroserviceCourse.Shared.Service;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceCourse.Shared.Extensions;

public static class CommonServiceExt
{
    public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
    {
        services.AddHttpContextAccessor();
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));
        services.AddValidatorsFromAssemblyContaining(assembly);

        services.AddScoped<IIdentityService, IdentityServiceFake>();

        services.AddAutoMapper(assembly);

        return services;
    }   
}
