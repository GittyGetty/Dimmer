using System;
using System.Runtime.InteropServices;

public class ScreenGamma
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

    private const int gammaMax = 0xFFFF;
    private const int gammaBase = 128;

    public static void SetGamma(int gamma)
    {
        gamma = Math.Min(gamma, 256);
        gamma = Math.Max(gamma, 1);

        RAMP ramp = new RAMP();
        ramp.Red = new ushort[256];
        ramp.Green = new ushort[256];
        ramp.Blue = new ushort[256];

        for (int i = 1; i < ramp.Red.Length; i++)
        {
            var val = (ushort)Math.Min(i * (gamma + gammaBase), gammaMax);
            ramp.Red[i] = ramp.Blue[i] = ramp.Green[i] = val;
        }

        SetDeviceGammaRamp(GetDC(IntPtr.Zero), ref ramp);
    }

    public static int GetGamma()
    {
        RAMP ramp = new RAMP();
        GetDeviceGammaRamp(GetDC(IntPtr.Zero), ref ramp);
        return ramp.Red[1] - gammaBase;
    }
}