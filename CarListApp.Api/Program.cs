using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CarListApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors( o =>
            {
                o.AddPolicy("AllowAll", a => a.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });

            builder.Services.AddDbContext<CarListDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapGet("/cars", async (CarListDBContext db) => await db.Cars_Table.ToListAsync());
            app.MapGet("/cars/{id}", async (int id, CarListDBContext db) =>
                await db.Cars_Table.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound()
                );

            app.MapPut("/cars/{id}", async (int id, Car car, CarListDBContext db) =>
            {
                var record = await db.Cars_Table.FindAsync(id);
                if(record is null) return Results.NotFound();

                record.Make = car.Make;
                record.Model = car.Model;
                record.Vin = car.Vin;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/cars/{id}", async (int id, CarListDBContext db) =>
            {
                var record = await db.Cars_Table.FindAsync(id);
                if (record is null) return Results.NotFound();
                db.Remove(record);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapPost("/cars", async (Car car, CarListDBContext db) =>
            {
                await db.Cars_Table.AddAsync(car);
                await db.SaveChangesAsync();

                return Results.Created($"/cars/{car.Id}", car);
            });

            app.Run();
        }
    }
}