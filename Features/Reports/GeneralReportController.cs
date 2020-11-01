using System;
using Microsoft.AspNetCore.Mvc;

namespace bagel_sales_control.Features.Reports
{
    [Route("api/v1/reports")]
    public class GeneralReportController : ControllerBase
    {
        private readonly ReportService _reportService;
        public GeneralReportController(ReportService reportService)
        {
            if (reportService == null) throw new ArgumentNullException(nameof(reportService));

            _reportService = reportService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GerGeneralReport()
        {
            var response = _reportService.GetGeneralReport();

            return Ok(response);
        }
    }
}