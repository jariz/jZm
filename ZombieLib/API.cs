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
    public class ZombieAPI
    {
        public event OnFrameHandler OnFrame;
        public event WriteHandler OnWrite;
        public event WriteHandler OnDebugWrite;
        public event OnCrashHandler OnCrash;
        public event OnPluginCrashHandler OnPluginCrash;
        public event MapHandler OnMapLoad;
        public event MapHandler OnMapDestroy;

        public static string Version = "1.2.3.0-DEV";
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

        public GEntity[] Entities
        {
            get
            {
                return _entities.ToArray();
            }
        }

        public Dictionary<int, string> Weapons
        {
            get
            {
                return w_eapons;
            }
        }

        Dictionary<int, string> w_eapons = new Dictionary<int, string>();
        PluginLoader pluginLoader;
        public jZmPlugin[] Plugins;

        public void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }

        public void WriteLine(string message, bool debug)
        {
            if(OnDebugWrite != null)
                OnDebugWrite(message + Environment.NewLine);
        }

        public void Write(string message)
        {
            if (OnWrite != null)
                OnWrite(message);
            // No console hooked, use system console as alternative
            else Console.Write(message);
        }

        public ZombieAPI()
        {
            
        }

        public GEntity GetBus()
        {
            foreach(GEntity x in _entities)
            {
                if (x.Type == EntityType.ET_VEHICLE)
                    return x;
            }
            return null;
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

        public GEntity[] GetActors()
        {
            return FilterByType(EntityType.ET_ACTOR);
        }

        public GEntity[] GetBarriers()
        {
            return FilterByType(EntityType.ET_ZBARRIER);
        }

        public GEntity[] GetScriptMovers()
        {
            return FilterByType(EntityType.ET_SCRIPTMOVER);
        }

        public void Crash(Exception z)
        {
            if (OnCrash != null)
                OnCrash(z);
        }

        string[] GameData = new string[] { };

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
            StringBuilder txt = new StringBuilder();
            while (x != 1024)
            {
                DVar dvar = new DVar(Game, Memory.ReadInt32(), this);
                _dvars.Add(dvar);
                string line = string.Format("#{0}\r\n\t\"{1}\"", dvar.Name, dvar.Value.Value);
                txt.Append(line+"\r\n");
                //Console.WriteLine(line);
                x++;
            }
            File.WriteAllText("dvardump.txt", txt.ToString());

            WriteLine("Loading plugins....", true);
            pluginLoader = new PluginLoader();
            Plugins = pluginLoader.Load();

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

            //new ModWarning().Init(this);
            new SVCMDTester().Init(this);

            DateTime initTime = new DateTime(DateTime.Now.Ticks - start);

            WriteLine("Initialized in " + initTime.Second + "." + initTime.Millisecond + " second(s)");
            ThreadPool.QueueUserWorkItem(new WaitCallback(zFrame), Game);
        }

        public List<Player> GetPlayers()
        {
            List<Player> p = new List<Player>();
            foreach (GEntity ent in _entities)
            {
                if (ent.Type == EntityType.ET_PLAYER)
                    if(ent.Player != null)
                     p.Add(ent.Player);
            }
            return p;
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

        void zFrame(object x)
        {
            LoopMem = new RemoteMemory((Process)x);
            int GameDataInterval = 0;
            while (true)
            {
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

                if (OnFrame != null)
                {
                    PluginEvent(OnFrame, new object[] { });
                }

                Thread.Sleep(200);
            }
        }
    }
}
