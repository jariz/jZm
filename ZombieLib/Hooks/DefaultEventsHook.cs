using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI.Hooks
{
    class DefaultEventsHook : Hook
    {
        private IntPtr ProcessHandle;

        public override void SetHook(IntPtr ProcessHandleLocal)
        {
            ProcessHandle = ProcessHandleLocal;
        }

        public override void HookFrame(ZombieAPI API)
        {

        }

        public override void Unhook()
        {

        }
    }
}
