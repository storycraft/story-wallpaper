using StoryWallpaper.Util;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace StoryWallpaper.View
{
    public class DesktopFileListView
    {
        private readonly uint LVM_OFFSET = 4096;

        public IntPtr Handle { get; private set; }

        public Color BackgroundColor
        {
            get
            {
                return NativeViewConverter.ConvertDwordToColor((uint)WindowNative.SendMessage(Handle, LVM_OFFSET, (IntPtr)0, (IntPtr)0));
            }
            set
            {
                WindowNative.SendMessage(Handle, LVM_OFFSET + 1U, (IntPtr)0, NativeViewConverter.ConvertColorToDword(value));
                Update();
            }
        }

        public LVBKIMAGE BackgroundImage
        {
            get
            {
                IntPtr num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LVBKIMAGE)));
                WindowNative.SendMessage(Handle, LVM_OFFSET + 139U, (IntPtr)0, num);
                return NativeViewConverter.ConvertIntPtrToImage(num);
            }
            set
            {
                WindowNative.SendMessage(Handle, LVM_OFFSET + 138U, (IntPtr)0, NativeViewConverter.ConvertImageToIntPtr(value));
                Update();
            }
        }

        public Color TextColor
        {
            get
            {
                return NativeViewConverter.ConvertDwordToColor((uint)WindowNative.SendMessage(Handle, LVM_OFFSET + 35U, (IntPtr)0, (IntPtr)0));
            }
            set
            {
                WindowNative.SendMessage(Handle, LVM_OFFSET + 36U, (IntPtr)0, (IntPtr)(NativeViewConverter.ConvertColorToDword(value)));
                Update();
            }
        }

        public Color TextBackgroundColor
        {
            get
            {
                return NativeViewConverter.ConvertDwordToColor((uint)WindowNative.SendMessage(Handle, LVM_OFFSET + 37U, (IntPtr)0, (IntPtr)0));
            }
            set
            {
                WindowNative.SendMessage(Handle, LVM_OFFSET + 38U, (IntPtr)0, (IntPtr)NativeViewConverter.ConvertColorToDword(value));
                Update();
            }
        }

        public Color InsertMaskColor
        {
            get
            {
                return NativeViewConverter.ConvertDwordToColor((uint)WindowNative.SendMessage(Handle, LVM_OFFSET + 171U, (IntPtr)0, (IntPtr)0));
            }
            set
            {
                WindowNative.SendMessage(Handle, LVM_OFFSET + 170U, (IntPtr)0, (IntPtr)NativeViewConverter.ConvertColorToDword(value));
                Update();
            }
        }

        public Color OutlineColor
        {
            get
            {
                return NativeViewConverter.ConvertDwordToColor((uint)(int)WindowNative.SendMessage(this.Handle, this.LVM_OFFSET + 176U, (IntPtr)0, (IntPtr)0));
            }
            set
            {
                WindowNative.SendMessage(Handle, LVM_OFFSET + 177U, (IntPtr)0, (IntPtr)NativeViewConverter.ConvertColorToDword(value));
                Update();
            }
        }

        public string ToolTip
        {
            get
            {
                StringBuilder lParam = new StringBuilder();
                WindowNative.SendMessage(Handle, LVM_OFFSET + 78U, (IntPtr)0, lParam);
                return lParam.ToString();
            }
            set
            {
                WindowNative.SendMessage(Handle, LVM_OFFSET + 74U, (IntPtr)0, new StringBuilder(value));
                Update();
            }
        }

        public int ItemCount
        {
            get
            {
                return (int) WindowNative.SendMessage(Handle, LVM_OFFSET + 4U, (IntPtr)0, (IntPtr)0);
            }
        }

        public void Update()
        {
            WindowNative.SendMessage(Handle, 15U, (IntPtr)0, (IntPtr)0);
        }

        public static DesktopFileListView FromListViewHandle(IntPtr listViewHandle)
        {
            return new DesktopFileListView()
            {
                Handle = listViewHandle
            };
        }
    }
}
