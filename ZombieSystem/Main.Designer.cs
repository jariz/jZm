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
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Console", 0);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Plugins", 1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Players", 2);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.listView1 = new System.Windows.Forms.ListView();
            this.menu = new System.Windows.Forms.ImageList(this.components);
            this.console = new System.Windows.Forms.TextBox();
            this.box_console = new System.Windows.Forms.Panel();
            this.box_plugins = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.plug_author = new System.Windows.Forms.Label();
            this.plug_desc = new System.Windows.Forms.Label();
            this.plug_name = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.box_players = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.ClientNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kills = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deaths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Headshots = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Downs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Revives = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.box_console.SuspendLayout();
            this.box_plugins.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.box_players.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5,
            listViewItem6});
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
            this.menu.Images.SetKeyName(2, "1362371896_client.png");
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
            this.box_console.Location = new System.Drawing.Point(130, 0);
            this.box_console.Name = "box_console";
            this.box_console.Size = new System.Drawing.Size(536, 452);
            this.box_console.TabIndex = 2;
            // 
            // box_plugins
            // 
            this.box_plugins.Controls.Add(this.groupBox1);
            this.box_plugins.Controls.Add(this.listBox1);
            this.box_plugins.Location = new System.Drawing.Point(129, 0);
            this.box_plugins.Name = "box_plugins";
            this.box_plugins.Size = new System.Drawing.Size(545, 452);
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
            // box_players
            // 
            this.box_players.Controls.Add(this.label2);
            this.box_players.Controls.Add(this.dataGridView1);
            this.box_players.Location = new System.Drawing.Point(129, 0);
            this.box_players.Name = "box_players";
            this.box_players.Size = new System.Drawing.Size(537, 452);
            this.box_players.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClientNum,
            this.Name,
            this.Kills,
            this.Deaths,
            this.Headshots,
            this.Downs,
            this.Revives});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(537, 399);
            this.dataGridView1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(380, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "This is a small player manager allowing you to manage the players in your lobby.\r" +
    "\nRight click a user to send several commands.";
            // 
            // ClientNum
            // 
            this.ClientNum.HeaderText = "#";
            this.ClientNum.Name = "ClientNum";
            this.ClientNum.ReadOnly = true;
            this.ClientNum.Width = 20;
            // 
            // Name
            // 
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            this.Name.Width = 199;
            // 
            // Kills
            // 
            this.Kills.HeaderText = "Kills";
            this.Kills.Name = "Kills";
            this.Kills.Width = 55;
            // 
            // Deaths
            // 
            this.Deaths.HeaderText = "Deaths";
            this.Deaths.Name = "Deaths";
            this.Deaths.Width = 55;
            // 
            // Headshots
            // 
            this.Headshots.HeaderText = "Headshots";
            this.Headshots.Name = "Headshots";
            this.Headshots.Width = 55;
            // 
            // Downs
            // 
            this.Downs.HeaderText = "Downs";
            this.Downs.Name = "Downs";
            this.Downs.Width = 55;
            // 
            // Revives
            // 
            this.Revives.HeaderText = "Revives";
            this.Revives.Name = "Revives";
            this.Revives.Width = 55;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 452);
            this.Controls.Add(this.box_players);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.box_plugins);
            this.Controls.Add(this.box_console);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            //this.Name = "Wut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "jZm";
            this.box_console.ResumeLayout(false);
            this.box_console.PerformLayout();
            this.box_plugins.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.box_players.ResumeLayout(false);
            this.box_players.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.Panel box_players;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kills;
        private System.Windows.Forms.DataGridViewTextBoxColumn Deaths;
        private System.Windows.Forms.DataGridViewTextBoxColumn Headshots;
        private System.Windows.Forms.DataGridViewTextBoxColumn Downs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Revives;
    }
}