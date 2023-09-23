using TimeTable203.Context;
using TimeTable203.Context.Contracts;
using TimeTable203.Repositories;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts;
using TimeTable203.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDisciplineService, DisciplineService>();
builder.Services.AddScoped<IDisciplineReadRepository, DisciplineReadRepository>();
builder.Services.AddSingleton<ITimeTableContext, TimeTableContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
