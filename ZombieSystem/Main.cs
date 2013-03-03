using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZombieSystem
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            Application.EnableVisualStyles();

            console.Text = ZombieAPI.ZombieAPI.Header;

            Program.API.OnWrite += new ZombieAPI.WriteHandler(API_OnWrite);
            Program.API.OnCrash += new ZombieAPI.OnCrashHandler(API_OnCrash);
            Program.API.OnPluginCrash += new ZombieAPI.OnPluginCrashHandler(API_OnPluginCrash);

            boxshow(box_console);
        }

        void API_OnPluginCrash(Exception exep, ZombieAPI.jZmPlugin plug)
        {
            Program.PluginExc(exep, plug);
        }

        void API_OnCrash(Exception exep)
        {
            Program.Crash(exep);
        }

        void API_OnWrite(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new ZombieAPI.WriteHandler(API_OnWrite), msg);
                return;
            }
            console.Text += msg;
        }

        void boxshow(Panel p)
        {
            foreach (Control x in Controls)
            {
                if (x.Name.StartsWith("box_"))
                    x.Hide();
            }
            p.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedIndices.Count > 0)
                switch (listView1.SelectedIndices[0])
                {
                    case 0:
                        boxshow(box_console);
                        break;
                    case 1:
                        boxshow(box_plugins);
                        plugin_init();
                        break;
                }
        }

        private void plugin_init()
        {
            listBox1.Items.Clear();
            foreach (ZombieAPI.jZmPlugin plug in Program.API.Plugins)
            {
                listBox1.Items.Add(plug.Name);
            }
            if (Program.API.Plugins.Length > 0)
                listBox1.SelectedIndex = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Hide();
            ZombieAPI.jZmPlugin plug = Program.API.Plugins[listBox1.SelectedIndex];
            plug_name.Text = plug.Name;
            plug_author.Text = "By " + plug.Author;
            plug_desc.Text = plug.Desc;
        }
    }
}
