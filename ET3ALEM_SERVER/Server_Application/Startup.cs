using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataServiceLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Server_Application.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //enables CORS for HTTP : note on deployment the site must be configured to use https
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowCORS,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200", "http://192.168.1.6:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                                  });
            });

            ConfigureDI(services);
            services.AddDbContextPool<ApplicationContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"),
                        new MySqlServerVersion(new Version(8, 0, 20)),
                        mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend))
                    // Everything from this point on is optional but helps with debugging.
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
              .AddEntityFrameworkStores<ApplicationContext>()
              .AddTokenProvider("UserRefresh", typeof(DataProtectorTokenProvider<IdentityUser>));

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Authentication:JwtIssuer"],
                        ValidAudience = Configuration["Authentication:JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:JwtKey"])),
                        // remove delay of token when expire
                        ClockSkew = TimeSpan.Zero
                    };
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddControllers().AddNewtonsoftJson(options =>
                            {
                                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                                options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new DefaultNamingStrategy() };
                            });
            //) AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; options.JsonSerializerOptions. });
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
        private static void ConfigureDI(IServiceCollection services)
        {
            services.AddScoped<IQuestionDal, QuestionDal>();
            services.AddScoped<IQuestionDsl, QuestionDsl>();
            services.AddScoped<IQuizDsl, QuizDsl>();
            services.AddScoped<IQuizDal, QuizDal>();
            services.AddScoped<IQuestionCollectionDsl, QuestionCollectionDsl>();
            services.AddScoped<IQuestionCollectionDal, QuestionCollectionDal>();
            services.AddScoped<IQuizAttemptDal, QuizAttemptDal>();
            services.AddScoped<IQuizAttemptDsl, QuizAttemptDsl>();
        }
    }
}
