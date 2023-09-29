using TimeTable203.Context;
using TimeTable203.Context.Contracts;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Repositories.Implementations;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDisciplineService, DisciplineService>();
builder.Services.AddScoped<IDisciplineReadRepository, DisciplineReadRepository>();

builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IDocumentReadRepository, DocumentReadRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();

builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGroupReadRepository, GroupReadRepository>();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonReadRepository, PersonReadRepository>();

builder.Services.AddScoped<ITimeTableItemService, TimeTableItemService>();
builder.Services.AddScoped<ITimeTableItemReadRepository, TimeTableItemReadRepository>();

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
