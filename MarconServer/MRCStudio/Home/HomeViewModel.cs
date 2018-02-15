using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRCStudio
{
  public class HomeViewModel: ObservableObject, IPageViewModel
  {
    public string Name
    {
      get
      {
        return "Home Page";
      }
    }
  }
}
