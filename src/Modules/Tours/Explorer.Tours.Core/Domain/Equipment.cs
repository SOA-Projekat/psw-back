using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class Equipment : Entity
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public int TourId { get; init; }

    public Equipment(string name, string? description, int tourId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
        Name = name;
        Description = description;
        TourId = tourId;
    }
}