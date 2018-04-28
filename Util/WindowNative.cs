using System;
using System.Runtime.InteropServices;
using System.Text;

namespace StoryWallpaper.Util
{
    internal static class WindowNative
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        public static extern int GetWindow(IntPtr hwndSibling, int wFlag);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWndChild);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, uint lParam);

        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0,
            SMTO_BLOCK = 0x1,
            SMTO_ABORTIFHUNG = 0x2,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x8,
            SMTO_ERRORONEXIT = 0x20
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(
            IntPtr hWnd,
            uint Msg,
            UIntPtr wParam,
            IntPtr lParam,
            SendMessageTimeoutFlags fuFlags,
            uint uTimeout,
            out UIntPtr lpdwResult);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(
            IntPtr windowHandle,
            uint Msg,
            IntPtr wParam,
            IntPtr lParam,
            SendMessageTimeoutFlags flags,
            uint timeout,
            out IntPtr result);

        [DllImport("user32.dll", EntryPoint = "SendMessageTimeout", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint SendMessageTimeoutText(
            IntPtr hWnd,
            int Msg,
            int countOfChars,
            StringBuilder text,
            SendMessageTimeoutFlags flags,
            uint uTImeoutj,
            out IntPtr result);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr dc);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndAddAfter, int x, int y, int cX, int cY, uint flag);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, WindowNative.HookProc lpfn, IntPtr hMod, uint dwThreadId);

        public enum ShowWindowCommand : int
        {
            Hide = 0,
            Normal = 1,
            ShowMinimized = 2,
            Maximize = 3,
            ShowMaximized = 3,
            ShowNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActive = 7,
            ShowNA = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimize = 11
        }

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommand nCmdShow);

        public enum WindowLongFlags : int
        {
            GWL_EXSTYLE = -20,
            GWLP_HINSTANCE = -6,
            GWLP_HWNDPARENT = -8,
            GWL_ID = -12,
            GWL_STYLE = -16,
            GWL_USERDATA = -21,
            GWL_WNDPROC = -4,
            DWLP_USER = 0x8,
            DWLP_MSGRESULT = 0x0,
            DWLP_DLGPROC = 0x4
        }

        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [Flags()]
        public enum DeviceContextValues : uint
        {
            Window = 0x00000001,
            Cache = 0x00000002,
            NoResetAttrs = 0x00000004,
            ClipChildren = 0x00000008,
            ClipSiblings = 0x00000010,
            ParentClip = 0x00000020,
            ExcludeRgn = 0x00000040,
            IntersectRgn = 0x00000080,
            ExcludeUpdate = 0x00000100,
            IntersectUpdate = 0x00000200,
            LockWindowUpdate = 0x00000400,
            Validate = 0x00200000,
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, DeviceContextValues flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("Kernel32.dll")]
        public static extern bool SetConsoleCtrlHandler(WindowNative.HandlerRoutine Handler, bool Add);

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6,
        }

        public delegate bool HandlerRoutine(WindowNative.CtrlTypes CtrlType);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
    }
}
