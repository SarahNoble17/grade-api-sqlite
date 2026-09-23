using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Day 11: a real, persistent database instead of the static in-memory list.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=students.db"));

// Day 9: CORS, so the Angular app from Day 10 can actually call this API.
const string AngularClientPolicy = "AngularClient";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AngularClientPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS must be registered before MapControllers, or the policy silently
// never applies.
app.UseCors(AngularClientPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
