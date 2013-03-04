using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    /// <summary>
    /// The public interface used for all jZm plugins and a starting point for everyone working with the API.
    /// </summary>
    /// <remarks>
    /// Note that your class should be named 'Plugin' and nothing else if you want it to get loaded by the PluginLoader.
    /// </remarks>
    public interface jZmPlugin
    {
        /// <summary>
        /// The plugin name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Your name.
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Short description on what your plugin does.
        /// </summary>
        string Desc { get; }

        /// <summary>
        /// Gets called right after loading the plugin. You're supposed to initialize your plugin here.
        /// </summary>
        /// <remarks>
        /// The only call used by jZm. If you want to get notified of any other jZm events, Create eventhandlers.
        /// </remarks>
        /// <param name="API">The jZm API you can use to manipulate the game.</param>
        void Init(ZombieAPI API);
    }
}
