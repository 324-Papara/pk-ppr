using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation.AspNetCore;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Para.Api.Middleware;
using Para.Api.Service;
using Para.Base;
using Para.Base.Log;
using Para.Base.Token;
using Para.Bussiness;
using Para.Bussiness.Cqrs;
using Para.Bussiness.Notification;
using Para.Bussiness.Token;
using Para.Bussiness.Validation;
using Para.Data.Context;
using Para.Data.UnitOfWork;
using Serilog;
using StackExchange.Redis;

namespace Para.Api;

public class Startup
{
    public IConfiguration Configuration;
    public static JwtConfig jwtConfig { get; private set; }

    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }


    public void ConfigureServices(IServiceCollection services)
    {
        jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.AddSingleton<JwtConfig>(jwtConfig);

        var connectionStringSql = Configuration.GetConnectionString("MsSqlConnection");
        services.AddDbContext<ParaDbContext>(options => options.UseSqlServer(connectionStringSql));
        //services.AddDbContext<ParaDbContext>(options => options.UseNpgsql(connectionStringPostgre));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
        services.AddControllers().AddFluentValidation(x =>
        {
            x.RegisterValidatorsFromAssemblyContaining<BaseValidator>();
        });


        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
        services.AddSingleton(config.CreateMapper());


        services.AddMediatR(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);

        services.AddTransient<CustomService1>();
        services.AddScoped<CustomService2>();
        services.AddSingleton<CustomService3>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddSingleton<INotificationService, NotificationService>();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
                ValidAudience = jwtConfig.Audience,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };
        });


        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Para Api Management", Version = "v1.0" });
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Para Management for IT Company",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });
        });

        services.AddMemoryCache();

        var redisConfig = new ConfigurationOptions();
        redisConfig.DefaultDatabase = 0;
        redisConfig.EndPoints.Add(Configuration["Redis:Host"], Convert.ToInt32(Configuration["Redis:Port"]));
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.ConfigurationOptions = redisConfig;
            opt.InstanceName = Configuration["Redis:InstanceName"];
        });
        
        
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));
        services.AddHangfireServer();
        

        services.AddScoped<ISessionContext>(provider =>
        {
            var context = provider.GetService<IHttpContextAccessor>();
            var sessionContext = new SessionContext();
            sessionContext.Session = JwtManager.GetSession(context.HttpContext);
            sessionContext.HttpContext = context.HttpContext;
            return sessionContext;
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Para.Api v1"));
        }


        app.UseMiddleware<HeartbeatMiddleware>();
        app.UseMiddleware<ErrorHandlerMiddleware>();
        Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
        {
            Log.Information("-------------Request-Begin------------");
            Log.Information(requestProfilerModel.Request);
            Log.Information(Environment.NewLine);
            Log.Information(requestProfilerModel.Response);
            Log.Information("-------------Request-End------------");
        };
        app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);

        app.UseHangfireDashboard();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.Use((context, next) =>
        {
            if (!string.IsNullOrEmpty(context.Request.Path) && context.Request.Path.Value.Contains("favicon"))
            {
                return next();
            }

            var service1 = context.RequestServices.GetRequiredService<CustomService1>();
            var service2 = context.RequestServices.GetRequiredService<CustomService2>();
            var service3 = context.RequestServices.GetRequiredService<CustomService3>();

            service1.Counter++;
            service2.Counter++;
            service3.Counter++;

            return next();
        });

        app.Run(async context =>
        {
            var service1 = context.RequestServices.GetRequiredService<CustomService1>();
            var service2 = context.RequestServices.GetRequiredService<CustomService2>();
            var service3 = context.RequestServices.GetRequiredService<CustomService3>();

            if (!string.IsNullOrEmpty(context.Request.Path) && !context.Request.Path.Value.Contains("favicon"))
            {
                service1.Counter++;
                service2.Counter++;
                service3.Counter++;
            }

            await context.Response.WriteAsync($"Service1 : {service1.Counter}\n");
            await context.Response.WriteAsync($"Service2 : {service2.Counter}\n");
            await context.Response.WriteAsync($"Service3 : {service3.Counter}\n");
        });
    }
}