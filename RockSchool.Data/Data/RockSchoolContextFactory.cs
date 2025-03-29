using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RockSchool.Data.Data;

public class RockSchoolContextFactory : IDesignTimeDbContextFactory<RockSchoolContext>
{
    public RockSchoolContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RockSchoolContext>();

        var connectionString =
            "User ID=postgres;Password=azsxdc!2;Host=localhost;Port=5432;Database=RockSchoolDB;Pooling=true;";
        // Host=192.168.50.107;Port=5432;Database=trade_signals;Username=trade_signals_user;Password=Gl0balink121
        optionsBuilder.UseNpgsql(connectionString);

        return new RockSchoolContext(optionsBuilder.Options);
    }
}