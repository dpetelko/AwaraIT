using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApi.Entities.Enums;

/// <summary>
/// Статус заявки
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ApplicationStatus
{
    [EnumMember(Value = "Черновик")] Draft,
    [EnumMember(Value = "На согласовании")] OnApproval,
    [EnumMember(Value = "Согласовано")] Approved,
    [EnumMember(Value = "Отклонено")] Rejected
}