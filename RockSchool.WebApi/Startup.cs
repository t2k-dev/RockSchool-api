using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RockSchool.BL.Services.AttendanceService;
using RockSchool.BL.Services.BranchService;
using RockSchool.BL.Services.DisciplineService;
using RockSchool.BL.Services.NoteService;
using RockSchool.BL.Services.RoomService;
using RockSchool.BL.Services.ScheduledWorkingPeriodsService;
using RockSchool.BL.Services.ScheduleService;
using RockSchool.BL.Services.StudentService;
using RockSchool.BL.Services.SubscriptionService;
using RockSchool.BL.Services.TeacherService;
using RockSchool.BL.Services.UserService;
using RockSchool.Data.Entities;
using RockSchool.Data.Extensions;
using RockSchool.Data.Repositories;

namespace RockSchool.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
        services.AddScoped<IAttendanceService, AttendanceService>();
        services.AddScoped<IDisciplineService, DisciplineService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<ITrialSubscriptionService, TrialSubscriptionService>();
        services.AddScoped<IReschedulingService, ReschedulingService>();
        services.AddScoped<IScheduledWorkingPeriodsService, ScheduledWorkingPeriodsService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IAttendanceSubmitService, AttendanceSubmitService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICancelSubscriptionService, CancelSubscriptionService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IRentalSubscriptionService, RentalSubscriptionService>();
        services.AddScoped<ITenderService, TenderService>();

        services.AddScoped<RoomRepository>();
        services.AddScoped<SubscriptionRepository>();
        services.AddScoped<BranchRepository>();
        services.AddScoped<AttendanceRepository>();
        services.AddScoped<DisciplineRepository>();
        services.AddScoped<ScheduleRepository>();
        services.AddScoped<StudentRepository>();
        services.AddScoped<TeacherRepository>();
        services.AddScoped<UserRepository>();
        services.AddScoped<NoteRepository>();
        services.AddScoped<WorkingPeriodsRepository>();
        services.AddScoped<ScheduledWorkingPeriodsRepository>();
        services.AddScoped<TenderRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // services.AddControllers();
        // services.AddAutoMapper(this.GetType().Assembly);
        services.AddRockSchoolData(Configuration["DbContextSettings:ConnectionString"]!);
        services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        );
        services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RockSchool API v1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCors();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}