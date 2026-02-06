using Microsoft.EntityFrameworkCore;
using RockSchool.Data.Data;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Notes;

namespace RockSchool.Data.Repositories
{
    public class NoteRepository : BaseRepository, INoteRepository
    {
        public NoteRepository(RockSchoolContext rockSchoolContext) : base(rockSchoolContext)
        {
        }

        public async Task<Note[]?> GetNotes(int branchId)
        {
            return await RockSchoolContext.Notes.Where(n => n.Branch.BranchId == branchId).ToArrayAsync();
        }

        public async Task<Note?> GetByIdAsync(Guid noteId)
        {
            return await RockSchoolContext.Notes.FirstOrDefaultAsync(n => n.NoteId == noteId);
        }

        public async Task<bool> AddNoteAsync(Note note)
        {
            await RockSchoolContext.Notes.AddAsync(note);
            await RockSchoolContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(Note note)
        {
            RockSchoolContext.Notes.Update(note);
            await RockSchoolContext.SaveChangesAsync();
        }
    }
}
