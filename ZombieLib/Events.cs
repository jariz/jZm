using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    /// <summary>
    /// Handles frame events
    /// </summary>
    public delegate void OnFrameHandler();
    
    /// <summary>
    /// Handles write events
    /// </summary>
    /// <param name="msg">The message to write</param>
    public delegate void WriteHandler(string msg);

    /// <summary>
    /// Handles exception events
    /// </summary>
    /// <param name="exep">The exception</param>
    public delegate void OnCrashHandler(Exception exep);
    
    /// <summary>
    /// Handles plugin exception events
    /// </summary>
    /// <param name="exep">The exception</param>
    /// <param name="plugin">The plugin that threw the exception</param>
    public delegate void OnPluginCrashHandler(Exception exep, jZmPlugin plugin);

    /// <summary>
    /// Handles map events
    /// </summary>
    public delegate void MapHandler();
}
