using System;
using System.Runtime.InteropServices;

namespace StoryWallpaper.Util
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct LVBKIMAGE
    {
        public ulong ulFlags;
        public IntPtr hbm;
        public IntPtr path;
        public uint cchImageMax;
        public int xOffsetPercent;
        public int yOffsetPercent;
    }
}
