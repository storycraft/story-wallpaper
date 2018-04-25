using System;

namespace StoryWallpaper.Util
{
    public static class HandleUtil
    {
        public static IntPtr ProgmanHandle
        {
            get
            {
                return WindowNative.FindWindow("Progman", "Program Manager");
            }
        }

        public static IntPtr DesktopAreaHandle
        {
            get
            {
                return FindDesktopArea();
            }
        }

        public static IntPtr FolderListWrapperHandle
        {
            get
            {
                return WindowNative.FindWindowEx(DesktopAreaHandle, (IntPtr)0, "SHELLDLL_DefView", null);
            }
        }

        public static IntPtr FolderListHandle
        {
            get
            {
                return WindowNative.FindWindowEx(FolderListWrapperHandle, (IntPtr)0, "SysListView32", null);
            }
        }

        public static IntPtr WallpaperArea
        {
            get
            {
                return TryFindWorker(DesktopAreaHandle);
            }
        }

        public static IntPtr WallpaperAreaDC
        {
            get
            {
                return WindowNative.GetDC(ProgmanHandle);
            }
        }

        public static bool NeedSeparation
        {
            get
            {
                return DesktopAreaHandle == ProgmanHandle;
            }
        }

        const uint WM_SPAWN_WORKER = 0x052C;

        public static void SpawnWorker()
        {
            IntPtr progman = ProgmanHandle;

            WindowNative.SendMessage(progman, WM_SPAWN_WORKER, IntPtr.Zero, IntPtr.Zero);
        }

        private static IntPtr TryFindWorker(IntPtr handleAfter)
        {
            return WindowNative.FindWindowEx(IntPtr.Zero, handleAfter, "WorkerW", null);
        }

        private static IntPtr FindListViewWrapperHandle(IntPtr desktopAreaHandle)
        {
            return WindowNative.FindWindowEx(desktopAreaHandle, IntPtr.Zero, "SHELLDLL_DefView", null);
        }

        private static IntPtr FindListViewHandle(IntPtr listViewHandle)
        {
            return WindowNative.FindWindowEx(listViewHandle, IntPtr.Zero, "SysListView32", null);
        }

        private static IntPtr FindDesktopArea()
        {
            if (FindListViewWrapperHandle(ProgmanHandle) != IntPtr.Zero)
                return ProgmanHandle;

            IntPtr worker = TryFindWorker(IntPtr.Zero);
            while (FindListViewWrapperHandle(worker) == IntPtr.Zero)
            {
                IntPtr nextWorker = TryFindWorker(worker);

                worker = nextWorker;
            }

            return worker;
        }
    }
}
