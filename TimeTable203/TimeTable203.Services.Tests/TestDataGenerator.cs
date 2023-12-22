using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Services.Tests
{
    static internal class TestDataGenerator
    {
        static internal Discipline Discipline(Action<Discipline>? settings = null)
        {
            var result = new Discipline
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Description = $"Description{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            settings?.Invoke(result);
            return result;
        }
        static internal DisciplineModel DisciplineModel(Action<DisciplineModel>? settings = null)
        {
            var result = new DisciplineModel
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Description = $"Description{Guid.NewGuid():N}"
            };

            settings?.Invoke(result);
            return result;
        }

        static internal Document Document(Action<Document>? action = null)
        {
            var item = new Document
            {
                Id = Guid.NewGuid(),
                DocumentType = DocumentTypes.Passport,
                IssuedAt = DateTime.UtcNow,
                IssuedBy = $"IssuedBy{Guid.NewGuid():N}",
                Number = $"Number{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }
        static internal DocumentRequestModel DocumentRequestModel(Action<DocumentRequestModel>? settings = null)
        {
            var result = new DocumentRequestModel
            {
                Id = Guid.NewGuid(),
                Number = $"Number{Guid.NewGuid():N}",
                Series = $"Series{Guid.NewGuid():N}",
                DocumentType = DocumentTypes.Passport,
                IssuedAt = DateTime.UtcNow,
                IssuedBy = $"IssuedBy{Guid.NewGuid():N}",
            };

            settings?.Invoke(result);
            return result;
        }
        static internal Person Person(Action<Person>? action = null)
        {
            var item = new Person
            {
                Id = Guid.NewGuid(),
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
                Patronymic = $"Patronymic{Guid.NewGuid():N}",
                Email = $"Email{Guid.NewGuid():N}",
                Phone = $"Phone{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal PersonRequestModel PersonRequestModel(Action<PersonRequestModel>? action = null)
        {
            var item = new PersonRequestModel
            {
                Id = Guid.NewGuid(),
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
                Patronymic = $"Patronymic{Guid.NewGuid():N}",
                Email = $"Email{Guid.NewGuid():N}",
                Phone = $"Phone{Guid.NewGuid():N}"
            };

            action?.Invoke(item);
            return item;
        }

        static internal Employee Employee(Action<Employee>? action = null)
        {
            var item = new Employee
            {
                Id = Guid.NewGuid(),
                EmployeeType = EmployeeTypes.Student,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal EmployeeRequestModel EmployeeRequestModel(Action<EmployeeRequestModel>? action = null)
        {
            var item = new EmployeeRequestModel
            {
                Id = Guid.NewGuid(),
                EmployeeType = EmployeeTypes.Student,
            };

            action?.Invoke(item);
            return item;
        }

        static internal Group Group(Action<Group>? action = null)
        {
            var item = new Group
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Description = $"Description{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }
        static internal GroupRequestModel GroupRequestModel(Action<GroupRequestModel>? action = null)
        {
            var item = new GroupRequestModel
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Description = $"Description{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal TimeTableItem TimeTableItem(Action<TimeTableItem>? action = null)
        {
            var item = new TimeTableItem
            {
                Id = Guid.NewGuid(),
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow.AddDays(1),
                RoomNumber = (short)Random.Shared.Next(0, 100),
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal TimeTableItemRequestModel TimeTableItemRequestModel(Action<TimeTableItemRequestModel>? action = null)
        {
            var item = new TimeTableItemRequestModel
            {
                Id = Guid.NewGuid(),
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow.AddDays(1),
                RoomNumber = (short)Random.Shared.Next(0, 100),
            };

            action?.Invoke(item);
            return item;
        }
    }
}
