using System;
using System.Data;
using System.Data.OleDb;
using System.IO;


namespace csv_search
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "..\\..\\db.csv";
            DataTable table = ReadCsv(fileName);

            Console.WriteLine("Insert needle author Name");
            string needleAuthor = Console.ReadLine();

            foreach (DataRow row in table.Rows)
            {
                if ((string)row["author"] == needleAuthor)
                {
                    Console.Write(row["id"] + ", ");
                }
            }

            Console.Write("\n");
            Console.ReadKey();
        }

        static public DataTable ReadCsv(string fileName)
        {
            DataTable dt = new DataTable("Data");
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" +
                Path.GetDirectoryName(fileName)+ "\";Extended Properties='text;HDR=yes;FMT=Delimited(,)';"))
                {
                    using (OleDbCommand cmd = new OleDbCommand(string.Format("select *from [{0}]", new FileInfo(fileName).Name), cn))
                    {
                        cn.Open();
                        using(OleDbDataAdapter adapter=new OleDbDataAdapter(cmd))
                        {
                        adapter.Fill(dt);
                        }
                    }
                }
                return dt;
        }
    }
}
