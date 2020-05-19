using GTAO_PublicSessionBlocker.Properties;
using Octokit;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTAO_PublicSessionBlocker
{
    public partial class GTAOPSBMain : Form
    {
        // Create variables that will be used globally
        string targetprocess = "GTA5"; // Process name to be targeted
        bool processSuspended = false;
        bool usingTimermode;
        bool blockingPort;
        bool keyBound = false;
        int realKey;

        public GTAOPSBMain()
        {
            InitializeComponent();
        }

        public async void CheckUpdate(bool ManuallyInvoked)
        {

            /***
             * Finally got around to fixing that exception that occurs
             * when the application is launched.
             * 
             * Replaced the whole update check system with something much
             * more elegant in order to guarantee that it'll continue to work
             * in the future.
             * 
             ***/

            // Hide main window
            Opacity = 0;
            ShowInTaskbar = false;

            // Local Version
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            Version localVersion = new Version(fileVersionInfo.ProductVersion);

            try
            {
                // Get info from GitHub
                var client = new GitHubClient(new ProductHeaderValue("GTAO-PSB"));
                var releases = await client.Repository.Release.GetAll("FaZeIlLuMiNaTi", "GTAO-PublicSessionBlocker");
                var latest = releases[0];
                Version remoteVersion = new Version(latest.TagName);

                string exename = AppDomain.CurrentDomain.FriendlyName; // Get name of currently running .exe file
                string url = "https://github.com/FaZeIlLuMiNaTi/GTAO-PublicSessionBlocker/releases/download/" + latest.TagName + "/" + exename;

                if (localVersion < remoteVersion)
                {
                    // Prompt the user to update
                    DialogResult result = MessageBox.Show(
                        "Update available!\n\n" +
                        "Current version: " + localVersion +
                        "\nLatest version: " + remoteVersion +
                        "\nRelease name: " + latest.Name +
                        "\n\nWould you like to update?", "Update available!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Extract and launch the updater
                        string updaterpath = Path.Combine(Path.GetTempPath(), "GitHubAppUpdater.exe");
                        File.WriteAllBytes(updaterpath, Resources.GitHubAppUpdater);

                        string arguments = url + " " + exename;
                        ProcessStartInfo ProcStartInfo = new ProcessStartInfo(updaterpath, arguments);
                        Process.Start(ProcStartInfo);
                        Close();
                    }
                    else if (result == DialogResult.No)
                    {
                        // Nothing?
                    }
                }
                else if (localVersion > remoteVersion)
                {
                    MessageBox.Show("You seem to be running a newer version than currently available to download.");
                }
                else if (ManuallyInvoked)
                {
                    MessageBox.Show("App is up-to-date.");
                }

                // Show main window
                Opacity = 100;
                ShowInTaskbar = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Unable to reach github project page.");
                MessageBox.Show("Exception details:\n\n" + exception);
            }

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

        // Listen for keypress
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
                        TimerMode();
                    }
                    else
                    {
                        Suspend();
                    }
                }
            }
            base.WndProc(ref m);
        }

        public async void TimerMode() // This stops the application UI from freezing
        {
            Suspend();
            await Task.Delay(10000); // Wait 10 seconds
            Resume();
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
            UnregisterHotKey(Handle, GetType().GetHashCode()); // Unbind the key
            CmbKey.Enabled = true;
        }

        public void Suspend()
        {
            var processlist = Process.GetProcessesByName(targetprocess); // Find the process
            foreach (var process in processlist)
            {
                process.Suspend(); // Suspend the process
            }
            processSuspended = true;
            BtnResume.Enabled = true;
            BtnSuspend.Enabled = false;
        }

        public void Resume()
        {
            var processlist = Process.GetProcessesByName(targetprocess); // Find the process
            foreach (var process in processlist)
            {
                process.Resume(); // Resume the process
            }
            processSuspended = false;
            BtnResume.Enabled = false;
            BtnSuspend.Enabled = true;
        }

        public void FireWallAdd()
        {
            // Add a firewall rule to block connections to the GTA Online port 
            string arguments = "advfirewall firewall add rule name=\"GTAO-PublicSessionBlocker\" dir=out action=block description=\"Block GTAO public session port\" profile=any localport=6672 remoteport=any protocol=UDP";
            ProcessStartInfo procStartInfo = new ProcessStartInfo("netsh", arguments);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            Process.Start(procStartInfo);
        }

        public void FireWallEnable() // Never used, but a useful feature for the future.
        {
            // Enable the firewall rule
            string arguments = "advfirewall firewall set rule name=\"GTAO-PublicSessionBlocker\" new enable=yes";

            ProcessStartInfo procStartInfo = new ProcessStartInfo("netsh", arguments);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            Process.Start(procStartInfo);
        }

        public void FireWallDisable() // Never used, but a useful feature for the future.
        {
            // Disable the firewall rule
            string arguments = "advfirewall firewall set rule name=\"GTAO-PublicSessionBlocker\" new enable=no";

            ProcessStartInfo procStartInfo = new ProcessStartInfo("netsh", arguments);

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            Process.Start(procStartInfo);
        }

        public void FireWallRemove()
        {
            // Remove the firewall rule
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
                box.ShowDialog(); // Show the about box
            }
        }

        public void CheckYourPrivilege() // Check application privelleges for modifying the firewall
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            if (!isElevated)
            {    
                string FileName = Process.GetCurrentProcess().MainModule.FileName;
                MessageBox.Show("The program needs to be relaunched as an administrator to manage the firewall.");

                ProcessStartInfo procStartInfo = new ProcessStartInfo(FileName);

                procStartInfo.Verb = "runas";
                procStartInfo.UseShellExecute = true;

                try
                {
                    Process.Start(procStartInfo); // Start the app as admin
                    Close(); // Close the old instance
                }
                catch (Exception)
                {
                    ChkBlockPort.Checked = false;
                    MessageBox.Show("The process failed to obtain administrator privileges, please try again.");
                }
            }
            else
            {
                // Nothing
            }
        }
        
        private void GTAOPSBMain_Load(object sender, EventArgs e)
        {            
            CheckUpdate(false); // Check for any updates - not manually invoked

            String[] keys = new String[] { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" }; // Keys to be used
            foreach (string ke in keys)
            {
                CmbKey.Items.Add(ke); // Add keys to combobox
            }

            // Set defaults based on settings
            CmbKey.SelectedIndex = Settings.Default.BoundKey;
            usingTimermode = ChkTimerMode.Checked = Settings.Default.UsingTimermode;
            blockingPort = ChkBlockPort.Checked = Settings.Default.BlockingPort;
            CmbTarget.SelectedIndex = Settings.Default.TargetGame;
            BtnResume.Enabled = false;
        }

        private void GTAOPSBMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(Handle, GetType().GetHashCode());

            FireWallRemove();

            Settings.Default.BoundKey = CmbKey.SelectedIndex;
            Settings.Default.UsingTimermode = usingTimermode;
            Settings.Default.BlockingPort = blockingPort;
            Settings.Default.TargetGame = CmbTarget.SelectedIndex;

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

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckUpdate(true); // Manually invoked update check
        }

        private void CmbTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CmbTarget.SelectedIndex)
            {
                case 0:
                    targetprocess = "GTAV";
                    break;
                case 1:
                    targetprocess = "RDR2";
                    break;
            }
            MessageBox.Show("targetprocess: " + targetprocess);
        }
    }
}
