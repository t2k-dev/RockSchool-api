using RockSchool.Data.Repositories;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;

namespace RockSchool.BL.Services.NoteService
{
    public class NoteService : INoteService
    {
        private readonly NoteRepository _noteRepository;

        public NoteService(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Note[]?> GetNotesAsync(int branchId)
        {
            var notes = await _noteRepository.GetNotes(branchId);

            return notes;
        }

        public async Task<bool> AddNoteAsync(int branchId, string description, DateTime? completeDate)
        {
            var note = Note.Create(description, completeDate, branchId);

            return await _noteRepository.AddNoteAsync(note);
        }

        public async Task UpdateNoteAsync(Note note)
        {
            throw new NotImplementedException("Note");
            var existingEntity = await _noteRepository.GetByIdAsync(note.NoteId);

            if (existingEntity == null)
                throw new KeyNotFoundException(
                    $"TeacherEntity with ID {note.NoteId} was not found.");


            await _noteRepository.UpdateAsync(existingEntity);
        }

        public async Task MarkComplete(Guid noteId)
        {
            var note = await _noteRepository.GetByIdAsync(noteId);
            
            note.Complete(DateTime.Now.ToUniversalTime());

            await _noteRepository.UpdateAsync(note);
        }
    }
}
