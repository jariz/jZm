using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZombieAPI.GameObjects;

namespace ZombieAPI
{
    class TestingPlugin : jZmPlugin
    {
        public string Name
        {
            get
            {
                return "testing";
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
                return "jZm core plugin";
            }
        }

        ZombieAPI API;
        public void Init(ZombieAPI API)
        {
            this.API = API;

            Portal p = new Portal(-6865.08154f, 5076.141f, 50f);
            p.PortalTriggered += new PortalHandler(p_PortalTriggered);
            API.Portals.Add(p);
        }

        void p_PortalTriggered(Portal portal, Player player)
        {
            float[] bus = API.GetBus().Origin;
            bus[2] += 200;
            player.World.Origin = bus;
            player.Ignite();
        }

    }
}
