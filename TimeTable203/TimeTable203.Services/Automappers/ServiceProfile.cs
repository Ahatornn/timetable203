﻿using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.Models.Enums;
using TimeTable203.Services.Implementations;

namespace TimeTable203.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<DocumentTypes, DocumentTypesModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<Discipline, DisciplineModel>(MemberList.Destination);

            CreateMap<Person, PersonModel>(MemberList.Destination);

            CreateMap<Document, DocumentModel>(MemberList.Destination)
                .ForMember(x => x.Person, next => next.Ignore());

            CreateMap<Employee, EmployeeModel>(MemberList.Destination);
            CreateMap<Person, EmployeeModel>(MemberList.Destination)
                .ForMember(x => x.Person, next => next.MapFrom(e => e));

            CreateMap<Group, GroupModel>(MemberList.Destination);
            CreateMap<Employee, GroupModel>(MemberList.Destination)
                .ForMember(x => x.Employee, next => next.MapFrom(e => e));

            CreateMap<TimeTableItem, TimeTableItemModel>(MemberList.Destination)
                .ForMember(x => x.Group, next => next.MapFrom(e => new Group()))
                .ForMember(x => x.Discipline, next => next.MapFrom(e => new Discipline()))
                .ForMember(x => x.Teacher, next => next.MapFrom(e => new Person()));
            CreateMap<Discipline, TimeTableItemModel>(MemberList.Destination)
                .ForMember(x => x.Discipline, next => next.MapFrom(e => e));
            CreateMap<Group, TimeTableItemModel>(MemberList.Destination)
                .ForMember(x => x.Group, next => next.MapFrom(e => e));
            CreateMap<Person, TimeTableItemModel>(MemberList.Destination)
                .ForMember(x => x.Teacher, next => next.MapFrom(e => e));
        }
    }
}
