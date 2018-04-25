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
        public static DesktopFileListView GetDesktopFileListView()
        {
            return DesktopFileListView.FromListViewHandle(HandleUtil.FolderListHandle);
        }

        public static Graphics GetWallpaperGraphics()
        {
            if (HandleUtil.NeedSeparation)
                HandleUtil.SpawnWorker();

            return Graphics.FromHwnd(HandleUtil.WallpaperArea);
        }

        public static void AppendToWallpaperArea(IntPtr handle)
        {
            if (HandleUtil.NeedSeparation)
                HandleUtil.SpawnWorker();

            WindowNative.SetParent(handle, HandleUtil.WallpaperArea);
        }

        public static void AppendToWallpaperArea(Form form)
        {
            AppendToWallpaperArea(form.Handle);
        }

        public static void RemoveFromWallpaperArea(IntPtr handle)
        {
            WindowNative.SetParent(handle, IntPtr.Zero);

            UpdateWallpaper();
        }

        public static void RemoveFromWallpaperArea(Form form)
        {
            RemoveFromWallpaperArea(form.Handle);
        }

        private const int SPI_SETDESKWALLPAPER = 0x14;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        public static void UpdateWallpaper()
        {
            WindowNative.SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                null,
                SPIF_SENDWININICHANGE);
        }
    }
}
