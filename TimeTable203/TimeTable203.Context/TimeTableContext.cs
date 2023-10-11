﻿using Microsoft.EntityFrameworkCore;
using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Context.DB;

namespace TimeTable203.Context
{
    public class TimeTableContext : ITimeTableContext, IContextAnchor
    {
        private readonly IList<Discipline> disciplines;
        private readonly IList<Document> documents;
        private readonly IList<Employee> employees;
        private readonly IList<Group> groups;
        private readonly IList<Person> persons;
        private readonly IList<TimeTableItem> timeTableItems;

        private readonly DbContextOptions<TimeTableApplicationContext> options;
        public TimeTableContext()
        {
            options = DataBaseHelper.Options();
            using (var timeTableApplicationContext = new TimeTableApplicationContext(options))
            {
                disciplines = timeTableApplicationContext.Disciplines.ToList();
                documents = timeTableApplicationContext.Documents.ToList();
                employees = timeTableApplicationContext.Employees.ToList();
                groups = timeTableApplicationContext.Groups.ToList();
                persons = timeTableApplicationContext.Persons.ToList();
                timeTableItems = timeTableApplicationContext.TimeTableItems.ToList();
            }
        }
        IEnumerable<Discipline> ITimeTableContext.Disciplines => disciplines;

        IEnumerable<Document> ITimeTableContext.Documents => documents;

        IEnumerable<Employee> ITimeTableContext.Employees => employees;

        IEnumerable<Group> ITimeTableContext.Groups => groups;

        IEnumerable<Person> ITimeTableContext.Persons => persons;

        IEnumerable<TimeTableItem> ITimeTableContext.TimeTableItems => timeTableItems;

        /*
        private void Seed()
        {
            var person1 = new Person
            {
                Id = new Guid("1cc8c3fc-daf3-4eef-b145-bfbc51348a66"),
                FirstName = "FirstName1",
                Phone = "Phone1",
                Patronymic = "Patronymic1",
                Email = "Email1",
                LastName = "LastName1",
            };
            var person2 = new Person
            {
                Id = new Guid("1cc8c3fc-daf3-4eef-b145-bfbc51348a65"),
                FirstName = "FirstName2",
                Phone = "Phone2",
                Patronymic = "Patronymic2",
                Email = "Email2",
                LastName = "LastName2",
            };

            persons.Add(person1);
            persons.Add(person2);

            var employee1 = new Employee
            {
                Id = Guid.NewGuid(),
                PersonId = person1.Id,
                EmployeeType = EmployeeTypes.Student,
            };

            var employee2 = new Employee
            {
                Id = new Guid("1cc8c3fc-daf3-4eef-b145-bfbc00000000"),
                PersonId = person2.Id,
                EmployeeType = EmployeeTypes.Teacher,
            };

            employees.Add(employee1);
            employees.Add(employee2);

            var dicsipline1 = new Discipline
            {
                Id = Guid.NewGuid(),
                Name = "Русский язык",
                Description = "Предмет обязательный ",
            };

            var dicsipline2 = new Discipline
            {
                Id = new Guid("1cc8c3fc-daf3-4eef-b145-bfbc51348a11"),
                Name = "Математика",
                Description = "Предмет обязательный ",
            };
            var dicsipline3 = new Discipline
            {
                Id = Guid.NewGuid(),
                Name = "Философия",
                Description = "Предмет не обязательный ",
                DeletedAt = DateTimeOffset.Now
            };

            disciplines.Add(dicsipline1);
            disciplines.Add(dicsipline2);
            disciplines.Add(dicsipline3);

            var document = new Document
            {
                Id = Guid.NewGuid(),
                Number = "2132",
                Series = "21323",
                DocumentType = DocumentTypes.Passport,
                IssuedAt = DateTime.Now,
                IssuedBy = "Вапа",
                PersonId = new Guid("1cc8c3fc-daf3-4eef-b145-bfbc51348a65")
            };
            var document2 = new Document
            {
                Id = Guid.NewGuid(),
                Number = "Очевидно",
                Series = "Ух ты, работает!",
                DocumentType = DocumentTypes.BirthCertificate,
                IssuedAt = DateTime.Now,
                IssuedBy = "нет",
                PersonId = new Guid("1cc8c3fc-daf3-4eef-b145-bfbc51348a66")
            };
            documents.Add(document);
            documents.Add(document2);
            var group = new Group
            {
                Id = new Guid("1cc8c3fc-daf3-4eef-b145-bfbc51348a00"),
                Name = "ИП-20-3",
                Description = "Информационные системы и программирования",
                EmployeeId = new Guid("1cc8c3fc-daf3-4eef-b145-bfbc00000000"),
            };
            groups.Add(group);
            var timeTableItem = new TimeTableItem
            {
                Id = Guid.NewGuid(),
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddHours(6),
                DisciplineId = new Guid("1cc8c3fc-daf3-4eef-b145-bfbc51348a11"),
                RoomNumber = 123,
                GroupId = new Guid("1cc8c3fc-daf3-4eef-b145-bfbc51348a00"),
                Teacher = new Guid("1cc8c3fc-daf3-4eef-b145-bfbc00000000")
            };
            timeTableItems.Add(timeTableItem);


        }
        */
    }
}
