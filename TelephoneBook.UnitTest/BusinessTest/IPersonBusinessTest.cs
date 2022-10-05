using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TelephoneBook.Api.Controllers;
using TelephoneBook.Business.PersonBusiness;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;
using Xunit;

namespace TelephoneBook.UnitTest.BusinessTest
{
    public class IPersonBusinessTest
    {
        private Mock<IPersonBusiness> personBusiness;
        private readonly PersonController personController;
        List<Person> persons;
        Person onePerson;
        Person createdTestPerson = new Person();
        PrimitiveResponse response = new PrimitiveResponse();
        public IPersonBusinessTest()
        {
            personBusiness = new Mock<IPersonBusiness>();
            personController = new PersonController(personBusiness.Object);
            persons = new List<Person>
            {
                new Person{Id = Guid.NewGuid(), Name="Test1", Surname ="Test1", Company ="test1", Deleted = false},
                new Person{Id = Guid.NewGuid(), Name="Test3", Surname ="Test3", Company ="test3", Deleted = false},
                new Person{Id = Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"), Name="Test2", Surname ="Test2", Company ="test2", Deleted = false}
            };
            onePerson =new Person { Id = Guid.NewGuid(), Name = "Create", Surname = "Test", Company = "CreateTest45456", Deleted = false };

            personBusiness.Setup(m => m.PersonList()).Returns(persons);

            personBusiness.Setup(m => m.GetPersonForEdit(Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"))).Returns(onePerson);

            personBusiness.Setup(m => m.CreatePerson(createdTestPerson)).Returns(response);

            personBusiness.Setup(m => m.DeletePerson(Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"))).Returns(response);

        }

        [Fact]
        public void Get_Person_For_Edit_Null_Test()
        {
            Guid id = new Guid();
            var result = personController.GetPersonForEdit(id);
            Assert.Null(result);
        }

        [Fact]
        public void Get_Person_For_Edit_Not_Null_Test()
        {
            var result = personController.GetPersonForEdit(Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"));
            Assert.NotNull(result);
        }

        [Fact]
        public void Person_List_Test()
        {
            List<Person> list = personController.PersonList();
            Assert.Equal(3, list.Count());
        }

        [Fact]
        public void Deleted_Person_True_Test()
        {
            var response = personController.DeletePerson(Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"));
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public void Deleted_Person_False_Test()
        {
            var response = personController.DeletePerson(Guid.Parse("8752c73b-0446-44ea-a25e-e4386c54d0ad"));
            Assert.False(response.IsSuccess);
        }

        [Fact]
        public void Create_Person__Test()
        {
            createdTestPerson.Id = Guid.NewGuid();
            createdTestPerson.Name = "Telephone";
            createdTestPerson.Surname = "Book";
            createdTestPerson.Company = "Test";
            createdTestPerson.Deleted = false;
            var response = personController.CreatePerson(createdTestPerson);
            Assert.True(response.IsSuccess);
        }


    }
}
