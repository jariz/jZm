using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    public class Hook
    {
        public virtual void SetHook(System.Diagnostics.Process BaseProcess)
        {
        }

        public virtual void Unhook()
        {
        }

        public virtual void HookFrame(ZombieAPI API)
        {
        }
    }
}
