using HSMBusiness;
using HSMBusiness.Services;
using HSMDataAccess.Data;
using HSMDataAccess.DTOs;
using HSMDataAccess.RepositoryServices;
using Microsoft.EntityFrameworkCore;
namespace HSMAPI
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
            var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine($"Connection String: {connectionString}");
            }
            builder.Services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<PersonRepository>();
            builder.Services.AddScoped<PersonService>();
            builder.Services.AddScoped<DoctorRepository>();
            builder.Services.AddScoped<DoctorService>();
            builder.Services.AddScoped<EmployeeRepository>();
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<UserService>();
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
