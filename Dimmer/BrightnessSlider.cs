using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;

namespace Dimmer
{
    public partial class SliderPopup : Form
    {
        public SliderPopup()
        {
            InitializeComponent();
            this.BrightnessSlider.Value = ScreenGamma.GetGamma();
            this.DoubleBuffered = true;
        }

        // http://msdn.microsoft.com/en-us/library/windows/desktop/ms645618(v=vs.85).aspx
        private const int HTCLIENT = 1;
        private const int HTRIGHT = 11;
        private const int HTLEFT = 10;
        private const int WM_NCHITTEST = 0x84;

        /// <summary>
        /// Resizing vertically makes the slider look strange, so this disables it.
        /// </summary>
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            int result = (int)message.Result;
            if (message.Msg != WM_NCHITTEST) return;
            if (result != HTLEFT && result != HTRIGHT)
                message.Result = (IntPtr)HTCLIENT;
        }

        private bool myVisible = false;
        public new bool Visible
        {
            get { return myVisible; }
            set
            {
                myVisible = value;
                base.Visible = myVisible;
            }
        }

        #region Event Handlers
        /// <summary>
        /// Lets us control the form's initial visibility so it remains hidden
        /// at startup.
        /// </summary>
        /// <param name="value"></param>
        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(myVisible);
        }

        private void SlideBrightness(object sender, EventArgs e)
        {
            ScreenGamma.SetGamma(this.BrightnessSlider.Value);
        }

        /// <summary>
        /// The offset of this form's location from the location of the mouse cursor
        /// at the time the user starts dragging the mouse.
        /// </summary>
        private int dx, dy;
        private bool dragging = false;
        private void DragToolbar(object sender, MouseEventArgs e)
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

        private void ScrollMouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Opacity *= e.Delta > 0 ? 1.1 : 0.9;
            this.Opacity = Math.Max(0.1, this.Opacity);

            // Mark this event as handled so that mouse events for the scroll wheel do not
            // get further processed and cause the slider to change.
            var handledArgs = (HandledMouseEventArgs)e;
            handledArgs.Handled = true;
        }

        private void ClickExitMenuItem(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        Random r = new Random();
        private void PressTrayIcon(object sender, MouseEventArgs e)
        {
            var files = Directory.GetFiles("backgrounds")
                                 .Where((file) => file.EndsWith(".jpg") || file.EndsWith("*.png") || file.EndsWith("*.bmp"))
                                 .ToArray();

            if (files.Length > 0)
            {
                var file = files[r.Next(files.Length - 1)];
                var background = Image.FromFile(file);
                this.BackgroundImage = background;
            }

            this.Visible = !this.Visible;
        }

        private void MainWindowDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = !this.Visible;
        }

        #endregion
    }
}
