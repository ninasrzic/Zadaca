using AzilEdu.Api.Data;
using AzilEdu.Shared.Models;
using Microsoft.EntityFrameworkCore;

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

    if (!await db.Volunteers.AnyAsync())
    {
        db.Volunteers.AddRange(
            new Volunteer
            {
                FirstName = "Jakov",
                LastName = "Puž",
                Email = "jakovpuz@example.com",
                Phone = "093 456 123",
                Skills = "Šetnja i hranjenje pasa",
                AvailableFrom = new DateTime(2026, 2, 5),
                Notes = "-.",
                VolunteerStatusId = 2
            },
            new Volunteer
            {
                FirstName = "Anja",
                LastName = "Anjić",
                Email ="aanjnj@example.com",
                Phone = "097 836 231",
                Skills = "Šetnja i hranjenje pasa",
                AvailableFrom = new DateTime(2026, 12, 2),
                Notes = "Alergija na mačke",
                VolunteerStatusId = 3
            },
            new Volunteer
            {
                FirstName = "Slavko",
                LastName = "Kul",
                Email = "skul@example.com",
                Phone = "099 999 9999",
                Skills = "Čišćenje kaveza",
                AvailableFrom = new DateTime(2026, 7, 3),
                Notes = "Probni rok",
                VolunteerStatusId = 1
            }
        );
    }
    if (!await db.Donors.AnyAsync())
    {
        db.Donors.AddRange(
            new Donor
            {
                FirstName = "Jana",
                LastName = "Janić",
                Email = "jana.janic@example.com",
                Phone = "099 121 1212",
                Address = "Solinska 7",
                City = "Split",
                Notes = "nema",
                CreatedAt = new DateTime(2026, 5, 20),
                DonorTypeId = 1,
                DonorStatusId = 2
            },
            new Donor
            {
                OrganizationName = "ZaPse d.o.o",
                Email = "zapse@example.com",
                Phone = "098 878 122",
                Address = "Pujanke 12",
                City = "Split",
                Notes = "Potencijalni donator opreme.",
                CreatedAt = new DateTime(2026, 7, 2),
                DonorTypeId = 2,
                DonorStatusId = 1
            }
        );
    }
    if (!await db.Employees.AnyAsync())
    {
        db.Employees.AddRange(
            new Employee
            {
                FirstName = "Jan",
                LastName = "Janić",
                Email = "jan.janić@aziledu.example.com",
                Phone = "095 545 234",
                EmployeeNumber = "EMP-001",
                HireDate = new DateTime(2020, 2, 12),
                Notes = "Voditelj smjene.",
                EmployeePositionId = 3,
                EmployeeStatusId = 1
            },
            new Employee
            {
                FirstName = "Lovre",
                LastName = "Lovrić",
                Email ="lovrelov@aziledu.example.com",
                Phone = "099 876 233",
                EmployeeNumber = "EMP-002",
                HireDate = new DateTime(2024, 9, 10),
                Notes = "..",
                EmployeePositionId = 1,
                EmployeeStatusId = 1
            }
        );
    }



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
    }

    await db.SaveChangesAsync();
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