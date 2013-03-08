using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using ZombieAPI.GameObjects;
using System.Threading;


namespace ZombieAPI
{
    /// <summary>
    /// The publicy available API to all plugin.
    /// </summary>
    public class ZombieAPI
    {
        /// <summary>
        /// Called on every jZm tick. Gets called 5 times a second.
        /// </summary>
        public event OnFrameHandler OnFrame;
        /// <summary>
        /// Internally used by jZm frontend to respond to ZombieAPI.Write call
        /// </summary>
        public event WriteHandler OnWrite;
        /// <summary>
        /// Same as OnWrite, will print in another color to console.
        /// </summary>
        public event WriteHandler OnDebugWrite;
        /// <summary>
        /// Called whenever jZm occures a fatal error from which it cannot recover. Not meant for plugins.
        /// </summary>
        public event OnCrashHandler OnCrash;
        /// <summary>
        /// Called whenever a plugin throws a unhandled exception. Not meant for plugins.
        /// </summary>
        public event OnPluginCrashHandler OnPluginCrash;
        /// <summary>
        /// Calls whenever the gamedata gets loaded.
        /// </summary>
        /// <remarks>
        /// Keep in mind that it actually takes a few more seconds before the players are actually able to play and still may be staring at their loading screens.
        /// </remarks>
        public event MapHandler OnMapLoad;
        /// <summary>
        /// Call whenever the gamedata gets removed. Basically whenever the user returns to the main menu.
        /// </summary>
        public event MapHandler OnMapDestroy;

        /// <summary>
        /// jZm version, format: x.x.x-BUILD
        /// </summary>
        public static string Version = "1.2.3.0-DEV";

        /// <summary>
        /// First thing shown on startup on console, provides name, description, version and credits.
        /// </summary>
        public static string Header
        {
            get
            {
                return "---------------- jZm " + Version + " ----------------\r\nAdvanced Game Manipulation Framework for BO II Zombies\r\nThanks to: Barata, Nukem, Crayon, Surtek\r\n-------------------------------------------------\r\n";
            }
        }
        RemoteMemory Memory;
        List<GEntity> _entities = new List<GEntity>();
        List<DVar> _dvars = new List<DVar>();
        public Process BaseProcess;

        /// <summary>
        /// All Entities in the game.
        /// </summary>
        /// <remarks>
        /// Use the several functions to filter these entities
        /// </remarks>
        /// <seealso cref="ZombieAPI.GetPlayers()"/>
        /// <seealso cref="ZombieAPI.GetActors()"/>
        /// <seealso cref="ZombieAPI.GetBus()"/>
        public GEntity[] Entities
        {
            get
            {
                return _entities.ToArray();
            }
        }

        /// <summary>
        /// All currently loaded weapons in the game.
        /// </summary>
        /// <remarks>
        /// The key is the WeaponID, The value is the weapon name
        /// </remarks>
        public Dictionary<int, string> Weapons
        {
            get
            {
                return w_eapons;
            }
        }

        Dictionary<int, string> w_eapons = new Dictionary<int, string>();
        PluginLoader pluginLoader;
        jZmPlugin[] plugin;

        /// <summary>
        /// All currently loaded plugins. Mostly used internally.
        /// </summary>
        public jZmPlugin[] Plugins
        {
            get
            {
                return plugin;
            }
        }

        /// <summary>
        /// Write a line to the jZm console.
        /// </summary>
        /// <param name="message">The message that will be written to the console</param>
        public void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }

        /// <summary>
        /// A overload for diagnostic messages. Works the same as WriteLine
        /// </summary>
        /// <param name="message">The message that will be written to the console</param>
        /// <param name="debug">Doesn't matter what you pass to this parameter, It'll be send to the debug event.</param>
        public void WriteLine(string message, bool debug)
        {
            if(OnDebugWrite != null)
                OnDebugWrite(message + Environment.NewLine);
        }

        /// <summary>
        /// Write a message to the jZm console.
        /// </summary>
        /// <param name="message">The message that will be written to the console</param>
        public void Write(string message)
        {
            if (OnWrite != null)
                OnWrite(message);
            // No console hooked, use system console as alternative
            else Console.Write(message);
        }

        /// <summary>
        /// If you're trying to do this from a plugin, you're doing it wrong. Use the Init(ZombieAPI) function to get the ZombieAPI object.
        /// </summary>
        public ZombieAPI()
        {
            
        }

        /// <summary>
        /// Gets the bus on transit.
        /// </summary>
        /// <remarks>
        /// Returns the first ET_VEHICLE it can find.
        /// </remarks>
        /// <returns>The first ET_VEHICLE it can find. KEEP IN MIND THAT: If none found, It'll return null (this means on all non-transit maps)</returns>
        public GEntity GetBus()
        {
            foreach(GEntity x in _entities)
            {
                if (x.Type == EntityType.ET_VEHICLE)
                    return x;
            }
            return null;
        }

        /// <summary>
        /// Returns the corresponding DVar object that has this name
        /// </summary>
        /// <param name="Name">The DVar name</param>
        /// <returns>The DVar object as requested, when not found, It'll be null</returns>
        /// <seealso cref="Player.SetClientDVar"/>
        public DVar GetDVar(string Name)
        {
            foreach (DVar dvar in _dvars)
            {
                if (dvar.Name.ToLower() == Name.ToLower())
                    return dvar;
            }
            return null;
        }

        /// <summary>
        /// All the DVars that the game currently has.
        /// </summary>
        /// <remarks>
        /// DVars are the game's "settings".
        /// </remarks>
        public DVar[] DVars
        {
            get
            {
                List<DVar> res = new List<DVar>();
                foreach (DVar dvar in _dvars)
                {
                    if (dvar.Name != string.Empty) res.Add(dvar);
                }
                return res.ToArray();
            }
        }

        /// <summary>
        /// Returns the corresponding DVar value that has this name
        /// </summary>
        /// <param name="Name">The DVar name</param>
        /// <returns>The DVar value as requested, when not found, It'll be null</returns>
        public object GetDVarValue(string Name)
        {
            DVar dvar = GetDVar(Name);
            return dvar == null ? null : dvar.Value.Value;
        }

        GEntity[] FilterByType(EntityType type)
        {
            List<GEntity> result = new List<GEntity>();
            foreach (GEntity x in Entities)
            {
                if (x.Type == type)
                    result.Add(x);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Loops trough all entities, returning only the actors (zombies and other NPC's)
        /// </summary>
        /// <returns>Entity's with as type ET_ACTOR</returns>
        public GEntity[] GetActors()
        {
            return FilterByType(EntityType.ET_ACTOR);
        }

        /// <summary>
        /// Returns all Zombie Barriers (the zombie entrances)
        /// </summary>
        /// <returns>Entity's with as type ET_ZBARRIER</returns>
        public GEntity[] GetBarriers()
        {
            return FilterByType(EntityType.ET_ZBARRIER);
        }

        /// <summary>
        /// Loops trough all entities, returning only the models (keep in mind that these are not static and can be moved)
        /// </summary>
        /// <returns>Entity's with as type ET_SCRIPTMOVER</returns>
        public GEntity[] GetScriptMovers()
        {
            return FilterByType(EntityType.ET_SCRIPTMOVER);
        }

        /// <summary>
        /// Invokes the OnCrash event, resulting in a fatal error.
        /// Mostly used internally.
        /// </summary>
        /// <param name="z">The exception you want to show details for to the user</param>
        public void Crash(Exception z)
        {
            if (OnCrash != null)
                OnCrash(z);
        }

        string[] GameData = new string[] { };

        /// <summary>
        /// Initializes jZm onto a certain game process. Make sure the game is the correct process, This function doesn't check if the process is correct.
        /// </summary>
        /// <remarks>
        /// DO NOT CALL FROM A PLUGIN. This function is called from the jZm frontend and you should not call it from a plugin.
        /// </remarks>
        /// <param name="Game">The game process jZm reads/writes to (must be a valid CODBOII zombies process)</param>
        public void Bootstrap(Process Game)
        {
            WriteLine("Initializing jZm..."); long start = DateTime.Now.Ticks;

            WriteLine("Connecting to game....", true);
            this.BaseProcess = Game;
            Memory = new RemoteMemory(Game);

            WriteLine("Recognizing patterns...", true);
            PatternRecognition.Run(Memory.ProcessHandle);

            WriteLine("Reading entities....", true);
            int x = 0;
            while (x != 1024)
            {
                _entities.Add(new GEntity(Memory.Process, Addresses.GEntity + (Addresses.GEntity_Size * x), this));
                x++;
            }

            WriteLine("Reading DVars...", true);
            x = 0;
            Memory.Position = Addresses.DvarPointers;
            while (x != 1024)
            {
                DVar dvar = new DVar(Game, Memory.ReadInt32(), this);
                _dvars.Add(dvar);
                x++;
            }

            initPlugins();

            new TestingPlugin().Init(this);

            DateTime initTime = new DateTime(DateTime.Now.Ticks - start);

            WriteLine("Initialized in " + initTime.Second + "." + initTime.Millisecond + " second(s)");
            ThreadPool.QueueUserWorkItem(new WaitCallback(zFrame), Game);
        }

        void initPlugins()
        {
            WriteLine("Loading plugins....", true);
            pluginLoader = new PluginLoader();
            plugin = pluginLoader.Load();

            foreach (jZmPlugin plug in Plugins)
            {
                try
                {
                    plug.Init(this);
                }
                catch (Exception z)
                {
                    if (OnPluginCrash != null)
                        OnPluginCrash(z, plug);
                }
            }
        }

        /// <summary>
        /// Get all clients in the server.
        /// </summary>
        /// <remarks>
        /// One of the most important functions in jZm
        /// </remarks>
        /// <returns>Clients on the server</returns>
        public Player[] GetPlayers()
        {
            List<Player> p = new List<Player>();
            foreach (GEntity ent in _entities)
            {
                if (ent.Type == EntityType.ET_PLAYER)
                    if(ent.Player != null)
                     p.Add(ent.Player);
            }
            return p.ToArray();
        }

        RemoteMemory LoopMem;

        void PluginEvent(Delegate Ev, params object[] Params)
        {
            //We don't call the events natively anymore because with this was we can catch exception and get the plugin that caused the exception (that is, if it was a plugin ofcourse)
            if(Ev != null)
                foreach (Delegate PluginEv in Ev.GetInvocationList())
                {
                    if (PluginEv != null)
                        try
                        {
                            Ev.DynamicInvoke(Params);
                        }
                        catch (Exception z)
                        {
                            try
                            {
                                OnPluginCrash(z, ((jZmPlugin)PluginEv.Target));
                            }
                            catch
                            {
                                //We've got no idea what exactly has happend because whatever crashed, wasn't a plugin
                                //So now we just print exception details to the console
                                WriteLine(z.ToString());
                            }
                        }
                }
        }

        List<Portal> _portals = new List<Portal>();
        public List<Portal> Portals
        {
            get
            {
                return _portals;
            }
        }

        void zFrame(object x)
        {
            LoopMem = new RemoteMemory((Process)x);
            int GameDataInterval = 0;
            while (true)
            {
                #region GameData gathering / Map events
                if (GameDataInterval++ == 10)
                {
                    GameDataInterval = 0;
                    string[] NewGameData = GameDataReader.Read(LoopMem);

                    //use the gamedata as indicator of a new map being loaded/destroyed because i'm a fucking noob and i don't know how to handle events (YET)
                    if (GameData.Length == 0 && NewGameData.Length > 0)
                    {
                        w_eapons = GameDataReader.GetWeapons(NewGameData);
                        PluginEvent(OnMapLoad);
                    }
                    else if (GameData.Length > 0 && NewGameData.Length == 0)
                    {
                        w_eapons = GameDataReader.GetWeapons(NewGameData);
                        PluginEvent(OnMapDestroy);
                    }
                    GameData = NewGameData;
                }
                #endregion
                #region Portal Manager
                if(Portals.Count > 0)
                    foreach (Player player in GetPlayers())
                    {
                        foreach (Portal portal in Portals)
                        {
                            float[] orig = player.World.Origin;
                            if (
                                orig[0] < portal.X + portal.Radius &&
                                orig[0] > portal.X - portal.Radius &&

                                orig[1] < portal.Y + portal.Radius &&
                                orig[1] > portal.Y - portal.Radius
                            )
                            {
                                WriteLine(string.Format("Portal @ {0}, {1} ({2}) triggered by {3}", portal.X, portal.Y, portal.Radius, player.Name), true);

                                if(portal.Teleport)
                                    player.World.Origin = portal.Destination;

                                portal.trigger(portal, player);
                            }
                        }
                    }
                #endregion


                if (OnFrame != null)
                {
                    PluginEvent(OnFrame, new object[] { });
                }

                Thread.Sleep(200);
            }
        }
    }
}
