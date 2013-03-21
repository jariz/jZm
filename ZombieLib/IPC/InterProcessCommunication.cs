// DO NOT MODIFY - UNFINISHED
// Written by kokole

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ZombieAPI
{
    class IPC
    {
        private IntPtr ProcessHandle = IntPtr.Zero;
        private IntPtr eventHandle = IntPtr.Zero;
        private IntPtr eventHandleInT6ZM = IntPtr.Zero;
        private const uint MaximumAmountOfEventHooks = 256;
        private uint CurrentlyHookedEvents = 0;
        // HookList saves a list of all the hook addresses
        private IntPtr[] HookList = new IntPtr[MaximumAmountOfEventHooks];
        private IntPtr AllocatedSyncEventStub = IntPtr.Zero;
        public IntPtr CurrentEventID = IntPtr.Zero;

        private void CreateSyncEventStub()
        {
            AllocatedSyncEventStub = I.VirtualAllocEx(ProcessHandle, IntPtr.Zero, (uint)Stubs.SyncEventStub.Length, I.MEM_COMMIT | I.MEM_RESERVE, I.PAGE_EXECUTE_READWRITE);
        }

        public void InitJZMEventHookingManager(IntPtr ProcessHandleLocal)
        {
            // call OpenEvent in t6zm
            ProcessHandle = ProcessHandleLocal;
            eventHandle = I.CreateEvent(IntPtr.Zero, 1, 0, "jZm_eventWaiter");
            CurrentEventID = I.VirtualAllocEx(ProcessHandle, IntPtr.Zero, 4, I.MEM_COMMIT | I.MEM_RESERVE, I.PAGE_EXECUTE_READWRITE);
            CreateSyncEventStub();
        }

        public void AddEventHook(IntPtr hookStubAddress, uint eventID)
        {

        }

        public void DeleteEventHook(uint eventID)
        {

        }

        public void ShutdownJZMEventHookingManager()
        {
            // safe hook shutdown here, unhook everything
            //I.VirtualFreeEx(ProcessHandle, allocatedPipeStub, 0, I.MEM_RELEASE);
            for (uint i = 0; i < CurrentlyHookedEvents; i++)
            {
                if (HookList[i] != IntPtr.Zero)
                {
                    I.VirtualFreeEx(ProcessHandle, HookList[i], 0, I.MEM_RELEASE);
                    HookList[i] = IntPtr.Zero;
                }
            }
            CurrentlyHookedEvents = 0;
            I.CloseHandle(eventHandle);
            eventHandle = IntPtr.Zero;
            I.CloseHandle(ProcessHandle);
            ProcessHandle = IntPtr.Zero;
        }
    }
}