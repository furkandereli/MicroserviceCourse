using MicroserviceCourse.Web.DelegateHandlers;
using MicroserviceCourse.Web.ExceptionHandlers;
using MicroserviceCourse.Web.Extensions;
using MicroserviceCourse.Web.Options;
using MicroserviceCourse.Web.Pages.Auth.SignIn;
using MicroserviceCourse.Web.Pages.Auth.SignUp;
using MicroserviceCourse.Web.Services;
using MicroserviceCourse.Web.Services.Refit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Refit;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddOptionsExt();

builder.Services.AddHttpClient<SignUpService>();
builder.Services.AddHttpClient<SignInService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<UserService>();


builder.Services.AddScoped<AuthenticatedHttpClientHandler>();
builder.Services.AddScoped<ClientAuthenticatedHttpClientHandler>();
builder.Services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();

builder.Services.AddRefitClient<ICatalogRefitService>().ConfigureHttpClient(configure =>
{
    var microserviceOption = builder.Configuration.GetSection(nameof(MicroserviceOption)).Get<MicroserviceOption>();
    configure.BaseAddress = new Uri(microserviceOption!.Catalog.BaseAddress);
}).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
  .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();

builder.Services.AddAuthentication(configureOptions =>
{
    configureOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configureOptions.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Auth/SignIn";
        options.ExpireTimeSpan = TimeSpan.FromDays(60);
        options.Cookie.Name = "MicroserviceCourseCookie";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

var app = builder.Build();

var cultureInfo = new CultureInfo("en-GB");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultureInfo),
    SupportedCultures = [cultureInfo],
    SupportedUICultures = [cultureInfo]
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
