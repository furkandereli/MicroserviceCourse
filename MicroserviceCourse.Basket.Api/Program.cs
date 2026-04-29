using MicroserviceCourse.Basket.Api;
using MicroserviceCourse.Basket.Api.Features.Baskets;
using MicroserviceCourse.Bus;
using MicroserviceCourse.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddScoped<BasketService>();
builder.Services.AddVersioningExt();
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(x => { });

app.AddBasketGroupEndpointExt(app.AddVersionSetExt());

app.UseAuthentication();
app.UseAuthorization();

app.Run();