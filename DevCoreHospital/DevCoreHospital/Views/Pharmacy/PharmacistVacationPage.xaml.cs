using System;
using System.Collections.Generic;
using System.Linq;
using DevCoreHospital.Configuration;
using DevCoreHospital.Data;
using DevCoreHospital.Models;
using DevCoreHospital.Repositories;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace DevCoreHospital.Views.Pharmacy;

public sealed partial class PharmacistVacationPage : Page
{
    private readonly StaffRepository _staffRepository;
    private readonly ShiftRepository _shiftRepository;

    public PharmacistVacationPage()
    {
        InitializeComponent();

        var dbManager = new DatabaseManager(AppSettings.ConnectionString);
        _staffRepository = new StaffRepository(dbManager);
        _shiftRepository = new ShiftRepository(dbManager);

        LoadPharmacists();
    }

    private void LoadPharmacists()
    {
        var pharmacists = _staffRepository
            .GetPharmacists()
            .OrderBy(p => p.FirstName)
            .ThenBy(p => p.LastName)
            .Select(p => new PharmacistChoice(
                p,
                string.Join(" ", new[] { p.FirstName?.Trim(), p.LastName?.Trim() }.Where(x => !string.IsNullOrWhiteSpace(x)))))
            .ToList();

        PharmacistComboBox.ItemsSource = pharmacists;
    }

    private void AddVacationShift_Click(object sender, RoutedEventArgs e)
    {
        if (PharmacistComboBox.SelectedItem is not PharmacistChoice selected)
        {
            ShowMessage("Select a pharmacist first.", InfoBarSeverity.Warning);
            return;
        }

        if (StartDatePicker.Date is null || EndDatePicker.Date is null)
        {
            ShowMessage("Select both start and end dates.", InfoBarSeverity.Warning);
            return;
        }

        var startDate = StartDatePicker.Date.Value.Date;
        var endDate = EndDatePicker.Date.Value.Date;
        if (endDate < startDate)
        {
            ShowMessage("End date must be on or after start date.", InfoBarSeverity.Error);
            return;
        }

        var newStart = startDate;
        var newEndExclusive = endDate.AddDays(1);
        var pharmacistShifts = _shiftRepository.GetShiftsByStaffID(selected.Staff.StaffID);

        var overlappingShift = pharmacistShifts.FirstOrDefault(s =>
            newStart < s.EndTime &&
            newEndExclusive > s.StartTime);
        if (overlappingShift is not null)
        {
            ShowMessage("Cannot add vacation: this period overlaps an existing shift.", InfoBarSeverity.Error);
            return;
        }

        if (WouldExceedMonthlyVacationLimit(pharmacistShifts, newStart, newEndExclusive, 4))
        {
            ShowMessage("Cannot add vacation: pharmacist would exceed 4 vacation days in a month.", InfoBarSeverity.Error);
            return;
        }

        var allShifts = _shiftRepository.GetShifts();
        var nextId = allShifts.Count == 0 ? 1 : allShifts.Max(s => s.Id) + 1;

        var vacationShift = new Shift(
            nextId,
            selected.Staff,
            "Vacation",
            startDate,
            newEndExclusive,
            ShiftStatus.VACATION);

        _shiftRepository.AddShift(vacationShift);
        ShowMessage("Vacation shift added to repository.", InfoBarSeverity.Success);
    }

    private void ShowMessage(string message, InfoBarSeverity severity)
    {
        StatusInfoBar.Message = message;
        StatusInfoBar.Severity = severity;
        StatusInfoBar.IsOpen = true;
    }

    private static bool WouldExceedMonthlyVacationLimit(
        IEnumerable<Shift> staffShifts,
        DateTime newStartInclusive,
        DateTime newEndExclusive,
        int maxDaysPerMonth)
    {
        var daysByMonth = new Dictionary<(int Year, int Month), HashSet<DateTime>>();

        foreach (var shift in staffShifts.Where(s => s.Status == ShiftStatus.VACATION))
        {
            AddShiftDaysToBuckets(daysByMonth, shift.StartTime.Date, shift.EndTime.Date);
        }

        AddShiftDaysToBuckets(daysByMonth, newStartInclusive.Date, newEndExclusive.Date);

        return daysByMonth.Values.Any(set => set.Count > maxDaysPerMonth);
    }

    private static void AddShiftDaysToBuckets(
        Dictionary<(int Year, int Month), HashSet<DateTime>> buckets,
        DateTime startInclusive,
        DateTime endExclusive)
    {
        for (var day = startInclusive.Date; day < endExclusive.Date; day = day.AddDays(1))
        {
            var key = (day.Year, day.Month);
            if (!buckets.TryGetValue(key, out var set))
            {
                set = new HashSet<DateTime>();
                buckets[key] = set;
            }

            set.Add(day);
        }
    }

    private sealed record PharmacistChoice(Pharmacyst Staff, string DisplayName);
}
