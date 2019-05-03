using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;

namespace GPIOReadGraph
{
    public partial class Form1 : Form
    {
        //  "  1:00001010"
        Regex cmdparse = new Regex("(\\d+):([\\d-]+)");
        //  "PIN: [MODE] [PULL_SEL] [DIN] [DOUT] [PULL EN] [DIR] [IES] [SMT]"
        Regex cmdparsePins = new Regex("^PIN: (.*)");

        string shellCmd     = "cat /sys/class/misc/mtgpio/pin ; echo nextLoop";
        string sellCmdSetPin = "echo \"-w={0}: {1}\" > /sys/class/misc/mtgpio/pin"; // 0 - pin num, 1 - pin set "0 0 1 1 1 1 1"

        Color[] m_ShowColors = { Color.LightYellow, Color.Yellow, Color.LightGreen, Color.Green, Color.LightBlue, Color.Blue };
		//	Режимы PIN
        int[] m_PinModes = { 0, 1, 2, 3, 4, 5, 6 };
		//	Задержки в обновлении состояния пинов
		int[] m_QueryDelays = { 0, 50, 100, 150, 200 };
		int m_QueryDelay = 100;

        CShellConsolde m_Command = new CShellConsolde();
        Dictionary<string, gpioPin> m_GPIO_values = new Dictionary<string, gpioPin>();
        Dictionary<string, Color> m_GPIO_color = new Dictionary<string, Color>();

        bool m_bUpdateTableAllow = false;
        string currentCommandResponce = "";
        int currentResponceCounter = 0;

        public Form1()
        {
            InitializeComponent();
            m_Command.onOutputChange += new EventHandler(onConsoleChange);
            refreshDataGrid.Interval = 50;

            m_GPIO_color["0110101"] = Color.Yellow;
            m_GPIO_color["0011011"] = Color.Gray;
			//           Dictionary<string, Color> m_GPIO_color = Properties.Settings.Default["GPIO_color"];

			foreach(int val in m_QueryDelays)
			{
				string menuItemName;
				if (val > 0)
				{
					menuItemName = String.Format("{0} ms.", val);
				}
				else
				{
					menuItemName = "No delay";
				}
				ToolStripMenuItem item = (ToolStripMenuItem)mainMenuDelay.DropDownItems.Add(menuItemName);
				item.Tag = val;
				item.Click += new EventHandler(cellMenu_QueryItemClicked);
				item.Checked = val == m_QueryDelay;
			}


			m_GPIO_DataTable.VirtualMode = true;
            m_GPIO_DataTable.AllowUserToDeleteRows = false;

			phoneAutoConnectToolStripMenuItem.Checked = true;

			makeGridTable();

			runQuery();
        }
        private string getAdbPath()
        {
            String path = Application.StartupPath;
            String adbPath = path + "/adb.exe";
            if (!System.IO.File.Exists(adbPath))
            {
                adbPath = "adb.exe";
            }
            else
            {
                //               m_Command.RunCommand(new string[] { String.Format("cd \"{0}\"", path) });
            }
            return adbPath;
        }
        private void makeGridTable()
        {
            m_GPIO_DataTable.Columns.Clear();

            m_GPIO_DataTable.Columns.Add("GPIO", "PIN");
            m_GPIO_DataTable.Columns.Add("Value", "CONFIG");
            foreach (string cName in gpioPin.g_pins)
            {
                m_GPIO_DataTable.Columns.Add(cName, cName);
            }

        }

        private ContextMenuStrip makePopupMenu(string pinName)
        {
            ContextMenuStrip m_CellMenu = new ContextMenuStrip();
            ToolStripItem i;

            foreach (Color clr in m_ShowColors)
            {
                i = m_CellMenu.Items.Add(clr.Name);
                i.Tag = clr;
                i.Click += new EventHandler(cellMenu_ItemClicked);
            }

            switch (pinName)
            {
                case "MODE":
                    m_CellMenu.Items.Add("-");
                    foreach (int nMode in m_PinModes)
                    {
                        i = m_CellMenu.Items.Add(String.Format("MODE{0}", nMode));
                        i.Tag = new String[] { pinName, nMode.ToString() };
                        i.Click += new EventHandler(cellMenu_ItemModeClicked);
                    }
                    break;
                case "PULL_SEL":
                case "PULL EN":
                case "DOUT":
                    m_CellMenu.Items.Add("-");
                    i = m_CellMenu.Items.Add(pinName);
                    i.Tag = new String[] { pinName, "1" };
                    i.Click += new EventHandler(cellMenu_ItemModeClicked);

                    i = m_CellMenu.Items.Add(pinName + " NONE");
                    i.Tag = new String[] { pinName, "0" };
                    i.Click += new EventHandler(cellMenu_ItemModeClicked);
                    break;
                case "DIR":
                    m_CellMenu.Items.Add("-");
                    i = m_CellMenu.Items.Add("DEF IN");
                    i.Tag = new String[] { pinName, "0" };
                    i.Click += new EventHandler(cellMenu_ItemModeClicked);

                    i = m_CellMenu.Items.Add("DEF OUT");
                    i.Tag = new String[] { pinName, "1" };
                    i.Click += new EventHandler(cellMenu_ItemModeClicked);
                    break;
            }

            return m_CellMenu;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
			if (phoneAutoConnectToolStripMenuItem.Checked)
			{
				if (m_Command.isRunning() == false) runQuery();
			}

            //            if (currentCommandResponce.Trim().Length > 0)
            this.Text = String.Format("Update: {0} {1}", currentResponceCounter, currentCommandResponce);
            if (m_bUpdateTableAllow) updateData(true);
        }

        public void updateData(bool bUpdateDelay = false)
        {
            Dictionary<string, gpioPin> m;
            lock (m_GPIO_values)
            {
                m = new Dictionary<string, gpioPin>(m_GPIO_values);
            }

            if (m_GPIO_DataTable.Rows.Count < m.Count)
            {
                m_GPIO_DataTable.Rows.Add(m.Count - m_GPIO_DataTable.Rows.Count);
            }

            int rowIndex = 0;
            foreach (KeyValuePair<String, gpioPin> i in m)
            {
                gpioPin pin = i.Value;
                if (bUpdateDelay && pin.isChanged())
                {
                    lock (m_GPIO_values)
                    {
                        pin = m_GPIO_values[i.Key];
                        pin.tickChange();
                        m_GPIO_values[i.Key] = pin;
                    }
                    m_GPIO_DataTable.InvalidateRow(rowIndex);
                }
                ++rowIndex;
            }
        }
        /* table */

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int cellIndex = e.ColumnIndex;
            if (rowIndex < 0 || cellIndex < 0) return;

            gpioPin pin = getRowPin(rowIndex);

            if (pin.rowDisable)
            {
                e.CellStyle.BackColor = Color.LightGray;
                return;
            }

            String cellValue = pin.Value;
            Color bkColor = Color.White;
            Color textColor = Color.Black;

            DataGridView view = (DataGridView)sender;
            String cName = view.Columns[cellIndex].Name;
 //           DataGridViewCell cell = view.Rows[rowIndex].Cells[cellIndex];

            switch (cName)
            {
                case "GPIO":
                    try
                    {
                        bkColor = m_GPIO_color[pin.Value];
                    }
                    catch (Exception) { }
                    break;
                case "Value":
                    if (pin.isChanged())
                    {
                        bkColor = Color.Red;
                        textColor = Color.White;
                    }
                    break;
                default:
                    if (pin.Value.Length != gpioPin.g_pins.Length)
                    {
                        if (pin.isChanged())
                        {
                            bkColor = Color.Red;
                            textColor = Color.White;
                        }
                        break;
                    }
                    int nPin = pin.getPinIndex(cName);
                    if (nPin >= 0 && pin.IsChangedPin(nPin))
                    {
                        bkColor = Color.Red;
                        textColor = Color.White;
                    }
                    break;
            }

            e.CellStyle.BackColor = bkColor;
            e.CellStyle.ForeColor = textColor;
        }
        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int cellIndex = e.ColumnIndex;
            DataGridView view = (DataGridView)sender;

            if (rowIndex >= m_GPIO_values.Count)
            {
                e.Value = String.Empty;
                return;
            }

            gpioPin pin = getRowPin(rowIndex);

            String cName = view.Columns[cellIndex].Name;
            DataGridViewCell cell = view.Rows[rowIndex].Cells[cellIndex];

            String cellValue = pin.Value;
            switch (cName)
            {
                case "GPIO":
                    cellValue = pin.pinID;
                    break;
                case "Value":
                    cellValue = pin.Value;
                    break;
                default:
                    cellValue = pin.pinToString(pin.getPinIndex(cName));
                    break;
            }
            if (e.Value == null ||  e.Value.ToString() != cellValue)
            {
                e.Value = cellValue;
            }
        }

         private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0) return;
            DataGridView view = (DataGridView)sender;

            gpioPin pin = getRowPin(rowIndex);
            pin.rowDisable = pin.rowDisable == false;
            setRowPin(rowIndex, pin);
            view.InvalidateRow(rowIndex);
        }

        public gpioPin getRowPin(int rowIndex)
        {
            String pinKey;
            gpioPin pin;

            lock (m_GPIO_values)
            {
                pinKey = m_GPIO_values.Keys.ElementAt(rowIndex);
                pin = m_GPIO_values[pinKey];
            }
            return pin;
        }
        public void setRowPin(int rowIndex, gpioPin pin)
        {
            String pinKey;

            lock (m_GPIO_values)
            {
                pinKey = m_GPIO_values.Keys.ElementAt(rowIndex);
                m_GPIO_values[pinKey] = pin;
            }
        }

        /// <summary>
        /// onConsoleChange
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onConsoleChange(object sender, EventArgs e)
        {
            // this code will run on main (UI) thread 
            DataReceivedEventArgs ev = (DataReceivedEventArgs)e;
            if (ev.Data == null) return;

            Match m;
            
            m = cmdparsePins.Match(ev.Data);
            if (m.Groups.Count > 1)
            {
                string columns = m.Groups[1].Value;
                Regex r = new Regex("\\[([^\\]]+)\\]");
                MatchCollection coll = r.Matches(columns);
                if (coll.Count == gpioPin.g_pins.Length) return;

                List<string> colNames = new List<string>();
                foreach (Match m2 in coll)
                {
                    colNames.Add(m2.Groups[1].Value);
                }
                gpioPin.g_pins = colNames.ToArray<string>();
                Invoke((MethodInvoker)delegate { makeGridTable(); });
            }

            if (ev.Data == "nextLoop")
            {
                m_Command.RunCommand( new string[] { shellCmd });

                m_bUpdateTableAllow = true;
                currentResponceCounter++;

//                Invoke((MethodInvoker)delegate { updateData(false); });
				if (m_QueryDelay > 0)
				{
					System.Threading.Thread.Sleep(m_QueryDelay);
				}
                return;
            }
            if (ev.Data.Length > 0) currentCommandResponce = ev.Data;

            m = cmdparse.Match(ev.Data);
            if (m.Groups.Count != 3) return;

            String pinID = m.Groups[1].Value;
            String pinCFG = m.Groups[2].Value;

            lock (m_GPIO_values)
            {
                gpioPin pin;
                try
                {
                    pin = m_GPIO_values[pinID];
                    pin.setValue(pinCFG);
                 }
                catch (Exception)
                {
                    pin = new gpioPin(pinCFG, pinID);
                }
                m_GPIO_values[pinID] = pin;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.Button != System.Windows.Forms.MouseButtons.Right)
                return;

            DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];

            Rectangle r = c.DataGridView.GetCellDisplayRectangle(c.ColumnIndex, c.RowIndex, false);
            Point p = new Point(r.X + r.Width, r.Y + r.Height);
            c.ContextMenuStrip.Show(c.DataGridView, p);
        }

        private void dataGridView1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DataGridView view = sender as DataGridView;
            DataGridViewCell c = view[e.ColumnIndex, e.RowIndex];
            c.DataGridView.CurrentCell = c;
            c.Selected = true;

            String cName = view.Columns[e.ColumnIndex].Name;
            e.ContextMenuStrip = makePopupMenu(cName);
            e.ContextMenuStrip.Tag = getRowPin(e.RowIndex).Value;
        }
        void cellMenu_ItemModeClicked(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            ContextMenuStrip menu = item.GetCurrentParent() as ContextMenuStrip;

            int nRowIndex = m_GPIO_DataTable.CurrentRow.Index;
            gpioPin pin = getRowPin(nRowIndex);
            String[] mode = (string[])item.Tag;

            setPinValue(pin, pin.getPinIndex(mode[0]), int.Parse(mode[1]));
        }
        void cellMenu_ItemClicked(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            ContextMenuStrip menu = item.GetCurrentParent() as ContextMenuStrip;

            // your code here
            m_GPIO_color[(string)menu.Tag] = (Color)item.Tag;
            m_GPIO_DataTable.Invalidate();
        }
        void setPinValue(gpioPin pin, int nPin, int nValue)
        {
            StringBuilder value = new StringBuilder(pin.Value);

            if (nPin > 2) --nPin;
            value.Remove(2, 1);

            value[nPin] = nValue.ToString()[0];

            int nLen = value.Length;
            for (int ix = 0; ix < nLen-1; ++ix) {
                value.Insert(ix*2+1, ' ');
            }

            String cmd = String.Format(sellCmdSetPin, pin.pinID, value);

            if (false)
            {
                CShellConsolde shell = new CShellConsolde();
                shell.bShowlog = true;

                string[] cmds = new string[] { cmd, "exit" };
                shell.RunCommand(cmds, getAdbPath(), "shell su");
 //               shell.WaitForExit();
                shell.Close();
            }
            else
            {
                m_Command.RunCommand( new string [] { cmd } );
            }
        }

		private void cellMenu_QueryItemClicked(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			m_QueryDelay = int.Parse(item.Tag.ToString());

			ToolStripItemCollection items = item.GetCurrentParent().Items;
			foreach (ToolStripItem i in items)
			{
				ToolStripMenuItem i2 = i as ToolStripMenuItem;
				i2.Checked = m_QueryDelay == int.Parse(i.Tag.ToString());
			};
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}


		private void runQuery()
		{
			if (m_Command.isRunning()) return;
			m_Command.Close();

			string[] cmd = {
				"su",
				shellCmd
			};
			m_Command.RunCommand(cmd, getAdbPath(), "shell");
		}
		private void stopQuery()
		{
			m_Command.RunCommand(new string[] { "exit", "exit" });
			m_Command.Close();
		}
		public String makeGPIOstring()
		{
			Dictionary<string, gpioPin> pins;
			lock (m_GPIO_values)
			{
				pins = new Dictionary<string, gpioPin>(m_GPIO_values);
			}

			String clipState = String.Empty;

			clipState += "PIN ID\t";
			clipState += String.Join("\t", gpioPin.g_pins) + "\r\n";

			foreach (KeyValuePair<string, gpioPin> key in pins)
			{
				String value = String.Empty;
				gpioPin pin = key.Value;

				if (pin.rowDisable)
				{
					value = "\tignore";
				}
				else
				{
					for (int ix = 0; ix < gpioPin.g_pins.Length; ++ix)
					{
						string v = pin.pinToString(ix);
						value += "\t";
						if (ix > 0 || v.Length > 0)
						{
							value += v.Length > 0 ? v : "-";
						}
						else
						{
							value += pin.Value;
						}
					}
				}
				clipState += String.Format("GPIO{0}{1}\r\n", key.Key, value);
			}
			return clipState;
		}
		private void copyCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(makeGPIOstring());
		}

		private async void savePINstateAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			String pinState = makeGPIOstring();

			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Text file|*.txt";
			dlg.Title = "Save GPIO PIN state as file";
			dlg.ShowDialog();

			if (dlg.FileName == "") return;

			using (StreamWriter outfile = new StreamWriter(dlg.FileName, false))
			{
				await outfile.WriteAsync(pinState);
			}
		}

		private void phoneAutoConnectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			phoneAutoConnectToolStripMenuItem.Checked = phoneAutoConnectToolStripMenuItem.Checked != true;
		}

		private void startGPIOScanToolStripMenuItem_Click(object sender, EventArgs e)
		{
			stopQuery();
			runQuery();
		}

		private void stopGPIOScanToolStripMenuItem_Click(object sender, EventArgs e)
		{
			phoneAutoConnectToolStripMenuItem.Checked = false;
			stopQuery();
		}

		private void lOGCATLogToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void kMSGKernelLogToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.Location = Properties.Settings.Default.FormPosition;
			this.Size = Properties.Settings.Default.FormSize;
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_Command.onOutputChange -= onConsoleChange;
			stopQuery();
			m_Command.Close();

			Properties.Settings.Default.FormPosition = this.Location;
			Properties.Settings.Default.FormSize = this.Size;

			Properties.Settings.Default.Save();
		}
	}
}