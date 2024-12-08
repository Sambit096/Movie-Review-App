using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Data;
using MovieReviewApp.Implementations;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Services;
using System.Text.Json.Serialization; // Ensure this is included

var builder = WebApplication.CreateBuilder(args);

// Remove the first AddControllers() call
// builder.Services.AddControllers();

// Register MovieReviewDbContext with dependency injection
builder.Services.AddDbContext<MovieReviewDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options => {
    options.AddPolicy("MyAllowSpecificOrigins", policy => {
        policy.AllowAnyOrigin() // Use specific origin for production
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// **Single AddControllers() call with JsonStringEnumConverter**
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

// Register services
builder.Services.AddScoped<IShowTimeService, ShowTimeService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

// Add logging if not already added
builder.Services.AddLogging();
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
app.Run();
