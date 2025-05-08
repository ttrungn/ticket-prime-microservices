using System.Text.Json.Serialization;

namespace CoreService.Application.Venues.Commands.ImportVenue.Dtos;

public class SizeDto
{
    [JsonPropertyName("width")]
    public int Width { get; init; }
    [JsonPropertyName("height")]
    public int Height { get; init; }
}
