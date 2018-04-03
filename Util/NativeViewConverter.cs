using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace StoryWallpaper.Util
{
    internal static class NativeViewConverter
    {
        public static Color ConvertDwordToColor(uint dWord)
        {
            byte num1 = (byte)(dWord >> 16 & (uint)byte.MaxValue);
            byte num2 = (byte)(dWord >> 8 & (uint)byte.MaxValue);
            return Color.FromArgb((int)(byte)(dWord & (uint)byte.MaxValue) << 16 | (int)num2 << 8 | (int)num1 | (int)dWord & -16777216);
        }

        public static uint ConvertColorToDword(Color color)
        {
            return (uint)((int)color.R | (int)color.G << 8 | (int)color.B << 16 | ((int)color.A == 0 ? -16777216 : 0));
        }

        public static LVBKIMAGE ConvertIntPtrToImage(IntPtr ptr)
        {
            LVBKIMAGE structure = (LVBKIMAGE)Marshal.PtrToStructure(ptr, typeof(LVBKIMAGE));
            Console.Out.WriteLine((object)structure.path);
            return structure;
        }

        public static IntPtr ConvertImageToIntPtr(LVBKIMAGE img)
        {
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(LVBKIMAGE)));
            Marshal.StructureToPtr<LVBKIMAGE>(img, ptr, false);
            return ptr;
        }

        public static Bitmap BitmapFromImage(LVBKIMAGE img)
        {
            return Image.FromHbitmap(img.hbm);
        }

        public static LVBKIMAGE ImageFromBitmap(Bitmap bitmap, ulong options)
        {
            return new LVBKIMAGE()
            {
                hbm = bitmap.GetHbitmap(),
                xOffsetPercent = 100,
                yOffsetPercent = 100,
                ulFlags = options
            };
        }
    }
}
