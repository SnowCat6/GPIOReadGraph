namespace GPIOReadGraph
{
    partial class Form1
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
			this.m_GPIO_DataTable = new System.Windows.Forms.DataGridView();
			this.refreshDataGrid = new System.Windows.Forms.Timer(this.components);
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveGPIOAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.phoneAutoConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startGPIOScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopGPIOScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuDelay = new System.Windows.Forms.ToolStripMenuItem();
			this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lOGCATLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.kMSGKernelLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.m_GPIO_DataTable)).BeginInit();
			this.mainMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_GPIO_DataTable
			// 
			this.m_GPIO_DataTable.AllowUserToAddRows = false;
			this.m_GPIO_DataTable.AllowUserToDeleteRows = false;
			this.m_GPIO_DataTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_GPIO_DataTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.m_GPIO_DataTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_GPIO_DataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.m_GPIO_DataTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.m_GPIO_DataTable.Location = new System.Drawing.Point(0, 27);
			this.m_GPIO_DataTable.Name = "m_GPIO_DataTable";
			this.m_GPIO_DataTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.m_GPIO_DataTable.ShowCellToolTips = false;
			this.m_GPIO_DataTable.ShowEditingIcon = false;
			this.m_GPIO_DataTable.Size = new System.Drawing.Size(620, 336);
			this.m_GPIO_DataTable.TabIndex = 0;
			this.m_GPIO_DataTable.VirtualMode = true;
			this.m_GPIO_DataTable.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dataGridView1_CellContextMenuStripNeeded);
			this.m_GPIO_DataTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
			this.m_GPIO_DataTable.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
			this.m_GPIO_DataTable.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
			this.m_GPIO_DataTable.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView1_CellValueNeeded);
			this.m_GPIO_DataTable.DoubleClick += new System.EventHandler(this.timer1_Tick);
			// 
			// refreshDataGrid
			// 
			this.refreshDataGrid.Enabled = true;
			this.refreshDataGrid.Interval = 50;
			this.refreshDataGrid.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.mainMenuDelay,
            this.logToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(622, 24);
			this.mainMenu.TabIndex = 1;
			this.mainMenu.Text = "mainMenu";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveGPIOAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// saveGPIOAsToolStripMenuItem
			// 
			this.saveGPIOAsToolStripMenuItem.Name = "saveGPIOAsToolStripMenuItem";
			this.saveGPIOAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveGPIOAsToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
			this.saveGPIOAsToolStripMenuItem.Text = "Save PIN state as ...";
			this.saveGPIOAsToolStripMenuItem.Click += new System.EventHandler(this.savePINstateAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(211, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyCodeToolStripMenuItem,
            this.toolStripSeparator2,
            this.phoneAutoConnectToolStripMenuItem,
            this.startGPIOScanToolStripMenuItem,
            this.stopGPIOScanToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// copyCodeToolStripMenuItem
			// 
			this.copyCodeToolStripMenuItem.Name = "copyCodeToolStripMenuItem";
			this.copyCodeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyCodeToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.copyCodeToolStripMenuItem.Text = "Copy PIN state";
			this.copyCodeToolStripMenuItem.Click += new System.EventHandler(this.copyCodeToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
			// 
			// phoneAutoConnectToolStripMenuItem
			// 
			this.phoneAutoConnectToolStripMenuItem.Name = "phoneAutoConnectToolStripMenuItem";
			this.phoneAutoConnectToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.phoneAutoConnectToolStripMenuItem.Text = "Auto connect to phone";
			this.phoneAutoConnectToolStripMenuItem.Click += new System.EventHandler(this.phoneAutoConnectToolStripMenuItem_Click);
			// 
			// startGPIOScanToolStripMenuItem
			// 
			this.startGPIOScanToolStripMenuItem.Name = "startGPIOScanToolStripMenuItem";
			this.startGPIOScanToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.startGPIOScanToolStripMenuItem.Text = "Restart GPIO scan";
			this.startGPIOScanToolStripMenuItem.Click += new System.EventHandler(this.startGPIOScanToolStripMenuItem_Click);
			// 
			// stopGPIOScanToolStripMenuItem
			// 
			this.stopGPIOScanToolStripMenuItem.Name = "stopGPIOScanToolStripMenuItem";
			this.stopGPIOScanToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.stopGPIOScanToolStripMenuItem.Text = "Stop GPIO scan";
			this.stopGPIOScanToolStripMenuItem.Click += new System.EventHandler(this.stopGPIOScanToolStripMenuItem_Click);
			// 
			// mainMenuDelay
			// 
			this.mainMenuDelay.AccessibleName = "mainMenuDelay";
			this.mainMenuDelay.Name = "mainMenuDelay";
			this.mainMenuDelay.Size = new System.Drawing.Size(57, 20);
			this.mainMenuDelay.Text = "Update";
			// 
			// logToolStripMenuItem
			// 
			this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lOGCATLogToolStripMenuItem,
            this.kMSGKernelLogToolStripMenuItem});
			this.logToolStripMenuItem.Name = "logToolStripMenuItem";
			this.logToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.logToolStripMenuItem.Text = "Log";
			// 
			// lOGCATLogToolStripMenuItem
			// 
			this.lOGCATLogToolStripMenuItem.Name = "lOGCATLogToolStripMenuItem";
			this.lOGCATLogToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.lOGCATLogToolStripMenuItem.Text = "LOGCAT user log";
			this.lOGCATLogToolStripMenuItem.Click += new System.EventHandler(this.lOGCATLogToolStripMenuItem_Click);
			// 
			// kMSGKernelLogToolStripMenuItem
			// 
			this.kMSGKernelLogToolStripMenuItem.Name = "kMSGKernelLogToolStripMenuItem";
			this.kMSGKernelLogToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.kMSGKernelLogToolStripMenuItem.Text = "KMSG kernel log";
			this.kMSGKernelLogToolStripMenuItem.Click += new System.EventHandler(this.kMSGKernelLogToolStripMenuItem_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
			this.aboutToolStripMenuItem.Text = "About";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(622, 363);
			this.Controls.Add(this.m_GPIO_DataTable);
			this.Controls.Add(this.mainMenu);
			this.MainMenuStrip = this.mainMenu;
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.m_GPIO_DataTable)).EndInit();
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView m_GPIO_DataTable;
        private System.Windows.Forms.Timer refreshDataGrid;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainMenuDelay;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyCodeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveGPIOAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem phoneAutoConnectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem startGPIOScanToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stopGPIOScanToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem lOGCATLogToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem kMSGKernelLogToolStripMenuItem;
	}
}

