using AutoMapper;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;
using TimeTable203.Services.Contracts.Exceptions;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;
using TimeTable203.Services.Helps;

namespace TimeTable203.Services.Implementations
{
    public class GroupService : IGroupService, IServiceAnchor
    {
        private readonly IGroupReadRepository groupReadRepository;
        private readonly IGroupWriteRepository groupWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IMapper mapper;

        public GroupService(IGroupReadRepository groupReadRepository,
            IGroupWriteRepository groupWriteRepository,
            IUnitOfWork unitOfWork,
            IPersonReadRepository personReadRepository,
            IEmployeeReadRepository employeeReadRepository,
            IMapper mapper)
        {
            this.groupReadRepository = groupReadRepository;
            this.groupWriteRepository = groupWriteRepository;
            this.unitOfWork = unitOfWork;
            this.personReadRepository = personReadRepository;
            this.employeeReadRepository = employeeReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<GroupModel>> IGroupService.GetAllAsync(CancellationToken cancellationToken)
        {
            var groups = await groupReadRepository.GetAllAsync(cancellationToken);
            var employeeIds = groups.Where(x => x.EmployeeId.HasValue)
                .Select(x => x.EmployeeId!.Value)
                .Distinct();
            var teacherDictionary = await employeeReadRepository.GetPersonByEmployeeIdsAsync(employeeIds, cancellationToken);

            var listGroupModel = new List<GroupModel>();
            foreach (var group in groups)
            {
                var groupModel = mapper.Map<GroupModel>(group);
                groupModel.ClassroomTeacher = group.EmployeeId.HasValue &&
                                              teacherDictionary.TryGetValue(group.EmployeeId!.Value, out var teacher)
                    ? mapper.Map<PersonModel>(teacher)
                    : null;

                var students = await personReadRepository.GetAllByGroupIdAsync(group.Id, cancellationToken);
                groupModel.Students = mapper.Map<ICollection<PersonModel>>(students);

                listGroupModel.Add(groupModel);
            }
            return listGroupModel;
        }

        async Task<GroupModel?> IGroupService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await groupReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            var groupModel = mapper.Map<GroupModel>(item);
            if (item.EmployeeId.HasValue)
            {
                var teacherDictionary = await employeeReadRepository.GetPersonByEmployeeIdsAsync(new[] { item.EmployeeId.Value }, cancellationToken);
                groupModel.ClassroomTeacher = teacherDictionary.TryGetValue(item.EmployeeId!.Value, out var teacher)
                    ? mapper.Map<PersonModel>(teacher)
                    : null;
            }

            var students = await personReadRepository.GetAllByGroupIdAsync(item.Id, cancellationToken);
            groupModel.Students = mapper.Map<ICollection<PersonModel>>(students);

            return groupModel;
        }

        async Task<GroupModel> IGroupService.AddAsync(GroupRequestModel groupRequestModel, CancellationToken cancellationToken)
        {
            var item = new Group
            {
                Id = Guid.NewGuid(),
                Name = groupRequestModel.Name,
                Description = groupRequestModel.Description,
            };

            var employeeValidate = new PersonHelpValidate(employeeReadRepository);
            var employee = await employeeValidate.GetEmployeeByIdTeacherAsync(groupRequestModel.ClassroomTeacher!.Value, cancellationToken);
            if (employee != null)
            {
                item.EmployeeId = employee.Id;
                item.Employee = employee;
            }

            groupWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<GroupModel>(item);
        }
        async Task<GroupModel> IGroupService.EditAsync(GroupModel source, CancellationToken cancellationToken)
        {
            var targetGroup = await groupReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetGroup == null)
            {
                throw new TimeTableEntityNotFoundException<Group>(source.Id);
            }
            var employeeValidate = new PersonHelpValidate(employeeReadRepository);
            var employee = await employeeValidate.GetEmployeeByIdTeacherAsync(source.ClassroomTeacher!.Id, cancellationToken);
            if (employee != null)
            {
                targetGroup.EmployeeId = employee.Id;
                targetGroup.Employee = employee;
            }

            targetGroup.Name = source.Name;
            targetGroup.Description = source.Description;

            groupWriteRepository.Update(targetGroup);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<GroupModel>(targetGroup);
        }

        async Task IGroupService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetGroup = await groupReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetGroup == null)
            {
                throw new TimeTableEntityNotFoundException<Group>(id);
            }

            if (targetGroup.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Группу с идентификатором {id} уже удалена");
            }

            groupWriteRepository.Delete(targetGroup);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
