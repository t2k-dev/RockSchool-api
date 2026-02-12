namespace RockSchool.BL.Home
{
    public interface IHomeService
    {
        public Task<HomeDetailsDto> GetByBranch(int branchId);
    }
}
