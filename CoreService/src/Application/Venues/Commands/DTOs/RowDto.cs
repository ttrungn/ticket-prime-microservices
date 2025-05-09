using System.Text.Json.Serialization;

namespace CoreService.Application.Venues.Commands.DTOs;

public class RowDto
{
    [JsonPropertyName("position")]
    public PositionDto Position { get; init; } = null!;

    [JsonPropertyName("row_number")]
    public string RowNumber { get; init; } = null!;

    [JsonPropertyName("row_number_position")]
    public string RowNumberPosition { get; init; } = null!;

    public List<SeatDto> Seats { get; init; } = new();
    
    [JsonPropertyName("uuid")]
    public string Uuid { get; init; } = null!;
}
