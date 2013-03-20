﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    internal class Stubs
    {
        // Pipe stub used for all hooks (you need a new pipe stub for every hook)
        // t6zm to jZm (client to server) is used to handle data recieving (events, etc...)
        // An event handler in jZm will wait forever for the pipe to be aviable (this means that
        // an event has been executed in t6zm)
        // This stub is synchronous, meaning that t6zm wont resume execution until jZm has completed the processing
        public static byte[] SyncPipeStubForHooks = new byte[]
        {

        };

        public static byte[] WrapperTocBuf_AddText = new byte[]
        {
	        0x55, // push ebp
	        0x8B, 0xEC, // mov ebp, esp
	        0x83, 0xEC, 0x08, // sub esp, 8
	        0xC7, 0x45, 0xF8, 0x00, 0x00, 0x00, 0x00, // mov dword ptr [ebp-8], 00000000 -> char* command
	        0xC7, 0x45, 0xFC, 0x00, 0x00, 0x00, 0x00, // mov dword ptr [ebp-4], 00000000 -> call address (cBuf_AddText)
	        0xFF, 0x75, 0xF8, // push [ebp-8]
	        0x6A, 0x00, // push 0 -> client number
	        0xFF, 0x55, 0xFC, // call [ebp-4]
	        0x83, 0xC4, 0x08, // add esp, 8
	        0x8B, 0xE5, // mov esp, ebp
	        0x5D, // pop ebp
	        0xC3 // retn
        };

        public static byte[] WrapperToSV_GameSendServerCommand = new byte[]
        {
	        0x55, // push ebp
	        0x8B, 0xEC, // mov ebp, esp
	        0x83, 0xEC, 0x08, // sub esp, 8
	        0xC7, 0x45, 0xF8, 0x00, 0x00, 0x00, 0x00, // mov dword ptr [ebp-8], 00000000 -> char* command
	        0xC7, 0x45, 0xFC, 0x00, 0x00, 0x00, 0x00, // mov dword ptr [ebp-4], 00000000 -> call address (SV_GameSendServerCommand)
	        0xFF, 0x75, 0xF8, // push [ebp-8]
	        0x6A, 0x00, // push 0 -> svscmd type
            0x6A, 0x00, // push 0 -> Client Number
	        0xFF, 0x55, 0xFC, // call [ebp-4]
	        0x83, 0xC4, 0x08, // add esp, 8
	        0x8B, 0xE5, // mov esp, ebp
	        0x5D, // pop ebp
	        0xC3 // retn
        };

        public static byte[] G_Say_Stub = new byte[]
        {
           0x8B, 0xB4, 0x24, 0xF4, 0x00, 0x00, 0x00,//MOV ESI,DWORD PTR SS:[ESP+F4]
           0x89, 0x34, 0x25, 0x00, 0x00, 0x00, 0x00, // MOV [00000000],ESI
           0x89, 0x3C, 0x25, 0x00, 0x00, 0x00, 0x00, // MOV [00000000],EDI
           0xE9, 0x00, 0x00, 0x00, 0x00 //jmp back
        };
    }
}
