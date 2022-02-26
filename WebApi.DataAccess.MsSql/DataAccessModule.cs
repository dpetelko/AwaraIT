

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Infrastructure.Interfaces.DataAccess;
using WebApi.Utils.Modules;

namespace WebApi.DataAccess.MsSql;

/// <summary>
/// Модуль доступа к БД
/// </summary>
public class DataAccessModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddDbContext<IDbContext, AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")));
    }
}