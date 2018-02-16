using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRCStudio
{
  public class User
  {
    public User(int id, string username, string group, string email)
    {
      ID = id;
      UserName = username;
      Group = group;
      Email = email;
    }

    public int ID { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Group { get; set; }
  }
}
