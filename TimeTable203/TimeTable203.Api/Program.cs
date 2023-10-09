using AutoMapper;
using Microsoft.OpenApi.Models;
using TimeTable203.Api.Infrastructures;
using TimeTable203.Services.Automappers;

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


builder.Services.AddDependences();

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
