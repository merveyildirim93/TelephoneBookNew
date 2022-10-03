using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.DataAccess.Dtos.ReportDtos;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;

namespace TelephoneBook.Business.ReportBusiness
{
    public interface IReportBusiness
    {
        public PrimitiveResponse CreateReport(Report report);
        public List<Report> ReportList();
        public LocationReportDto LocationReport();
    }
}
