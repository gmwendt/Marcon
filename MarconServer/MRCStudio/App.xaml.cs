using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MRCStudio
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      MainWindow main = new MainWindow();
      MRCStudioViewModel context = new MRCStudioViewModel();
      main.DataContext = context;

      if (context.CurrentUser != null)
        main.Show();
      else
        main.Close();
    }
  }
}
