﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Browl.Service.MarketDataCollector.Configuration;

public static class JwtConfiguration
{
    public static void AddJwtTConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddSingleton<IJwtService, JwtService>();

        var chave = Encoding.ASCII.GetBytes(configuration.GetSection("JWT:Secret").Value);

        services.AddAuthentication(p =>
       {
           p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       })
        .AddJwtBearer(p =>
        {
            p.RequireHttpsMetadata = false;
            p.SaveToken = true;
            p.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(chave),
                ValidateIssuer = true,
                ValidIssuer = configuration.GetSection("JWT:Issuer").Value,
                ValidateAudience = true,
                ValidAudience = configuration.GetSection("JWT:Audience").Value,
                ValidateLifetime = true
            };
        });
    }

    public static void UseJwtConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}