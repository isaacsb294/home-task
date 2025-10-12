using Domain.Chores;

namespace Domain.UnitTests.Chores;

internal static class ChoreData
{
    public const string Name = "Test Task Item";
    public const string Description = "This is a test task";
    public const ChorePriority Priority = ChorePriority.Medium;
    public const ChoreFrequency Frequency = ChoreFrequency.Weekly;
    public const ChoreCategory Category = ChoreCategory.Existential;
    public static DayOfWeek DayOfWeek = DayOfWeek.Wednesday;
}