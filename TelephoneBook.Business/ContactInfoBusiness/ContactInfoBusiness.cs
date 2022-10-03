using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.DataAccess.Context;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;

namespace TelephoneBook.Business.ContactInfoBusiness
{
    public class ContactInfoBusiness : IContactInfoBusiness
    {
        private readonly PostreSqlContext database;
        public ContactInfoBusiness(PostreSqlContext database)
        {
            this.database = database;
        }
        public List<ContactInfo> ContactInfoList(Guid personId)
        {
            var list = database.ContactInfo.Where(x => x.Deleted != true && x.FkPerson == personId).ToList();
            return list;
        }

        public PrimitiveResponse CreateContactInfo(ContactInfo model)
        {
            try
            {
                database.ContactInfo.Add(model);
                database.SaveChanges();
                return new PrimitiveResponse { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Kayıt işlemi gerçekleştirildi" };
            }
            catch (Exception)
            {
                return new PrimitiveResponse { ResponseCode = ResponseCodes.DbError, ResponseMessage = "Kayıt işlemi sırasında hata oluştu" };
            }
        }

        public PrimitiveResponse DeleteContactInfo(Guid id)
        {
            try
            {
                var contactControl = database.ContactInfo.FirstOrDefault(x => x.Id == id);
                if (contactControl != null)
                {
                    contactControl.Deleted = true;
                    database.SaveChanges();
                    return new PrimitiveResponse { ResponseCode = ResponseCodes.Successful };
                }
                else
                {
                    return new PrimitiveResponse { ResponseCode = ResponseCodes.DbError, ResponseMessage = "Silme işlemi sırasında hata oluştu" };
                }
            }
            catch (Exception)
            {
                return new PrimitiveResponse { ResponseCode = ResponseCodes.DbError, ResponseMessage = "Silme işlemi sırasında hata oluştu" };
            }
        }

        public PrimitiveResponse EditContactInfo(ContactInfo model)
        {
            try
            {
                var control = database.ContactInfo.FirstOrDefault(x => x.Id == model.Id);
                if (control != null)
                {
                    control.Phone = model.Phone;
                    control.Mail = model.Mail;
                    control.Location = model.Location;
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

        public ContactInfo GetContactInfoForEdit(Guid id)
        {
            try
            {
                ContactInfo contactInfoModel = new ContactInfo();
                var contactInfoControl = database.ContactInfo.FirstOrDefault(x => x.Id == id);
                if (contactInfoControl != null)
                {
                    return contactInfoModel;
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
    }
}
