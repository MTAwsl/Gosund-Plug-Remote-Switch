using System;
using System.Windows;
using System.Windows.Forms;

namespace Gosund_Plug_Remote_Switch
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainContext());
        }
    }

    public class MainContext : ApplicationContext
    {
        private NotifyIcon SystemTrayIcon;
        private ControlForm ControlForm1;
        public MainContext()
        {
            SystemTrayIcon = new NotifyIcon();
            this.ControlForm1 = new ControlForm();
            this.SystemTrayIcon.Icon = Properties.Resources.Icon1;

            // Change the Text property to the name of your application
            this.SystemTrayIcon.Text = "System Tray App";
            this.SystemTrayIcon.Visible = true;

            // Modify the right-click menu of your system tray icon here
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripItem exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += ContextMenuExit;
            menu.Items.Add(exitItem);
            this.SystemTrayIcon.ContextMenuStrip = menu;
            this.SystemTrayIcon.MouseClick += ShowWindow;
        }
        private void ContextMenuExit(object sender, EventArgs e)
        {
            this.SystemTrayIcon.Visible = false;
            Application.Exit();
            Environment.Exit(0);
        }

        private void ShowWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ControlForm1.Show();
                this.ControlForm1.Activate();
            }
        }
    }
}