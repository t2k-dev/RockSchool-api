using RockSchool.Data.Repositories;
using RockSchool.BL.Dtos;
using RockSchool.Data.Entities;
using RockSchool.Data.Enums;

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
            var noteDtos = notes.Select(n => new Note
                {
                    NoteId = n.NoteId,
                    Description = n.Description,
                    Status = n.Status,
                    BranchId = n.BranchId,
                    CompleteDate = n.CompleteDate,
                    ActualCompleteDate = n.ActualCompleteDate,
                })
                .ToArray();

            return noteDtos;
        }

        public async Task<bool> AddNoteAsync(int branchId, string description, DateTime? completeDate)
        {
            var note = new NoteEntity
            {
                BranchId = branchId,
                Description = description,
                CompleteDate = completeDate,
                Status = NoteStatus.New,
            };

            return await _noteRepository.AddNote(note);
        }

        public async Task UpdateNoteAsync(Note note)
        {
            var existingEntity = await _noteRepository.GetByIdAsync(note.NoteId);

            if (existingEntity == null)
                throw new KeyNotFoundException(
                    $"TeacherEntity with ID {note.NoteId} was not found.");

            ModifyNoteAttributes(note, existingEntity);

            await _noteRepository.UpdateAsync(existingEntity);
        }

        public async Task MarkComplete(Guid noteId)
        {
            var note = await _noteRepository.GetByIdAsync(noteId);
            note.Status = NoteStatus.Completed;
            note.ActualCompleteDate = DateTime.Now.ToUniversalTime();

            await _noteRepository.UpdateAsync(note);
        }

        private static void ModifyNoteAttributes(Note updatedNote, NoteEntity existingEntity)
        {
            if (updatedNote.Status != default)
                existingEntity.Status = updatedNote.Status;

            if (updatedNote.Description!= default)
                existingEntity.Description = updatedNote.Description;

            if (updatedNote.Comment != default)
                existingEntity.Comment = updatedNote.Comment;

            if (updatedNote.CompleteDate != default)
                existingEntity.CompleteDate = updatedNote.CompleteDate;

            if (updatedNote.ActualCompleteDate != default)
                existingEntity.ActualCompleteDate = updatedNote.ActualCompleteDate;
        }
    }
}
