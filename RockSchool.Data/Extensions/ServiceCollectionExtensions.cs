using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRockSchoolData(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<RockSchoolContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}