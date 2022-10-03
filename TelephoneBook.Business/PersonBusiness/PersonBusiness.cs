using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.DataAccess.Context;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;

namespace TelephoneBook.Business.PersonBusiness
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly PostreSqlContext database;
        public PersonBusiness(PostreSqlContext database)
        {
            this.database = database;
        }

        public PrimitiveResponse CreatePerson(Person model)
        {
            try
            {
                database.Person.Add(model);
                database.SaveChanges();
                return new PrimitiveResponse { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Kayıt işlemi gerçekleştirildi" };
            }
            catch (Exception)
            {
                return new PrimitiveResponse { ResponseCode = ResponseCodes.DbError, ResponseMessage = "Kayıt işlemi sırasında hata oluştu" };
            }
        }

        public PrimitiveResponse DeletePerson(Guid id)
        {
            try
            {
                var personControl = database.Person.FirstOrDefault(x => x.Id == id);
                var contactControl = database.ContactInfo.Where(x => x.FkPerson == id).ToList();
                if (contactControl.Count > 0)
                {
                    return new PrimitiveResponse { ResponseCode = ResponseCodes.DbError, ResponseMessage = "Kişiye ait iletişim bilgisi olduğu için silemezsiniz" };
                }
                else
                {
                    if (personControl != null)
                    {
                        personControl.Deleted = true;
                        database.SaveChanges();
                        return new PrimitiveResponse { ResponseCode = ResponseCodes.Successful };
                    }
                    else
                    {
                        return new PrimitiveResponse { ResponseCode = ResponseCodes.DbError, ResponseMessage = "Silme işlemi sırasında hata oluştu" };
                    }
                }
            }
            catch (Exception)
            {
                return new PrimitiveResponse { ResponseCode = ResponseCodes.DbError, ResponseMessage = "Silme işlemi sırasında hata oluştu" };
            }
        }

        public Person GetPersonForEdit(Guid id)
        {
            try
            {
                Person personModel = new Person();
                var personControl = database.Person.FirstOrDefault(x => x.Id == id);
                if (personControl != null)
                {
                    return personModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public PrimitiveResponse EditPerson(Person model)
        {
            try
            {
                var personControl = database.Person.FirstOrDefault(x => x.Id == model.Id);
                if (personControl != null)
                {
                    personControl.Name = model.Name;
                    personControl.Surname = model.Surname;
                    personControl.Company = model.Company;
                    database.SaveChanges();
                    return new PrimitiveResponse { ResponseCode = ResponseCodes.Successful };
                }
                else
                {
                    return new PrimitiveResponse { ResponseCode = ResponseCodes.DbError, ResponseMessage = "Düzenleme işlemi sırasında hata oluştu" };
                }
            }
            catch (Exception)
            {
                return new PrimitiveResponse { ResponseCode = ResponseCodes.DbError, ResponseMessage = "Düzenleme işlemi sırasında hata oluştu" };
            }
        }

        public List<Person> PersonList()
        {
            var list = database.Person.Where(x => x.Deleted != true).ToList();
            return list;
        }
    }
}
