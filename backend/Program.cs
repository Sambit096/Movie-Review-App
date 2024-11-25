using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Data;
using MovieReviewApp.Implementations;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Register MovieReviewDbContext with dependency injection
builder.Services.AddDbContext<MovieReviewDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Need to add ShowTimeService to Scope
builder.Services.AddScoped<IShowTimeService, ShowTimeService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddSwaggerGen();
/* builder.Services.AddSwaggerGen(c =>
 {
     var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
     var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
     c.IncludeXmlComments(xmlPath);
 });*/

var app = builder.Build();

app.UseCors("MyAllowSpecificOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    /* app.UseSwaggerUI(c =>
     {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Review API v1");
         c.RoutePrefix = string.Empty;  // To serve the Swagger UI at the app's root
     });*/
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Add a minimal API endpoint to test ticket data
app.MapGet("/testtickets", async (MovieReviewDbContext dbContext) =>
{
    var tickets = await dbContext.Tickets.ToListAsync();
    return Results.Ok(tickets);
})
.WithName("GetTestTickets")
.WithOpenApi();
app.Run();
/*var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}*/
