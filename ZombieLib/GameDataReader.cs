using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieAPI
{
    class GameDataReader
    {
        //A very .NET approach on doing this :D
        public static string[] Read(RemoteMemory Mem)
        {
            Mem.Position = Addresses.GameData;
            StringBuilder builder = new StringBuilder();
            List<string> final = new List<string>();
            while (true)
            {
                string buffer = Mem.ReadString(256);
                if (buffer.Count(c => c == '\0') != 256)
                {
                    builder.Append(buffer);
                }
                else break;
            }

            foreach (string data in builder.ToString().Split('\0'))
            {
                if (data != string.Empty)
                    final.Add(data);
            }

            return final.ToArray();
        }

        public static Dictionary<int, string> GetWeapons(string[] GameData)
        {
            Dictionary<int, string> w_eapons = new Dictionary<int, string>();
            int o;
            foreach (string wep_on in GameData.Where(wep => wep.Length > 3 && Int32.TryParse(wep.Substring(0, 2), out o) && (wep.Contains("_zm") || wep.Contains("_mp") || wep.Contains("zombie_"))).ToArray())
            {
                string[] wep = wep_on.Split(' ');
                w_eapons.Add(Convert.ToInt32(wep[0]), wep[1]);
            }
            w_eapons.Add(0, "");
            return w_eapons;
        }
    }
}
