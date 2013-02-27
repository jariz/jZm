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
    }
}
