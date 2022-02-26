using System;
using System.Text;
using WebApi.Entities.Enums;

namespace WebApi.Entities.Models;

/// <summary>
/// Сущность Пользователь
/// </summary>
public class User : Entity
{
    protected User() { }

    public User(
        string name,
        Role role,
        string login,
        string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Role = role;
        Login = login;
        Password = password;
    }

    /// <summary> Имя пользователя </summary>
    public string Name { get; private set; }
    
    /// <summary> Роль Пользователя </summary>
    public Role Role { get; private set; }
    
    /// <summary> Логин пользователя </summary>
    public string Login { get; private set; }
    
    /// <summary> Пароль пользователя </summary>
    public string Password { get; private set; }
}