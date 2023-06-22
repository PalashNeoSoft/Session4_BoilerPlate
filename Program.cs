using Microsoft.EntityFrameworkCore;
using Session4_BoilerPlate.AddDbContext;
using Session4_BoilerPlate.Configurations;
using Session4_BoilerPlate.Repository;

using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Hosting;


namespace Session4_BoilerPlate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            string con = builder.Configuration.GetConnectionString("localConnectionString");
            builder.Services.AddDbContext<ProductDbContext>(p => p.UseSqlServer(con));
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
           
            //Logging the logs
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()    
                 .WriteTo.File("C:\\Users\\User\\source\\repos\\.NetCore\\ADO\\Asp.net\\BiolerPlate\\Session4_BoilerPlate\\log.txt", rollingInterval: RollingInterval.Day)
                 .CreateLogger();

            builder.Logging.AddSerilog();

            //API Versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });


            //Encoding - Decoding
            builder.Services.AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo("C:\\Users\\User\\source\\repos\\.NetCore\\ADO\\Asp.net\\BiolerPlate\\Session4_BoilerPlate")) // Specify the directory to persist the keys
                    .SetDefaultKeyLifetime(TimeSpan.FromDays(30))
                    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
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