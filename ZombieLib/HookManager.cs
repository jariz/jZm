using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZombieAPI.Hooks;

// Info for OpenProcess:
/*
 * PROCESS_ALL_ACCESS doesnt work on XP if you compile this on Windows Server 2008 or higher,
 * so we use a combination of PROCESS_VM_OPERATION, PROCESS_VM_READ and PROCESS_VM_WRITE
*/

// Info for (mostly) all winapi functions that return a handle
/*
 * We need to call CloseHandle to avoid any memory leaks
*/

// Info for Hook.HookFrame and Hook.Unhook:
/*
 * Dont call HookFrame or Unhook before calling Hook.Hook!
*/

namespace ZombieAPI
{
    internal class HookManager
    {
        static List<Hook> Hooks = new List<Hook>();
        static ZombieAPI API;
        public static void Init(IntPtr ProcessHandle, ZombieAPI You)
        {
            // API unused?
            API = You;
            Hooks.Add(new ChatHook());

            foreach (Hook hook in Hooks)
            {
                hook.SetHook(ProcessHandle);
            }
        }

        public static void Destroy()
        {
            foreach (Hook hook in Hooks)
            {
                hook.Unhook();
            }
        }

        public static void Frame()
        {
            foreach (Hook hook in Hooks)
            {
                hook.HookFrame(API);
            }
        }
    }
}
