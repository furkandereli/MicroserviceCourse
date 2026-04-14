using MicroserviceCourse.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MicroserviceCourse.Shared.Extensions;

public static class AuthenticationExt
{
    public static IServiceCollection AddAuthenticationAndAuthorizationExt(this IServiceCollection services, IConfiguration configuration)
    {
        var identityOptions = configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.Authority = identityOptions.Address;
            options.Audience = identityOptions.Audience;
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.NameIdentifier
            };

        }).AddJwtBearer("ClientCredentialSchema", options =>
        {
            options.Authority = identityOptions.Address;
            options.Audience = identityOptions.Audience;
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = true
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Password", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(ClaimTypes.Email);
            });

            options.AddPolicy("ClientCredential", policy =>
            {
                policy.AuthenticationSchemes.Add("ClientCredentialSchema");
                policy.RequireAuthenticatedUser();
            });       
        });

        return services;
    }

}
