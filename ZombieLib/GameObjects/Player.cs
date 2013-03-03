using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ZombieAPI.GameObjects
{
    public class Player : RemoteObject
    {
        GEntity Parent;
        public Player(Process Game, int PlayerAddr, GEntity ParentEntity)
        {
            this.Mem = new RemoteMemory(Game);
            this.Parent = ParentEntity;
            this.Position = PlayerAddr;
            this.BaseOffset = PlayerAddr;

            a_ServerTime = aInt(); //0x0000
            Move(36); //0x0004
            a_Origin = aVec(3); //0x0028
            a_Velocity = aVec(3); //0x0034
            Move(76); //0x0040
            a_Gravity = aInt(); //0x008C
            Move(4); //0x0090
            a_Speed = aInt(); //0x0094
            Move(352); //0x0098
            a_ViewAngles = aVec(3); //0x01F8
            a_PlayerHeightInt = aInt(); //0x0204
            a_PlayerHeightFloat = aInt(); //0x0208 (WARNING: Actually is a float, but it's 4 bytes eitherway so fuck the police)
            Move(44); //0x020C
            a_Health = aInt(); //0x0238
            Move(4); //0x023C
            a_iMaxHealth = aInt(); //0x0240
            Move(32); //0x0244
            a_PrimaryWeaponID = aInt(); //0x0264
            Move(24); //0x0268
            a_LethalWeaponID = aInt(); //0x0280
            Move(24); //0x0284
            a_SecondaryWeaponID = aInt(); //0x029C
            Move(24); //0x02A0
            a_TacticalWeaponID = aInt(); //0x02B8
            Move(308); //0x02BC
            a_PrimaryAmmoStock = aInt(); //0x03F0
            Move(4); //0x03F4
            a_SecondaryAmmoStock = aInt(); //0x03F8
            Move(48); //0x03FC
            a_PrimaryAmmoClip = aInt(); //0x042C
            a_LethalAmmo = aInt(); //0x0430
            a_SecondaryAmmoClip = aInt(); //0x0434
            a_TacticalAmmo = aInt(); //0x0438
            Move(272); //0x043C
            a_PerkFlags = aInt(); //0x054C 0x10 = Reduced Spread, 0x80000 = Faster Knife
            Move(5752); //0x0550
            a_Alive = aInt(); //0x1BC8 
            Move(14560); //0x1BCC
            a_Name2 = aString(); //0x54AC
            Move(16); //0x54BC
            a_MaxHealth = aInt(); //0x54CC
            Move(100); //0x54D0
            a_Name = aString(); //0x5534
            Move(20); //0x5544
            a_Rank = aInt(); //0x5558 
            a_Prestige = aInt(); //0x555C 
            Move(16); //0x55560
            a_XUID = aLong(); //0x5570
            Move(76); //0x5578
            a_Headshots = aInt(); //0x55C4 
            a_Money = aInt(); //0x55C8
            a_Kills = aInt(); //0x55CC 
            a_Assists = aInt(); //0x55D0 
            a_Deaths = aInt(); //0x55D4
            Move(24); //0x55D8 
            a_Downs = aInt(); //0x55F0 
            a_Revives = aInt(); //0x55F4 
            Move(140); //0x55F8
            a_ClippingMode = aInt(); //0x5684
            Move(368); //0x5688

            Weapons = new Weapons_(this);
            World = new World_(this);
            Stats = new Stats_(this);
        }

        public Team Team
        {
            get
            {
                return Parent.Team.Team;
            }
            set
            {
                Parent.Team.Team = value;
            }
        }


        public Weapons_ Weapons;
        public World_ World;


        public class Weapons_
        {
            Player Player;
            public Weapons_(Player owner)
            {
                Player = owner;
            }

            string id2name(int id)
            {
                try
                {
                    return Player.Parent.Parent.Weapons[id];
                }
                catch
                {
                    //Player.Parent.Parent.WriteLine("WARNING: Unknown weaponID found!");
                    return "jzm_unknown_weapon";
                }
            }

            int name2id(string name)
            {
                return Player.Parent.Parent.Weapons.FirstOrDefault(x => x.Value == name).Key;
            }

            public string PrimaryWeapon
            {
                get
                {
                    Player.Mem.Position = Player.a_PrimaryWeaponID;
                    return id2name(Player.Mem.ReadInt32());
                }
                set
                {
                    Player.Mem.Position = Player.a_PrimaryWeaponID;
                    Player.Mem.Write(name2id(value));
                }
            }

            public string SecondaryWeapon
            {
                get
                {
                    Player.Mem.Position = Player.a_SecondaryWeaponID;
                    return id2name(Player.Mem.ReadInt32());
                }
                set
                {
                    Player.Mem.Position = Player.a_SecondaryWeaponID;
                    Player.Mem.Write(name2id(value));
                }
            }

            public string LethalWeapon
            {
                get
                {
                    Player.Mem.Position = Player.a_LethalWeaponID;
                    return id2name(Player.Mem.ReadInt32());
                }
                set
                {
                    Player.Mem.Position = Player.a_LethalWeaponID;
                    Player.Mem.Write(name2id(value));
                }
            }

            public string CurrentWeapon
            {
                get
                {
                    return id2name(Player.Parent.CurrentWeapon);
                }
                set
                {
                    Player.Parent.CurrentWeapon = name2id(value);
                }
            }

            public string TacticalWeapon
            {
                get
                {
                    Player.Mem.Position = Player.a_TacticalWeaponID;
                    return id2name(Player.Mem.ReadInt32());
                }
                set
                {
                    Player.Mem.Position = Player.a_TacticalWeaponID;
                    Player.Mem.Write(name2id(value));
                }
            }

            public int PrimaryAmmoClip
            {
                get
                {
                    Player.Mem.Position = Player.a_PrimaryAmmoClip;
                    return Player.Mem.ReadInt32();
                }
                set
                {
                    Player.Mem.Position = Player.a_PrimaryAmmoClip;
                    Player.Mem.Write(value);
                }
            }

            public int SecondaryAmmoClip
            {
                get
                {
                    Player.Mem.Position = Player.a_SecondaryAmmoClip;
                    return Player.Mem.ReadInt32();
                }
                set
                {
                    Player.Mem.Position = Player.a_SecondaryAmmoClip;
                    Player.Mem.Write(value);
                }
            }

            public int PrimaryAmmoStock
            {
                get
                {
                    Player.Mem.Position = Player.a_PrimaryAmmoStock;
                    return Player.Mem.ReadInt32();
                }
                set
                {
                    Player.Mem.Position = Player.a_PrimaryAmmoStock;
                    Player.Mem.Write(value);
                }
            }

            public int SecondaryAmmoStock
            {
                get
                {
                    Player.Mem.Position = Player.a_SecondaryAmmoStock;
                    return Player.Mem.ReadInt32();
                }
                set
                {
                    Player.Mem.Position = Player.a_SecondaryAmmoStock;
                    Player.Mem.Write(value);
                }
            }

            public int TacticalAmmo
            {
                get
                {
                    Player.Mem.Position = Player.a_TacticalAmmo;
                    return Player.Mem.ReadInt32();
                }
                set
                {
                    Player.Mem.Position = Player.a_TacticalAmmo;
                    Player.Mem.Write(value);
                }
            }

            public int LethalAmmo
            {
                get
                {
                    Player.Mem.Position = Player.a_LethalAmmo;
                    return Player.Mem.ReadInt32();
                }
                set
                {
                    Player.Mem.Position = Player.a_LethalAmmo;
                    Player.Mem.Write(value);
                }
            }
        }

        public class World_
        {
            Player Player;
            public World_(Player owner)
            {
                Player = owner;
            }

            public bool Freeze
            {
                get
                {
                    Player.Mem.Position = Player.a_ClippingMode;
                    return Player.Mem.ReadInt32() == 4;
                }
                set
                {
                    Player.Mem.Position = Player.a_ClippingMode;
                    Player.Mem.Write(value ? 4 : 0);
                }
            }

            public int RemoveMe_CLIPPING_debug
            {
                get
                {
                    Player.Mem.Position = Player.a_ClippingMode;
                    return Player.Mem.ReadInt32();
                }
                set
                {
                    Player.Mem.Position = Player.a_ClippingMode;
                    Player.Mem.Write(value);
                }
            }

            public int Speed
            {
                get
                {
                    Player.Mem.Position = Player.a_Speed;
                    return Player.Mem.ReadInt32();
                }
                set
                {
                    Player.Mem.Position = Player.a_Speed;
                    Player.Mem.Write(value);
                }
            }

            public float[] Velocity
            {
                get
                {
                    Player.Mem.Position = Player.a_Velocity;
                    return Player.Mem.ReadVec3();
                }
                set
                {
                    Player.Mem.Position = Player.a_Velocity;
                    Player.Mem.Write(value);
                }
            }

            public float[] Origin
            {
                get
                {
                    Player.Mem.Position = Player.a_Origin;
                    return Player.Mem.ReadVec3();
                }
                set
                {
                    Player.Mem.Position = Player.a_Origin;
                    Player.Mem.Write(value);
                }
            }

            public float[] ViewAngles
            {
                get
                {
                    Player.Mem.Position = Player.a_ViewAngles;
                    return Player.Mem.ReadVec3();
                }
                set
                {
                    Player.Mem.Position = Player.a_ViewAngles;
                    Player.Mem.Write(value);
                }
            }

            public float PlayerHeight
            {
                get
                {
                    Player.Mem.Position = Player.a_PlayerHeightFloat;
                    return Player.Mem.ReadFloat();
                }
                set
                {
                    Player.Mem.Position = Player.a_PlayerHeightFloat;
                    Player.Mem.Write(value);
                }
            }

            public int Gravity
            {
                get
                {
                    Player.Mem.Position = Player.a_Gravity;
                    return Player.Mem.ReadInt32();
                }
                set
                {
                    Player.Mem.Position = Player.a_Gravity;
                    Player.Mem.Write(value);
                }
            }
        }

        public Stats_ Stats;
        public class Stats_
        {
            RemoteMemory Mem;
            Player Player;
            public Stats_(Player a)
            {
                Mem = new RemoteMemory(a.Mem.Process);
                Player = a;
            }

            public int Rank
            {
                get
                {
                    Mem.Position = Player.a_Rank;
                    return Mem.ReadInt32();
                }
                set
                {
                    Mem.Position = Player.a_Rank;
                    Mem.Write(value);
                }
            }

            public int Prestige
            {
                get
                {
                    Mem.Position = Player.a_Prestige;
                    return Mem.ReadInt32();
                }
                set
                {
                    Mem.Position = Player.a_Prestige;
                    Mem.Write(value);
                }
            }

            public int Deaths
            {
                get
                {
                    Mem.Position = Player.a_Deaths;
                    return Mem.ReadInt32();
                }
                set
                {
                    Mem.Position = Player.a_Deaths;
                    Mem.Write(value);
                }
            }

            public int Kills
            {
                get
                {
                    Mem.Position = Player.a_Kills;
                    return Mem.ReadInt32();
                }
                set
                {
                    Mem.Position = Player.a_Kills;
                    Mem.Write(value);
                }
            }

            public int Assists
            {
                get
                {
                    Mem.Position = Player.a_Assists;
                    return Mem.ReadInt32();
                }
                set
                {
                    Mem.Position = Player.a_Assists;
                    Mem.Write(value);
                }
            }

            public int Headshots
            {
                get
                {
                    Mem.Position = Player.a_Headshots;
                    return Mem.ReadInt32();
                }
                set
                {
                    Mem.Position = Player.a_Headshots;
                    Mem.Write(value);
                }
            }

            public int Downs
            {
                get
                {
                    Mem.Position = Player.a_Downs;
                    return Mem.ReadInt32();
                }
                set
                {
                    Mem.Position = Player.a_Downs;
                    Mem.Write(value);
                }
            }

            public int Revives
            {
                get
                {
                    Mem.Position = Player.a_Revives;
                    return Mem.ReadInt32();
                }
                set
                {
                    Mem.Position = Player.a_Revives;
                    Mem.Write(value);
                }
            }
        }

        public bool isAlive
        {
            get
            {
                Mem.Position = a_Alive;
                return Mem.ReadInt32() == 5;
            }
            set
            {
                Mem.Position = a_Alive;
                Mem.Write(value ? 5 : 0);
            }
        }

        public Stances Stance
        {
            get
            {
                return (Stances)Parent.Stance;
            }
            set
            {
                Parent.Stance = value;
            }
        }

        public int ClientNum
        {
            get
            {
                return Parent.ClientNum;
            }
        }

        public int ModelIndex
        {
            get
            {
                return Parent.ModelIndex;
            }
            set
            {
                Parent.ModelIndex = value;
            }
        }

        public string Name
        {
            get
            {
                Mem.Position = a_Name;
                return Mem.ReadString().Replace("\0", "");
            }
            set
            {
                Mem.Position = a_Name;
                Mem.Write(value);
            }
        }

        public string XUID
        {
            get
            {
                Mem.Position = a_XUID;
                return Mem.ReadInt64().ToString("x2");
            }
        }

        public int Money
        {
            get
            {
                Mem.Position = a_Money;
                return Mem.ReadInt32();
            }
            set
            {
                Mem.Position = a_Money;
                Mem.Write(value);
            }
        }

        public int Health
        {
            get
            {
                Mem.Position = a_Health;
                return Mem.ReadInt32();
            }
            set
            {
                Mem.Position = a_Health;
                Mem.Write(value);
            }
        }

        public int MaxHealth
        {
            get
            {
                Mem.Position = a_MaxHealth;
                return Mem.ReadInt32();
            }
            set
            {
                Mem.Position = a_MaxHealth;
                Mem.Write(value);
            }
        }

        public void Kick(string Message)
        {
            ServerCommand(Message, 53, ClientNum);
        }

        public void iPrintBoldLn(string Message)
        {
            ServerCommand(Message, 60, ClientNum);
        }

        public void Tell(string Message, bool Raw)
        {
            if (!Raw) Message = "^7Server: " + Message;
            ServerCommand(Message, 43, ClientNum);
        }

        public void iPrintLn(string Message)
        {
            ServerCommand(Message, 59, ClientNum);
        }

        public void MovePlayerCamera(string args)
        {
            ServerCommand(args, 88, ClientNum);
        }

        public void Electrocute()
        {
            ServerCommand("1", 89, ClientNum);
        }

        public void CustomSVCMD(int x, string param)
        {
            ServerCommand(param, x, ClientNum);
        }

        public void Ignite()
        {
            ServerCommand("1", 87, ClientNum);
        }

        public void SetClientDVar(string dvar)
        {
            ClientCommand(dvar, ClientNum);
        }

        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, [Out] int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);

        [DllImport("kernel32")]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);

        private IntPtr _SV_GameSendServerCommandAddress = IntPtr.Zero;
		private  IntPtr _cBuf_addTextFuncAddress = IntPtr.Zero;
        private IntPtr commandAddress = IntPtr.Zero;
        private byte[] commandBytes;
        private byte[] callBytes;
        private const uint MEM_COMMIT = 0x1000;
        private const uint MEM_RESERVE = 0x2000;
        private const uint PAGE_EXECUTE_READWRITE = 0x40;

        void ClientCommand(string CMD, int ClientNum)
        {
			if (_cBuf_addTextFuncAddress == IntPtr.Zero)
            {
                // Allocate memory for the stub.
                _cBuf_addTextFuncAddress = VirtualAllocEx(Mem.ProcessHandle, IntPtr.Zero, (uint)Stubs.WrapperTocBuf_AddText.Length, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);

                // Allocate memory for the command.
                commandBytes = Encoding.ASCII.GetBytes(CMD+"\0");
                commandAddress = VirtualAllocEx(Mem.ProcessHandle, IntPtr.Zero, (uint)commandBytes.Length, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);

                // Write the command into the allocated memory.
                int bytesWritten = 0;
                WriteProcessMemory(Mem.ProcessHandle, commandAddress, commandBytes, (uint)commandBytes.Length, bytesWritten);

                // Fix the stub with the parameter address.
                Array.Copy(BitConverter.GetBytes(commandAddress.ToInt32()), 0, Stubs.WrapperTocBuf_AddText, 9, 4);

                

                // Write the patched stub.
                WriteProcessMemory(Mem.ProcessHandle, _cBuf_addTextFuncAddress, Stubs.WrapperTocBuf_AddText, (uint)Stubs.WrapperTocBuf_AddText.Length, bytesWritten);

                // Create a new thread.
                IntPtr bytesout;
                CreateRemoteThread(Mem.ProcessHandle, IntPtr.Zero, 0, _cBuf_addTextFuncAddress, IntPtr.Zero, 0, out bytesout);

                if (_cBuf_addTextFuncAddress != IntPtr.Zero && commandAddress != IntPtr.Zero)
                {
                    VirtualFreeEx(Mem.ProcessHandle, _cBuf_addTextFuncAddress, (UIntPtr)Stubs.WrapperTocBuf_AddText.Length, 0x8000);
                    VirtualFreeEx(Mem.ProcessHandle, commandAddress, (UIntPtr)commandBytes.Length, 0x8000);
                }

                _cBuf_addTextFuncAddress = IntPtr.Zero;
            }
        }

        void ServerCommand(string Parameter, int CMDType, int ClientNum)
        {
            callBytes = BitConverter.GetBytes(Addresses.SV_GameSendServerCommand);

            if (_SV_GameSendServerCommandAddress == IntPtr.Zero)
            {
                // Allocate memory for the stub.
                _SV_GameSendServerCommandAddress = VirtualAllocEx(Mem.ProcessHandle, IntPtr.Zero, (uint)Stubs.WrapperToSV_GameSendServerCommand.Length, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);

                // Allocate memory for the command.
                commandBytes = Encoding.ASCII.GetBytes(String.Format("{0} \"{1}\"", Convert.ToChar(CMDType), Parameter));
                commandAddress = VirtualAllocEx(Mem.ProcessHandle, IntPtr.Zero, (uint)commandBytes.Length, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
                
                // Write the command into the allocated memory.
                int bytesWritten = 0;
                WriteProcessMemory(Mem.ProcessHandle, commandAddress, commandBytes, (uint)commandBytes.Length, bytesWritten);

                // Fix the stub with the parameter address.
                Array.Copy(BitConverter.GetBytes(commandAddress.ToInt32()), 0, Stubs.WrapperToSV_GameSendServerCommand, 9, 4);

                // Fix the stub with the call address.
                Array.Copy(callBytes, 0, Stubs.WrapperToSV_GameSendServerCommand, 16, 4);

                // Fix the stub with clientnum
                Array.Copy(new byte[] { (byte)ClientNum }, 0, Stubs.WrapperToSV_GameSendServerCommand, 26, 1);

                // Write the patched stub.
                WriteProcessMemory(Mem.ProcessHandle, _SV_GameSendServerCommandAddress, Stubs.WrapperToSV_GameSendServerCommand, (uint)Stubs.WrapperToSV_GameSendServerCommand.Length, bytesWritten);

                // Create a new thread.
                IntPtr bytesout;
                CreateRemoteThread(Mem.ProcessHandle, IntPtr.Zero, 0, _SV_GameSendServerCommandAddress, IntPtr.Zero, 0, out bytesout);

                if (_SV_GameSendServerCommandAddress != IntPtr.Zero && commandAddress != IntPtr.Zero)
                {
                    VirtualFreeEx(Mem.ProcessHandle, _SV_GameSendServerCommandAddress, (UIntPtr)Stubs.WrapperToSV_GameSendServerCommand.Length, 0x8000);
                    VirtualFreeEx(Mem.ProcessHandle, commandAddress, (UIntPtr)commandBytes.Length, 0x8000);
                }

                _SV_GameSendServerCommandAddress = IntPtr.Zero;
            }
        }

        int a_ServerTime;
        int a_Origin;
        int a_Velocity;
        int a_Gravity;
        int a_Speed;
        int a_ViewAngles;
        int a_PlayerHeightInt;
        int a_PlayerHeightFloat;
        int a_Health;
        int a_iMaxHealth;
        int a_PrimaryWeaponID;
        int a_LethalWeaponID;
        int a_SecondaryWeaponID;
        int a_TacticalWeaponID;
        int a_PrimaryAmmoStock;
        int a_SecondaryAmmoStock;
        int a_PrimaryAmmoClip;
        int a_LethalAmmo;
        int a_SecondaryAmmoClip;
        int a_TacticalAmmo;
        int a_PerkFlags;
        int a_Alive;
        int a_Name2;
        int a_MaxHealth;
        int a_Name;
        int a_Rank;
        int a_Prestige;
        int a_XUID;
        int a_Headshots;
        int a_Score;
        int a_Kills;
        int a_Assists;
        int a_Deaths;
        int a_Downs;
        int a_Revives;
        int a_Money;
        int a_ClippingMode;
    }
}
