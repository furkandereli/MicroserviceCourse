using MicroserviceCourse.Bus;
using MicroserviceCourse.Order.Api.Endpoints.Orders;
using MicroserviceCourse.Order.Application;
using MicroserviceCourse.Order.Application.BackgroundServices;
using MicroserviceCourse.Order.Application.Contracts.Refit;
using MicroserviceCourse.Order.Application.Contracts.Repositories;
using MicroserviceCourse.Order.Application.Contracts.UnitOfWork;
using MicroserviceCourse.Order.Persistence;
using MicroserviceCourse.Order.Persistence.Repositories;
using MicroserviceCourse.Order.Persistence.UnitOfWork;
using MicroserviceCourse.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersioningExt();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
builder.Services.AddCommonMasstransitExt(builder.Configuration);
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddRefitConfigurationExt(builder.Configuration);
builder.Services.AddHostedService<CheckPaymentStatusOrderBackgroundService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

app.AddOrderGroupEndpointExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
