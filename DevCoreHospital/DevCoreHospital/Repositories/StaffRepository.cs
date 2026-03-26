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
        private List<Staff> _staffList;
        private DatabaseManager _dbManager;

        public StaffRepository(DatabaseManager dbManager)
        {
            this._staffList = new List<Staff>();
            this._dbManager = dbManager;
        }
        public void LoadStaff()
        {
            _staffList = _dbManager.GetStaff();
        }
        public List<Doctor> GetAvailableDoctors()
        {
            var availableDoctors = _dbManager.GetStaff().OfType<Doctor>().Where(doctor => doctor.available).ToList();
            return availableDoctors;
        }
        private object GetAvailablePharmacists()
        {
            var availablePharmacists = _dbManager.GetStaff().OfType<Pharmacyst>.Where(ph => ph.available).ToList();
            return availablePharmacists
        }
        public List<Staff> getAvailableStaff(string doctorSpecialization, string pharmacystCertification)
        {
            var availableDoctors = GetAvailableDoctors();
            var availablePharmacists = GetAvailablePharmacists();
            // filter the 2 lists and merge them together into one list
            var filteredDoctors = availableDoctors.Where(doctor => doctor.specialization.Equals(doctorSpecialization));
            var filteredPharmacists;

        }
        public void RegisterStaff(Staff newStaff)
        {
            // Here you would add code to save the new staff member to the database
            // For now, we will just add it to the local list
            _staffList.Add(newStaff);
        }
        public void RemoveStaff(int staffId)
        {
            var staffToRemove = _staffList.FirstOrDefault(staff => staff.staffID == staffId);
            if (staffToRemove != null)
            {
                // Here you would add code to remove the staff member from the database
                // For now, we will just remove it from the local list
                _staffList.Remove(staffToRemove);
            }
        }
        public List<Doctor> GetDoctorsBySpecialization(string specialization)
        {
            var doctors = _dbManager.GetStaff().OfType<Doctor>().Where(doctor => doctor.specialization.Equals(specialization, StringComparison.OrdinalIgnoreCase)).ToList();
            return doctors;
        }

        //public void UpdateStaffAvailability(int staffId, bool isAvailable, DoctorStatus status = DoctorStatus.OFF_DUTY)
        //{
        //    var staff = _staffList.FirstOrDefault(staff => staff.staffID == staffId);
        //    if (staff != null)
        //    {
        //        staff.available = isAvailable;
        //        if (staff is Doctor doc) doc.doctorStatus = status;
        //    }
        //}
    }
}
