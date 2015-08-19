using System;
using System.Text;

namespace UnicodeCharsetDetector
{
    public static class CharsetExtensions
    {
        public static Encoding ToEncoding(this Charset charset,
            bool addByteOrderMask = true,
            bool throwOnInvalidBytes = true)
        {
            switch (charset)
            {
                case Charset.None:
                case Charset.Ascii:
                case Charset.Ansi:
                case Charset.Bom:
                    return Encoding.Default;
                case Charset.Utf7:
                case Charset.Utf7Bom:
                    return Encoding.UTF7;
                case Charset.Utf8:
                case Charset.Utf8Bom:
                    return new UTF8Encoding(addByteOrderMask, throwOnInvalidBytes);
                case Charset.Utf16Le:
                case Charset.Utf16LeBom:
                    return new UnicodeEncoding(false, addByteOrderMask, throwOnInvalidBytes);
                case Charset.Utf16Be:
                case Charset.Utf16BeBom:
                    return new UnicodeEncoding(true, addByteOrderMask, throwOnInvalidBytes);
                case Charset.Utf32Le:
                case Charset.Utf32LeBom:
                    return new UTF32Encoding(false, addByteOrderMask, throwOnInvalidBytes);
                case Charset.Utf32Be:
                case Charset.Utf32BeBom:
                    return new UTF32Encoding(true, addByteOrderMask, throwOnInvalidBytes);
                default:
                    throw new ArgumentOutOfRangeException(nameof(charset), charset, null);
            }
        }

        public static int BomLength(this Charset charset)
        {
            var length = 0;

            switch (charset)
            {
                case Charset.Utf32BeBom:
                case Charset.Utf32LeBom:
                    length = 4;
                    break;
                case Charset.Utf16BeBom:
                case Charset.Utf16LeBom:
                    length = 2;
                    break;
                case Charset.Utf7Bom:
                case Charset.Utf8Bom:
                    length = 3;
                    break;
            }

            return length;
        }

    }
}