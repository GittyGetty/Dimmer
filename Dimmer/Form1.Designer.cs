using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Drawing2D;

namespace Dimmer
{
    class MyTrackBar : TrackBar
    {
        public MyTrackBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnValueChanged(EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var dimensions = TrackBarRenderer.GetBottomPointingThumbSize(g, TrackBarThumbState.Normal);

            int sliderWidth = dimensions.Width;
            int sliderHeight = dimensions.Height;
            
            float trackMargin = 10;
            float trackLength = this.Width - 2 * trackMargin - sliderWidth;
            float value = (float)Program.GetGamma();
            float sliderPosition = trackMargin + trackLength * (value / this.Maximum);

            Bitmap track = Properties.Resources.SliderTrack1;
            Bitmap slider = Properties.Resources.Slider1;

            Rectangle sliderBounds = new Rectangle((int)sliderPosition, 0, sliderWidth, sliderHeight);
            RectangleF trackBounds = new Rectangle(0, (sliderBounds.Height - track.Height) / 2, ClientSize.Width, track.Height);

            TextureBrush brush = new TextureBrush(track, new Rectangle(0, 0, track.Width, track.Height), new ImageAttributes());
            brush.WrapMode = WrapMode.Tile;
            brush.TranslateTransform(0, trackBounds.Y);

            g.FillRectangle(brush, trackBounds.X, trackBounds.Y, trackBounds.Width, track.Height);
            g.DrawImage(slider, sliderBounds, 0, 0, slider.Width, slider.Height, GraphicsUnit.Pixel, new ImageAttributes());
        }
    }

    partial class SliderPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SliderPopup));
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BrightnessIcon = new System.Windows.Forms.PictureBox();
            this.BrightnessSlider = new Dimmer.MyTrackBar();
            this.TrayContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // TrayIcon
            // 
            this.TrayIcon.ContextMenuStrip = this.TrayContextMenu;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "Display Dimmer";
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PressTrayIcon);
            // 
            // TrayContextMenu
            // 
            this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitMenuItem});
            this.TrayContextMenu.Name = "contextMenuStrip1";
            this.TrayContextMenu.Size = new System.Drawing.Size(93, 26);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(92, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ClickExitMenuItem);
            // 
            // BrightnessIcon
            // 
            this.BrightnessIcon.BackColor = System.Drawing.Color.Transparent;
            this.BrightnessIcon.Image = ((System.Drawing.Image)(resources.GetObject("BrightnessIcon.Image")));
            this.BrightnessIcon.InitialImage = ((System.Drawing.Image)(resources.GetObject("BrightnessIcon.InitialImage")));
            this.BrightnessIcon.Location = new System.Drawing.Point(5, 5);
            this.BrightnessIcon.Name = "BrightnessIcon";
            this.BrightnessIcon.Size = new System.Drawing.Size(54, 57);
            this.BrightnessIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BrightnessIcon.TabIndex = 2;
            this.BrightnessIcon.TabStop = false;
            this.BrightnessIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragToolbar);
            // 
            // BrightnessSlider
            // 
            this.BrightnessSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BrightnessSlider.BackColor = System.Drawing.Color.Transparent;
            this.BrightnessSlider.Location = new System.Drawing.Point(65, 24);
            this.BrightnessSlider.Maximum = 255;
            this.BrightnessSlider.Minimum = 1;
            this.BrightnessSlider.Name = "BrightnessSlider";
            this.BrightnessSlider.Size = new System.Drawing.Size(425, 45);
            this.BrightnessSlider.SmallChange = 5;
            this.BrightnessSlider.TabIndex = 1;
            this.BrightnessSlider.TickFrequency = 5;
            this.BrightnessSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.BrightnessSlider.Value = 1;
            this.BrightnessSlider.Scroll += new System.EventHandler(this.SlideBrightness);
            this.BrightnessSlider.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ScrollMouseWheel);
            // 
            // SliderPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Dimmer.Properties.Resources.Background1;
            this.ClientSize = new System.Drawing.Size(502, 72);
            this.ControlBox = false;
            this.Controls.Add(this.BrightnessIcon);
            this.Controls.Add(this.BrightnessSlider);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SliderPopup";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragToolbar);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ScrollMouseWheel);
            this.TrayContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ContextMenuStrip TrayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private MyTrackBar BrightnessSlider;
        private PictureBox BrightnessIcon;
    }
}

