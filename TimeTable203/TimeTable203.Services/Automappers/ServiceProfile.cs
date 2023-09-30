using AutoMapper;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Discipline, DisciplineModel>(MemberList.Destination);
        }
    }
}
