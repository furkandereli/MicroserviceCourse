using MicroserviceCourse.Bus;
using MicroserviceCourse.Discount.Api;
using MicroserviceCourse.Discount.Api.Features.Discounts;
using MicroserviceCourse.Discount.Api.Options;
using MicroserviceCourse.Discount.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddVersioningExt();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();

app.AddDiscountGroupEndpointExt(app.AddVersionSetExt());

app.UseExceptionHandler(x => { });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();