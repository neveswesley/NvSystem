using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NvSystem.Application.Mapper;
using NvSystem.Application.Services;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Application.UseCases.User.Commands;

namespace NvSystem.Application;

public static class ServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        AddMediatR(services);
        AddAutoMapper(services);

        return services;
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { cfg.AddProfile<AutoMapping>(); }, typeof(AutoMapping).Assembly);
    }

    private static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateUserCommand).Assembly));

        services.AddValidatorsFromAssembly(Assembly.Load("NvSystem.Application"));
    }
}