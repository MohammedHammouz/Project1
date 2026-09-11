using FluentValidation;
using HSMBusiness;
using HSMBusiness.Features.Doctors.Commands.CreateDoctor;
using HSMBusiness.Features.Doctors.Commands.DeleteDoctor;
using HSMBusiness.Features.Doctors.Commands.UpdateDoctor;
using HSMBusiness.Features.Doctors.Queries.GetDoctorById;
using HSMBusiness.Features.Doctors.Queries.GetDoctors;
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
            builder.Services.AddScoped<AppointmentRepository>();
            builder.Services.AddScoped<AuditLogRepository>();
            builder.Services.AddScoped<BillRepository>();
            builder.Services.AddScoped<DepartmentRepository>();
            builder.Services.AddScoped<DoctorRepository>();
            builder.Services.AddScoped<EmployeeRepository>();
            builder.Services.AddScoped<MedicalRecordRepository>();
            builder.Services.AddScoped<MedicalServiceRepository>();
            builder.Services.AddScoped<MedicalServiceBillRepository>();
            builder.Services.AddScoped<NotifictionRepository>();
            builder.Services.AddScoped<PatientRepository>();
            builder.Services.AddScoped<PersonRepository>();
            builder.Services.AddScoped<ReportRepository>();
            builder.Services.AddScoped<ServicesCategoryRepository>();
            builder.Services.AddScoped<UserRepository>();

            var handlerAssembly = typeof(CreateDoctorCommand).Assembly;

            var handlerTypes = handlerAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract
                    && (t.Name.EndsWith("CommandHandler") || t.Name.EndsWith("QueryHandler")));

            foreach (var handlerType in handlerTypes)
            {
                builder.Services.AddScoped(handlerType);
            }

            builder.Services.AddValidatorsFromAssembly(handlerAssembly);
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
