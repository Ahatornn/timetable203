using AutoMapper;
using FluentAssertions;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Tests;
using TimeTable203.Repositories.Implementations;
using TimeTable203.Services.Automappers;
using TimeTable203.Services.Contracts.Interface;
using TimeTable203.Services.Implementations;
using Xunit;

namespace TimeTable203.Services.Tests.Tests
{
    public class TimeTableItemServiceTests : TimeTableContextInMemory
    {
        private readonly ITimeTableItemService timeTableItemService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableItemServiceTests"/>
        /// </summary>

        public TimeTableItemServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            timeTableItemService = new TimeTableItemService(
                new TimeTableItemReadRepository(Reader),
                new TimeTableItemWriteRepository(WriterContext),
                new DisciplineReadRepository(Reader),
                new GroupReadRepository(Reader),
                new EmployeeReadRepository(Reader),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение расписания по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await timeTableItemService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение расписания по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.TimeTableItem();
            await Context.TimeTableItems.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await timeTableItemService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.StartDate,
                    target.EndDate
                });
        }

        // <summary>
        /// Добавление расписания, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.TimeTableItemRequestModel();
            var discipline = TestDataGenerator.Discipline();
            await Context.Disciplines.AddAsync(discipline);
            var group = TestDataGenerator.Group();
            await Context.Groups.AddAsync(group);
            var person = TestDataGenerator.Person();
            await Context.Persons.AddAsync(person);
            var teacher = TestDataGenerator.Employee();
            teacher.PersonId = person.Id;
            teacher.EmployeeType = EmployeeTypes.Teacher;
            await Context.Employees.AddAsync(teacher);
            target.Discipline = discipline.Id;
            target.Group = group.Id;
            target.Teacher = teacher.Id;

            //Act
            var act = await timeTableItemService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.TimeTableItems.Single(x =>
                x.Id == act.Id &&
                x.DisciplineId == target.Discipline &&
                x.GroupId == target.Group &&
                x.TeacherId == target.Teacher
            );
            entity.Should().NotBeNull();

            teacher.EmployeeType.Should().Be(EmployeeTypes.Teacher);
        }

        /// <summary>
        /// Изменение расписания, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.TimeTableItem();
            var discipline = TestDataGenerator.Discipline();
            var disciplineModel = TestDataGenerator.Discipline();
            await Context.Disciplines.AddRangeAsync(discipline, disciplineModel);
            var group = TestDataGenerator.Group();
            var groupModel = TestDataGenerator.Group();
            await Context.Groups.AddRangeAsync(group, groupModel);
            var person = TestDataGenerator.Person();
            var personModel = TestDataGenerator.Person();
            await Context.Persons.AddRangeAsync(person, personModel);
            var teacher = TestDataGenerator.Employee();
            teacher.PersonId = person.Id;
            teacher.EmployeeType = EmployeeTypes.Teacher;
            var teacherModel = TestDataGenerator.Employee();
            teacherModel.PersonId = personModel.Id;
            teacherModel.EmployeeType = EmployeeTypes.Teacher;
            await Context.Employees.AddRangeAsync(teacher, teacherModel);
            target.DisciplineId = discipline.Id;
            target.GroupId = group.Id;
            target.TeacherId = teacher.Id;
            await Context.TimeTableItems.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.TimeTableItemRequestModel();
            targetModel.Id = target.Id;
            targetModel.Discipline = disciplineModel.Id;
            targetModel.Group = groupModel.Id;
            targetModel.Teacher = teacherModel.Id;

            //Act
            var act = await timeTableItemService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.TimeTableItems.Single(x =>
               x.Id == act.Id &&
                x.DisciplineId == targetModel.Discipline &&
                x.GroupId == targetModel.Group &&
                x.TeacherId == targetModel.Teacher
            );
            entity.Should().NotBeNull();
            teacherModel.EmployeeType.Should().Be(EmployeeTypes.Teacher);
        }

        /// <summary>
        /// Удаление расписания, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.TimeTableItem();
            await Context.TimeTableItems.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => timeTableItemService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.TimeTableItems.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}

