using DevCoreHospital.ViewModels.Doctor;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace DevCoreHospital.Views.Doctor
{
    public sealed partial class AppointmentDetailsPage : Page
    {
        public string PatientLine { get; private set; } = "Patient: -";
        public string ReasonLine { get; private set; } = "Reason: -";
        public string TypeLine { get; private set; } = "Type: -";
        public string LocationLine { get; private set; } = "Location: Location TBD";
        public string StatusLine { get; private set; } = "Status: Unknown";
        public string TimeLine { get; private set; } = "Time: -";

        public AppointmentDetailsPage()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is AppointmentItemViewModel item)
            {
                PatientLine = $"Patient: {(string.IsNullOrWhiteSpace(item.PatientName) ? "Patient hidden/unknown" : item.PatientName)}";
                ReasonLine = $"Reason: {(string.IsNullOrWhiteSpace(item.Notes) ? "N/A" : item.Notes)}";
                TypeLine = $"Type: {(string.IsNullOrWhiteSpace(item.Type) ? "N/A" : item.Type)}";
                LocationLine = $"Location: {(string.IsNullOrWhiteSpace(item.Location) ? "Location TBD" : item.Location)}";
                StatusLine = $"Status: {(string.IsNullOrWhiteSpace(item.Status) ? "Unknown" : item.Status)}";
                TimeLine = $"Time: {item.Date:yyyy-MM-dd} {item.StartTime:hh\\:mm} - {item.EndTime:hh\\:mm}";
            }

            DataContext = null;
            DataContext = this;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Frame?.CanGoBack == true)
                Frame.GoBack();
        }
    }
}