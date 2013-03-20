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

namespace ZombieAPI
{
    class IPC
    {
        private IntPtr ProcessHandle = IntPtr.Zero;
        private IntPtr pipeHandle = IntPtr.Zero;
        private IntPtr allocatedPipeStub = IntPtr.Zero;
        public const uint bufferSize = 256;
        public byte[] inboundBuffer = new byte[bufferSize];

        public void InitJZMPipe(IntPtr ProcessHandleLocal)
        {
            if (ProcessHandle == IntPtr.Zero || pipeHandle == IntPtr.Zero || allocatedPipeStub == IntPtr.Zero)
            {
                if (ProcessHandle == IntPtr.Zero)
                    ProcessHandle = ProcessHandleLocal;
                if (pipeHandle == IntPtr.Zero)
                    pipeHandle = I.CreateNamedPipe("\\\\.\\pipe\\jZm", 1, 0, 1, 0, bufferSize, 0, IntPtr.Zero);
                if (allocatedPipeStub == IntPtr.Zero)
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
                }
            }
            if (ProcessHandle == IntPtr.Zero || pipeHandle == (IntPtr)I.INVALID_HANDLE_VALUE || allocatedPipeStub == IntPtr.Zero)
                throw new Exception("Failed to init jZm communication pipe");
        }

        public void ShutdownJZMPipe()
        {
            // safe hook shutdown here, put WaitNamedPipe in t6zm to unpatch code
            I.VirtualFreeEx(ProcessHandle, allocatedPipeStub, 0, I.MEM_RELEASE);
            allocatedPipeStub = IntPtr.Zero;
            I.CloseHandle(pipeHandle);
            pipeHandle = IntPtr.Zero;
            I.CloseHandle(ProcessHandle);
            ProcessHandle = IntPtr.Zero;
        }
    }
}
