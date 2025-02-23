using RockSchool.Data.Data;

namespace RockSchool.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected BaseRepository(RockSchoolContext rockSchoolContext)
        {
            RockSchoolContext = rockSchoolContext;
        }

        public RockSchoolContext RockSchoolContext { get; set; }
    }
}
