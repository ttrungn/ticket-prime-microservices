using System.Text.Json.Serialization;

namespace CoreService.Application.Venues.Commands.ImportVenue.Dtos;

public class SeatDto
{
    [JsonPropertyName("seat_number")]
    public string SeatNumber { get; init; } = null!;
    [JsonPropertyName("seat_guid")]
    public string SeatGuid { get; init; } = null!;
    [JsonPropertyName("uuid")]
    public string Uuid { get; init; } = null!;
    [JsonPropertyName("position")]
    public PositionDto Position { get; init; } = null!;
    [JsonPropertyName("category")]
    public string Category { get; init; } = null!;
    [JsonPropertyName("radius")]
    public int Radius { get; init; }
}
