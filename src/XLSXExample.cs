using Elements;
using OfficeOpenXml;

namespace XLSXExample
{
  public static class XLSXExample
  {
    /// <summary>
    /// The XLSXExample function.
    /// </summary>
    /// <param name="model">The input model.</param>
    /// <param name="input">The arguments to the execution.</param>
    /// <returns>A XLSXExampleOutputs instance containing computed results and the model with any new elements.</returns>
    public static XLSXExampleOutputs Execute(Dictionary<string, Model> inputModels, XLSXExampleInputs input)
    {
      var fileName = DateTime.Now.ToString("yyyyMMdd_HHmm") + "_Excel Test.xlsx";
      var filePath = Path.Combine(@"/tmp/", fileName);

      FileInfo file = new FileInfo(filePath);

      using (ExcelPackage package = new ExcelPackage(file))
      {
        ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

        if (worksheet == null)
        {
          worksheet = package.Workbook.Worksheets.Add("Sheet1");
        }

        worksheet.Cells[1, 1].Value = "Test";
        package.Save();
      }

#if DEBUG
      input.DownloadExcel = new Hypar.Model.FileDestination("TEST");
      input.DownloadExcel.Extension = "xlsx";
#endif

      var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
      input.DownloadExcel.SetExportStream(stream);
      stream.Seek(0, SeekOrigin.Begin);

      var output = new XLSXExampleOutputs();
      return output;
    }
  }
}
