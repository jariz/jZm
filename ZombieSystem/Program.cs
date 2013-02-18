using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using JariZ;
using JariZ.GameObjects;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;

namespace ZombieSystem
{
    class Program
    {

        public static void var_dump(object obj)
        {
            Console.WriteLine("{0,-18} {1}", "Name", "Value");
            string ln = @"-------------------------------------   
               ----------------------------";
            Console.WriteLine(ln);

            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                try
                {
                    Console.WriteLine("{0,-18} {1}",
                          props[i].Name, props[i].GetValue(obj, null));
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);   
                }
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WindowWidth = Console.LargestWindowWidth - 35;
            Console.WindowHeight = Console.LargestWindowHeight - 40;

            ZombieAPI z = new ZombieAPI(Process.GetProcessesByName("t6zm")[0]);

            List<Player> p = z.GetPlayers();
            
            Application.Run();
        }


    }
}
