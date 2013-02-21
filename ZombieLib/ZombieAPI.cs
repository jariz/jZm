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

        RemoteMemory Memory;
        List<GEntity> _entities = new List<GEntity>();

        public GEntity[] Entities
        {
            get
            {
                return _entities.ToArray();
            }
        }

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

        public void Init(Process Game)
        {
            WriteLine("Connecting to game....");
            Memory = new RemoteMemory(Game);

            WriteLine("Reading entities....");
            int x = 0;
            while (x != 1024)
            {
                _entities.Add(new GEntity(Memory, Addresses.GEntity + (Addresses.GEntity_Size * x)));
                x++;
            }

            WriteLine("Loading plugins....");
            pluginLoader = new PluginLoader();
            Plugins = pluginLoader.Load();

            foreach (jZmPlugin plug in Plugins)
            {
                plug.Init(this);
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(zFrame));
        }

        public List<Player> GetPlayers()
        {
            List<Player> p = new List<Player>();
            foreach (GEntity ent in _entities)
            {
                if (ent.Type == EntityType.Player)
                    if(ent.Player == null)
                        
                    p.Add(ent.Player);
            }
            return p;
        }

        void zFrame(object x)
        {
            while (true)
            {
                if (OnFrame != null)
                    OnFrame();
                Thread.Sleep(200);
            }
        }

        


    }
}
