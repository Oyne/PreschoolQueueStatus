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
                 
                // Iterate over all rows in the worksheet
                foreach (var xlRow in worksheet.RowsUsed().Skip(2))
                {
                    PreschoolStatus status = new PreschoolStatus();
                    // Check if the row has any non-empty cells

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

        }
    }
}