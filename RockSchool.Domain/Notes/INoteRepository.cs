using RockSchool.Domain.Entities;

namespace RockSchool.Domain.Notes
{
    public interface INoteRepository
    {
        Task<Note[]?> GetNotes(int branchId);
        Task<Note?> GetByIdAsync(Guid noteId);
        Task<bool> AddNoteAsync(Note note);
        Task UpdateAsync(Note note);
    }
}
