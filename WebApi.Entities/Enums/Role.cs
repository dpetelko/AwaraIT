using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApi.Entities.Enums;

/// <summary>
/// Роль Пользователя
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum Role
{
    [EnumMember(Value = "Клерк")] Clerk,
    [EnumMember(Value = "Инициатор")] Initiator,
    [EnumMember(Value = "Бухгалтер")] Accountant,
    [EnumMember(Value = "Менеджер")] Manager,
    [EnumMember(Value = "Руководитель")] Chief
    
}