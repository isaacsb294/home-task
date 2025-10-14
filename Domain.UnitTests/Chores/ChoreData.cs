using Domain.Chores;

namespace Domain.UnitTests.Chores;

internal static class ChoreData
{
    public static Guid UserId =  Guid.CreateVersion7();
    public static Guid ChoreListId = Guid.CreateVersion7();
    public const string Name = "Test Task Item";
    public const string Description = "This is a test task";
    public const ChorePriority Priority = ChorePriority.Medium;
    public const ChoreFrequency Frequency = ChoreFrequency.Weekly;
    public const ChoreCategory Category = ChoreCategory.Existential;
    public static DayOfWeek DayOfWeek = DayOfWeek.Wednesday;
}