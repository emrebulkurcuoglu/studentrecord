using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studenrecordsystem
{
    class Database
    {
        public static void connect()
        {
            string connetionString;
            SqlConnection cnn;
            SqlCommand cmd;

            connetionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            try
            {
                
                using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"C:\Users\emre.bulkurcuoglu\Desktop\studenrecordsystem\studenrecordsystem\record.xlsx")))
                {
                    var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
                    var myWorksheet2 = xlPackage.Workbook.Worksheets[2]; //select sheet here
                    var totalRows = myWorksheet.Dimension.End.Row;
                    var totalColumns = myWorksheet.Dimension.End.Column;
                    var totalRows2 = myWorksheet2.Dimension.End.Row;
                    var totalColumns2 = myWorksheet2.Dimension.End.Column;

                    int rowno2 = 1;

                    for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
                    {
                        var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                        string query1 = "INSERT INTO [dbo].[Students] \n([SName],\n[Surname],\n[Birthday],\n[StudentId],\n[GSM])\n VALUES";
                        query1 = query1 + "(" + "\n'"+row.ElementAt<string>(0)+ "'" + "\n,'" + row.ElementAt<string>(1) + "'" + "\n,'" + Utils.GetDateTimeOnConsoleWithValidationAndFormat(row.ElementAt<string>(2).Substring(0, 10), "", "").ToString().Substring(0, 10) + "'" + "\n,'" + row.ElementAt<string>(3) + "'" + "\n,'" + row.ElementAt<string>(4) + "')";
                        int no = Convert.ToInt32(row.ElementAt(5));
                        for (int i = 0; i <no ; i++)
                        {
                            var row2 = myWorksheet2.Cells[rowno2, 1, rowno2, totalColumns2].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                            Adress adresstoadd2 = new Adress();

                            adresstoadd2.Street = row2.ElementAt<string>(0);
                            adresstoadd2.Neighborhood = row2.ElementAt<string>(1);
                            adresstoadd2.District = row2.ElementAt<string>(2);
                            adresstoadd2.State = row2.ElementAt<string>(3);

                            rowno2++;
                        }

                        cmd = new SqlCommand(query1, cnn);
                        cmd.ExecuteNonQuery();
                    }
                }

                

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            cnn.Close();


        }
    }
}
