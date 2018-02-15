using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarconServer
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private OdbcConnection _odbcConnection;

    public MainWindow()
    {
      InitializeComponent();

    }

    private void testConButton_Click(object sender, RoutedEventArgs e)
    {
      _odbcConnection = new OdbcConnection("Driver={SQL Server};Server=GUSTAVO-PC;UID=sa;PWD=Abcd1234;Database=MarconDB;");
      _odbcConnection.Open();
    }

    private void Window_Closed(object sender, EventArgs e)
    {
      if (_odbcConnection.State == System.Data.ConnectionState.Open)
        _odbcConnection.Close();
    }
  }
}
