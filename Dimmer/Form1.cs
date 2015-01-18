using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dimmer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.trackBar1.Value = Program.GetGamma();
            this.trackBar1.MouseWheel += trackBar1_MouseWheel;
            this.MouseWheel += Form1_MouseWheel;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Program.SetGamma(this.trackBar1.Value);
        }


        bool dragging = false;
        int dx, dy;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((Control.MouseButtons & MouseButtons.Left) == 0)
            {
                dragging = false;
                return;
            }

            if (!dragging)
            {
                dragging = true;
                dx = this.Location.X - Control.MousePosition.X;
                dy = this.Location.Y - Control.MousePosition.Y;
            }

            int newX = Control.MousePosition.X + dx;
            int newY = Control.MousePosition.Y + dy;
            this.Location = new Point(newX, newY);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            this.myVisible = !myVisible;
            this.Visible = myVisible;
            this.Focus();
        }

        void trackBar1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Opacity *= e.Delta > 0 ? 1.1 : 0.9;
            this.Opacity = Math.Max(0.1, this.Opacity);
            var ee = (System.Windows.Forms.HandledMouseEventArgs)e;
            ee.Handled = true;
        }

        void Form1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Opacity *= e.Delta > 0 ? 1.1 : 0.9;
            this.Opacity = Math.Max(0.1, this.Opacity);
        }
    }
}
