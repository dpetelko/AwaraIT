using System;
using System.Runtime.Serialization;

namespace WebApi.UseCases.Exceptions;

/// <summary>
/// Кастомное исключение для кейса, если сущность отсутствует в БД
/// </summary>
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message)
    {
    }
}