using System;
using System.Collections.Generic;

namespace WebApi.UseCases.Exceptions;

/// <summary>
/// Кастомное исключение для кейса, когда выявлена ошибка валидации
/// </summary>
public class ValidationException : Exception
{
    private new Dictionary<string, List<string>> Data { get; }

    public ValidationException(Dictionary<string, List<string>> data)
    {
        Data = data;
    }

    public ValidationException(string message) : base(message) { }
}