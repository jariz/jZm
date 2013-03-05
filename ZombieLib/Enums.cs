using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    /// <summary>
    /// The different positions a user can stand in
    /// </summary>
    public enum Stances 
    { 
        /// <summary>
        /// User is normally standing
        /// </summary>
        Standing = 2,
        /// <summary>
        /// User has pressed C and is crouching
        /// </summary>
        Crouched = 6, 
        /// <summary>
        /// User has pressed CTRL and is proning
        /// </summary>
        Prone = 10,
        /// <summary>
        /// User is currently firing his weapon
        /// </summary>
        Firing = 0x80042,
        /// <summary>
        /// User is running
        /// </summary>
        Running = 0x20002,
        /// <summary>
        /// User is aiming down sights
        /// </summary>
        IronSights = 0x80002
    }

    /// <summary>
    /// COD color codes helper
    /// </summary>
    public enum ColorCodes
    {
        Red = 1,
        Green = 2,
        Yellow = 3,
        Blue = 4,
        Cyan = 5,
        Pink = 6,
        White = 7
    }

    /// <summary>
    /// The user perks
    /// </summary>
    /// <remarks>
    /// Not many are known yet, feel free to contact me if you found new flags.
    /// </remarks>
    public enum Perks
    {
        /// <summary>
        /// Lower the weapon spread
        /// </summary>
        ReducedSpread = 0x10, 
        /// <summary>
        /// Increases the knife time
        /// </summary>
        FasterKnife = 0x80000
    }

    public enum EntityType
    {
        /// <summary>
        /// Invisible, not 100% sure
        /// </summary>
        ET_GENERAL,
        /// <summary>
        /// A player
        /// </summary>
        ET_PLAYER,
        /// <summary>
        /// Self-explainatory
        /// </summary>
        ET_PLAYER_CORPSE,
        ET_ITEM,
        ET_MISSILE,
        ET_INVISIBLE,
        /// <summary>
        /// A dynamic object that can be moved. Mostly a model and mostly interactive (doors etc)
        /// </summary>
        ET_SCRIPTMOVER,
        ET_SOUND_BLEND,
        ET_FX,
        ET_LOOP_FX,
        ET_PRIMARY_LIGHT,
        ET_TURRET,
        ET_HELICOPTER,
        ET_PLANE,
        /// <summary>
        /// Used for the bus on tranzit, No other ET_VEHICLES are known to exist in ZM
        /// </summary>
        ET_VEHICLE,
        /// <summary>
        /// If the bus ever crashes, then this'll be it's entity type
        /// </summary>
        ET_VEHICLE_CORPSE,
        /// <summary>
        /// Zombies and other stuff like that, NPC's etc
        /// </summary>
        ET_ACTOR,
        ET_ACTOR_SPAWNER,
        /// <summary>
        /// Zombie corpse
        /// </summary>
        ET_ACTOR_CORPSE,
        ET_STREAMER_HINT,
        /// <summary>
        /// 
        /// </summary>
        ET_ZBARRIER
    };

    public enum Team
    {
        /// <summary>
        /// Free for all
        /// </summary>
        TEAM_FREE,
        TEAM_ALLIES,
        TEAM_AXIS,
        TEAM_THREE,
        TEAM_FOUR,
        TEAM_FIVE,
        TEAM_SIX,
        TEAM_SEVEN,
        TEAM_EIGHT,
        TEAM_SPECTATOR
    }

    public enum DVarType
    {
        Integer = 6,
		String = 8,
		Boolean = 1,
		Float = 2,
		UnsignedInteger = 9,
        Vector = 4
    }

    /// <summary>
    /// These values are incorrect and have to be updated. 
    /// This enum was stolen from some other project. 
    /// I think it's NTA's...?
    /// </summary>
    public enum DVarFlag
    {
        DVAR_FLAG_NONE = 0x0,                  //no flags
        DVAR_FLAG_SAVED = 0x1,                      //saves in config_mp.cfg for clients
        DVAR_FLAG_LATCHED = 0x2,                  //no changing apart from initial value (although it might apply on a map reload)
        DVAR_FLAG_CHEAT = 0x4,                  //cheat
        DVAR_FLAG_REPLICATED = 0x8,                  //on change, this is sent to all clients (if you are host)
        DVAR_FLAG_USERCREATED = 0x100,                //a 'set' type command created it
        DVAR_FLAG_USERINFO = 0x200,                //userinfo?
        DVAR_FLAG_SERVERINFO = 0x400,                //in the getstatus oob
        DVAR_FLAG_WRITEPROTECTED = 0x800,                //write protected
        DVAR_FLAG_READONLY = 0x2000,               //read only (same as 0x800?)
        DVAR_FLAG_NONEXISTENT = 0    //no such dvar
    }
}
