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
}
