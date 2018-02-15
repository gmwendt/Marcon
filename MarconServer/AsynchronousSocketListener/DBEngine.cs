using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousSocketListener
{
  public class DBEngine
  {
    private OdbcConnection l_odbcConnection;
    private const string CONN_STR = "Driver={SQL Server};Server=GUSTAVO-PC;UID=sa;PWD=Abcd1234;Database=MarconDB;";

    public DBEngine()
    {
    }

    public void ExecuteCommand(string command)
    {
      using (OdbcConnection connection =
               new OdbcConnection(CONN_STR))
      {
        // The insertSQL string contains a SQL statement that
        // inserts a new row in the source table.
        OdbcCommand odbcCmd = new OdbcCommand(command, connection);

        // Open the connection and execute the insert command.
        try
        {
          connection.Open();
          odbcCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
        // The connection is automatically closed when the
        // code exits the using block.
      }
    }

    public void Close() 
    {
      if (l_odbcConnection != null)
        l_odbcConnection.Close();
    }
  }
}
