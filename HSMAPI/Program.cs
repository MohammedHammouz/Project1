using HSMAPI.Filters;
using HSMAPI.Middleware;
using HSMBusiness;
using HSMBusiness.Erorrs;
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
            builder.Services.AddControllers(options=>{ 
                options.Filters.Add<LogActivityFilter>(); 
            });
            // options.Filters.Add<LogActivityFilter>(); 
            //لما اكون بدي احط 
            // Global filter:بيشتغل عاي اكشن بال بالكونترولير
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
            builder.Services.AddScoped<AppointmentError>();
            builder.Services.AddScoped<AppointmentService>();
            builder.Services.AddScoped<AuditLogRepository>();
            builder.Services.AddScoped<AuditLogService>();
            builder.Services.AddScoped<BillRepository>();
            builder.Services.AddScoped<BillService>();
            builder.Services.AddScoped<BillServiceRepository>();
            builder.Services.AddScoped<BillServiceService>();
            builder.Services.AddScoped<DepartmentRepository>();
            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddScoped<DoctorRepository>();
            builder.Services.AddScoped<DoctorError>();
            builder.Services.AddScoped<DoctorService>();
            builder.Services.AddScoped<EmployeeRepository>();
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<MedicalRecordRepository>();
            builder.Services.AddScoped<MedicalRecordService>();
            builder.Services.AddScoped<NotifictionRepository>();
            builder.Services.AddScoped<NotifictionService>();
            builder.Services.AddScoped<PatientRepository>();
            builder.Services.AddScoped<PatientService>();
            builder.Services.AddScoped<PersonRepository>();
            builder.Services.AddScoped<PersonService>();
            builder.Services.AddScoped<ReportRepository>();
            builder.Services.AddScoped<ReportService>();
            builder.Services.AddScoped<ServicesCategoryRepository>();
            builder.Services.AddScoped<ServicesCategoryService>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<UserError>();
            builder.Services.AddScoped<UserService>();
           
            var app = builder.Build();  
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //Any thing app.Use it means middleware
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();
            app.UseAuthorization();
            
            app.UseMiddleware<ProfilingMiddleware>();
            app.UseMiddleware<RateLimitingMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
}