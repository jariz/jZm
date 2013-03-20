// DO NOT MODIFY
// UNFINISHED

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
 * THANK YOU JARIZ FOR THIS BEING EXTERNAL
*/

// MULTIPLE STUBS BUT ONLY 1 PIPE

namespace ZombieAPI
{
    class IPC
    {
        private IntPtr ProcessHandle = IntPtr.Zero;
        private IntPtr pipeHandle = IntPtr.Zero;
        //private IntPtr allocatedPipeStub = IntPtr.Zero;
        public const uint bufferSize = 256;
        // create outbound buffer?
        public byte[] inboundBuffer = new byte[bufferSize];

        public void InitJZMPipeManager(IntPtr ProcessHandleLocal)
        {
            if (ProcessHandle == IntPtr.Zero || pipeHandle == IntPtr.Zero)
            {
                if (ProcessHandle == IntPtr.Zero)
                    ProcessHandle = ProcessHandleLocal;
                if (pipeHandle == IntPtr.Zero)
                    pipeHandle = I.CreateNamedPipe("\\\\.\\pipe\\jZm", 3, 0, 1, bufferSize, bufferSize, 0, IntPtr.Zero);
                // we are going to do this inside hooks
                /*if (allocatedPipeStub == IntPtr.Zero)
                {
                    int result = 0;
                    uint bytesWritten = 0;
                    allocatedPipeStub = I.VirtualAllocEx(ProcessHandle, IntPtr.Zero, (uint)Stubs.SyncPipeStubForHooks.Length, I.MEM_COMMIT | I.MEM_RESERVE, I.PAGE_EXECUTE_READWRITE);
                    result = I.WriteProcessMemory(
                        ProcessHandle,
                        allocatedPipeStub,
                        Stubs.SyncPipeStubForHooks,
                        (uint)Stubs.SyncPipeStubForHooks.Length,
                        out bytesWritten
                        );
                    if (result == 0) // R.I.P. C++ style if (result)
                        throw new Exception("Failed to write jZm communication pipe stub");
                }*/
            }
            if (ProcessHandle == IntPtr.Zero || pipeHandle == (IntPtr)I.INVALID_HANDLE_VALUE)
                throw new Exception("Failed to init jZm communication pipe");
        }

        public void ShutdownJZMPipeManager()
        {
            // safe hook shutdown here, put WaitNamedPipe in t6zm to unpatch code
            //I.VirtualFreeEx(ProcessHandle, allocatedPipeStub, 0, I.MEM_RELEASE);
            //allocatedPipeStub = IntPtr.Zero;
            I.CloseHandle(pipeHandle);
            pipeHandle = IntPtr.Zero;
            I.CloseHandle(ProcessHandle);
            ProcessHandle = IntPtr.Zero;
        }
    }
}
