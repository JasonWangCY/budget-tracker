using System.Text.Json;
using System.Text.Json.Serialization;

namespace BudgetTracker.Application.Utils;

public static class GeneralUtils
{
    public static string? ConcatNonEmptyStringsWithVerticalBar(IEnumerable<string> strArray)
    {
        return ConcatNonEmptyStringsWithVerticalBar(strArray.ToArray());
    }

    public static string? ConcatNonEmptyStringsWithVerticalBar(params string[] strArray)
    {
        string? result = null;

        foreach (var str in strArray)
        {
            if (string.IsNullOrEmpty(str)) continue;
            result = string.Concat(result, str, "|");
        }

        return result;
    }

    public static string DumpJson(this object obj)
    {
        return JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}
