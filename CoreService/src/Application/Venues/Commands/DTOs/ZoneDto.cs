using System.Text.Json.Serialization;

namespace CoreService.Application.Venues.Commands.DTOs;

public class ZoneDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = null!;

    [JsonPropertyName("position")]
    public PositionDto Position { get; init; } = null!;

    [JsonPropertyName("rows")]
    public List<RowDto> Rows { get; init; } = new();

    [JsonPropertyName("areas")]
    public List<AreaDto> Areas { get; init; } = new();

    [JsonPropertyName("uuid")]
    public string Uuid { get; init; } = null!;

    [JsonPropertyName("zone_id")]
    public string ZoneId { get; init; } = null!;
}
