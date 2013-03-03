using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    public enum Stances 
    { 
        Standing = 2,
        Crouched = 6, 
        Prone = 10,
        Firing = 0x80042,
        Running = 0x20002,
        IronSights = 0x80002
    }

    public enum Perks
    {
        ReducedSpread = 0x10, 
        FasterKnife = 0x80000
    }

    public enum EntityType
    {
        ET_GENERAL,
        ET_PLAYER,
        ET_PLAYER_CORPSE,
        ET_ITEM,
        ET_MISSILE,
        ET_INVISIBLE,
        ET_SCRIPTMOVER,
        ET_SOUND_BLEND,
        ET_FX,
        ET_LOOP_FX,
        ET_PRIMARY_LIGHT,
        ET_TURRET,
        ET_HELICOPTER,
        ET_PLANE,
        ET_VEHICLE,
        ET_VEHICLE_CORPSE,
        ET_ACTOR,
        ET_ACTOR_SPAWNER,
        ET_ACTOR_CORPSE,
        ET_STREAMER_HINT,
        ET_ZBARRIER
    };

    public enum Team
    {
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
