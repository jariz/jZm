// DO NOT MODIFY - UNFINISHED
// Written by kokole

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

/*
 * Inter-process communication functions, used to communicate
 * between Black Ops II and jZm (synchronous)
 * It's a local named pipe declared as:
 * Server - jZm
 * Client - t6zm
 * Every hook has its pipe stub
 * Actually this is only to hook events, but in a very easy way
 * The only limitation is that the first byte of the hook address
 * must be 0xE8 (call)
*/

namespace ZombieAPI
{
    class IPC
    {
        private IntPtr ProcessHandle = IntPtr.Zero;
        private IntPtr pipeHandle = IntPtr.Zero;
        private const uint MaximumAmountOfEventHooks = 256;
        private uint CurrentlyHookedEvents = 0;
        public const uint outboundBufferSize = 4096;
        public const uint inboundBufferSize = 4096;
        // HookList saves a list of all the hook addresses
        private IntPtr[] HookList = new IntPtr[MaximumAmountOfEventHooks];
        // when deleting a hook, it restores the original bytes
        private uint[] OriginalBytesList = new uint[MaximumAmountOfEventHooks];
        public byte[] outboundBuffer = new byte[outboundBufferSize];
        public byte[] inboundBuffer = new byte[inboundBufferSize];

        public void InitJZMEventHookingManager(IntPtr ProcessHandleLocal)
        {
            if (ProcessHandle == IntPtr.Zero || pipeHandle == IntPtr.Zero)
            {
                if (ProcessHandle == IntPtr.Zero)
                    ProcessHandle = ProcessHandleLocal;
                if (pipeHandle == IntPtr.Zero)
                    pipeHandle = I.CreateNamedPipe("\\\\.\\pipe\\jZm", 3, 0, 1, outboundBufferSize, inboundBufferSize, 0, IntPtr.Zero);
            }
            if (ProcessHandle == IntPtr.Zero || pipeHandle == (IntPtr)I.INVALID_HANDLE_VALUE)
                throw new Exception("Failed to init jZm communication pipe");
        }

        private void FixPipeStub(IntPtr hookAddress, IntPtr pipeStub, uint eventID)
        {

        }

        public void AddEventHook(IntPtr hookAddress, uint eventID)
        {
            if (CurrentlyHookedEvents < MaximumAmountOfEventHooks)
            {
                if (HookList[eventID] == IntPtr.Zero)
                {
                    byte[] firstByte = new byte[1];
                    uint bytesRead = 0;
                    // check that the first byte of the hook address is 0xE8
                    I.ReadProcessMemory(
                        ProcessHandle,
                        hookAddress,
                        firstByte,
                        1,
                        out bytesRead
                        );
                    if (firstByte[0] != 0xE8)
                        throw new Exception("Check failed #1. You're not hooking a call, or couldn't read.");
                    HookList[eventID] = I.VirtualAllocEx(ProcessHandle, IntPtr.Zero, (uint)Stubs.SyncPipeStubForHooks.Length, I.MEM_COMMIT | I.MEM_RESERVE, I.PAGE_EXECUTE_READWRITE);
                    if (HookList[eventID] == IntPtr.Zero)
                        throw new Exception("Failed to write jZm communication pipe stub #3. GetLastWin32Error returned " + Marshal.GetLastWin32Error());
                    uint bytesWritten = 0;
                    // write stub
                    I.WriteProcessMemory(
                        ProcessHandle,
                        HookList[eventID],
                        Stubs.SyncPipeStubForHooks,
                        (uint)Stubs.SyncPipeStubForHooks.Length,
                        out bytesWritten
                        );
                    // fix stub (addresses)
                    FixPipeStub(hookAddress, HookList[eventID], eventID);
                    CurrentlyHookedEvents++;
                }
                else
                    throw new Exception("Failed to write jZm communication pipe stub #2. This ID is already hooked.");
            }
            else
                throw new Exception("Failed to write jZm communication pipe stub #4. This ID is already hooked.");
        }

        public void DeleteEventHook(uint eventID)
        {
            if (CurrentlyHookedEvents != 0)
            {
                CurrentlyHookedEvents--;
            }
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
            I.CloseHandle(pipeHandle);
            pipeHandle = IntPtr.Zero;
            I.CloseHandle(ProcessHandle);
            ProcessHandle = IntPtr.Zero;
        }
    }
}