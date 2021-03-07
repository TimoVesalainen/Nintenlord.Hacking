using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Utility;
using Nintenlord.Utility.Primitives;
using Nintenlord.MemoryManagement;

namespace Nintenlord.Hacking.GBA.MemoryManagement
{
    public sealed class GBAPointer : IMemoryPointer, IComparable<GBAPointer>, IEquatable<GBAPointer>, IDisposable
    {
        IFreeMemoryManager<GBAPointer> manager;
        int offset;
        int size;

        public int OffsetAfter { get { return offset + size; } }

        public static readonly GBAPointer NullPointer = new GBAPointer(null, -1, 0);

        public GBAPointer(IFreeMemoryManager<GBAPointer> manager, int offset, int size)
        {
            this.manager = manager;
            this.offset = offset;
            this.size = size;
        }

        private GBAPointer(GBAPointer copyMe)
        {
            manager = copyMe.manager;
            offset = copyMe.offset;
            size = copyMe.size;
        }

        #region IMemoryPointer Members

        public bool IsNull
        {
            get { return offset < 0; }
        }

        public int Offset
        {
            get { return offset; }
            internal set { offset = value; }
        }

        public int Size
        {
            get { return size; }
            internal set { size = value; }
        }

        #endregion

        #region IComparable<GBAPointer> Members

        public int CompareTo(GBAPointer other)
        {
            return offset - other.offset;
        }

        #endregion

        #region IEquatable<GBAPointer> Members

        public bool Equals(GBAPointer other)
        {
            if (!IsNull && !other.IsNull)
            {
                return offset == other.offset &&
                       size == other.size;
            }
            else
            {
                return IsNull == other.IsNull;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (manager.IsAllocated(this))
            {
                manager.Deallocate(this);
            }
            offset = -1;
            size = 0;
            manager = null;
        }

        #endregion

        public override int GetHashCode()
        {
            return size;
        }

        public override string ToString()
        {
            return string.Format("${0}: 0x{1}",
                (offset + 0x8000000).ToHexString(""),
                size.ToHexString(""));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is GBAPointer)
            {
                return Equals((GBAPointer)obj);
            }
            return false;
        }

        public static explicit operator OffsetSizePair(GBAPointer ptr)
        {
            return new OffsetSizePair(ptr.offset, ptr.size);
        }
    }
}
