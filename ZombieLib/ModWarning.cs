using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZombieAPI.GameObjects;

namespace ZombieAPI
{
    class ModWarning : jZmPlugin
    {
        public string Name
        {
            get
            {
                return "ModWarning";
            }
        }

        public string Author
        {
            get
            {
                return "jZm";
            }
        }

        public string Desc
        {
            get
            {
                return "jZm core plugin, Shows warning to all joined players";
            }
        }

        ZombieAPI API;
        public void Init(ZombieAPI API)
        {
            this.API = API;
            API.OnMapLoad += new MapHandler(API_OnMapLoad);
            API.OnFrame += new OnFrameHandler(API_OnFrame);
        }

        void API_OnFrame()
        {
            if (countdown)
            {
                foreach (Player pl in API.GetPlayers())
                {
                    pl.World.Freeze = true;
                    pl.MaxHealth = int.MaxValue;
                    pl.Health = int.MaxValue;
                }

                if (lasttick == 0) lasttick = DateTime.Now.Ticks;

                long x = DateTime.Now.Ticks - lasttick;

                if (new DateTime(x).Second > 0)
                {
                    foreach (Player pl in API.GetPlayers())
                    {
                        pl.iPrintBoldLn("^3WARNING: ^2You've joined a modded lobby, ^3If you don't want to play a modified game\r\n^4LEAVE IN >" + count + "< SECONDS");
                    }
                    if (count == 0)
                        countdown = false;
                    count--;
                    API.Write(count.ToString());

                    lasttick = DateTime.Now.Ticks;
                }
                
            }
        }

        bool countdown;
        int count;
        long lasttick;



        void API_OnMapLoad()
        {
            foreach (Player player in API.GetPlayers())
            {
                player.iPrintLn("^7WARNING: ^1You've joined a 'modded lobby'.\r\n^3If you do not wish to play a modified game,\r\n^2Please leave now");
                player.World.Freeze = true;
                count = 11;
                countdown = true;
            }
        }
    }
}
