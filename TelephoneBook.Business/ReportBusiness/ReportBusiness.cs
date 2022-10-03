using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneBook.DataAccess.Context;
using TelephoneBook.DataAccess.Dtos.ReportDtos;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.RabbitMq;
using TelephoneBook.Helper.Response;

namespace TelephoneBook.Business.ReportBusiness
{
    public class ReportBusiness : IReportBusiness
    {
        private readonly PostreSqlContext database;
        public ReportBusiness(PostreSqlContext database)
        {
            this.database = database;
        }
        public PrimitiveResponse CreateReport(Report report)
        {
            //report.RequestDate = DateTime.Now;
            //report.Status = 1;
            RabbitMq.Consume(report, database);
            return new PrimitiveResponse { ResponseCode = ResponseCodes.Successful, ResponseMessage = "Rapor talebiniz kuyruğa alınmıştır" };
        }
        public List<Report> ReportList()
        {
            var list = database.Report.OrderBy(x => x.RequestDate).ToList();          
            return list;
        }

        public LocationReportDto LocationReport()
        {
            LocationReportDto dto = new LocationReportDto();

            var groupQuery = database.ContactInfo.GroupBy(x => x.Location).Select(z => new { Konum = z.Key, Total = z.Count() }).ToList();

            foreach (var item in groupQuery)
            {
                dto.PersonLocationInfoLists.Add(new PersonLocationInfoListDto { Location = item.Konum, LocationCount = item.Total });
            }

            return dto;
        }
    }
}
