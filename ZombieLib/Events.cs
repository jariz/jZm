using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    public delegate void OnFrameHandler();
    public delegate void WriteHandler(string msg);
    public delegate void OnCrashHandler(Exception exep);
    public delegate void OnPluginCrashHandler(Exception exep, jZmPlugin plugin);
    public delegate void MapHandler();
}
