using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace ClassWorkSqlConnection;

internal class Program
{
    [DllImport("kernel32.dll", ExactSpelling = true)]

    private static extern IntPtr GetConsoleWindow();
    private static IntPtr ThisConsole = GetConsoleWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    private const int HIDE = 0;
    private const int MAXIMIZE = 3;
    private const int MINIMIZE = 6;
    private const int RESTORE = 9;

    static void Main(string[] args)
    {

        ShowWindow(ThisConsole, MAXIMIZE);

        using (SqlConnection connection = new SqlConnection("Server=DESKTOP-MLES57C;Database=DynamicDataMasking;Integrated Security=true"))
        {
            string query = "execute as user = 'Feru' select top 20 * from Kullanicilar";

            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = cmd;

            DataTable dataTable = new DataTable("Kullanicilar");

            dataAdapter.Fill(dataTable);

            int rowLength = 20;
            Console.WriteLine("\n" + dataTable.TableName + " : ");
            Console.WriteLine("\n" + new String('-', 229));
            foreach (DataColumn column in dataTable.Columns)
            {
                int spaceSize = rowLength - column.ColumnName.Length;
                Console.Write(column.ColumnName + new String(' ', spaceSize) + " | ", ConsoleColor.Red);
            }
            Console.WriteLine("\n" + new String('-', 229));
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    int spaceSize = rowLength - item.ToString().Length;
                    Console.Write(item + new String(' ', spaceSize) + " | ");
                }
                Console.WriteLine("\n" + new String('-', 229));

            }
            connection.Close();
        }

    }
}