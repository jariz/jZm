﻿using System;
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
            /*
             * TODO: Fix crash when exiting jZm and then trying to chat
             * Fix null end character (that fkn "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")
            */
            this.API = API;
            API.OnChat += new ChatHandler(API_OnChat);
            API.OnNativeEvent += new NativeEventHandler(API_OnNativeEvent);
        }

        void API_OnChat(Player Player, string Message)
        {
            API.WriteLine(Player.Name + ": " + Message);
        }

        void API_OnNativeEvent(GEntity Entity, string EventName)
        {
            // dont test, event not finished
        }
    }
}
