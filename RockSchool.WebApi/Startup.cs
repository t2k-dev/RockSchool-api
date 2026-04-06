using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RockSchool.BL.Attendances.Rescheduling;
using RockSchool.BL.Bands;
using RockSchool.BL.Home;
using RockSchool.BL.Schedules;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.AttendeeService;
using RockSchool.BL.Services.BranchService;
using RockSchool.BL.Services.BusySlotsService;
using RockSchool.BL.Services.DisciplineService;
using RockSchool.BL.Services.EmailService;
using RockSchool.BL.Services.NoteService;
using RockSchool.BL.Services.RoomService;
using RockSchool.BL.Services.SubscriptionDetailsService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.BL.Services.TariffService;
using RockSchool.BL.Students;
using RockSchool.BL.Students.AddStudent;
using RockSchool.BL.Subscriptions;
using RockSchool.BL.Subscriptions.Payments;
using RockSchool.BL.Subscriptions.Rehearsal;
using RockSchool.BL.Subscriptions.Trial;
using RockSchool.BL.Teachers;
using RockSchool.BL.Teachers.AddTeacher;
using RockSchool.BL.Teachers.AvailableTeachers;
using RockSchool.Data;
using RockSchool.Data.Extensions;
using RockSchool.Data.Repositories;
using RockSchool.Domain.Repositories;
using RockSchool.WebApi.Extensions;
using RockSchool.WebApi.Middleware;

namespace RockSchool.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IAttendanceService, AttendanceService>();
        services.AddScoped<IBandService, BandService>();
        services.AddScoped<IBandFormDataService, BandFormDataService>();
        services.AddScoped<IBandScreenDetailsService, BandScreenDetailsService>();
        services.AddScoped<IBandMemberService, BandMemberService>();
        services.AddScoped<IDisciplineService, DisciplineService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<ITrialSubscriptionService, TrialSubscriptionService>();
        services.AddScoped<IRehearsalSubscriptionService, RehearsalSubscriptionService>();
        services.AddScoped<IReschedulingService, ReschedulingService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IAttendanceSubmitService, AttendanceSubmitService>();
        services.AddScoped<IAttendeeService, AttendeeService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICancelSubscriptionService, CancelSubscriptionService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IRentalSubscriptionService, RentalSubscriptionService>();
        services.AddScoped<ITariffService, TariffService>();
        services.AddScoped<IHomeService, HomeService>();
        services.AddScoped<IBusySlotsService, BusySlotsService>();

        services.AddScoped<IAddTeacherService, AddTeacherService>();
        services.AddScoped<ITeacherScreenDetailsService, TeacherScreenDetailsService>();
        services.AddScoped<IAvailableTeachersService, AvailableTeachersService>();
        services.AddScoped<IAttendanceQueryService, AttendanceQueryService>();

        services.AddScoped<IStudentScreenDetailsService, StudentScreenDetailsService>();
        services.AddScoped<IAddStudentService, AddStudentService>();

        services.AddScoped<ISubscriptionScreenDetailsService, SubscriptionScreenDetailsService>();
        services.AddScoped<ISubscriptionFormDataService, SubscriptionFormDataService>();
        services.AddScoped<ISubscriptionGetService, SubscriptionGetService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBandRepository, BandRepository>();
        services.AddScoped<IBandMemberRepository, BandMemberRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IAttendeeRepository, AttendeeRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        services.AddScoped<IDisciplineRepository, DisciplineRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<IScheduleSlotRepository, ScheduleSlotRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IWorkingPeriodsRepository, WorkingPeriodsRepository>();
        services.AddScoped<IScheduledWorkingPeriodsRepository, ScheduledWorkingPeriodsRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ITariffRepository, TariffRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddRockSchoolData(Configuration["DbContextSettings:ConnectionString"]!);
        services.AddRockSchoolAuth(Configuration);

        services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        );

        services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RockSchool API v1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseRouting();
        app.UseCors("MyPolicy");
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
