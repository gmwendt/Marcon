using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousSocketListener
{
  public class DBEngine
  {
    private const string CONN_STR = "Driver={SQL Server};Server=MARCON;Database=MarconDB;";
    //private const string CONN_STR = "Driver={SQL Server};Server=localhost;UID=sa;PWD=Abcd1234;Database=MarconDB;";

    public DBEngine()
    {
      VerifyDatabase();
    }

    public void ExecuteCommand(string query)
    {
      using (OdbcConnection connection =
               new OdbcConnection(CONN_STR))
      {
        // The insertSQL string contains a SQL statement that
        // inserts a new row in the source table.
        OdbcCommand odbcCmd = new OdbcCommand(query, connection);

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

    public object ExecuteScalar(string command)
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
          return odbcCmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          return null;
        }
        // The connection is automatically closed when the
        // code exits the using block.
      }
    }

    public int AddUser(string username, byte[] password, string salt, string fullname, string userGroup)
    {
      using (OdbcConnection connection =
               new OdbcConnection(CONN_STR))
      {
        OdbcCommand odbcCmd = new OdbcCommand("INSERT INTO [dbo].[Users] VALUES (?, ?, ?, ?)", connection);
        odbcCmd.Parameters.AddWithValue("@USERNAME", username);
        odbcCmd.Parameters.AddWithValue("@PWD", password);
        odbcCmd.Parameters.AddWithValue("@SALT", salt);
        odbcCmd.Parameters.AddWithValue("@FULLNAME", fullname);
        odbcCmd.Parameters.AddWithValue("@USERGROUP", userGroup);

        // Open the connection and execute the insert command.
        try
        {
          connection.Open();
          return odbcCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
          return -1;
        }
      }
    }

    private void VerifyDatabase()
    {
      //ONLY FOR TEST
      //#############
      byte[] output;
      string salt = "T5GXUdlsaNcUR3N4hpmhUV3foWJ296YtAxswJarSnC8=";

      byte[] passByte = Encoding.UTF8.GetBytes("Abcd1234" + salt);
      SHA512 SHA1 = new SHA512Managed();

      output = SHA1.ComputeHash(passByte);
      //#############

      string hasTable = (string)ExecuteScalar("SELECT table_name FROM information_schema.tables WHERE table_name='Users'");
      if (hasTable == "Users")
      {
        string hasSa = (string)ExecuteScalar("SELECT USERNAME FROM dbo.Users WHERE USERNAME='sa'");
        if (hasSa != "sa")
          AddUser("sa", passByte, salt, "Administrator", "Administrator");
      }
      else
      {
        string query = @"CREATE TABLE Users(
                          USERID    INT IDENTITY(1,1) NOT NULL,
                          USERNAME  VARCHAR (50)      NOT NULL,
                          PWD       binary (64)       NOT NULL,
                          SALT      VARCHAR (50)      NOT NULL,
                          FULLNAME  VARCHAR (50)      NOT NULL,
                          PRIMARY KEY (USERID));";
        ExecuteCommand(query);
      }
    }
  }
}
