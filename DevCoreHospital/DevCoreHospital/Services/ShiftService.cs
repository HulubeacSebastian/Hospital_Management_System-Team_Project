using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCoreHospital.Models;
using DevCoreHospital.Repositories;

namespace DevCoreHospital.Services
{
    public class ShiftService
    {
        private readonly StaffRepository _staffRepo;
        private readonly ShiftRepository _shiftRepo;

        public ShiftService(StaffRepository staffRepo, ShiftRepository shiftRepo)
        {
            _staffRepo = staffRepo;
            _shiftRepo = shiftRepo;
        }

        /*
         AssignStaffToShift(staffId): This method assigns a staff member to a shift. It checks if the staff member is available
        and if there are no overlapping shifts before creating a new shift entry in the database.

         */
        public void SetShiftActive(int shiftId)
        {
            var shift = _shiftRepo.GetShifts().FirstOrDefault(s => s.Id == shiftId);
            if (shift != null)
            {
                _shiftRepo.UpdateShiftStatus(shiftId, ShiftStatus.ACTIVE);
                _staffRepo.UpdateStaffAvailability(shift.AppointedStaff.staffID, true, DoctorStatus.AVAILABLE);
            }
        }

        public void CancelShift(int shiftId)
        {
            var shift = _shiftRepo.GetShifts().FirstOrDefault(s => s.Id == shiftId);
            if (shift != null)
            {
                _staffRepo.UpdateStaffAvailability(shift.AppointedStaff.staffID, false, DoctorStatus.OFF_DUTY);
                _shiftRepo.UpdateShiftStatus(shiftId, ShiftStatus.COMPLETED);
            }
        }

        public bool ValidateNoOverlap(int staffId, DateTime start, DateTime end)
        {
            return !_shiftRepo.GetShifts().Any(shift => (shift.AppointedStaff.staffID == staffId) &&
                ((start >= shift.StartTime && start < shift.EndTime) || (end > shift.StartTime && end <= shift.EndTime)));
        }
    }
}