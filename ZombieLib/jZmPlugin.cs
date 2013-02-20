using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    public interface jZmPlugin
    {
        string Name { get; }
        string Author { get; }
        string Desc { get; }
        void Init(ZombieAPI API);
    }
}
