using IronPdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using TelephoneBook.DataAccess.Dtos.ReportDtos;
using TelephoneBook.DataAccess.Entity;
using TelephoneBook.Helper.Response;
using TelephoneBook.Helper.RestHelper;
using TelephoneBook.Web.Common;
using System.IO;

namespace TelephoneDirectory.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IOptions<ConfigModel> _config;
        public ReportController(IOptions<ConfigModel> _config)
        {
            this._config = _config;
        }
        public IActionResult Index()
        {
            var list = RestHelper.Get<List<Report>>(_config.Value.ApiBaseUrl + "Report/ReportList");
            foreach (var item in list)
            {
                if (System.IO.File.Exists(item.ReportName))
                {
                    item.Status = 2;
                }
            }
            return View(list);
        }

        [HttpPost]
        public PrimitiveResponse CreateReport(Report model)
        {
            model.ReportName = model.RequestDate.ToString("yyyyMMddhhmmssff") + ".pdf";
            var returnPost = RestHelper.Post<PrimitiveResponse, Report>(_config.Value.ApiBaseUrl + "Report/CreateReport", model);

            var locationReport = RestHelper.Get<LocationReportDto>(_config.Value.ApiBaseUrl + "Report/GetReport");
            string tableHtml = "";
            if (locationReport.PersonLocationInfoLists.Count > 0)
            {
                tableHtml = "<table>";
                foreach (var item in locationReport.PersonLocationInfoLists)
                {
                    tableHtml += "<tr><td>Lokasyon: " + item.Location + "-- Konuma Kayıtlı Kişi Sayısı: " + item.LocationCount + "-- Kayıtlı Telefon Sayısı: " + item.LocationCount + "</td></tr>";
                }
                tableHtml += "</table>";
            }
            else
            {
                tableHtml = "Herhangi bir kayıt bulunamadı.";
            }

            var pdfRender = new HtmlToPdf();
            pdfRender.RenderHtmlAsPdf(tableHtml).SaveAs(model.ReportName);
            return returnPost;
        }

    }
}
