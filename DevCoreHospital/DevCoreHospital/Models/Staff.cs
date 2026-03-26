using System.Linq;

namespace DevCoreHospital.Models;

public class Staff
{
    public int StaffID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactInfo { get; set; } = string.Empty;
    public bool Available { get; set; } = true;

    public void UpdateAvailability(bool newAvailability) => Available = newAvailability;

    public int Id
    {
        get => StaffID;
        set => StaffID = value;
    }

    public string StaffCode
    {
        get => ContactInfo;
        set => ContactInfo = value ?? string.Empty;
    }

    public string DisplayName
    {
        get
        {
            var fn = FirstName?.Trim() ?? string.Empty;
            var ln = LastName?.Trim() ?? string.Empty;
            var full = string.Join(" ", new[] { fn, ln }.Where(s => !string.IsNullOrWhiteSpace(s)));
            return string.IsNullOrWhiteSpace(full) ? _displayName : full;
        }
        set => _displayName = value ?? string.Empty;
    }
    private string _displayName = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string? Specialization { get; set; }

    public bool IsAvailable
    {
        get => Available;
        set => Available = value;
    }
}
