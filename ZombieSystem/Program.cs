using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ZombieAPI;
using ZombieAPI.GameObjects;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Dialogs;

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
                catch
                {
                }
            }
            Console.WriteLine();
        }

        public static Main MainForm;
        public static ZombieAPI.ZombieAPI API;

        static void Main(string[] args)
        {
            string gameName;
            if (args.Length > 0)
                gameName = args[0];
            else gameName = "t6zm";

            Process[] games = Process.GetProcessesByName(gameName);
            Process gameProcess = null;
            if (games.Length > 1)
            {
                TaskDialog diag = new TaskDialog();
                diag.Caption = "Woops!";
                diag.InstructionText = "I found more than 2 running games!";
                diag.Icon = TaskDialogStandardIcon.Warning;
                diag.Text = "jZm has found more than just one game.\r\nWhat would you like to do?";
                foreach (Process game in games)
                {
                    TaskDialogCommandLink link = new TaskDialogCommandLink(game.ProcessName, game.ProcessName, "PID " + game.Id);
                    link.Click += delegate(object sender, EventArgs argz)
                    {
                        gameProcess = game;
                        diag.Close();
                    };
                    diag.Controls.Add(link);
                }
                TaskDialogCommandLink linkz = new TaskDialogCommandLink("r", "Restart", "Restart jZm");
                linkz.ShowElevationIcon = true;
                linkz.Click += delegate(object sender, EventArgs argz)
                {
                    diag.Close();
                    Application.Restart();
                    //mare sure we're dead
                    Environment.Exit(-1);
                };
                diag.Controls.Add(linkz);
                linkz = new TaskDialogCommandLink("r", "Exit", "Exit jZm");
                linkz.ShowElevationIcon = true;
                linkz.Click += delegate(object sender, EventArgs argz)
                {
                    diag.Close();
                    Environment.Exit(-1);
                };
                diag.Controls.Add(linkz);

                diag.Show();
            }
            else if (games.Length == 0)
            {
                TaskDialog diag = new TaskDialog();
                diag.Caption = "Woops!";
                diag.InstructionText = "I was unable to find any games";
                diag.Icon = TaskDialogStandardIcon.Error;
                diag.Text = "jZm was unable to find any processes matching the name '" + gameName + "'.\r\nWhat would you like to do?";
                TaskDialogCommandLink linkz = new TaskDialogCommandLink("r", "Restart jZm", "Restart and look for the game again");
                linkz.ShowElevationIcon = true;
                linkz.Click += delegate(object sender, EventArgs argz)
                {
                    diag.Close();
                    Application.Restart();
                    //mare sure we're dead
                    Environment.Exit(-1);
                };
                diag.Controls.Add(linkz);
                linkz = new TaskDialogCommandLink("r", "Exit jZm");
                linkz.Click += delegate(object sender, EventArgs argz)
                {
                    diag.Close();
                    Environment.Exit(-1);
                };
                diag.Controls.Add(linkz);
                diag.Show();
            }
            else gameProcess = games[0];

            API = new ZombieAPI.ZombieAPI();

            MainForm = new Main();
            MainForm.Show();

            try
            {
                API.Init(gameProcess);
            }
            catch (Exception z)
            {
                Crash(z);
            }

            Application.Run();
        }

        public static void Crash(Exception z)
        {
            TaskDialog diag = new TaskDialog();
            diag.InstructionText = "An unhandled exception was caught";
            diag.Text = "jZm has crashed because of a unhandled exception, this means something happend that shouldn't happen.";
            diag.Caption = "WTF?";
            diag.Icon = TaskDialogStandardIcon.Error;
            diag.DetailsExpandedText = z.ToString();
            TaskDialogCommandLink linkz = new TaskDialogCommandLink("r", "Restart jZm");
            linkz.ShowElevationIcon = true;
            linkz.Click += delegate(object sender, EventArgs argz)
            {
                diag.Close();
                Application.Restart();
            };
            diag.Controls.Add(linkz);
            linkz = new TaskDialogCommandLink("r", "Exit jZm");
            linkz.Click += delegate(object sender, EventArgs argz)
            {
                diag.Close();
                Environment.Exit(-1);
            };
            diag.Controls.Add(linkz);
            diag.Show();
            Environment.Exit(-1);
        }


    }
}
