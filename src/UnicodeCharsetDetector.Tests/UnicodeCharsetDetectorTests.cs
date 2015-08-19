using System;
using System.IO;
using NUnit.Framework;

namespace UnicodeCharsetDetector.Tests
{
    [TestFixture]
    public class UnicodeCharsetDetectorTests
    {
        [Test]
        public void TestCheckBom()
        {
            Assert.Throws<ArgumentNullException>(() => UnicodeCharsetDetector.CheckBom(null, 0));
            Assert.Throws<ArgumentException>(() => UnicodeCharsetDetector.CheckBom(new byte[0], 1));

            // UTF-7
            Assert.AreEqual(Charset.Utf7Bom, UnicodeCharsetDetector.CheckBom(new byte[] { 0x2B, 0x2F, 0x76, 0x38 }, 4));
            Assert.AreEqual(Charset.Utf7Bom, UnicodeCharsetDetector.CheckBom(new byte[] { 0x2B, 0x2F, 0x76, 0x39 }, 4));
            Assert.AreEqual(Charset.Utf7Bom, UnicodeCharsetDetector.CheckBom(new byte[] { 0x2B, 0x2F, 0x76, 0x2B }, 4));
            Assert.AreEqual(Charset.Utf7Bom, UnicodeCharsetDetector.CheckBom(new byte[] { 0x2B, 0x2F, 0x76, 0x2F }, 4));

            // UTF-7 BAD
            Assert.AreNotEqual(Charset.Utf7Bom, UnicodeCharsetDetector.CheckBom(new byte[] { 0x2B, 0x2F, 0x76, 0x2F }, 3));
            Assert.AreNotEqual(Charset.Utf7Bom, UnicodeCharsetDetector.CheckBom(new byte[] { 0x2B, 0x2F, 0x76, 0 }, 4));

            // UTF-8
            Assert.AreEqual(Charset.Utf8Bom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xEF, 0xBB, 0xBF }, 3));
            Assert.AreEqual(Charset.Utf8Bom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xEF, 0xBB, 0xBF, 0 }, 4));
            Assert.AreEqual(Charset.Utf8Bom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xEF, 0xBB, 0xBF, 1 }, 4));

            // UTF-16 LE
            Assert.AreEqual(Charset.Utf16LeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xFF, 0xFE }, 2));
            Assert.AreEqual(Charset.Utf16LeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xFF, 0xFE, 0 }, 3));
            Assert.AreEqual(Charset.Utf16LeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xFF, 0xFE, 1 }, 3));
            Assert.AreEqual(Charset.Utf16LeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xFF, 0xFE, 1, 0 }, 4));
            Assert.AreEqual(Charset.Utf16LeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xFF, 0xFE, 0, 1 }, 4));

            // UTF-16 BE
            Assert.AreEqual(Charset.Utf16BeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xFE, 0xFF }, 2));
            Assert.AreEqual(Charset.Utf16BeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xFE, 0xFF, 0 }, 3));
            Assert.AreEqual(Charset.Utf16BeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xFE, 0xFF, 1 }, 3));
            Assert.AreEqual(Charset.Utf16BeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xFE, 0xFF, 1, 0 }, 4));
            Assert.AreEqual(Charset.Utf16BeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xFE, 0xFF, 0, 1 }, 4));

            // UTF-32 LE/BE
            Assert.AreEqual(Charset.Utf32LeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0xFF, 0xFE, 0, 0 }, 4));
            Assert.AreEqual(Charset.Utf32BeBom, UnicodeCharsetDetector.CheckBom(new byte[] { 0, 0, 0xFE, 0xFF }, 4));
        }

        [Test]
        public void TestDataFiles()
        {
            var dataFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "..\\..\\Data");
            var testFiles = Directory.EnumerateFiles(dataFolder, "*.txt");
            var detector = new UnicodeCharsetDetector();
            foreach (var path in testFiles)
            {
                var fileName = Path.GetFileName(path) ?? String.Empty;
                using (var stream = File.OpenRead(path))
                {
                    var charset = detector.Check(stream);
                    var charsetName = charset.ToString();
                    Console.WriteLine("File: {0}, Charset: {1}", fileName, charset);
                    Assert.True(fileName.StartsWith(charsetName, StringComparison.InvariantCultureIgnoreCase));
                }
            }
        }

    }
}
