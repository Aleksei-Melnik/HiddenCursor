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


        public Form1()
        {
            InitializeComponent();

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


    }
}
