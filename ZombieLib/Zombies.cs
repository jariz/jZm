using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using ZombieLib.GameObjects;
using System.Threading;

namespace ZombieLib
{
    public class Zombies
    {
        public event OnFrameHandler OnFrame;

        RemoteMemory Memory;
        List<GEntity> Entities = new List<GEntity>();
        List<GEntity> Zombies = new List<GEntity>();
        List<GEntity> Players = new List<GEntity>();
        

        public Zombies(Process Game)
        {
            Memory = new RemoteMemory(Game);

            int x = 0;

            while (x != 256)
            {
                Entities.Add(new GEntity(Memory, Addresses.EntityBase + (0x01AC * x)));
                x++;
            }
        }

        public void GetPlayers()
        {
            foreach (GEntity ent in Entities)
            {
                
            }
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
