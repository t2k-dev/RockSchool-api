using RockSchool.Data.Data;
using RockSchool.Domain.Repositories;

namespace RockSchool.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RockSchoolContext _dbContext;

        public UnitOfWork(RockSchoolContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //UpdateAuditableEntities();

            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        /*
        private void UpdateAuditableEntities()
        {
            IEnumerable<EntityEntry<IAuditableEntity>> entries =
                _dbContext
                    .ChangeTracker
                    .Entries<IAuditableEntity>();

            foreach (EntityEntry<IAuditableEntity> entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(a => a.CreatedOnUtc)
                        .CurrentValue = DateTime.UtcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(a => a.ModifiedOnUtc)
                        .CurrentValue = DateTime.UtcNow;
                }
            }
        }
        */
    }

}
