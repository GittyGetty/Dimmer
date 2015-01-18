using System.Collections;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class Program
{
    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("gdi32.dll")]
    public static extern bool SetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

    [DllImport("gdi32.dll")]
    public static extern int GetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct RAMP
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Red;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Green;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Blue;
    }

    public static void SetGamma(int gamma)
    {
        gamma = Math.Min(gamma, 256);
        gamma = Math.Max(gamma, 1);

        RAMP ramp = new RAMP();
        ramp.Red = new ushort[256];
        ramp.Green = new ushort[256];
        ramp.Blue = new ushort[256];

        for (int i = 1; i < 256; i++)
        {
            var val = (ushort)Math.Min(i * (gamma + 128), 65535);
            ramp.Red[i] = ramp.Blue[i] = ramp.Green[i] = val;
        }

        SetDeviceGammaRamp(GetDC(IntPtr.Zero), ref ramp);
    }

    public static int GetGamma()
    {
        RAMP ramp = new RAMP();
        GetDeviceGammaRamp(GetDC(IntPtr.Zero), ref ramp);
        return ramp.Red[1] - 128;
    }

    public static void Main(string[] args)
    {
        GetGamma();
        var mainWindow = new Dimmer.Form1();
        mainWindow.Show();
        Application.Run(mainWindow);
    }
}