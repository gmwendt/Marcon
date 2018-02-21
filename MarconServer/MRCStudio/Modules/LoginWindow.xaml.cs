using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace MRCStudio
{
  /// <summary>
  /// Interaction logic for LoginWindow.xaml
  /// </summary>
  public partial class LoginWindow : Window
  {
    MRCStudioViewModel _model;
    AsynchronousClient _client;
    public LoginWindow(MRCStudioViewModel context)
    {
      InitializeComponent();
      _model = context;
    }

    private void btnConnect_Click(object sender, RoutedEventArgs e)
    {
      byte[] loginResponse;

      _client = new AsynchronousClient(comboBoxServer.Text);
      string loginQuery = String.Format("SELECT * FROM dbo.Users WHERE USERNAME='{0}'", textBoxUserName.Text);

      try
      {
        _client.SendCommand(loginQuery, out loginResponse);
        DataTable table = ConvertByteArrayToDataTable(loginResponse);


        int id = (int)table.Rows[0]["USERID"];
        string name = table.Rows[0]["USERNAME"].ToString();
        string userGroup = table.Rows[0]["USERGROUP"].ToString();
        string salt = table.Rows[0]["SALT"].ToString();
        byte[] pwd = (byte[])table.Rows[0]["PWD"];

        byte[] hashedPwd = StringToSha512(textBoxPassword.Text + salt);

        if (hashedPwd.SequenceEqual(pwd))
        {
          _model.CurrentUser = new User(id, name, userGroup, "email@email.com");
          this.Close();
        }
        else
          MessageBox.Show("Invalid password", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private DataTable ConvertByteArrayToDataTable(byte[] byteArray)
    {
      DataTable table = new DataTable();
      string text = (Convert.ToBase64String(byteArray));
      BinaryFormatter bformatter = new BinaryFormatter();
      MemoryStream stream = new MemoryStream();
      stream = new MemoryStream(byteArray);
      table = (DataTable)bformatter.Deserialize(stream);

      stream.Close();
      return table;
    }

    private byte[] StringToSha512(string input)
    {
      byte[] pwdByte = Encoding.UTF8.GetBytes(input);
      SHA512 SHA1 = new SHA512Managed();

      return SHA1.ComputeHash(pwdByte);
    }
  }
}
