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
        public string ExecutableDir
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }

        public string ExecutablePath
        {
            get
            {
                return Assembly.GetExecutingAssembly().Location;
            }
        }

        public jZmPlugin[] Load(ZombieAPI API)
        {
            List<jZmPlugin> jzmp = new List<jZmPlugin>();

            try
            {
                if (!Directory.Exists(ExecutableDir + "\\plugins")) Directory.CreateDirectory(ExecutableDir + "\\plugins");
                foreach (string plug in Directory.GetFiles(ExecutableDir + "\\plugins", "*.dll"))
                {
                    try
                    {
                        Assembly aplugin = Assembly.LoadFile(plug);
                        Type[] rrwe = aplugin.GetTypes();
                        foreach (Type type in rrwe)
                        {
                            
                            if (type.Name == "Plugin")
                            {
                                jZmPlugin plugin = (jZmPlugin)Activator.CreateInstance(type);
                                API.WriteLine(string.Format("- '{0}' by {1} loaded", plugin.Name, plugin.Author), false);
                                jzmp.Add(plugin);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
            return jzmp.ToArray();
        }
    }
}
