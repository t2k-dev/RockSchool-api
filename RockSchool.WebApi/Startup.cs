using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
using RockSchool.Data.Data;
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
        services.AddScoped<PaymentRepository>();
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
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        // Configure Identity
        services.AddIdentity<UserEntity, RoleEntity>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<RockSchoolContext>()
            .AddDefaultTokenProviders();

        // Configure JWT Authentication
        var jwtKey = Configuration["Jwt:Key"];
        var jwtIssuer = Configuration["Jwt:Issuer"];
        var jwtAudience = Configuration["Jwt:Audience"];

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

        services.AddAuthorization();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "RockSchool API", Version = "v1" });

            // Add JWT Authentication to Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

        app.UseCors();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}