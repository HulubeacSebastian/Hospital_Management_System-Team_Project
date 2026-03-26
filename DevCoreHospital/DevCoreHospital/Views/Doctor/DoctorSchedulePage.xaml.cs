using DevCoreHospital.Data;
using DevCoreHospital.Services;
using DevCoreHospital.ViewModels.Doctor;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace DevCoreHospital.Views.Doctor
{
    public sealed partial class DoctorSchedulePage : Page
    {
        private readonly DoctorScheduleViewModel _vm;
        private readonly IDialogService _dialogService;
        private bool _initialized;

        public DoctorSchedulePage()
        {
            InitializeComponent();

            _dialogService = new DialogService();
            _vm = new DoctorScheduleViewModel(
                new CurrentUserService(),
                new DoctorAppointmentService(new SqlConnectionFactory()),
                _dialogService);

            DataContext = _vm;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _dialogService.SetXamlRoot(this.XamlRoot);

            if (_initialized) return;
            _initialized = true;

            await _vm.InitializeAsync();
        }

        private void OpenDatePicker_Click(object sender, RoutedEventArgs e)
        {
            InlineCalendarPicker.IsCalendarOpen = true;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is AppointmentItemViewModel item)
            {
                Frame?.Navigate(typeof(AppointmentDetailsPage), item);
            }
        }
    }
}