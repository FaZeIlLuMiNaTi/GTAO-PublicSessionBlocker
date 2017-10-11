using GTAO_PublicSessionBlocker.Properties;
using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace GTAO_PublicSessionBlocker
{
    public partial class GTAOPSBMain : Form
    {
        string targetprocess = "GTA5";
        bool processSuspended = false;
        bool usingTimermode;
        bool blockingPort;
        bool keyBound = false;
        int realKey;

        public GTAOPSBMain()
        {
            InitializeComponent();
        }

        enum Keys // Key codes
        {
            F1 = 0x70,
            F2 = 0x71,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 0x78,
            F10 = 0x79,
            F11 = 0x7A,
            F12 = 0x7B,
        }

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                if (processSuspended) // If the process has been suspended, resume the process
                {
                    Resume();
                }
                else
                {
                    if (usingTimermode) // If the user has timermode enabled, suspend, wait 10 seconds, resume
                    {
                        Suspend();
                        System.Threading.Thread.Sleep(10000); // 10 seconds (miliseconds)
                        Resume();
                    }
                    else
                    {
                        Suspend();
                    }
                }
            }
            base.WndProc(ref m);
        }

        public void Bind()
        {
            keyBound = true;
            RegisterHotKey(Handle, GetType().GetHashCode(), 0x0000, realKey); // Bind to the selected key
            BtnBind.Text = "Unbind from " + CmbKey.Text;
            CmbKey.Enabled = false;
        }

        public void Unbind()
        {
            keyBound = false;
            BtnBind.Text = "Bind to " + CmbKey.Text;
            UnregisterHotKey(Handle, GetType().GetHashCode());
            CmbKey.Enabled = true;
        }

        public void Suspend()
        {
            var processlist = Process.GetProcessesByName(targetprocess);
            foreach (var process in processlist)
            {
                process.Suspend();
            }
            processSuspended = true;
            BtnResume.Enabled = true;
            BtnSuspend.Enabled = false;
        }

        public void Resume()
        {
            var processlist = Process.GetProcessesByName(targetprocess);
            foreach (var process in processlist)
            {
                process.Resume();
            }
            processSuspended = false;
            BtnResume.Enabled = false;
            BtnSuspend.Enabled = true;
        }

        public void FireWallAdd()
        {
            string arguments = "advfirewall firewall add rule name=\"GTAO-PublicSessionBlocker\" dir=out action=block description=\"Block GTAO public session port\" profile=any localport=6672 remoteport=any protocol=UDP";
            ProcessStartInfo procStartInfo = new ProcessStartInfo("netsh", arguments);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            Process.Start(procStartInfo);
        }

        public void FireWallEnable()
        {
            string arguments = "advfirewall firewall set rule name=\"GTAO-PublicSessionBlocker\" new enable=yes";

            ProcessStartInfo procStartInfo = new ProcessStartInfo("netsh", arguments);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            Process.Start(procStartInfo);
        }

        public void FireWallDisable()
        {
            string arguments = "advfirewall firewall set rule name=\"GTAO-PublicSessionBlocker\" new enable=no";

            ProcessStartInfo procStartInfo = new ProcessStartInfo("netsh", arguments);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            Process.Start(procStartInfo);
        }

        public void FireWallRemove()
        {
            string arguments = "advfirewall firewall delete rule name=\"GTAO-PublicSessionBlocker\"";
            ProcessStartInfo procStartInfo = new ProcessStartInfo("netsh", arguments);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            Process.Start(procStartInfo);
        }

        public void AboutBox()
        {
            using (About box = new About())
            {
                box.ShowDialog();
            }
        }

        public void CheckYourPrivilege()
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            if (!isElevated)
            {    
                string xd = Process.GetCurrentProcess().MainModule.FileName;
                MessageBox.Show("The program needs to be relaunched as an administrator to manage the firewall.");

                ProcessStartInfo procStartInfo = new ProcessStartInfo(xd);

                procStartInfo.Verb = "runas";
                procStartInfo.UseShellExecute = true;


                try
                {
                    Process.Start(procStartInfo);
                    Close();
                }
                catch (Exception)
                {
                    ChkBlockPort.Checked = false;
                    MessageBox.Show("The process failed to obtain administrator privileges, please try again.");
                }
                
                
            }
            else
            {
                //MessageBox.Show("The process is running as administrator.");
            }
        }

        private void GTAOPSBMain_Load(object sender, EventArgs e)
        {
            String[] keys = new String[] { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" }; // Keys to be used
            foreach (string ke in keys)
            {
                CmbKey.Items.Add(ke); // Add keys to combobox
            }

            CmbKey.SelectedIndex = Settings.Default.BoundKey;

            usingTimermode = ChkTimerMode.Checked = Settings.Default.UsingTimermode;
            blockingPort = ChkBlockPort.Checked = Settings.Default.BlockingPort;
            BtnResume.Enabled = false;



            CheckUpdate();

        }

        public void CheckUpdate()
        {
            string url = "https://github.com/FaZeIlLuMiNaTi/GTAO-PublicSessionBlocker/releases/latest";
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            Version currentVersion = new Version(fileVersionInfo.ProductVersion);
            
            Version newVersion = new Version(doc.DocumentNode.SelectNodes("//*[@id=\"js-repo-pjax-container\"]/div[2]/div[1]/div[2]/div/div[1]/ul/li[1]/a/span")[0].InnerText);
            if (currentVersion < newVersion)
            {
                DialogResult dialogResult = MessageBox.Show("Update available!\nCurrent version: " + currentVersion + "\nNew version: " + newVersion +"\nWould you like to update?", "Update available", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string updaterpath = Path.Combine(Path.GetTempPath(), "updater.exe");
                    File.WriteAllBytes(updaterpath, Resources.Updater);
                    ProcessStartInfo ProcStartInfo = new ProcessStartInfo(updaterpath);
                    Process.Start(ProcStartInfo);
                    Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    // Nothing
                }
            }
            else
            {
                // Nothing
            }
        }

        private void GTAOPSBMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(Handle, GetType().GetHashCode());

            FireWallRemove();

            Settings.Default.BoundKey = CmbKey.SelectedIndex;
            Settings.Default.UsingTimermode = usingTimermode;
            Settings.Default.BlockingPort = blockingPort;

            Settings.Default.Save(); // Save the settings
        }

        private void BtnBind_Click(object sender, EventArgs e)
        {
            if (keyBound) // If the bound boolean is true
            {
                Unbind(); // Unbind the key
            }
            else // If the bound boolean isn't true
            {
                Bind(); // Bind the key
            }
        }

        private void BtnSuspend_Click(object sender, EventArgs e)
        {
            Suspend();
        }

        private void BtnResume_Click(object sender, EventArgs e)
        {
            Resume();
        }

        private void ChkBlockPort_CheckedChanged(object sender, EventArgs e)
        {
            blockingPort = ChkBlockPort.Checked;
            if (blockingPort) { // This shouldn't be necessary, but it should protect against somehow messing up the check box state
                CheckYourPrivilege();
                FireWallAdd();
            }
            else
            {
                FireWallRemove();
            }
        }

        private void CmbKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            realKey = (int)(Keys)Enum.Parse(typeof(Keys), CmbKey.Text, true);
            BtnBind.Text = "Bind to " + CmbKey.Text;
        }

        private void ChkTimerMode_CheckedChanged(object sender, EventArgs e)
        {
            usingTimermode = ChkTimerMode.Checked;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
