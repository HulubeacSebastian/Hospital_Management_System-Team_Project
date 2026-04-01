using DevCoreHospital.Models;
using System;
using System.Collections.Generic;

namespace DevCoreHospital.Repositories
{

    public interface IFatigueAuditRepository
    {
  
        IReadOnlyList<RosterShift> GetAllShifts();

    
        IReadOnlyList<StaffProfile> GetStaffProfiles();

        bool ReassignShift(int shiftId, int newStaffId);
    }
}
