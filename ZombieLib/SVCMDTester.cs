using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZombieAPI.GameObjects;

namespace ZombieAPI
{
    class SVCMDTester
    {
        public string Name
        {
            get
            {
                return "jZm Core SVCMDTester";
            }
        }

        public string Author
        {
            get
            {
                return "JariZ";
            }
        }

        public string Desc
        {
            get
            {
                return "Core testing plugin";
            }
        }

        int[] skip = new int[] { 53, 74, 85, 88 };
        public void Init(ZombieAPI API)
        {
            foreach (Player player in API.GetPlayers())
            {
                GEntity bus =  API.GetBus();
                float[] vec = bus.Origin;

                vec[2] += 200;

                player.World.Origin = vec;
                bus.ModelIndex = 0;
                player.MaxHealth = int.MaxValue;
                player.Health = int.MaxValue;


                int x = 0;
                while (true)
                {
                    //player.Ignite();
                    //string arg = string.Format("{0} {1} {2}", new Random().Next(0, 100), new Random().Next(0, 1000), new Random().Next(600, 1000));
                    //string arg = "0";
                    //API.WriteLine(arg);
                    //player.MovePlayerCamera(arg);
                    if (!skip.Contains(x))
                    {
                        player.CustomSVCMD(x, "100");
                        API.WriteLine("SVCMD TEST: " + x);
                        //player.Tell("^1SVCMD_^2" + x, false);
                    }
                    else API.WriteLine("!!SKIPPED SVCMD " + x + "!!");
                    System.Threading.Thread.Sleep(150);

                    //System.Threading.Thread.Sleep(100);
                    //Console.ReadLine();
                    x++;
                }
            }
        }
    }
}
