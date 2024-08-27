using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Pratica.Application.Models;
using System.Text;

namespace Pratica.API.Configuration
{
    public static class ConfigurationAuth
    {
        public static void ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            //Add Connection DB
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                        .Build();

            var authSettingsSection = config.GetSection("AuthSettings");

            builder.Services.Configure<AuthSettings>(authSettingsSection);

            var authSettings = authSettingsSection.Get<AuthSettings>();
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(authSettings!.Secret));

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddPolicyScheme(authenticationScheme: "praticanet", displayName: "Authorization Bearer or AccessToken", options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        if (context.Request.Headers[key: "Access-Token"].Any())
                        {
                            return "Access-Token";
                        }

                        return JwtBearerDefaults.AuthenticationScheme;
                    };
                })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "praticanet",

                        ValidateAudience = false,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,

                        ValidateLifetime = true,
                        RequireExpirationTime = true
                    };
                });
        }
    }
}
