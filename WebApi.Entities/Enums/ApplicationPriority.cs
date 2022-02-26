using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace WebApi.Entities.Enums;

/// <summary>
/// Приоритет заявки
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ApplicationPriority
{
    [EnumMember(Value = "Низкий")] Low,
    [EnumMember(Value = "Средний")] Medium,
    [EnumMember(Value = "Высокий")] High
    
    
}