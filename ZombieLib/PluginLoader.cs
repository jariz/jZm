using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace ZombieAPI
{
    class PluginLoader
    {
        string ExecutablePath
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        public jZmPlugin[] Load()
        {
            // 'fix' current dir

            List<jZmPlugin> jzmp = new List<jZmPlugin>();

            try
            {
                if (!Directory.Exists(ExecutablePath + "\\plugins")) Directory.CreateDirectory(ExecutablePath + "\\plugins");
                foreach (string plug in Directory.GetFiles(ExecutablePath + "\\plugins", "*.dll"))
                {
                    Assembly aplugin = Assembly.LoadFile(plug);
                    foreach (Type type in aplugin.GetTypes())
                    {
                        jZmPlugin plugin = (jZmPlugin)Activator.CreateInstance(type);
                        jzmp.Add(plugin);
                    }
                }

                Environment.CurrentDirectory = ExecutablePath;
            }
            catch
            {
            }
            return jzmp.ToArray();
        }
    }
}
