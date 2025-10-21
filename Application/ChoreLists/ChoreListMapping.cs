using Domain.ChoreLists;

namespace Application.ChoreLists;

public static class ChoreListMapping
{
    public static ChoreListDto ToDto(this ChoreList choreList)
    {
        return new ChoreListDto
        {
            Id = choreList.Id,
            Description = choreList.Description,
            Name = choreList.Name
        };
    }
}