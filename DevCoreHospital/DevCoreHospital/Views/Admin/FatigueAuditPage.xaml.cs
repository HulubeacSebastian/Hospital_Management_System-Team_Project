using DevCoreHospital.Data;
using DevCoreHospital.Configuration;
using DevCoreHospital.Services;
using DevCoreHospital.ViewModels;
using DevCoreHospital.Repositories;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DevCoreHospital.Views.Admin
{
    public sealed partial class FatigueAuditPage : Page
    {
        private readonly FatigueShiftAuditViewModel _viewModel;
        private readonly IFatigueAuditService _auditService;
        private readonly IFatigueAuditRepository _auditRepository;
        private readonly SqlFatigueShiftDataSource _sqlDataSource;

        public FatigueAuditPage()
        {
            InitializeComponent();

            // Initialize SQL data source
            _sqlDataSource = new SqlFatigueShiftDataSource(AppSettings.ConnectionString);
            
            // Initialize repository
            _auditRepository = new FatigueAuditRepository(_sqlDataSource);
            
            // Initialize service with repository
            _auditService = new FatigueAuditService(_auditRepository);
            
            // Initialize ViewModel
            _viewModel = new FatigueShiftAuditViewModel(_auditService);

            // Set DataContext to ViewModel for binding
            DataContext = _viewModel;

            // Initialize week picker to today
            WeekStartPicker.Date = new DateTimeOffset(DateTime.Today);
        }

        private void WeekStartPicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (sender.Date.HasValue)
            {
                _viewModel.SelectedWeekStart = sender.Date.Value;
            }
        }

        private void RunAutoAudit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Run the audit through ViewModel - all UI updates handled automatically via binding
                _viewModel.RunAutoAudit();
            }
            catch (Exception ex)
            {
                _viewModel.StatusMessage = $"Error during audit: {ex.Message}";
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            // Clear violations and suggestions through ViewModel
            _viewModel.Violations.Clear();
            _viewModel.Suggestions.Clear();
        }

        private async void ApplyReassignment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int shiftId)
            {
                // Find the suggestion for this shift
                var suggestion = _viewModel.Suggestions.FirstOrDefault(s => s.ShiftId == shiftId);
                if (suggestion == null || !suggestion.SuggestedStaffId.HasValue)
                {
                    var errorDialog = new ContentDialog
                    {
                        Title = "Invalid Reassignment",
                        Content = "No valid reassignment candidate found for this shift.",
                        CloseButtonText = "OK",
                        XamlRoot = Content.XamlRoot
                    };
                    await errorDialog.ShowAsync();
                    return;
                }

                // Apply the reassignment through the repository
                bool success = _auditRepository.ReassignShift(shiftId, suggestion.SuggestedStaffId.Value);
                if (success)
                {
                    // Show confirmation dialog
                    var confirmDialog = new ContentDialog
                    {
                        Title = "Reassignment Applied",
                        Content = $"Shift #{shiftId} has been reassigned to {suggestion.SuggestedStaffName}.\n\nRunning audit to verify changes...",
                        CloseButtonText = "OK",
                        XamlRoot = Content.XamlRoot
                    };
                    await confirmDialog.ShowAsync();

                    // Auto-run audit again to show updated violations
                    _viewModel.RunAutoAudit();
                }
                else
                {
                    var failDialog = new ContentDialog
                    {
                        Title = "Reassignment Failed",
                        Content = "Could not reassign shift. Please try again.",
                        CloseButtonText = "OK",
                        XamlRoot = Content.XamlRoot
                    };
                    await failDialog.ShowAsync();
                }
            }
        }

        private void PublishRoster_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CanPublish)
            {
                var dialog = new ContentDialog
                {
                    Title = "Roster Published",
                    Content = $"The roster for the {_viewModel.WeekLabel} has been published successfully.",
                    CloseButtonText = "OK",
                    XamlRoot = Content.XamlRoot
                };
                dialog.ShowAsync();
            }
        }
        
    }
}

