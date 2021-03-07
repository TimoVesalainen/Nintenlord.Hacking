using System;
using System.Collections.Generic;

namespace Nintenlord.Hacking.GBA
{
    public static unsafe class Pointer
    {
        enum ScanDepth : byte
        {
            Byte = 1,
            HalfWord = 2,
            Word = 4
        };
        private const ScanDepth scanDepth = ScanDepth.Word;
        const int MaxOffset = 0x2000000;

        public static int[] Scan(byte* pointer, int lenght)
        {
            List<int> offsets = new List<int>();

            for (int i = 0; i < lenght; i += (int)scanDepth)
            {
                int offset;
                if (GetOffset(*(pointer + i), out offset))
                {
                    offsets.Add(i);
                }
            }
            return offsets.ToArray();
        }

        public static int[] ScanForPointer(byte* pointer, int lenght, int offset)
        {
            List<int> results = new List<int>();

            for (int i = 0; i < lenght; i += (int)scanDepth)
            {
                int newOffset;
                if (GetOffset(*(int*)(pointer + i), out newOffset))
                {
                    if (newOffset == offset)
                    {
                        results.Add(i);
                    }
                }
            }

            return results.ToArray();
        }

        public static bool GetOffset(int value, out int offset)
        {
            if (value == 0)
            {
                offset = 0;
                return true;
            }
            offset = value & 0x1FFFFFF;
            return offset < MaxOffset && (value & 0xF7FFFFFF) != 0;
        }

        public static int GetOffset(int value)
        {
            int val;
            if (!GetOffset(value, out val))
            {
                throw new ArgumentException();
            }
            return val;
        }

        public static bool MakePointer(int offset, out int pointer)
        {
            pointer = offset | 0x8000000;
            return offset < MaxOffset;
        }

        public static int MakePointer(int offset)
        {
            return offset | 0x8000000;
        }
    }
}