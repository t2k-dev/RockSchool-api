using RockSchool.Domain.Enums;

namespace RockSchool.Domain.Entities;

public class Note
{
    public Guid NoteId { get; private set; }
    public int BranchId { get; private set; }
    public Branch Branch { get; private set; }
    public string Description { get; private set; }
    public NoteStatus Status { get; private set; }
    public DateTime? CompleteDate { get; private set; }
    public DateTime? ActualCompleteDate { get; private set; }
    public string Comment { get; private set; }

    private Note() { }

    public static Note Create(string description, DateTime? completeDate, int branchId)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required", nameof(description));

        if (branchId <= 0)
            throw new ArgumentException("Branch ID is required", nameof(branchId));

        return new Note
        {
            NoteId = Guid.NewGuid(),
            Status = NoteStatus.New,
            Description = description,
            CompleteDate = completeDate,
            BranchId = branchId
        };
    }

    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required", nameof(description));

        Description = description;
    }

    public void UpdateCompleteDate(DateTime? completeDate)
    {
        CompleteDate = completeDate;
    }

    public void Complete(DateTime actualCompleteDate, string comment = null)
    {
        if (Status == NoteStatus.Completed)
            throw new InvalidOperationException("Note is already completed");

        Status = NoteStatus.Completed;
        ActualCompleteDate = actualCompleteDate;
        Comment = comment;
    }

    public void Reopen()
    {
        Status = NoteStatus.New;
        ActualCompleteDate = null;
    }

    public void AddComment(string comment)
    {
        Comment = comment;
    }
}
