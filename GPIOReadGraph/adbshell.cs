using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GPIOReadGraph
{
    class CShellConsolde
    {
        public event System.EventHandler onOutputChange;
        public bool bShowlog = false;

        Process cmdProcess = null;
		int processRunning = 0;

        public void RunCommand(string[] args, string shellCmd = "cmd.exe", string cmdArguments = "")
        {
			processRunning += 1;

			if (cmdProcess == null)
            {
                ProcessStartInfo cmdStartInfo = new ProcessStartInfo();
                cmdStartInfo.FileName = shellCmd;
                cmdStartInfo.Arguments = cmdArguments;
                cmdStartInfo.RedirectStandardOutput = true;
                cmdStartInfo.RedirectStandardError = true;
                cmdStartInfo.RedirectStandardInput = true;
                cmdStartInfo.UseShellExecute = false;
                cmdStartInfo.CreateNoWindow = true;

                cmdProcess = new Process();
                cmdProcess.StartInfo = cmdStartInfo;
                cmdProcess.ErrorDataReceived += cmd_Error;
                cmdProcess.OutputDataReceived += cmd_DataReceived;
                cmdProcess.EnableRaisingEvents = true;
				cmdProcess.Exited += cmd_Exited;
                cmdProcess.Start();
                cmdProcess.BeginOutputReadLine();
                cmdProcess.BeginErrorReadLine();
            }

            lock (cmdProcess)
            {
                foreach (string cmd in args)
                {
                    if (bShowlog) Console.WriteLine(cmd);
                    cmdProcess.StandardInput.WriteLine(cmd);     //Execute ping bing.com
                }
            }
			processRunning -= 1;
        }
		public bool isRunning()
		{
			if (cmdProcess == null) return false;
			if (processRunning > 0) return true;

			try
			{
				Process.GetProcessById(cmdProcess.Id);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}
        public void WaitForExit()
        {
            if (cmdProcess != null) cmdProcess.WaitForExit();
        }
        public void Close()
        {
  //          onOutputChange = null;
            if (cmdProcess == null)
                return;

            try
            {
                cmdProcess.Kill();
            }
            catch (Exception) { }

            cmdProcess.Close();
            cmdProcess = null;
        }

        void cmd_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (bShowlog) Console.WriteLine(e.Data);
//            Console.WriteLine("Output from other process");
            if (onOutputChange != null)
            {
                onOutputChange(this, e);
            }
        }

        void cmd_Error(object sender, DataReceivedEventArgs e)
        {
  //          Console.WriteLine("Error from other process");
            if (bShowlog) Console.WriteLine(e.Data);
        }
		void cmd_Exited(object sender, EventArgs e)
		{

		}
    }
}
