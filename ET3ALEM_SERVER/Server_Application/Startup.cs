using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessEntities.AutoMapperProfiles;
using BusinessEntities.Models;
using DataAccessLayer;
using DataServiceLayer;
using ExceptionHandling;
using Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Rollbar;
using Server_Application.Data;

namespace Server_Application
{
    public class Startup
    {
        private readonly string AllowCORS = "_AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            EnableCors(services);
            ConfigureDi(services);
            ConfigureApplicationPool(services);
            ConfigureUserIdentity(services);
            ConfigureJWT(services);
            ConfigureNewtonsoft(services);
            ConfigureRollbar();
            services.AddAutoMapper(typeof(UserProfile).Assembly);
        }

        private static void ConfigureRollbar()
        {
            RollbarInfrastructureConfig config =
                new RollbarInfrastructureConfig(
                    "f4dc4b60bcab441c82943406090dbd02",
                    "Et3allim_DEV"
                );
            RollbarInfrastructure
                .Instance
                .Init(config);
        }

        private static void ConfigureNewtonsoft(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                { NamingStrategy = new DefaultNamingStrategy() };
            });
        }

        private void ConfigureJWT(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options => { ConfigureAuthenticationOptions(options); })
                .AddJwtBearer(cfg => { ConfigureJWTBearerOptions(cfg); });
        }

        private static void ConfigureAuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        private void ConfigureJWTBearerOptions(JwtBearerOptions cfg)
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = Configuration["Authentication:JwtIssuer"],
                ValidAudience = Configuration["Authentication:JwtIssuer"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:JwtKey"])),
                // remove delay of token when expire
                ClockSkew = TimeSpan.Zero
            };
            cfg.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        context.Response.Headers.Add("Token-Expired", "true");
                    return Task.CompletedTask;
                }
            };
        }

        private static void ConfigureUserIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddTokenProvider("UserRefresh", typeof(DataProtectorTokenProvider<User>))
                .AddDefaultTokenProviders();
        }

        private void ConfigureApplicationPool(IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        Configuration.GetConnectionString("DefaultConnection"),
                        new MySqlServerVersion(new Version(8, 0, 20)),
                        mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend))
                    // Everything from this point on is optional but helps with debugging.
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());
        }

        private void EnableCors(IServiceCollection services)
        {
            string[] allowedOrigins = new string[] { "http://localhost:4200", "http://192.168.1.6:4200", "https://et3allim.com", "https://www.et3allim.com" };
            services.AddCors(options =>
            {
                options.AddPolicy(AllowCORS,
                    builder =>
                    {
                        builder.WithOrigins(allowedOrigins)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(AllowCORS);
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseGlobalErrorHandlerMiddleware();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private static void ConfigureDi(IServiceCollection services)
        {
            services.AddScoped<IQuestionDal, QuestionDal>();
            services.AddScoped<IQuestionDsl, QuestionDsl>();
            services.AddScoped<IQuizDsl, QuizDsl>();
            services.AddScoped<IQuizDal, QuizDal>();
            services.AddScoped<IQuestionCollectionDsl, QuestionCollectionDsl>();
            services.AddScoped<IQuestionCollectionDal, QuestionCollectionDal>();
            services.AddScoped<IQuizAttemptDal, QuizAttemptDal>();
            services.AddScoped<IQuizAttemptDsl, QuizAttemptDsl>();
            services.AddScoped<IEmailDsl, SendGridEmailDsl>();
            services.AddScoped<IContactUsDsl, ContactUsDsl>();
            services.AddScoped<IContactUsDal, ContactUsDal>();

            services.AddSingleton<IAccountHelper, AccountHelper>();
            services.AddSingleton<IQuizHelper, QuizHelper>();
        }
    }
}