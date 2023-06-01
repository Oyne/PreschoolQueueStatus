using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace PreschoolStatusDataSet
{
    public class PreschoolStatus
    {

        public int Identifier { get; set; }
        public string DateReceived { get; set; }
        public int Number { get; set; }
        public int Gender { get; set; }
        public string AgeGroup { get; set; }
        public string BenefitType { get; set; }
        public string InstitutionName { get; set; }
        public int InstitutionIdentifier { get; set; }
        public string DateProvided { get; set; }
        public string Status { get; set; }
    }



    public class DataSet
    {
        Exception ConvertException = new Exception("Can`t convert");
        private List<PreschoolStatus> statuses = new List<PreschoolStatus>();

        public List<PreschoolStatus> GetStatuses { get => statuses; }


        public void ImportDataSet(string fileName)
        {
            // Open the workbook
            using (var workbook = new XLWorkbook(fileName))
            {
                // Get the first worksheet in the workbook
                var worksheet = workbook.Worksheet(1);
                 
                // Iterate over all rows in the worksheet except header ones
                foreach (var xlRow in worksheet.RowsUsed().Skip(2))
                {
                    PreschoolStatus status = new PreschoolStatus();

                    status.Identifier = int.Parse(xlRow.Cell(1).Value.ToString());
                    status.DateReceived = xlRow.Cell(2).Value.ToString();
                    status.Number = int.Parse(xlRow.Cell(3).Value.ToString());
                    status.Gender = int.Parse(xlRow.Cell(4).Value.IsBlank ? "0" : xlRow.Cell(4).Value.ToString());
                    status.AgeGroup = xlRow.Cell(5).Value.ToString();
                    status.BenefitType = xlRow.Cell(6).Value.ToString();
                    status.InstitutionName = xlRow.Cell(7).Value.ToString();
                    status.InstitutionIdentifier = int.Parse(xlRow.Cell(8).Value.ToString());
                    status.DateProvided = xlRow.Cell(9).Value.ToString();
                    status.Status = xlRow.Cell(10).Value.ToString();

                    statuses.Add(status);
                }
            }
        }

        public void ExportDataSet(string fileName)
        {
            // Open the workbook
            using (var workbook = new XLWorkbook())
            {
                // Get the first worksheet in the workbook
                var worksheet = workbook.Worksheets.Add("Sheet1");

                worksheet.Cell(1, 1).Value = "identifier";
                worksheet.Cell(2, 1).Value = "Ідентифікатор";

                worksheet.Cell(1, 2).Value = "dateReceived";
                worksheet.Cell(2, 2).Value = "Дата подання";

                worksheet.Cell(1, 3).Value = "number";
                worksheet.Cell(2, 3).Value = "Номер в черзі";

                worksheet.Cell(1, 4).Value = "gender";
                worksheet.Cell(2, 4).Value = "Стать";

                worksheet.Cell(1, 5).Value = "ageGroup";
                worksheet.Cell(2, 5).Value = "Вікова група";

                worksheet.Cell(1, 6).Value = "benefitType";
                worksheet.Cell(2, 6).Value = "Наявність пільг";

                worksheet.Cell(1, 7).Value = "institutionName";
                worksheet.Cell(2, 7).Value = "Назва закладу";

                worksheet.Cell(1, 8).Value = "institutionIdentifier";
                worksheet.Cell(2, 8).Value = "Ідентифікатор закладу";

                worksheet.Cell(1, 9).Value = "dateProvided";
                worksheet.Cell(2, 9).Value = "Дата виконання";

                worksheet.Cell(1, 10).Value = "status";
                worksheet.Cell(2, 10).Value = "Статус заявки";

                workbook.SaveAs(fileName);

                //// Iterate over all rows in the worksheet except header ones
                //foreach (var status in statuses)
                //{
                //    PreschoolStatus status = new PreschoolStatus();

                //    status.Identifier = int.Parse(xlRow.Cell(1).Value.ToString());
                //    status.DateReceived = xlRow.Cell(2).Value.ToString();
                //    status.Number = int.Parse(xlRow.Cell(3).Value.ToString());
                //    status.Gender = int.Parse(xlRow.Cell(4).Value.IsBlank ? "0" : xlRow.Cell(4).Value.ToString());
                //    status.AgeGroup = xlRow.Cell(5).Value.ToString();
                //    status.BenefitType = xlRow.Cell(6).Value.ToString();
                //    status.InstitutionName = xlRow.Cell(7).Value.ToString();
                //    status.InstitutionIdentifier = int.Parse(xlRow.Cell(8).Value.ToString());
                //    status.DateProvided = xlRow.Cell(9).Value.ToString();
                //    status.Status = xlRow.Cell(10).Value.ToString();

                //    statuses.Add(status);
                //}
            }
        }
    }
}