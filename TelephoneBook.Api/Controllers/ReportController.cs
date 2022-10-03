using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TelephoneBook.Business.ReportBusiness;
using TelephoneBook.DataAccess.Dtos.ReportDtos;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;

namespace TelephoneBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        IReportBusiness reportBusiness;
        public ReportController(IReportBusiness reportBusiness)
        {
            this.reportBusiness = reportBusiness;
        }

        [HttpGet]
        [Route("ReportList")]
        public List<Report> ReportList()
        {
            return reportBusiness.ReportList();
        }

        [HttpPost]
        [Route("CreateReport")]
        public PrimitiveResponse CreateReport(Report model)
        {
            return reportBusiness.CreateReport(model);
        }

        [HttpGet]
        [Route("GetReport")]
        public LocationReportDto GetReport()
        {
            return reportBusiness.LocationReport();
        }

    }
}
