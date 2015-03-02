using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SidBy.Common.Crypt
{
    public static class RandomGenerator
    {
        private static RNGCryptoServiceProvider _global = new RNGCryptoServiceProvider();

        [ThreadStatic]
        private static Random _local;

        private static int Next(int maxValue)
        {
            Random inst = _local;
            if (inst == null)
            {
                byte[] buffer = new byte[4];
                _global.GetBytes(buffer);
                _local = inst = new Random(BitConverter.ToInt32(buffer, 0));
            }
            return inst.Next(maxValue);
        }

        public static string GenerateRandomText(int textLength)
        {
            const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var result = new string(
                Enumerable.Repeat(Chars, textLength)
                    .Select(s => s[RandomGenerator.Next(s.Length)])
                    .ToArray());
            return result;
        }

        public static string GenerateRandomText()
        {
            return RandomGenerator.GenerateRandomText(10);
        }
    }
}
