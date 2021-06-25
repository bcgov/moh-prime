using System;
using System.Linq;
using System.Collections.Generic;

namespace Prime.Engines
{
    public static class Gpid
    {
        public const int Length = 20;
        public const string CharacterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,?!@#$%*";

        public static string NewGpid()
        {
            return GenerateCharacters(Length);
        }

        public static string NewGpid(string initialCharacters)
        {
            if (initialCharacters.Length > Length)
            {
                throw new ArgumentOutOfRangeException($"GPIDs have a max length of {Length}.");
            }

            if (initialCharacters.Any(ch => !CharacterSet.Contains(ch)))
            {
                throw new ArgumentException($"GPIDs can only contain the characters [{CharacterSet}].");
            }

            return initialCharacters + GenerateCharacters(Length - initialCharacters.Length);
        }

        private static string GenerateCharacters(int count)
        {
            Random r = new Random();

            IEnumerable<char> chars = Enumerable.Repeat(CharacterSet, count).Select(s => s[r.Next(s.Length)]);

            return new string(chars.ToArray());
        }
    }
}
