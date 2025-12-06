using MicroserviceCourse.Bus;
using MicroserviceCourse.Payment.Api;
using MicroserviceCourse.Payment.Api.Features.Payments;
using MicroserviceCourse.Payment.Api.Repositories;
using MicroserviceCourse.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersioningExt();
builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("payment-in-memory-db");
});

var app = builder.Build();

app.AddPaymentGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
