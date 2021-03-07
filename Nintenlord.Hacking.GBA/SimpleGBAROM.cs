using System;
using Nintenlord.Hacking.Core.GameData;
using Nintenlord.Hacking.Core.MemoryManagement;

namespace Nintenlord.Hacking.GBA
{
    public class SimpleGBAROM : GBAROM, IROM
    {
        #region IROM Members

        public void WriteData(ManagedPointer ptr, byte[] data, int index, int length)
        {
            if (length > ptr.Size)
            {
                throw new IndexOutOfRangeException();
            }
            InsertData(ptr.Offset, data, index, length);
        }

        public byte[] ReadData(int offset, int length)
        {
            return GetData(offset, length);
        }

        public byte[] ReadData(ManagedPointer ptr)
        {
            return GetData(ptr.Offset, ptr.Size);
        }

        #endregion

        #region IROM Members


        public void AddCustomData(string name, string data)
        {
            throw new NotImplementedException();
        }

        public string GetCustomData(string name)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
