using System.IO;

namespace UnicodeCharsetDetector
{
    public class Utf8CharsetDetector : CharsetDetector
    {
        public bool NullSuggestsBinary { get; set; } = true;

        public override Charset Check(Stream stream)
        {
            // UTF8 Valid sequences
            // 0xxxxxxx  ASCII
            // 110xxxxx 10xxxxxx  2-byte
            // 1110xxxx 10xxxxxx 10xxxxxx  3-byte
            // 11110xxx 10xxxxxx 10xxxxxx 10xxxxxx  4-byte
            //
            // Width in UTF8
            // Decimal      Width
            // 0-127        1 byte
            // 194-223      2 bytes
            // 224-239      3 bytes
            // 240-244      4 bytes
            //
            // Subsequent chars are in the range 128-191
            var onlySawAsciiRange = true;

            int ch;
            while ((ch = stream.ReadByte()) >= 0)
            {
                if (ch == 0 && NullSuggestsBinary)
                {
                    return Charset.None;
                }

                int moreChars;
                if (ch <= 127)
                    moreChars = 0;
                else if (ch >= 194 && ch <= 223)
                    moreChars = 1;
                else if (ch >= 224 && ch <= 239)
                    moreChars = 2;
                else if (ch >= 240 && ch <= 244)
                    moreChars = 3;
                else
                    return Charset.None;

                // Check secondary chars are in range if we are expecting any
                while (moreChars > 0 && (ch = stream.ReadByte()) >= 0)
                {
                    // Seen non-ascii chars now
                    onlySawAsciiRange = false;
                    if (ch < 128 || ch > 191)
                    {
                        return Charset.None;
                    }
                    --moreChars;
                }
            }

            if (onlySawAsciiRange)
            {
                return Charset.Ascii;
            }

            return Charset.Utf8;
        }
    }
}