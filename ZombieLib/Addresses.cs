﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    /// <summary>
    /// Class that contains all offsets to the game's data and functions.
    /// </summary>
    /// <remarks>
    /// PatternRecognition will look for the right offsets for each address, so the actual addresses may vary.
    /// </remarks>
    class Addresses
    {
        // patterns :)
        public static int GEntity = 0x2140340;
        public static int GEntity_Size = 0x31C;
        public static int Entity = 0x10B6964;
        public static int Entity_Size = 0x380; 
        public static int CG = 0x10B5280;
        public static int CGS = 0x10B525C;
        public static int WeaponDef = 0x02C0DA4C;
        public static int SV_GameSendServerCommand;
        public static int cBuf_AddText;
        public static int G_Say = 0x00493df0;
        public static int GameData = 0x0118B5AD;
        public static int DvarPointers = 0x298B330;
        public static int Level = 0x22B6580;
    }
}
