using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BusinessEntities.Models;
using Server_Application.Data;
using Server_Application.Models;

namespace Server_Application
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService <ApplicationContext>();
                    context.Database.Migrate();
                    if (context.Quizzes.Count() == 0)
                    { 
                        context.Questions.AddRange(new TrueFalseQuestion { Body = "truefalse1" }, new TrueFalseQuestion { Body = "truefalse2" }, new MultipleChoiceQuestion { Body = "truefalse3" }, new MultipleChoiceQuestion { Body = "truefalse4" }); ;
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
