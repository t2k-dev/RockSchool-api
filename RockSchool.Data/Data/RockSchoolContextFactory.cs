using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RockSchool.Data.Data;

public class RockSchoolContextFactory : IDesignTimeDbContextFactory<RockSchoolContext>
{
    public RockSchoolContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RockSchoolContext>();

        var connectionString =
            "UserEntity ID=postgres;Password=azsxdc!2;Host=192.168.50.107;Port=5433;Database=RockSchoolDB;Pooling=true;";

        optionsBuilder.UseSqlServer(connectionString);

        return new RockSchoolContext(optionsBuilder.Options);
    }
}