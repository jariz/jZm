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
            Move(20316); //0x0550
            a_Name2 = aString(); //0x54AC
            Move(16); //0x54BC
            a_MaxHealth = aInt(); //0x54CC
            Move(100); //0x54D0
            a_Name = aString(); //0x5534
            Move(132); //0x5544
            a_Money = aInt(); //0x55C8
            Move(556); //0x55CC

            Weapons = new Weapons_(this);
            World = new World_(this);
        }

        public void iPrintBoldLn(string x)
        {

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
                    Player.Parent.Parent.WriteLine("WARNING: Unknown weaponID found!");
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

        public void Kick(int ClientNum, string Message)
        {
            ServerCommand(Message, 53, ClientNum);
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
        private IntPtr commandAddress = IntPtr.Zero;
        private byte[] commandBytes;
        private byte[] callBytes;
        private const uint MEM_COMMIT = 0x1000;
        private const uint MEM_RESERVE = 0x2000;
        private const uint PAGE_EXECUTE_READWRITE = 0x40;

        void ServerCommand(string Parameter, int CMDType, int ClientNum)
        {
            callBytes = BitConverter.GetBytes(Addresses.ServerCommand);

            if (_SV_GameSendServerCommandAddress == IntPtr.Zero)
            {
                // Allocate memory for the stub.
                _SV_GameSendServerCommandAddress = VirtualAllocEx(Mem.ProcessHandle, IntPtr.Zero, (uint)Stubs.WrapperToSV_GameSendServerCommand.Length, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);

                // Allocate memory for the command.

                commandBytes = Encoding.ASCII.GetBytes(String.Format(Convert.ToChar(CMDType)+" \"{0}\"", Parameter));
                commandAddress = VirtualAllocEx(Mem.ProcessHandle, IntPtr.Zero, (uint)commandBytes.Length, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
                
                // Write the command into the allocated memory.
                int bytesWritten = 0;
                WriteProcessMemory(Mem.ProcessHandle, commandAddress, commandBytes, (uint)commandBytes.Length, bytesWritten);

                // Fix the stub with the parameter address.
                Array.Copy(BitConverter.GetBytes(commandAddress.ToInt32()), 0, Stubs.WrapperToSV_GameSendServerCommand, 9, 4);

                // Fix the stub with the call address.
                Array.Copy(callBytes, 0, Stubs.WrapperToSV_GameSendServerCommand, 16, 4);

                // Fix the stub with clientnum
                Array.Copy(BitConverter.GetBytes(ClientNum), 0, Stubs.WrapperToSV_GameSendServerCommand, 26, 4);

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
        int a_Name2;
        int a_MaxHealth;
        int a_Name;
        int a_Money;
    }
}
