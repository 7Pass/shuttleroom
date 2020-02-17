using System;
using System.Runtime.InteropServices;

namespace shuttleroom
{
    public static class WinApi
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            IntPtr hwnd;
            uint message;
            UIntPtr wParam;
            IntPtr lParam;
            int time;
            POINT pt;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [DllImport("user32.dll")]
        public static extern int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

        [DllImport("user32.dll", ExactSpelling=true)]
        public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, IntPtr lpTimerFunc);

        [DllImport("user32.dll", ExactSpelling=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool KillTimer(IntPtr hWnd, IntPtr uIDEvent);
    }
}
