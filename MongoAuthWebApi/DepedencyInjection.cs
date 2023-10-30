using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using MongoAuthWebApi.MongoDb.Configuration;
using MongoAuthWebApi.MongoDb.Identity;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class DepedencyInjection
{
    public static IServiceCollection AddMyMongoDbService(this IServiceCollection services, IConfiguration configuration)
    {
        // MongoDB Defaults
        BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

        services.Configure<MongoDbConfig>(configuration.GetSection(MongoDbConfig.ConfigSection));
        var mongoDbConfig = configuration.GetSection(MongoDbConfig.ConfigSection).Get<MongoDbConfig>();

        var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
        {
            MongoDbSettings = new MongoDbSettings
            {
                ConnectionString = mongoDbConfig.ConnectionString,
                DatabaseName = mongoDbConfig.DatabaseName
            },
            IdentityOptionsAction = options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            }
        };

        services.ConfigureMongoDbIdentity<MongoUser, MongoIdentityRole, Guid>(mongoDbIdentityConfig)
            .AddUserManager<UserManager<MongoUser>>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddMyJwtAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection(JwtConfig.ConfigSection));
        var jwtConfig = configuration.GetSection(JwtConfig.ConfigSection).Get<JwtConfig>();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.ValidIssuer,
                    ValidAudience = jwtConfig.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });

        return services;
    }
}
