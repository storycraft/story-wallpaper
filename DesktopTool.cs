using StoryWallpaper.Util;
using StoryWallpaper.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StoryWallpaper
{
    public static class DesktopTool
    {
        private static Dictionary<IntPtr, IntPtr> originalHandleList = new Dictionary<IntPtr, IntPtr>();
        private static List<IntPtr> appendedList = new List<IntPtr>();

        public static DesktopFileListView GetDesktopFileListView()
        {
            return DesktopFileListView.FromListViewHandle(HandleFinder.ListViewHandle);
        }

        public static Graphics GetWallpaperGraphics()
        {
            if (HandleFinder.NeedSeparation)
                HandleFinder.SeparateWorker();

            return Graphics.FromHwnd(HandleFinder.WallpaperArea);
        }

        public static void AppendToWallpaperArea(IntPtr handle)
        {
            if (HandleFinder.NeedSeparation)
                HandleFinder.SeparateWorker();

            if (appendedList.Contains(handle))
                return;

            originalHandleList[handle] = WindowNative.GetParent(handle);

            WindowNative.SetParent(handle, HandleFinder.WallpaperArea);
        }

        public static void AppendToWallpaperArea(Form form)
        {
            AppendToWallpaperArea(form.Handle);
        }

        public static void RemoveFromWallpaperArea(IntPtr handle)
        {
            if (!appendedList.Contains(handle))
                return;

            WindowNative.SetParent(handle, originalHandleList[handle]);

            originalHandleList.Remove(handle);
        }

        public static void RemoveFromWallpaperArea(Form form)
        {
            RemoveFromWallpaperArea(form.Handle);
        }

        public static void RemoveAllFromWallpaperArea()
        {
            foreach (IntPtr handle in appendedList)
                RemoveFromWallpaperArea(handle);
        }
    }
}
