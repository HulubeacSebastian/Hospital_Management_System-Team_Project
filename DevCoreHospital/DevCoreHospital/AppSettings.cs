using Microsoft.UI.Xaml.Media;

namespace DevCoreHospital.Configuration;

public static class AppSettings
{
    public const string ConnectionString =
        @"Data Source=DESKTOP-BIG8P7V\SQLEXPRESS;Initial Catalog=HospitalApp;Integrated Security=True;Trust Server Certificate=True";

    public const int DefaultDoctorId = 1;
}
