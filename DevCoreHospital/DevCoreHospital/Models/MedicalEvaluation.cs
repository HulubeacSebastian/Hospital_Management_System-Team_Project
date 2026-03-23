using System;

namespace DevCoreHospital.Models
{
    public class MedicalEvaluation
    {
        public int EvaluationID { get; set; }

        public int PatientID { get; set; }

        public Doctor Evaluator { get; set; }

        public string Symptoms { get; set; }

        public string DiagnosisResult { get; set; }

        public string MedsList { get; set; }

        public string DoctorNotes { get; set; }

        public DateTime EvaluationDate { get; set; }

        public string Source { get; set; }

        public MedicalEvaluation()
        {
            EvaluationDate = DateTime.Now;
            Source = "PATIENT";
        }
    }

    public class Doctor
    {
        public int DoctorID { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
    }
}