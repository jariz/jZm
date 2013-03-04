using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ZombieAPI.GameObjects
{
    /// <summary>
    /// A entity used in the game.
    /// </summary>
    /// <remarks>
    /// Entities are pretty much all things that aren't part of the map.
    /// </remarks>
    public class GEntity : RemoteObject
    {
        /// <summary>
        /// Initialize a new GEntity based on the offset.
        /// </summary>
        /// <remarks>
        /// DO NOT CALL FROM PLUGIN. Use <see cref="ZombieAPI.Entities"/> to get all entities.
        /// </remarks>
        /// <param name="Game">The game process</param>
        /// <param name="EntityAddr">The offset of the entity</param>
        /// <param name="Parent">ZombieAPI that initializes this class</param>
        public GEntity(Process Game, int EntityAddr, ZombieAPI Parent)
        {
            this.Mem = new RemoteMemory(Game);
            Position = EntityAddr;
            BaseOffset = EntityAddr;
            _parent = Parent;

            a_ClientNum = aInt(); //0x0000
            a_Stance = aInt(); //0x0004
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
            a_playerAddr = aInt(); //0x154
            Move(4); //0x0158
            a_teamAddr = aInt(); //0x015C
            Move(12); //0x0160
            a_ModelIndex = aInt(); //0x016C
            Move(56); //0x0170
            a_Health = aInt(); //0x01A8
            Mem.Move(29320); //0x01AC
        }

        int a_ClientNum;
        int a_Stance;
        int a_Origin;
        int a_Angles;
        int a_CurrentWeapon;
        int a_entityType;
        int a_newOrigin;
        int a_playerAddr;
        int a_teamAddr;
        public int a_ModelIndex;
        public int a_Health;

        Player _player = null;
        TeamInfo _team = null;
        ZombieAPI _parent = null;

        /// <summary>
        /// The ZombieAPI that created this class.
        /// </summary>
        public ZombieAPI Parent
        {
            get
            {
                return _parent;
            }
        }

        /// <summary>
        /// If the entity is a player, this will return the <see cref="Player"/> object.
        /// </summary>
        public Player Player
        {
            get
            {
                Mem.Position = BaseOffset + 0x154;
                a_playerAddr = Mem.ReadInt32();
                _player = new Player(Mem.Process, a_playerAddr, this);
                return _player;
            }
        }

        /// <summary>
        /// TeamInfo object containing the Entity's team.
        /// </summary>
        /// <remarks>
        /// Pretty much only the Players actually use this.
        /// </remarks>
        public TeamInfo Team
        {
            get
            {
                Mem.Position = BaseOffset + 0x15c;
                a_teamAddr = Mem.ReadInt32();
                _team = new TeamInfo(Mem.Process, a_teamAddr, this);
                return _team;
            }
        }

        /// <summary>
        /// The entity's index, ranging from 0 to 1024
        /// </summary>
        public int ClientNum
        {
            get
            {
                Mem.Position = a_ClientNum;
                return Mem.ReadInt32();
            }
        }

        /// <summary>
        /// In what stance is this entity?
        /// </summary>
        /// <remarks>
        /// May return other numbers than defined in the Stances enum on entities that aren't players (for example zombies etc)
        /// </remarks>
        public Stances Stance
        {
            get
            {
                Mem.Position = a_Stance;
                return (Stances)Mem.ReadInt32();
            }
            set
            {
                Mem.Position = a_Stance;
                Mem.Write((int)value);
            }
        }

        /// <summary>
        /// The location on the map of this Entity
        /// </summary>
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

        /// <summary>
        /// The angles of this entity.
        /// </summary>
        /// <remarks>
        /// Returns a Vec2 with 2 values both ranging from 0 to 360. Can both also be negative.
        /// </remarks>
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

        /// <summary>
        /// Current weapon being used.
        /// </summary>
        /// <remarks>
        /// Only used by players.
        /// </remarks>
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

        /// <summary>
        /// Not sure what this is, If you've got any idea, contact me. D:
        /// </summary>
        public float[] newOrigin
        {
            get
            {
                Mem.Position = a_newOrigin;
                return Mem.ReadVec3();
            }
            set
            {
                Mem.Position = a_newOrigin;
                Mem.Write(value);
            }
        }

        /// <summary>
        /// The model index.
        /// </summary>
        /// <remarks>
        /// Perhaps in a later build it'll be possible to get the model name. Currently I have no clue what it refers to.
        /// </remarks>
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

        /// <summary>
        /// The entity's health.
        /// </summary>
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

        /// <summary>
        /// The entity type, ranges from player to barrier.
        /// </summary>
        public EntityType Type
        {
            get
            {
                Mem.Position = a_entityType;
                return (EntityType)Mem.ReadInt16();
            }
            //readable because changing a entity's type crashes the game
        }
    }
}
