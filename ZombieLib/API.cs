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
        public event OnCrashHandler OnCrash;
        public event OnPluginCrashHandler OnPluginCrash;

        RemoteMemory Memory;
        List<GEntity> _entities = new List<GEntity>();
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

        public void Init(Process Game)
        {
            WriteLine("Connecting to game....");
            this.BaseProcess = Game;
            Memory = new RemoteMemory(Game);

            WriteLine("Reading entities....");
            int x = 0;
            while (x != 1024)
            {
                _entities.Add(new GEntity(Memory.Process, Addresses.GEntity + (Addresses.GEntity_Size * x), this));
                x++;
            }

            WriteLine("Reading game data...");
            GameData = GameDataReader.Read(Memory);

            WriteLine("Filtering weapons from gamedata..."); int o;           
            //jariz's sik weapon id filter
            foreach (string wep_on in GameData.Where(wep => wep.Length > 3 && Int32.TryParse(wep.Substring(0, 2), out o) && (wep.Contains("_zm") || wep.Contains("_mp") || wep.Contains("zombie_"))).ToArray())
            {
                string[] wep = wep_on.Split(' ');
                w_eapons.Add(Convert.ToInt32(wep[0]), wep[1]);
            }
            w_eapons.Add(0, "");


            WriteLine("Loading plugins....");
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
                        OnPluginCrash(z, plug.Name);
                }
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(zFrame));
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

        void zFrame(object x)
        {
            while (true)
            {
                if (OnFrame != null)
                {
                    try
                    {
                        OnFrame();
                    }
                    catch (Exception z)
                    {
                        StackFrame sf = new StackFrame();
                        
                        if (OnPluginCrash != null)
                            OnPluginCrash(z, "Unknown");
                    }
                }
                Thread.Sleep(200);
            }
        }
    }
}
