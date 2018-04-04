using StoryWallpaper.Util;
using System;

namespace StoryWallpaper
{
    public static class HandleFinder
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
                return WindowNative.FindWindowEx(DesktopAreaHandle, (IntPtr)0, "SHELLDLL_DefView", (string)null);
            }
        }

        public static IntPtr FolderListHandle
        {
            get
            {
                return WindowNative.FindWindowEx(FolderListWrapperHandle, (IntPtr)0, "SysListView32", (string)null);
            }
        }

        public static IntPtr WallpaperArea
        {
            get
            {
                return TryFindWorker(DesktopAreaHandle);
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

        public static void SeparateWorker()
        {
            IntPtr progman = ProgmanHandle;

            //for users before windows 10 creator update

            WindowNative.SendMessage(progman, WM_SPAWN_WORKER, (IntPtr) 0, (IntPtr) 0);

            //for users after windows 10 creator update
            WindowNative.SendMessage(progman, WM_SPAWN_WORKER, (IntPtr) 0x0000000D, (IntPtr) 0);
            WindowNative.SendMessage(progman, WM_SPAWN_WORKER, (IntPtr) 0x0000000D, (IntPtr) 1);
        }

        private static IntPtr TryFindWorker(IntPtr handleAfter)
        {
            return WindowNative.FindWindowEx((IntPtr) null, handleAfter, "WorkerW", null);
        }

        private static IntPtr FindListViewWrapperHandle(IntPtr desktopAreaHandle)
        {
            return WindowNative.FindWindowEx(desktopAreaHandle, (IntPtr)0, "SHELLDLL_DefView", null);
        }

        private static IntPtr FindListViewHandle(IntPtr listViewHandle)
        {
            return WindowNative.FindWindowEx(listViewHandle, (IntPtr)0, "SysListView32", null);
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
