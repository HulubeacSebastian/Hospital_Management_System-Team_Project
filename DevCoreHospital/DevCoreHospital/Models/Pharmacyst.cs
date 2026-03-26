namespace DevCoreHospital.Models
{
    internal class Pharmacyst : Staff
    {
        public int staffID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string contactInfo { get; set; }
        public bool available { get; set; }
        public string certification { get; set; }

        public Pharmacyst() { }
        public Pharmacyst(int staffID, string firstName, string lastName, string contactInfo, bool available, string certification)
        {
            this.staffID = staffID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.contactInfo = contactInfo;
            this.available = available;
            this.certification = certification;
        }
    }
}