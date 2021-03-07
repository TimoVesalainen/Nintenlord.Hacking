﻿using Nintenlord.Hacking.Core.GameData;

namespace Nintenlord.Hacking.Core.MemoryManagement
{
    /// <summary>
    /// Reserves data only when needed
    /// </summary>
    struct LazyReserver
    {
        int offset;
        int size;
        ManagedPointer dataPointer;
        IROM rom;
        IMemoryManager man;


        public int Offset
        {
            get
            {
                if (dataPointer != null)
                {
                    return offset;
                }
                else
                {
                    return dataPointer.Offset;
                }
            }
        }
        public bool HasReservedData
        {
            get
            {
                return dataPointer == null || dataPointer.IsNull;
            }
        }
        public ManagedPointer Pointer
        {
            get
            {
                if (dataPointer == null)
                {
                    dataPointer = man.Reserve(offset, size);
                    if (!dataPointer.IsNull)
                    {
                        man.Unpin(dataPointer);
                    }
                }
                return dataPointer;
            }
        }

        public LazyReserver(int offset, int size, IROM rom, IMemoryManager manager)
        {
            this.offset = offset;
            this.size = size;
            this.rom = rom;
            man = manager;
            dataPointer = null;
        }

        public byte[] ReadData()
        {
            if (dataPointer != null)
            {
                return rom.ReadData(offset, size);
            }
            else
            {
                return rom.ReadData(dataPointer);
            }
        }
        public void WriteData(byte[] data)
        {
            WriteData(data, 0, data.Length);
        }
        public void WriteData(byte[] data, int index)
        {
            WriteData(data, index, data.Length - index);
        }
        public void WriteData(byte[] data, int index, int length)
        {
            rom.WriteData(Pointer, data, index, length);
        }
    }
}
