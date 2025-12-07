using MassTransit;
using MicroserviceCourse.Bus;
using MicroserviceCourse.File.Api.Consumer;

namespace MicroserviceCourse.File.Api;

public static class MasstransitConfigurationExt
{
    public static IServiceCollection AddMasstransitExt(this IServiceCollection services, IConfiguration configuration)
    {
        var busOptions = (configuration.GetSection(nameof(BusOption)).Get<BusOption>())!;

        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<UploadCoursePictureCommandConsumer>();
            configure.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                {
                    host.Username(busOptions.UserName);
                    host.Password(busOptions.Password);
                });

                //cfg.ConfigureEndpoints(ctx);

                cfg.ReceiveEndpoint("file-microservice.upload-course-picture-command.queue", e =>
                {
                    e.ConfigureConsumer<UploadCoursePictureCommandConsumer>(ctx);
                });
            });
        });

        return services;
    }
}
