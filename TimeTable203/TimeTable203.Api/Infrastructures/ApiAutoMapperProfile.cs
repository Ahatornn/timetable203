using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TimeTable203.Api.Models;
using TimeTable203.Api.Models.Enums;
using TimeTable203.Api.ModelsRequest.Discipline;
using TimeTable203.Api.ModelsRequest.Document;
using TimeTable203.Api.ModelsRequest.Employee;
using TimeTable203.Api.ModelsRequest.Group;
using TimeTable203.Api.ModelsRequest.Person;
using TimeTable203.Api.ModelsRequest.TimeTableItem;
using TimeTable203.Api.ModelsRequest.TimeTableItemRequest;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.Models.Enums;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Api.Infrastructures
{
    /// <summary>
    /// Профиль маппера АПИшки
    /// </summary>
    public class ApiAutoMapperProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiAutoMapperProfile"/>
        /// </summary>
        public ApiAutoMapperProfile()
        {
            CreateMap<DocumentTypesModel, DocumentTypesResponse>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();
            CreateMap<EmployeeTypesModel, EmployeeTypesResponse>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<DisciplineModel, DisciplineResponse>(MemberList.Destination);
            CreateMap<DisciplineRequest, DisciplineModel>(MemberList.Destination);

            CreateMap<DocumentModel, DocumentResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Person != null
                    ? $"{x.Person.LastName} {x.Person.FirstName} {x.Person.Patronymic}"
                    : string.Empty))
                .ForMember(x => x.MobilePhone, opt => opt.MapFrom(x => x.Person != null
                    ? x.Person.Phone
                    : string.Empty))
                .ForMember(x => x.DocumentType, opt => opt.MapFrom(x => x.DocumentType));

            CreateMap<CreateDocumentRequest, DocumentRequestModel>(MemberList.Destination);
            CreateMap<DocumentRequest, DocumentRequestModel>(MemberList.Destination);

            CreateMap<EmployeeModel, EmployeeResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Person != null
                    ? $"{x.Person.LastName} {x.Person.FirstName} {x.Person.Patronymic}"
                    : string.Empty))
                .ForMember(x => x.MobilePhone, opt => opt.MapFrom(x => x.Person != null
                    ? x.Person.Phone
                    : string.Empty))
                .ForMember(x => x.EmployeeType, opt => opt.MapFrom(x => x.EmployeeType));

            CreateMap<CreateEmployeeRequest, EmployeeRequestModel>(MemberList.Destination);
            CreateMap<EmployeeRequest, EmployeeRequestModel>(MemberList.Destination);

            CreateMap<PersonModel, PersonResponse>(MemberList.Destination);
            CreateMap<CreatePersonRequest, PersonRequestModel>(MemberList.Destination);
            CreateMap<PersonRequest, PersonRequestModel>(MemberList.Destination);

            CreateMap<GroupModel, GroupResponse>(MemberList.Destination);
            CreateMap<CreateGroupRequest, GroupRequestModel>(MemberList.Destination);
            CreateMap<GroupRequest, GroupRequestModel>(MemberList.Destination);

            CreateMap<TimeTableItemModel, TimeTableItemResponse>(MemberList.Destination)
                .ForMember(x => x.NameDiscipline, opt => opt.MapFrom(x => x.Discipline!.Name))
                .ForMember(x => x.NameGroup, opt => opt.MapFrom(x => x.Group!.Name))
                .ForMember(x => x.TeacherName, opt => opt.MapFrom(x => $"{x.Teacher!.LastName} {x.Teacher.FirstName} {x.Teacher.Patronymic}"))
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Teacher!.Phone));
            CreateMap<CreateTimeTableItemRequest, TimeTableItemRequestModel>(MemberList.Destination);
            CreateMap<TimeTableItemRequest, TimeTableItemRequestModel>(MemberList.Destination);


        }
    }

}
