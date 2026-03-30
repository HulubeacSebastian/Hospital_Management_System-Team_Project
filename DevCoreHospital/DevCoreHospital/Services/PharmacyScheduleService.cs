using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevCoreHospital.Models;
using DevCoreHospital.Repositories;

namespace DevCoreHospital.Services;

public sealed class PharmacyScheduleService : IPharmacyScheduleService
{
    private readonly IShiftRepository _shiftRepo;

    public PharmacyScheduleService(IShiftRepository shiftRepo)
    {
        _shiftRepo = shiftRepo;
    }

    public Task<IReadOnlyList<Shift>> GetShiftsAsync(int pharmacistStaffId, DateTime rangeStart, DateTime rangeEnd)
    {
        // Run the potentially blocking database call off the UI thread
        return Task.Run<IReadOnlyList<Shift>>(
            () => _shiftRepo.GetShiftsForStaffInRange(pharmacistStaffId, rangeStart, rangeEnd));
    }
}
