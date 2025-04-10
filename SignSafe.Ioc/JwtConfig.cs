using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SignSafe.Ioc
{
    public static class JwtConfig
    {
        public static void AddJwtConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("JWT:Secret").Value
                ?? throw new InvalidOperationException("JWT Secret is missing from configuration."));

            service.AddAuthentication(p =>
            {
                p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(p =>
                {
                    p.IncludeErrorDetails = true;
                    p.RequireHttpsMetadata = false;
                    p.SaveToken = true;
                    p.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidIssuer = configuration.GetSection("JWT:Issuer").Value,
                        ValidateAudience = false,
                        ValidAudience = configuration.GetSection("JWT:Audience").Value,
                        ValidateLifetime = true,
                    };

                    p.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Cookies["jwt"];
                            if (token is null)
                                return Task.CompletedTask;

                            if (!string.IsNullOrEmpty(token))
                            {
                                context.Token = token;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
