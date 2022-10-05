using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TelephoneBook.Api.Controllers;
using TelephoneBook.Business.ContactInfoBusiness;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;
using Xunit;

namespace TelephoneBook.UnitTest.BusinessTest
{
    public class IContactInfoBusinessTest
    {
        private Mock<IContactInfoBusiness> contactInfoBusiness;
        private readonly ContactInfoController contactInfoController;
        List<ContactInfo> contactInfos;
        ContactInfo oneContactInfo;
        ContactInfo createdTestModel = new ContactInfo();
        PrimitiveResponse response = new PrimitiveResponse();
        public IContactInfoBusinessTest()
        {
            contactInfoBusiness = new Mock<IContactInfoBusiness>();
            contactInfoController = new ContactInfoController(contactInfoBusiness.Object);
            contactInfos = new List<ContactInfo>
            {
                new ContactInfo{Id = Guid.Parse("c1187b54-c519-4e91-a862-9c4d972188b6"), FkPerson = Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"), Mail = "merve@merve.com", Phone = "1234 125 12 14", Location = "Ankara", Deleted = false },

                new ContactInfo{Id = Guid.NewGuid(), FkPerson = Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"), Mail = "merve2@merve2.com", Phone = "1498 125 87 14", Location = "İstanbul", Deleted = false },

                new ContactInfo{Id = Guid.NewGuid(), FkPerson = Guid.Parse("79906069-9d58-48c6-8a38-baaade484a85"), Mail = "merve3@merve3.com", Phone = "2458 125 87 99", Location = "İstanbul", Deleted = false },

                new ContactInfo{Id = Guid.NewGuid(), FkPerson = Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"), Mail = "merve4@merve4.com", Phone = "2458 125 87 99", Location = "Ankara", Deleted = false },
            };
            oneContactInfo = new ContactInfo { Id = Guid.NewGuid(), FkPerson = Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"), Mail = "merve@merve.com", Phone = "1234 125 12 14", Location = "Ankara", Deleted = true };

            contactInfoBusiness.Setup(m => m.ContactInfoList(Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"))).Returns(contactInfos);

            contactInfoBusiness.Setup(m => m.GetContactInfoForEdit(Guid.Parse("c1187b54-c519-4e91-a862-9c4d972188b6"))).Returns(oneContactInfo);

            contactInfoBusiness.Setup(m => m.CreateContactInfo(createdTestModel)).Returns(response);

            contactInfoBusiness.Setup(m => m.DeleteContactInfo(Guid.Parse("c1187b54-c519-4e91-a862-9c4d972188b6"))).Returns(response);

        }

        [Fact]
        public void Get_Contact_Info_For_Edit_Null_Test()
        {
            Guid id = new Guid();
            var result = contactInfoController.GetContactInfoForEdit(id);
            Assert.Null(result);
        }

        [Fact]
        public void Get_Contact_Info_For_Edit_Not_Null_Test()
        {
            var result = contactInfoController.GetContactInfoForEdit(Guid.Parse("c1187b54-c519-4e91-a862-9c4d972188b6"));
            Assert.NotNull(result);
        }

        [Fact]
        public void Contact_Info_List_Test()
        {
            List<ContactInfo> list = contactInfoController.ContactInfoList(Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"));
            Assert.Equal(4, list.Count());
        }

        //[Fact]
        //public void Contact_Info_List_Error_Test()
        //{
        //    List<ContactInfo> list = contactInfoController.ContactInfoList(Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"));
        //    Assert.Equal(3, list.Count());
        //}

        [Fact]
        public void Deleted_Contact_Info_True_Test()
        {
            var response = contactInfoController.DeleteContactInfo(Guid.Parse("c1187b54-c519-4e91-a862-9c4d972188b6"));
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public void Deleted_Contact_Info_False_Test()
        {
            var response = contactInfoController.DeleteContactInfo(Guid.Parse("392f073b-0446-44ea-a25e-e4386c54d0ad"));
            Assert.False(response.IsSuccess);
        }


        [Fact]
        public void Create_Contact_Info_Test()
        {
            createdTestModel.Id = Guid.NewGuid();
            createdTestModel.FkPerson = Guid.Parse("79906069-9d58-48c6-8a38-baaade484a85");
            createdTestModel.Location = "İzmir";
            createdTestModel.Mail = "izmir@izmir.com";
            createdTestModel.Phone = "9874 654 32 14";
            createdTestModel.Deleted = false;
            var response = contactInfoController.CreateContactInfo(createdTestModel);
            Assert.True(response.IsSuccess);
        }


    }
}
