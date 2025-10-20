using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure;

public static class JsonExtensions
{
    // Default options: camelCase, ignore nulls, enums as strings, not indented
    public static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    // Serialize any object (including null) to JSON
    public static string ToJson(this object? value, JsonSerializerOptions? options = null)
        => JsonSerializer.Serialize(value, options ?? DefaultOptions);

    // Deserialize JSON to a specific type
    public static T? FromJson<T>(this string json, JsonSerializerOptions? options = null)
        => string.IsNullOrWhiteSpace(json)
            ? default
            : JsonSerializer.Deserialize<T>(json, options ?? DefaultOptions);

    // Non-generic overload when the target type is only known at runtime
    public static object? FromJson(this string json, Type returnType, JsonSerializerOptions? options = null)
        => string.IsNullOrWhiteSpace(json)
            ? null
            : JsonSerializer.Deserialize(json, returnType, options ?? DefaultOptions);
}