﻿using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Serilog;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.Models.Enums;
namespace TimeTable203.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<DocumentTypes, DocumentTypesModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<EmployeeTypes, EmployeeTypesModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<Discipline, DisciplineModel>(MemberList.Destination);

            CreateMap<Person, PersonModel>(MemberList.Destination);

            CreateMap<Document, DocumentModel>(MemberList.Destination);

            CreateMap<Employee, EmployeeModel>(MemberList.Destination)
                .ForMember(x => x.Person, next => next.Ignore());

            CreateMap<Group, GroupModel>(MemberList.Destination)
                .ForMember(x => x.Students, next => next.Ignore())
                .ForMember(x => x.ClassroomTeacher, next => next.Ignore());

            CreateMap<TimeTableItem, TimeTableItemModel>(MemberList.Destination)
                .ForMember(x => x.Group, next => next.Ignore())
                .ForMember(x => x.Discipline, next => next.Ignore())
                .ForMember(x => x.Teacher, next => next.Ignore());

            Log.Information("Инициализирован Mapper в классе ServiceProfile");
        }
    }
}
