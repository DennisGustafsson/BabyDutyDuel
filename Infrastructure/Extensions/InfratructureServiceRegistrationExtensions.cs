using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Application.UseCases.Authentication;
using Application.UseCases.BabyChores;
using Application.UseCases.Contracts;
using Application.UseCases.Points;
using Infrastructure.Authentication;
using Infrastructure.Persistence.EFC.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extensions;

public static class InfratructureServiceRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(env);

        if (env.IsDevelopment())
        {
            services.AddSingleton(_ =>
            {
                var connectionString = "Data Source=:memory:;Cache=Shared";
                var conn = new SqliteConnection(connectionString);
                conn.Open();

                Console.WriteLine($"DEVELOPMENT: {connectionString}");

                return conn;
            });

            services.AddDbContext<DataContext>((serviceProvider, options) =>
            {
                var conn = serviceProvider.GetRequiredService<SqliteConnection>();
                options.UseSqlite(conn);
            });
        }
        else
        {
            var connectionString = configuration.GetConnectionString("ProductionDatabase")
                ?? throw new InvalidOperationException("Missing ConnectionString to Production Database");

            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
        }

        // Repositories
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IParentRepository, ParentRepository>();
        services.AddScoped<IBabyChoreRepository, BabyChoreRepository>();
        services.AddScoped<IChoreCompletionRepository, ChoreCompletionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Authentication Adapters
        services.AddHttpClient();
        services.AddScoped<IGoogleAuthService, GoogleAuthAdapter>();
        services.AddScoped<IAppleAuthService, AppleAuthAdapter>();
        services.AddScoped<ITokenService, JwtTokenService>();

        // Use Cases - Contracts
        services.AddScoped<CreateContractUseCase>();
        services.AddScoped<GetContractByIdUseCase>();

        // Use Cases - BabyChores
        services.AddScoped<CreateBabyChoreUseCase>();
        services.AddScoped<CompleteChoreUseCase>();
        services.AddScoped<GetChoresByContractUseCase>();

        // Use Cases - Points
        services.AddScoped<GetLeaderboardUseCase>();

        // Use Cases - Authentication
        services.AddScoped<LoginWithGoogleUseCase>();
        services.AddScoped<LoginWithAppleUseCase>();

        return services;
    }
}

