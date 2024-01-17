using System.Reflection;
using System.Text;
using AutoMapper;
using ExpensePaymentSystem.Base.Token;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Business.Mapper;
using ExpensePaymentSystem.Business.Services;
using ExpensePaymentSystem.Business.Validators;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.WebApi.Middlewares;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ExpensePaymentSystem.WebApi;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string connection = Configuration.GetConnectionString("MsSqlConnection");
        
        services.AddDbContext<ExpensePaymentSystemDbContext>(options => options.UseSqlServer(connection));
        //services.AddDbContext<ExpensePaymentSystemDbContext>(options => options.UseNpgsql(connection));
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).GetTypeInfo().Assembly));

        // services.AddMediatR(cfg => {
        //
        //     cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        //
        // });

        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
        services.AddSingleton(mapperConfig.CreateMapper());


        services.AddControllers().AddFluentValidation(x =>
        {
            x.RegisterValidatorsFromAssemblyContaining<ExpenseValidator>();
        });
        
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vb Api Management", Version = "v1.0" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Vb Management for IT Company",
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
        

        JwtConfig jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
        
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
        
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<INotificationService, NotificationService>();
    }
    
    public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseMiddleware<HeartBeatMiddleware>(); 
        app.UseMiddleware<ErrorHandlerMiddleware>(); 
        
        app.UseHttpsRedirection();

        //app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        
        app.UseEndpoints(x => { x.MapControllers(); });
    }
}