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

        public async Task<NoteDto[]?> GetNotesAsync(int branchId)
        {
            var notes = await _noteRepository.GetNotes(branchId);
            var noteDtos = notes.Select(n => new NoteDto
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

        public async Task MarkComplete(Guid noteId)
        {
            var note = await _noteRepository.GetNote(noteId);
            note.Status = NoteStatus.Completed;
            note.ActualCompleteDate = DateTime.Now.ToUniversalTime();

            await _noteRepository.UpdateAsync(note);
        }
    }
}
