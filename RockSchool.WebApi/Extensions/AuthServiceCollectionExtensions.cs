using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RockSchool.BL.Services.EmailService;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.WebApi.Options;
using RockSchool.WebApi.Security;
using RockSchool.WebApi.Security.Passwords;
using RockSchool.WebApi.Security.Tokens;
using RockSchool.WebApi.Services.Auth;
using System.Security.Claims;
using System.Text;

namespace RockSchool.WebApi.Extensions;

public static class AuthServiceCollectionExtensions
{
    public static IServiceCollection AddRockSchoolAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authBehaviorOptions = configuration.GetSection(AuthBehaviorOptions.SectionName).Get<AuthBehaviorOptions>()
                                  ?? new AuthBehaviorOptions();

        services
            .AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .Validate(options => options.ExpirationInMinutes > 0, "JWT expiration must be greater than zero.")
            .ValidateOnStart();

        services
            .AddOptions<AuthBehaviorOptions>()
            .Bind(configuration.GetSection(AuthBehaviorOptions.SectionName))
            .ValidateOnStart();

        services
            .AddOptions<EmailOptions>()
            .Bind(configuration.GetSection(EmailOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddIdentityCore<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<RockSchoolContext>();

        services.AddScoped<IUserAccountService, UserAccountService>();
        services.AddScoped<IAuthRegistrationService, AuthRegistrationService>();
        services.AddScoped<IAuthLoginService, AuthLoginService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddSingleton<IPasswordGenerator, PasswordGenerator>();

        services
            .AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<IOptions<JwtOptions>>((options, jwtOptionsAccessor) =>
            {
                var jwtOptions = jwtOptionsAccessor.Value;
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = signingKey,
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

        if (authBehaviorOptions.UseAuthorization)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy(AuthorizationPolicyNames.AuthenticatedUser, policy =>
                    policy.RequireAuthenticatedUser());

                options.AddPolicy(AuthorizationPolicyNames.SuperAdmin, policy =>
                    policy.RequireRole(RockSchoolRoles.SuperAdmin));

                options.AddPolicy(AuthorizationPolicyNames.Admin, policy =>
                    policy.RequireRole(RockSchoolRoles.Admin));

                options.AddPolicy(AuthorizationPolicyNames.Teacher, policy =>
                    policy.RequireRole(RockSchoolRoles.Teacher));

                options.AddPolicy(AuthorizationPolicyNames.Student, policy =>
                    policy.RequireRole(RockSchoolRoles.Student));
            });
        }
        else
        {
            var allowAllPolicy = new AuthorizationPolicyBuilder()
                .RequireAssertion(_ => true)
                .Build();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = allowAllPolicy;
                options.FallbackPolicy = allowAllPolicy;

                options.AddPolicy(AuthorizationPolicyNames.AuthenticatedUser, allowAllPolicy);
                options.AddPolicy(AuthorizationPolicyNames.SuperAdmin, allowAllPolicy);
                options.AddPolicy(AuthorizationPolicyNames.Admin, allowAllPolicy);
                options.AddPolicy(AuthorizationPolicyNames.Teacher, allowAllPolicy);
                options.AddPolicy(AuthorizationPolicyNames.Student, allowAllPolicy);
            });
        }

        return services;
    }
}
