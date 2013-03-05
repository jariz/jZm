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

        Dictionary<int, float[]> PlayerOrigins = new Dictionary<int, float[]>();

        public bool Go = false;

        bool yes = true;
        void API_OnFrame()
        {
            if (countdown)
            {
                Player[] pls = API.GetPlayers();
                //wait for players to actually be in the server
                if (pls.Length > 0)
                {
                    foreach (Player pl in pls)
                    {
                        pl.World.Freeze = true;
                        pl.MaxHealth = int.MaxValue;
                        API.WriteLine(pl.Health.ToString());
                        pl.Health = int.MaxValue;
                    }

                    if (lasttick == 0) lasttick = DateTime.Now.Ticks;

                    long x = DateTime.Now.Ticks - lasttick;

                    if (new DateTime(x).Second > 0)
                    {
                        foreach (Player pl in pls)
                        {
                            string msg2 = "^3WARNING: ^2You've joined a modded lobby!";
                            string msg = "If you don't want to play a modified game: ^1LEAVE IN ^7>^3" + count + "^7<^1 SECONDS";

                            if (count == 8)
                                pl.iPrintLn(msg);
                            else if(count == 9)
                                pl.iPrintLn(msg2);
                            else if (count == 10)
                            {
                                PlayerOrigins.Add(pl.ClientNum, pl.World.Origin);
                                float[] t = pl.World.Origin;
                                t[2] += 3800;
                                pl.World.Origin = t;
                            }

                            yes = !yes;
                            if (yes)
                                pl.iPrintBoldLn(msg);
                            else
                                pl.iPrintBoldLn(msg2);
                        }
                        if (count == 0)
                        {
                            countdown = false;
                            foreach (Player pl in pls)
                            {
                                pl.World.Freeze = false;
                                pl.MaxHealth = 190;
                                pl.Health = 190;
                                if (!PlayerOrigins.ContainsKey(pl.ClientNum))
                                    pl.Kick("^3jZm: ^1Unable to start match, please try again later");
                                else
                                {
                                    pl.World.Origin = PlayerOrigins[pl.ClientNum];
                                    PlayerOrigins.Remove(pl.ClientNum);
                                }
                            }
                            Go = true;
                        }
                        count--;
                        //API.Write(count.ToString());

                        lasttick = DateTime.Now.Ticks;

                    }
                }
                
            }
        }

        bool countdown;
        int count;
        long lasttick;



        void API_OnMapLoad()
        {
            count = 15;
            countdown = true;
        }
    }
}
