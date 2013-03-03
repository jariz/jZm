namespace ZombieSystem
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Console", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Plugins", 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.listView1 = new System.Windows.Forms.ListView();
            this.menu = new System.Windows.Forms.ImageList(this.components);
            this.console = new System.Windows.Forms.TextBox();
            this.box_console = new System.Windows.Forms.Panel();
            this.box_plugins = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.plug_author = new System.Windows.Forms.Label();
            this.plug_desc = new System.Windows.Forms.Label();
            this.plug_name = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.box_console.SuspendLayout();
            this.box_plugins.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listView1.LargeImageList = this.menu;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(130, 452);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // menu
            // 
            this.menu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("menu.ImageStream")));
            this.menu.TransparentColor = System.Drawing.Color.Transparent;
            this.menu.Images.SetKeyName(0, "Untitled-1.png");
            this.menu.Images.SetKeyName(1, "1361334286_connect.png");
            // 
            // console
            // 
            this.console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.console.BackColor = System.Drawing.Color.Black;
            this.console.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.console.Cursor = System.Windows.Forms.Cursors.Default;
            this.console.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.console.ForeColor = System.Drawing.Color.White;
            this.console.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.console.Location = new System.Drawing.Point(6, 12);
            this.console.Margin = new System.Windows.Forms.Padding(10);
            this.console.Multiline = true;
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console.Size = new System.Drawing.Size(520, 430);
            this.console.TabIndex = 1;
            // 
            // box_console
            // 
            this.box_console.BackColor = System.Drawing.Color.Black;
            this.box_console.Controls.Add(this.console);
            this.box_console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.box_console.Location = new System.Drawing.Point(130, 0);
            this.box_console.Name = "box_console";
            this.box_console.Size = new System.Drawing.Size(536, 452);
            this.box_console.TabIndex = 2;
            // 
            // box_plugins
            // 
            this.box_plugins.Controls.Add(this.groupBox1);
            this.box_plugins.Controls.Add(this.listBox1);
            this.box_plugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.box_plugins.Location = new System.Drawing.Point(0, 0);
            this.box_plugins.Name = "box_plugins";
            this.box_plugins.Size = new System.Drawing.Size(666, 452);
            this.box_plugins.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.plug_author);
            this.groupBox1.Controls.Add(this.plug_desc);
            this.groupBox1.Controls.Add(this.plug_name);
            this.groupBox1.Location = new System.Drawing.Point(190, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 430);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plugin information";
            // 
            // plug_author
            // 
            this.plug_author.Location = new System.Drawing.Point(17, 50);
            this.plug_author.Name = "plug_author";
            this.plug_author.Size = new System.Drawing.Size(308, 18);
            this.plug_author.TabIndex = 2;
            // 
            // plug_desc
            // 
            this.plug_desc.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.plug_desc.Location = new System.Drawing.Point(17, 68);
            this.plug_desc.Name = "plug_desc";
            this.plug_desc.Size = new System.Drawing.Size(311, 343);
            this.plug_desc.TabIndex = 1;
            // 
            // plug_name
            // 
            this.plug_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plug_name.Location = new System.Drawing.Point(16, 24);
            this.plug_name.Name = "plug_name";
            this.plug_name.Size = new System.Drawing.Size(309, 27);
            this.plug_name.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 10);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(178, 433);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(317, 411);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ain\'t nobody here but us chickens!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 452);
            this.Controls.Add(this.box_console);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.box_plugins);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "jZm";
            this.box_console.ResumeLayout(false);
            this.box_console.PerformLayout();
            this.box_plugins.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Panel box_console;
        private System.Windows.Forms.ImageList menu;
        public System.Windows.Forms.TextBox console;
        private System.Windows.Forms.Panel box_plugins;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label plug_author;
        private System.Windows.Forms.Label plug_desc;
        private System.Windows.Forms.Label plug_name;
        private System.Windows.Forms.Label label1;
    }
}