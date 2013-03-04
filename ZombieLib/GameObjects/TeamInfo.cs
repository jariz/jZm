using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ZombieAPI.GameObjects
{
    /// <summary>
    /// The teaminfo object is a object that.... holds team info?
    /// </summary>
    public class TeamInfo : RemoteObject
    {
        /// <summary>
        /// Initialize a new TeamInfo based on the offset.
        /// </summary>
        /// <remarks>
        /// DO NOT CALL FROM PLUGIN. Use <see cref="GEntity.Team"/> to get this object.
        /// </remarks>
        /// <param name="Game">The game process</param>
        /// <param name="TeamInfoAddr">The offset of the entity</param>
        /// <param name="ParentEntity">ZombieAPI that initializes this class</param>
        public TeamInfo(Process Game, int TeamInfoAddr, GEntity ParentEntity)
        {
            this.Mem = new RemoteMemory(Game);
            this.BaseOffset = TeamInfoAddr;

            //This points back to the parent enity, however we don't need it so move over...
            Move(4); //0x0000
            a_Team = aInt(); //0x0004
        }

        int a_Team;

        /// <summary>
        /// The associated gentity's Team.
        /// </summary>
        public Team Team
        {
            get
            {
                Mem.Position = a_Team;
                return (Team)Mem.ReadInt32();
            }
            set
            {
                Mem.Position = a_Team;
                Mem.Write(value);
            }
        }
    }
}
