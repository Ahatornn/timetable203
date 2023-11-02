using Microsoft.EntityFrameworkCore;
using TimeTable203.Api.Infrastructures;
using TimeTable203.Context.DB;
using TimeTable203.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.GetSwaggerDocument();

builder.Services.AddLoggerRegistr();

builder.Services.AddDependences();

var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<TimeTableContext>(
     options =>
     {
         options.UseSqlServer(conString);
         options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
     },
    ServiceLifetime.Scoped);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.GetSwaggerDocumentUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
