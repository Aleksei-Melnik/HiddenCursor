using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CursorManager
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public struct POINT
        {
            public int X;
            public int Y;
        }

        private POINT _lastPosition;
        private DateTime _lastMoveTime;
        private Timer _timer;
        private NotifyIcon _notifyIcon;

        public Form1()
        {
            InitializeComponent();
            InitializeTrayIcon();

            _lastMoveTime = DateTime.Now;
            _timer = new Timer();
            _timer.Interval = 100;
            _timer.Tick += new EventHandler(CheckCursor);
            _timer.Start();

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Load += (sender, e) => { this.Hide(); };
        }
        private void CheckCursor(object sender, EventArgs e)
        {
            if (GetCursorPos(out POINT currentPos))
            {
                if (currentPos.X != _lastPosition.X || currentPos.Y != _lastPosition.Y)
                {
                    _lastPosition = currentPos;
                    _lastMoveTime = DateTime.Now;
                }
                else if ((DateTime.Now - _lastMoveTime).TotalSeconds > 3)
                {
                    int screenX = Screen.PrimaryScreen.Bounds.Width;
                    int screenY = Screen.PrimaryScreen.Bounds.Height;
                    SetCursorPos(screenX - 1, screenY - 1);
                }
            }
        }
        private void InitializeTrayIcon()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = new Icon(GetTrayIconPath(), 16, 16);
            _notifyIcon.Visible = true;

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit", null, ExitMenuItem_Click);
            contextMenu.Items.Add(exitMenuItem);

            _notifyIcon.ContextMenuStrip = contextMenu;
        }
        private string GetTrayIconPath()
        {
            string iconPath = "icons/icon_x16.ico";
            float dpi = GetDpi();

            if (dpi > 96 && dpi <= 120) // 125%
                iconPath = "icons/icon_x24.ico";
            else if (dpi > 120 && dpi <= 144) // 150%
                iconPath = "icons/icon_x32.ico";
            else if (dpi > 144) // 200% or higher
                iconPath = "icons/icon_x48.ico";

            return iconPath;
        }
        private float GetDpi()
        {
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                return graphics.DpiX;
            }
        }
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            _notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
