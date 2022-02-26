using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities.Models;

/// <summary>
/// Базовый класс сущности БД
/// </summary>
public abstract class Entity
{
    /// <summary> Первичный ключ </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}