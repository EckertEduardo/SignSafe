using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignSafe.Application.Auth;
using SignSafe.Application.Behaviors;
using SignSafe.Application.Users.Commands.Delete;
using SignSafe.Application.Users.Commands.Insert;
using SignSafe.Application.Users.Commands.Update;
using SignSafe.Application.Users.Commands.UpdateRole;
using SignSafe.Application.Users.Dtos;
using SignSafe.Application.Users.Queries.Get;
using SignSafe.Application.Users.Queries.GetAll;
using SignSafe.Application.Users.Queries.Login;
using SignSafe.Domain.Contracts.Api;
using SignSafe.Domain.RepositoryInterfaces;
using SignSafe.Infrastructure.Context;
using SignSafe.Infrastructure.Repositories;
using SignSafe.Infrastructure.UoW;
using System.Reflection;

namespace SignSafe.Ioc
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(WebApplicationBuilder builder)
        {
            AddInfrastructure(builder);
            AddContext(builder.Services);
            AddMediatrRegistration(builder.Services);
            AddServices(builder.Services);
            AddRepositories(builder.Services);

            ///Automatically registered using AddMediatrRegistration()
            //AddCommands(builder.Services);
            //AddQueries(builder.Services);
        }

        private static void AddInfrastructure(WebApplicationBuilder builder)
        {
            var dataSource = Environment.GetEnvironmentVariable("DB_HOST");
            var catalog = Environment.GetEnvironmentVariable("DB_NAME");
            var password = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var connectionString = !string.IsNullOrEmpty(dataSource)
                ? $"Data Source={dataSource};Initial Catalog={catalog};User ID=sa;Password={password};Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
                : builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));
        }

        private static void AddContext(IServiceCollection services)
        {
            services.AddScoped<DbContext, MyContext>();
        }

        private static void AddMediatrRegistration(IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.Load("SignSafe.Application"));
                config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
                config.AddOpenRequestPreProcessor(typeof(ValidationPreProcessorBehavior<>));
            });
            services.AddValidatorsFromAssembly(Assembly.Load("SignSafe.Application"));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
        private static void AddServices(IServiceCollection services)
        {
            //User
            services.AddTransient<IJwtService, JwtService>();
        }

        private static void AddCommands(IServiceCollection services)
        {
            //User
            services.AddScoped<IRequestHandler<InsertUserCommand>, InsertUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand>, UpdateUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserRolesCommand>, UpdateUserRolesCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
        }

        private static void AddQueries(IServiceCollection services)
        {
            //User
            services.AddScoped<IRequestHandler<GetUsersByFilterQuery, PaginatedResult<List<UserDto>>>, GetUsersByFilterQueryHandler>();
            services.AddScoped<IRequestHandler<GetUserQuery, UserDto?>, GetUserQueryHandler>();
            services.AddScoped<IRequestHandler<LoginUserQuery, string?>, LoginUserQueryHandler>();
        }
    }
}
