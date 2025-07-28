using Microsoft.EntityFrameworkCore;
using MinimalApis.Data;
using MinimalApis.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.MapGet("/orders", async (AppDbContext db) => 
    await db.Orders.Include(o => o.Products).ToListAsync());

app.MapGet("/orders/{id:int}", async (int id, AppDbContext db) =>
    await db.Orders.Include(o => o.Products)
        .FirstOrDefaultAsync(o => o.Id == id) is Order order
        ? Results.Ok(order)
        : Results.NotFound());

app.MapPost("/orders", async (Order order, AppDbContext db) =>
{
    db.Orders.Add(order);
    await db.SaveChangesAsync();
    return Results.Created($"/orders/{order.Id}", order);
});

app.MapPut("/orders/{id:int}", async (int id, Order updatedOrder, AppDbContext db) =>
{
    var order = await db.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.Id == id);
    if (order is null) return Results.NotFound();

    order.Name = updatedOrder.Name;

    await db.SaveChangesAsync();
    return Results.Ok(order);
});

app.MapDelete("/orders/{id:int}", async(int id, AppDbContext db) =>
{
    var order = await db.Orders.FindAsync(id);
    if (order is null) return Results.NotFound();

    db.Orders.Remove(order);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
