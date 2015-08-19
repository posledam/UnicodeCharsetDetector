using System.IO;

namespace UnicodeCharsetDetector
{
    public abstract class CharsetDetector
    {
        public abstract Charset Check(Stream stream);
    }
}
