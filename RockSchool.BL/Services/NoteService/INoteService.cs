using RockSchool.Domain.Entities;

namespace RockSchool.BL.Services.NoteService
{
    public interface INoteService
    {
        Task<Note[]?> GetNotesAsync(int branchId);
        Task<bool> AddNoteAsync(int branchId, string description, DateTime? completeDate);
        Task UpdateNoteAsync(Note note);
        Task MarkComplete(Guid noteId);
    }
}
