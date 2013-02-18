using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using JariZ.GameObjects;
using System.Threading;

namespace JariZ
{
    public class ZombieAPI
    {
        public event OnFrameHandler OnFrame;

        RemoteMemory Memory;
        List<GEntity> Entities = new List<GEntity>();
        List<GEntity> Zombies = new List<GEntity>();
        List<GEntity> Players = new List<GEntity>();
        

        public ZombieAPI(Process Game)
        {
            Memory = new RemoteMemory(Game);

            int x = 0;

            while (x != 256)
            {
                Entities.Add(new GEntity(Memory, Addresses.EntityBase + (0x01AC * x)));
                x++;
            }
        }

        public List<Player> GetPlayers()
        {
            List<Player> p = new List<Player>();
            foreach (GEntity ent in Entities)
            {
                if (ent.Type == EntityType.Player)
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
