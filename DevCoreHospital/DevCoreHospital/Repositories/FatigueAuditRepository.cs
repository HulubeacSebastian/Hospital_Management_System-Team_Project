using DevCoreHospital.Data;
using DevCoreHospital.Models;
using System;
using System.Collections.Generic;

namespace DevCoreHospital.Repositories
{
  
    public sealed class FatigueAuditRepository : IFatigueAuditRepository
    {
        private readonly IFatigueShiftDataSource _dataSource;

        public FatigueAuditRepository(IFatigueShiftDataSource dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

      
        public IReadOnlyList<RosterShift> GetAllShifts()
        {
            return _dataSource.GetAllShifts();
        }

        public IReadOnlyList<StaffProfile> GetStaffProfiles()
        {
            return _dataSource.GetStaffProfiles();
        }

        public bool ReassignShift(int shiftId, int newStaffId)
        {
            return _dataSource.ReassignShift(shiftId, newStaffId);
        }
    }
}
