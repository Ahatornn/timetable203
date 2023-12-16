using Microsoft.EntityFrameworkCore;
using TimeTable203.Api.Infrastructures;
using TimeTable203.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(x =>
{
    x.Filters.Add<TimeTableExceptionFilter>();
})
    .AddControllersAsServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.GetSwaggerDocument();

// У кого логгер есть - тот использует это
//builder.Services.AddLoggerRegistr();

builder.Services.AddDependencies();

var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<TimeTableContext>(options => options.UseSqlServer(conString),
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

public partial class Program { }
