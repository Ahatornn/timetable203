using AutoMapper;
using Microsoft.OpenApi.Models;
using TimeTable203.Context;
using TimeTable203.Context.Contracts;
using TimeTable203.Repositories.Contracts.Interface;
using TimeTable203.Repositories.Implementations;
using TimeTable203.Services.Automappers;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("Discipline", new OpenApiInfo{Title = "�������� ����������", Version = "v1"});
    c.SwaggerDoc("Document", new OpenApiInfo{Title = "�������� ���������", Version = "v1"});
    c.SwaggerDoc("Employee", new OpenApiInfo{Title = "�������� ���������", Version = "v1"});
    c.SwaggerDoc("Group", new OpenApiInfo{Title = "�������� ������", Version = "v1"});
    c.SwaggerDoc("Person", new OpenApiInfo{Title = "�������� �������", Version = "v1"});
    c.SwaggerDoc("TimeTableItem", new OpenApiInfo{Title = "�������� ������� ����������", Version = "v1"});
});


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

var mapperConfig = new MapperConfiguration(ms =>
{
    ms.AddProfile(new ServiceProfile());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("Discipline/swagger.json", "����������");
        x.SwaggerEndpoint("Document/swagger.json", "���������");
        x.SwaggerEndpoint("Employee/swagger.json", "���������");
        x.SwaggerEndpoint("Group/swagger.json", "������");
        x.SwaggerEndpoint("Person/swagger.json", "�������");
        x.SwaggerEndpoint("TimeTableItem/swagger.json", "������� ����������");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
