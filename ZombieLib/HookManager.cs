using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZombieAPI.Hooks;

namespace ZombieAPI
{
    internal class HookManager
    {
        static List<Hook> Hooks = new List<Hook>();
        static ZombieAPI API;
        public static void Init(System.Diagnostics.Process BaseProcess, ZombieAPI You)
        {
            API = You;
            Hooks.Add(new ChatHook());

            foreach (Hook hook in Hooks)
            {
                hook.SetHook(BaseProcess);
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
