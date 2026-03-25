using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCoreHospital.Models;
using DevCoreHospital.Data;

namespace DevCoreHospital.Repositories
{
    public class StaffRepository
    {
        private List<Staff> staffList;
        private DatabaseManager dbManager;

        public StaffRepository(DatabaseManager dbManager)
        {
            this.staffList = new List<Staff>();
            this.dbManager = dbManager;
        }
        public void LoadStaff()
        {
            staffList = dbManager.GetStaff();
        }
        public List<Doctor> GetAvailableDoctors()
        {
            var availableDoctors = dbManager.GetStaff().OfType<Doctor>().Where(doctor => doctor.available).ToList();
            return availableDoctors;
        }
        public void RegisterStaff(Staff newStaff)
        {
            // Here you would add code to save the new staff member to the database
            // For now, we will just add it to the local list
            staffList.Add(newStaff);
        }
        public void RemoveStaff(int staffId)
        {
            var staffToRemove = staffList.FirstOrDefault(staff => staff.staffID == staffId);
            if (staffToRemove != null)
            {
                // Here you would add code to remove the staff member from the database
                // For now, we will just remove it from the local list
                staffList.Remove(staffToRemove);
            }
        }
        public List<Doctor> GetDoctorsBySpecialization(string specialization)
        {
            var doctors = dbManager.GetStaff().OfType<Doctor>().Where(doctor => doctor.specialization.Equals(specialization, StringComparison.OrdinalIgnoreCase)).ToList();
            return doctors;
        }

        //public void UpdateStaffAvailability(int staffId, bool isAvailable, DoctorStatus status = DoctorStatus.OFF_DUTY)
        //{
        //    var staff = staffList.FirstOrDefault(staff => staff.staffID == staffId);
        //    if (staff != null)
        //    {
        //        staff.available = isAvailable;
        //        if (staff is Doctor doc) doc.doctorStatus = status;
        //    }
        //}
    }
}
