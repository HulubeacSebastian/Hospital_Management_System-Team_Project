using DevCoreHospital.Data;
using DevCoreHospital.Configuration;
using DevCoreHospital.Services;
using DevCoreHospital.ViewModels;
using DevCoreHospital.Repositories;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace DevCoreHospital.Views
{
    public sealed partial class ShiftSwapPlaceholderPage : Page
    {
        public FatigueShiftAuditViewModel ViewModel { get; }

        public ShiftSwapPlaceholderPage()
        {
            InitializeComponent();

            var sqlDataSource = new SqlFatigueShiftDataSource(AppSettings.ConnectionString);
            var repository = new FatigueAuditRepository(sqlDataSource);
            var service = new FatigueAuditService(repository);

            ViewModel = new FatigueShiftAuditViewModel(service);

            DataContext = ViewModel;
        }

        private void RunAutoAudit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RunAutoAudit();
        }
    }
}