
using Intelligent.Solution.API.Extensions;
using Intelligent.Solution.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Intelligent.Solution.API
{
    /// <summary>
    /// Defines the <see cref="Program"/> class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the <see cref="Main(string[])"/> function
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<IntelligentContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("SampleDb"), x =>
                {
                    x.EnableRetryOnFailure(3);
                }); 
            });

            builder.Services.RegisterDependencies();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Intelligent API",
                    Description = "Sample Boilerplate API solution",
                    Contact = new OpenApiContact
                    {
                        Email = "debasis.yours@gmail.com",
                        Name = "Debasis Chakraborty"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "No License"                        
                    }
                });

                var xmlPath = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlPath), true);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
