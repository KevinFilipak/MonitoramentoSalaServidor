using AspNetCore.Reporting;
using System.Data;

namespace SistemaMonitoramento.Web.Models
{
    public static class ReportModel
    {
        public static ReportResult GerarRelatorio(RenderType type, string reportName, DataSet ds, IWebHostEnvironment _webHostEnvironment)
        {
            try
            {
                string mimType = "";
                int extension = 1;
                var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\" + reportName + ".rdlc";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                LocalReport localReport = new LocalReport(path);                
                localReport.AddDataSource("DataSet1", ds.Tables[0]);
                return localReport.Execute(type, extension, parameters, mimType);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}