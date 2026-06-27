using AzilEdu.Api.Data;
using AzilEdu.Shared.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AzilEduDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AzilEduDbContext>();

    await db.Database.MigrateAsync();

    if (!await db.HousingUnits.AnyAsync())
    {

        db.HousingUnits.AddRange(
            new HousingUnit
            {
                Id = 1,
                Name = "Istočni boks",
                UnitType = "Boks",
                Capacity = 20,
                Occupied = 9,
                IsActive = true,
                LastCleanedAt = new DateTime(2026, 5, 10),
                ImageUrl = "/images/housing-units/box-1.png"
            },

        new HousingUnit
        {
            Id = 2,
            Name = "Južni boks",
            UnitType = "Boks",
            Capacity = 15,
            Occupied = 8,
            IsActive = true,
            LastCleanedAt = new DateTime(2026, 4, 13),
            ImageUrl = "/images/housing-units/box-2.png"
        },

        new HousingUnit
        {
            Id = 3,
            Name = "Novi boks",
            UnitType = "Boks",
            Capacity = 10,
            Occupied = 6,
            IsActive = true,
            LastCleanedAt = new DateTime(2026, 6, 7),
            ImageUrl = "/images/housing-units/box-3.png"
        },

        new HousingUnit
        {
            Id = 4,
            Name = "Mačkosvijet",
            UnitType = "Prostor za mačke",
            Capacity = 20,
            Occupied = 7,
            IsActive = true,
            LastCleanedAt = new DateTime(2026, 6, 25),
            ImageUrl = "/images/housing-units/cat-room.png"
        },

        new HousingUnit
        {
            Id = 5,
            Name = "Karantena",
            UnitType = "Karantena",
            Capacity = 1,
            Occupied = 1,
            IsActive = true,
            LastCleanedAt = new DateTime(2026, 6, 14),
            ImageUrl = "/images/housing-units/quarantine.webp"
        },

        new HousingUnit
        {
            Id = 6,
            Name = "Neaktivan boks",
            UnitType = "Boks",
            Capacity = 0,
            Occupied = 0,
            IsActive = false,
            LastCleanedAt = null,
            ImageUrl = "/images/housing-units/inactive-unit.webp"
        }
        );

        await db.SaveChangesAsync();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
