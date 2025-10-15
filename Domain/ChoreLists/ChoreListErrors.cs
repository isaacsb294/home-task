using Shared;

namespace Domain.ChoreLists;

public static class ChoreListErrors
{
    public static readonly Error NotFound = new(
        "ChoreList.NotFound",
        "The chore list specified could not be found.");
}