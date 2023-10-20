using Microsoft.EntityFrameworkCore;
using TimeTable203.Api.Infrastructures;
using TimeTable203.Context.Contracts;
using TimeTable203.Context.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.GetSwaggerDocument();

builder.Services.AddDependences();

var conString = DataBaseHelper.GetConnectingString();
builder.Services.AddDbContextFactory<TimeTableContext>(Options => Options.UseSqlServer(conString));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.GetSwaggerDocumentUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
