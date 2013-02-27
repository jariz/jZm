using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ZombieAPI.GameObjects
{
    public class TeamInfo : RemoteObject
    {
        public TeamInfo(Process Game, int TeamInfoAddr, GEntity ParentEntity)
        {
            this.Mem = new RemoteMemory(Game);
            this.BaseOffset = TeamInfoAddr;

            //This points back to the parent enity, however we don't need it so move over...
            Move(4); //0x0000
            a_Team = aInt(); //0x0004
        }

        int a_Team;

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
