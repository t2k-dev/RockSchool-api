using RockSchool.BL.Services.TariffService;
using RockSchool.Domain.Entities;
using RockSchool.Domain.Enums;
using RockSchool.Domain.Repositories;

namespace RockSchool.BL.Subscriptions.Rehearsal;

public class RehearsalSubscriptionService(
    ISubscriptionRepository subscriptionRepository,
    IAttendanceRepository attendanceRepository,
    IAttendeeRepository attendeeRepository,
    ITariffService tariffService,
    IBandRepository bandRepository,
    IBandMemberRepository bandMemberRepository,
    IUnitOfWork unitOfWork) : IRehearsalSubscriptionService
{
    public async Task AddRehearsal(AddRehearsalDto addRehearsalDto)
    {
        var tariff = await tariffService.GetTariffAsync(addRehearsalDto.TariffId);
        if (tariff == null)
        {
            throw new InvalidOperationException("Tariff is not found");
        }

        if (tariff.SubscriptionType != SubscriptionType.Rehearsal)
        {
            throw new InvalidOperationException("Tariff is not a rehearsal tariff");
        }

        var band = await bandRepository.GetByIdAsync(addRehearsalDto.BandId);
        if (band == null)
        {
            throw new InvalidOperationException("Band is not found");
        }

        var bandMembers = await bandMemberRepository.GetByBandIdAsync(addRehearsalDto.BandId);
        if (bandMembers.All(bm => bm.StudentId != addRehearsalDto.StudentId))
        {
            throw new InvalidOperationException("Student is not a member of the band");
        }

        var now = DateTime.Now;
        var selectedAttendances = (await attendanceRepository.GetByBandIdAsync(addRehearsalDto.BandId))
            .Where(a => a.StartDate >= now)
            .OrderBy(a => a.StartDate)
            .Take(tariff.AttendanceCount)
            .ToArray();

        if (selectedAttendances.Length != tariff.AttendanceCount)
        {
            throw new InvalidOperationException("Not enough future band attendances found");
        }

        if (selectedAttendances.Any(a => a.Attendees.Any(att => att.StudentId == addRehearsalDto.StudentId)))
        {
            throw new InvalidOperationException("Student is already linked to one or more selected band attendances");
        }

        var subscription = Subscription.Create(
            addRehearsalDto.StudentId,
            selectedAttendances[0].BranchId,
            DateOnly.FromDateTime(now),
            tariff.AttendanceCount,
            tariff.AttendanceLength,
            SubscriptionType.Rehearsal,
            tariff.Amount,
            tariff.Amount,
            null,
            null,
            band.TeacherId);

        await subscriptionRepository.AddAsync(subscription);

        foreach (var attendance in selectedAttendances)
        {
            var attendee = Attendee.Create(subscription.SubscriptionId, attendance.AttendanceId, addRehearsalDto.StudentId);
            await attendeeRepository.AddAsync(attendee);
        }

        await unitOfWork.SaveChangesAsync();
    }
}
