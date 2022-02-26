using Microsoft.EntityFrameworkCore;
using WebApi.Entities.Enums;
using WebApi.Entities.Models;

namespace WebApi.DataAccess.MsSql;

public static class DataSeed
{
    public static void Seed(this ModelBuilder builder)
    {
       builder.Entity<User>().HasData(
            new User("Андрей Палтусов", Role.Chief, "andrey.paltusov@awara-it.com", "5555"),
            new User("Дмитрий Петелько", Role.Clerk, "dpetelko@gmail.com", "5555"),
            new User("Ирина Иванова", Role.Accountant, "irina.ivanova@awara-it.com", "5555"),
            new User("Иван Сергеев", Role.Initiator, "ivan.sergeev@awara-it.com", "5555"),
            new User("Петр Смирнов", Role.Chief, "petr.smirnov@awara-it.com", "5555")
        );
    }
}