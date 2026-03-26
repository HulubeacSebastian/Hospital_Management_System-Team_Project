namespace DevCoreHospital.Models
{
    public enum DoctorStatus
    {
        AVAILABLE,
        IN_EXAMINATION,
        OFF_DUTY
    }

    public class Doctor : Staff
    {
        public string? Specialization { get; set; }
        public string? LicenseNumber { get; set; }
        public DoctorStatus DoctorStatus { get; set; } = DoctorStatus.AVAILABLE;

        public string Id
        {
            get => StaffCode;
            set => StaffCode = value ?? string.Empty;
        }

        public string Name
        {
            get => DisplayName;
            set => DisplayName = value ?? string.Empty;
        }
    }
}