using System.Text.Json.Serialization;

namespace AnalyticsService.Service.Model;

/// <summary>
/// A record encapsulating a cloud event.
/// </summary>
internal sealed record CloudEvent<T>(
    [property: JsonPropertyName("source")]
    string Source,
    [property: JsonPropertyName("topic")]
    string Topic,
    [property: JsonPropertyName("data")]
    T Data
);