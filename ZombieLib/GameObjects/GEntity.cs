using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieLib.GameObjects
{
    public enum EntityType { Unknown = 0, Player = 1, Spectator = 5, Zombie = 16 };
    public class GEntity : RemoteObject
    {

        public GEntity(RemoteMemory Mem, int EntityAddr)
        {
            this.Mem = Mem;
            Position = EntityAddr;
            BaseOffset = EntityAddr;

            a_ClientNum = aInt(); //0x0000
            a_eFlag = aInt(); //0x0004
            Move(16); //0x0008
            a_Origin = aVec(3); //0x0018
            Move(24); //0x0024
            a_Angles = aVec(2); //0x003C
            Move(28); //0x0044
            a_CurrentWeapon = aInt(); //0x0060
            Move(116); //0x0064
            a_entityType = aShort(); //0x00D8
            Move(66); //0x00DA
            a_newOrigin = aVec(3); //0x0011C
            Move(44); //0x0128
            Mem.Position = Position; a_playerAddr = Mem.ReadInt32(); Move(4); //0x154
            Move(4); //0x0158
            Mem.Position = Position;  a_teamAddr = Mem.ReadInt32(); Move(4); //0x015C
            Move(12); //0x0160
            a_ModelIndex = aInt(); //0x016C
            Move(56); //0x0170
            a_Health = aInt(); //0x01A8
            Mem.Move(29320); //0x01AC
        }


        int a_ClientNum;
        int a_eFlag;
        int a_Origin;
        int a_Angles;
        int a_CurrentWeapon;
        int a_entityType;
        int a_newOrigin;
        int a_playerAddr;
        int a_teamAddr;
        public int a_ModelIndex;
        public int a_Health;

        public int ClientNum
        {
            get
            {
                Mem.Position = a_ClientNum;
                return Mem.ReadInt32();
            }
        }

        int eFlag
        {
            get
            {
                Mem.Position = a_eFlag;
                return Mem.ReadInt32();
            }
        }

        public float[] Origin
        {
            get
            {
                Mem.Position = a_Origin;
                return Mem.ReadVec3();
            }
            set
            {
                Mem.Position = a_Origin;
                Mem.Write(value);
            }
        }

        public float[] Angles
        {
            get
            {
                Mem.Position = a_Angles;
                return Mem.ReadVec2();
            }
            set
            {
                Mem.Position = a_Angles;
                Mem.Write(value);
            }
        }

        public int CurrentWeapon
        {
            get
            {
                Mem.Position = a_CurrentWeapon;
                return Mem.ReadInt32();
            }
            set
            {
                Mem.Position = a_CurrentWeapon;
                Mem.Write(value);
            }
        }

        float[] newOrigin
        {
            get
            {
                Mem.Position = a_newOrigin;
                return Mem.ReadVec2();
            }
            set
            {
                Mem.Position = a_newOrigin;
                Mem.Write(value);
            }
        }

        public int ModelIndex
        {
            get
            {
                Mem.Position = a_ModelIndex;
                return Mem.ReadInt32();
            }
            set
            {
                Mem.Position = a_ModelIndex;
                Mem.Write(value);
            }
        }

        public int Health
        {
            get
            {
                Mem.Position = a_Health;
                return Mem.ReadInt32();
            }
            set
            {
                Mem.Position = a_Health;
                Mem.Write(value);
            }
        }

        public EntityType Type
        {
            get
            {
                Mem.Position = a_entityType;
                return (EntityType)Mem.ReadInt16();
            }
        }
    }
}
