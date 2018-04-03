namespace StoryWallpaper.Util
{
    internal enum LVBKIF : ulong
    {
        SOURCE_NONE = 0,
        STYLE_NORMAL = 0,
        SOURCE_HBITMAP = 1,
        SOURCE_URL = 2,
        SOURCE_MASK = 3,
        STYLE_MASK = 16, // 0x0000000000000010
        STYLE_TILE = 16, // 0x0000000000000010
        FLAG_TILEOFFSET = 256, // 0x0000000000000100
    }
}
