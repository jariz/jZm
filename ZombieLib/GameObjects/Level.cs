using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ZombieAPI.GameObjects
{
    /// <summary>
    /// Level provides information such as how much entities are loaded, how much 
    /// </summary>
    public class Level : RemoteObject
    {
        ZombieAPI Parent;
        public Level(Process Game, int LevelAddr, ZombieAPI Parent)
        {
            this.Mem = new RemoteMemory(Game);
            this.Offset = LevelAddr;
            this.Parent = Parent;
            this.Position = LevelAddr;

            a_Players = aInt(); //0x0000
            a_Entities = aInt(); //0x004
            Move(4); //0x008
            a_numEntities = aInt(); //0x000C
            a_firstFreeEnt = aInt(); //0x0010
            a_lastFreeEnt = aInt(); //0x00014
            Move(48); //0x0018
            a_maxClients = aInt();
            Move(1864);
            a_frameNum = aInt(); //0x0794
            a_Time = aInt(); //0x798
            a_previousTime = aInt(); //0x79C
            a_frameTime = aInt(); //0x07A0
            Move(2724); //0x07A4
            a_currentEntityThink = aInt(); //0x1248
            Move(2676); //0x124C
        }

        public int FrameNum
        {
            get
            {
                Mem.Position = a_frameNum;
                return Mem.ReadInt32();
            }
        }

        public int CurrentEntityThink
        {
            get
            {
                Mem.Position = a_currentEntityThink;
                return Mem.ReadInt32();
            }
        }

        public int NumEntities
        {
            get
            {
                Mem.Position = a_numEntities;
                return Mem.ReadInt32();
            }
        }

        public GEntity LastFreeEntity
        {
            get
            {
                Mem.Position = a_lastFreeEnt;
                return new GEntity(Parent.BaseProcess, Mem.ReadInt32(), Parent);
            }
        }

        public GEntity FistFreeEntity
        {
            get
            {
                Mem.Position = a_firstFreeEnt;
                return new GEntity(Parent.BaseProcess, Mem.ReadInt32(), Parent);
            }
        }

        public int Players
        {
            get
            {
                Mem.Position = a_Players;
                return Mem.ReadInt32();
            }
        }

        public int MaxClients
        {
            get
            {
                Mem.Position = a_maxClients;
                return Mem.ReadInt32();
            }
            set
            {
                Mem.Position = a_maxClients;
                Mem.Write(value);
            }
        }

        int a_Players;
        int a_Entities;
        int a_numEntities;
        int a_firstFreeEnt;
        int a_lastFreeEnt;
        int a_maxClients;
        int a_frameNum;
        int a_Time;
        int a_previousTime;
        int a_frameTime;
        int a_currentEntityThink;
    }
}
