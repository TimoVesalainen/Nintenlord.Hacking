using NUnit.Framework;
using System;
using Nintenlord.Hacking.Core;

namespace Nintenlord.Hacking.Core.Tests
{
    public class UPS
    {
        [Test]
        public void TestEncrypt()
        {
            Assert.AreEqual(new byte[] { 128 }, UPSfile.Encrypt(0));
            Assert.AreEqual(new byte[] { 129 }, UPSfile.Encrypt(1));
            Assert.AreEqual(new byte[] { 0, 128 }, UPSfile.Encrypt(128));
            Assert.AreEqual(new byte[] { 1, 128 }, UPSfile.Encrypt(129));
        }

        [Test]
        public void TestDecrypt()
        {
            Assert.AreEqual(UPSfile.Decrypt(new byte[] { 128 }), 0);
            Assert.AreEqual(UPSfile.Decrypt(new byte[] { 129 }), 1);
            Assert.AreEqual(UPSfile.Decrypt(new byte[] { 0, 128 }), 128);
            Assert.AreEqual(UPSfile.Decrypt(new byte[] { 1, 128 }), 129);
        }

        [TestCase((ulong)0)]
        [TestCase((ulong)13)]
        [TestCase((ulong)3453535)]
        [TestCase((ulong)567)]
        [TestCase((ulong)9999)]
        [TestCase((ulong)898989899)]
        public void TestEncryptDecrypt(ulong n)
        {
            Assert.AreEqual(UPSfile.Decrypt(UPSfile.Encrypt(n)), n);
        }
    }
}
