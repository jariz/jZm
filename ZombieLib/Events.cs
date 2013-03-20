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

    /// <summary>
    /// Handles portal events
    /// </summary>
    /// <param name="portal">The portal that triggered this event</param>
    /// <param name="player">The player that triggered this event</param>
    public delegate void PortalHandler(Portal portal, GameObjects.Player player);

    /// <summary>
    /// Handles chat events
    /// </summary>
    /// <param name="Player">The player that sent the chat message</param>
    /// <param name="Message">The message that the player sent</param>
    public delegate void ChatHandler(GameObjects.Player Player, string Message);

    /// <summary>
    /// Handles a game frame
    /// </summary>
    public delegate void GameFrameHandler();
}
