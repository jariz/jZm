using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace ZombieAPI.Hooks
{
    class ChatHook : Hook
    {
        private IntPtr chatAddress = IntPtr.Zero;
        private IntPtr chatHookAddress = IntPtr.Zero;
        private IntPtr gentityAddress = IntPtr.Zero;
        private byte[] chatBytes = new byte[128];
        private byte[] gentityBytes = new byte[4];
        private IntPtr ProcessHandle;
        private System.Diagnostics.Process BaseP;

        public override void SetHook(System.Diagnostics.Process BaseProcess)
        {
            BaseP = BaseProcess;
            ProcessHandle = I.OpenProcess(0x001F0FFF /*all*/, false, BaseProcess.Id);
            if (ProcessHandle == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error());

            UIntPtr bytesWritten;

            Int32 FinalAddress, DwJump;

            //Jmp Bytes for the hook//

            byte[] hkBytes = new byte[]
            { 
                0xE9, 0x00, 0x00, 0x00, 0x00, 0x90, 0x90,
            };

            //Allocate memory for Stubs.G_Say_GetChatStub//

            chatHookAddress = I.VirtualAllocEx(ProcessHandle, IntPtr.Zero, (uint)Stubs.G_Say_Stub.Length, I.MEM_COMMIT | I.MEM_RESERVE, I.PAGE_EXECUTE_READWRITE);

            //Allocate memory for the Chat Bytes//

            chatAddress = I.VirtualAllocEx(ProcessHandle, IntPtr.Zero, 4, I.MEM_COMMIT | I.MEM_RESERVE, I.PAGE_EXECUTE_READWRITE);

            //Allocate memory for the gentity Number//

            gentityAddress = I.VirtualAllocEx(ProcessHandle, IntPtr.Zero, 4, I.MEM_COMMIT | I.MEM_RESERVE, I.PAGE_EXECUTE_READWRITE);

            //Calculate the jumps//

            FinalAddress = (chatHookAddress.ToInt32() - 0x004BF5D3) - 5;

            DwJump = ((0x004BF5DF - chatHookAddress.ToInt32()) - 0x20) + 1;

            //Correct the stubs//

            Array.Copy(BitConverter.GetBytes(FinalAddress), 0, hkBytes, 1, 4);//Hook first jump

            Array.Copy(BitConverter.GetBytes(DwJump), 0, Stubs.G_Say_Stub, 22, 4);//Stub second jump

            Array.Copy(BitConverter.GetBytes(chatAddress.ToInt32()), 0, Stubs.G_Say_Stub, 10, 4);//allocated memory address for the chat

            Array.Copy(BitConverter.GetBytes(gentityAddress.ToInt32()), 0, Stubs.G_Say_Stub, 17, 4);//allocated memory address for the gentity number

            //Write the stub and bytes to memory//

            I.WriteProcessMemory(ProcessHandle, chatHookAddress, Stubs.G_Say_Stub, (uint)Stubs.G_Say_Stub.Length, out bytesWritten);

            I.WriteProcessMemory(ProcessHandle, (IntPtr)0x004BF5D3, hkBytes, (uint)hkBytes.Length, out bytesWritten);
        }

        public override void HookFrame(ZombieAPI API)
        {
            IntPtr bytesout;

            UIntPtr bytesWritten;

            byte[] ptrChat = new byte[4];

            byte[] btnull = new byte[]
            {
                0x00,
            };

            I.ReadProcessMemory(ProcessHandle, chatAddress, ptrChat, 4, out bytesout);

            if (ptrChat[0] != 0x00 && ptrChat[2] != 0x00)
            {
                I.ReadProcessMemory(ProcessHandle, (IntPtr)BitConverter.ToInt32(ptrChat, 0), chatBytes, 128, out bytesout);

                if (chatBytes[0] != 0x00)
                {
                    string FinalChat = System.Text.Encoding.ASCII.GetString(chatBytes);

                    GameObjects.Player player = ReadG_SayClient(API);

                    I.WriteProcessMemory(ProcessHandle, (IntPtr)BitConverter.ToInt32(ptrChat, 0), btnull, (uint)btnull.Length, out bytesWritten);

                    API.TriggerChat(player, FinalChat);
                }
            }
        }

        private GameObjects.Player ReadG_SayClient(ZombieAPI api)
        {
            IntPtr bytesout;

            byte[] ptrGNumber = new byte[4];

            I.ReadProcessMemory(ProcessHandle, gentityAddress, ptrGNumber, 4, out bytesout);

            if (ptrGNumber[0] != 0x00 && ptrGNumber[2] != 0x00)
            {
                I.ReadProcessMemory(ProcessHandle, (IntPtr)BitConverter.ToInt32(ptrGNumber, 0), gentityBytes, 4, out bytesout);

                if (gentityBytes != null)
                {
                    return api.Entities[BitConverter.ToInt32(gentityBytes, 0)].Player;
                }
            }
            return null;
        }

        public override void Unhook()
        {
            int bytesWritten = 0;

            byte[] hkBytes = new byte[]
            { 
                0x8B, 0xB4, 0x24, 0xF4, 0x00, 0x00, 0x00,

            };

            I.WriteProcessMemory(ProcessHandle, (IntPtr)0x004BF5D3, hkBytes, (uint)hkBytes.Length, bytesWritten);

            I.VirtualFreeEx(ProcessHandle, chatHookAddress, (UIntPtr)Stubs.G_Say_Stub.Length, 0x8000);
            if (chatBytes != null)
                I.VirtualFreeEx(ProcessHandle, chatAddress, (UIntPtr)chatBytes.Length, 0x8000);
        }
    }
}
