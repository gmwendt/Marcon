﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MRCStudio
{
  public class ExpanderItem
  {

    public string Header { get; set; }
    public string ItemId { get; set; }
    public FrameworkElement Content { get; set; }
  }
}
