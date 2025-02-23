using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Data.Entities;

namespace RockSchool.Data.Repositories
{
    public class NoteRepository : BaseRepository
    {
        public NoteRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
        {
        }

        public async Task<NoteEntity[]?> GetNotes(int branchId)
        {
            return await RockSchoolContext.Notes.Where(n => n.Branch.BranchId == branchId).ToArrayAsync();
        }

        public async Task<bool> AddNote(NoteEntity noteEntity)
        {
            await RockSchoolContext.Notes.AddAsync(noteEntity);
            await RockSchoolContext.SaveChangesAsync();
            return true;
        }
    }
}
