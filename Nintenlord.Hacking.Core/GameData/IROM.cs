using Nintenlord.Hacking.Core.MemoryManagement;

namespace Nintenlord.Hacking.Core.GameData
{
    public interface IROM
    {
        //GBA data
        string GameTitle { get; }
        string GameCode  { get; }
        string MakerCode { get; }

        int Length { get; }

        void WriteData(ManagedPointer ptr, byte[] data, int index, int length);
        byte[] ReadData(int offset, int length);
        byte[] ReadData(ManagedPointer ptr);

        void AddCustomData(string name, string data);
        string GetCustomData(string name);
    }
}