using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ZombieAPI
{
    /// <summary>
    /// PatternRecognition is a class that dynamically searches for addresses so when the offsets change,
    /// We don't need the hardcoded addresses anymore.
    /// Completely made by Barata, Edited slightly by JariZ
    /// </summary>
    public class PatternRecognition
    {
        public static void Run(IntPtr ProcessHandle)
        {
            PHandle = ProcessHandle;

            //CG_Init = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x81\xEC\x00\x00\x00\x00\xA1\x00\x00\x00\x00\x53\x8B\x9C\x24", "xx????x????xxxx");
            G_OnSay = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x81\xEC\x00\x00\x00\x00\x53\x8B\x9C\x24\x00\x00\x00\x00\x55\x56\x57\x8B\xBC\x24\x00\x00\x00\x00\x83\xFB\x01", "xx????xxxx????xxxxxx????xxx");
            Entity = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x03\x34\x9D\x00\x00\x00\x00\x83\xE8\x00\xF3\x0F\x10", "xxx????xxxxxx");
            EntitySize = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x69\xF6\x00\x00\x00\x00\x03\x34\x9D\x00\x00\x00\x00\x6A\x01\x6A\x00\x6A\x00", "xx????xxx????xxxxxx");
            GEntity = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x81\xC6\x00\x00\x00\x00\x39\x8E\x00\x00\x00\x00\x75\x29\x50\x68", "xx????xx????xxxx");
            //WepDef_t = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x8b\x04\x85\x00\x00\x00\x00\x89\x3d\x00\x00\x00\x00\x89\x3d\x00\x00\x00\x00\x8b\x70\x08", "xxx????xx????xx????xxx");
            SV_GameSendServerCommand = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x56\x8b\x74\x24\x08\x83\xfe\xff\x75\x00\x8b\x44\x24\x10\x8b\x4c\x24\x0c\x50\x68", "xxxxxxxxx?xxxxxxxxxx");
            cBuf_AddText = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x6a\x33\xe8\x00\x00\x00\x00\x8b\x4c\x24\x0c\x83\xc4\x04\x8b\xd1\x85\xc9\x74\x00\x8a\x01\x3c\x70\x74", "xxx????xxxxxxxxxxxx?xxxxx");
            CGS_t = FindPattern(ProcessHandle, 0x401000, 0x900000, "\xA1\x00\x00\x00\x00\x8B\xBC\xB8\x00\x00\x00\x00\x85\xFF\x74\x3D\x8B\x4C\x24\x10\x53\x8B\x99", "x????xxx????xxxxxxxxxxx");
            CG_t = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x8B\x35\x00\x00\x00\x00\xF3\x0F\x10\x86\x00\x00\x00\x00\x0F\x2E\x05\x00\x00\x00\x00\xF3\x0F\x10\x8E", "xx????xxxx????xxx????xxxx");
            GEntitySize = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x69\xF6\x00\x00\x00\x00\x81\xC6\x00\x00\x00\x00\x39\x8E\x00\x00\x00\x00\x75\x29\x50\x68", "xx????xx????xx????xxxx");
            DvarPointers = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x8B\xA8\x00\x00\x00\x00\x85\xED\x0F\x84\x00\x00\x00\x00\x8B\xFF\x8B\x4C\x24\x1C\x85\x4D\x0C\x0F\x84\x00\x00\x00\x00", "xx????xxxx????xxxxxxxxxxx????");
            GameData = FindPattern(ProcessHandle, 0x401000, 0x900000, "\x8D\x80\x00\x00\x00\x00\x68\x00\x00\x00\x00\x50\x8D\x71\xFF\x23\x35\x00\x00\x00\x00\xE8\x00\x00\x00\x00\x83\xC4\x08\x50\x8D\x54\x24\x18", "xx????x????xxxxxx????x????xxxxxxxx");

            Apply();
        }

        public static void Apply()
        {
            //dataz
            Addresses.Entity = Fix(Entity, 0x3, 0);
            Addresses.Entity_Size = Fix(EntitySize, 0x2, 0);
            Addresses.GEntity = Fix(GEntity, 0x2, 0);
            Addresses.GEntity_Size = Fix(GEntitySize, 0x2, 0);
            Addresses.CG = Fix(CG_t, 0x2, 0);
            Addresses.CGS = Fix(CGS_t, 0x1, 0);
            Addresses.WeaponDef = Fix(WepDef_t, 0x3, 0);
            Addresses.DvarPointers = Fix(DvarPointers, 0x2, 0);
            Addresses.GameData = Fix(GameData, 0x2, 0x1);


            //functions
            Addresses.G_OnSay = G_OnSay;
            Addresses.cBuf_AddText = cBuf_AddText;
            Addresses.SV_GameSendServerCommand = SV_GameSendServerCommand;
        }

        private static IntPtr PHandle = IntPtr.Zero;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, IntPtr dwSize, out IntPtr lpNumberOfBytesRead);

        static int FindPattern(IntPtr pHandle, int startAddress, int endAddress, string pattern, string mask)
        {
            var buffer = new byte[endAddress - startAddress];
            IntPtr bytesRead;
            if (ReadProcessMemory(pHandle, (IntPtr)startAddress, buffer, (IntPtr)buffer.Length, out bytesRead))
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    for (int x = 0; x < pattern.Length; x++)
                    {
                        if (buffer[i + x] == pattern[x] || mask[x] == '?')
                        {
                            if (x == pattern.Length - 1)
                                return startAddress + i;
                            continue;
                        }
                        break;
                    }
                }
            }

            return -1;
        }

        static int Fix(int address, int offset, int correct)
        {
            var buffer = new byte[4];
            IntPtr bytesRead;
            ReadProcessMemory(PHandle, (IntPtr)(address + offset), buffer, (IntPtr)4, out bytesRead);
            return BitConverter.ToInt32(buffer, 0) + correct;
        }

        static int CG_Init;
        static int G_OnSay;
        static int Entity;
        static int EntitySize;
        static int GEntity;
        static int GEntitySize;
        static int CG_t;
        static int CGS_t;
        static int DvarPointers;
        static int WepDef_t;
        static int cBuf_AddText;
        static int SV_GameSendServerCommand;
        static int GameData;

    }
}
