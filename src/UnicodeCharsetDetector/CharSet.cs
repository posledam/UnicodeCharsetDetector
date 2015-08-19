using System;

namespace UnicodeCharsetDetector
{
    /// <summary>
    /// Detected charset encoding flags.
    /// </summary>
    [Flags]
    public enum Charset
    {
        /// <summary>
        /// Unknown or binary.
        /// </summary>
        None = 0,

        /// <summary>
        /// ASCII charset in 0-127 range.
        /// </summary>
        Ascii = 1,

        /// <summary>
        /// ANSI charset in 0-255 range (also has ASCII flag).
        /// </summary>
        Ansi = 3,

        /// <summary>
        /// UTF-7.
        /// </summary>
        Utf7 = 1 << 2,

        /// <summary>
        /// UTF-7 with BOM.
        /// </summary>
        Utf7Bom = Utf7 | Bom,

        /// <summary>
        /// UTF-8.
        /// </summary>
        Utf8 = 1 << 3,

        /// <summary>
        /// UTF-8 with BOM.
        /// </summary>
        Utf8Bom = Utf8 | Bom,

        /// <summary>
        /// UTF-16 LE.
        /// </summary>
        Utf16Le = 1 << 4,

        /// <summary>
        /// UTF-16 LE with BOM.
        /// </summary>
        Utf16LeBom = Utf16Le | Bom,

        /// <summary>
        /// UTF-16 BE.
        /// </summary>
        Utf16Be = 1 << 5,

        /// <summary>
        /// UTF-16 with BOM.
        /// </summary>
        Utf16BeBom = Utf16Be | Bom,

        /// <summary>
        /// UTF-32 LE.
        /// </summary>
        Utf32Le = 1 << 6,

        /// <summary>
        /// UTF-32 LE with BOM.
        /// </summary>
        Utf32LeBom = Utf32Le | Bom,

        /// <summary>
        /// UTF-32 BE.
        /// </summary>
        Utf32Be = 1 << 7,

        /// <summary>
        /// UTF-32 BE with BOM.
        /// </summary>
        Utf32BeBom = Utf32Be | Bom,

        /// <summary>
        /// BOM flag.
        /// </summary>
        Bom = 1 << 16
    }
}