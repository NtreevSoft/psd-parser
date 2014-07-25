using System;
using System.IO;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    class PSDReader : IDisposable
    {
        private readonly BinaryReader reader;

        public PSDReader(BinaryReader reader)
        {
            this.reader = reader;
        }

        public PSDReader(Stream stream)
            : this(new InternalBinaryReader(stream))
        {

        }

        public void Dispose()
        {
            if (this.reader is InternalBinaryReader == true)
                this.reader.Dispose();
            else
                this.reader.Close();
        }

        public string ReadAscii(int length)
        {
            return Encoding.ASCII.GetString(this.reader.ReadBytes(length));
        }

        public string ReadPascalString(int modLength)
        {
            byte count = this.reader.ReadByte();
            string str = "";
            if (count == 0)
            {
                Stream baseStream = this.reader.BaseStream;
                baseStream.Position += modLength - 1;
                return str;
            }
            byte[] bytes = this.reader.ReadBytes(count);
            str = Encoding.UTF8.GetString(bytes);
            for (int i = count + 1; (i % modLength) != 0; i++)
            {
                Stream stream2 = this.reader.BaseStream;
                stream2.Position += 1L;
            }
            return str;
        }

        public string ReadUnicodeString()
        {
            int num = 4;
            int num2 = EndianReverser.getInt32(this.reader);
            string str = "";
            if (num2 == 0)
            {
                Stream baseStream = this.reader.BaseStream;
                baseStream.Position += num - 1;
                return str;
            }
            byte[] bytes = this.reader.ReadBytes(num2 * 2);
            for (int i = 0; i < num2; i++)
            {
                int index = i * 2;
                byte num5 = bytes[index];
                bytes[index] = bytes[index + 1];
                bytes[index + 1] = num5;
            }
            str = Encoding.Unicode.GetString(bytes);
            for (int j = num2 + 1; (j % num) != 0; j++)
            {
                Stream stream2 = this.reader.BaseStream;
                stream2.Position += 1L;
            }
            return str;
        }

        public bool ReadBoolean()
        {
            return this.ReverseValue(this.reader.ReadBoolean());
        }

        public double ReadDouble()
        {
            return this.ReverseValue(this.reader.ReadDouble());
        }

        public double[] ReadDouble(int count)
        {
            double[] values = new double[count];
            for (int i = 0; i < count; i++)
            {
                values[i] = this.ReverseValue(this.reader.ReadDouble());
            }
            return values;
        }

        public short ReadInt16()
        {
            return this.ReverseValue(this.reader.ReadInt16());
        }

        public int ReadInt32()
        {
            return this.ReverseValue(this.reader.ReadInt32());
        }

        public long ReadInt64()
        {
            return this.ReverseValue(this.reader.ReadInt64());
        }

        public ushort ReadUInt16()
        {
            return this.ReverseValue(this.reader.ReadUInt16());
        }

        public uint ReadUInt32()
        {
            return this.ReverseValue(this.reader.ReadUInt32());
        }

        public ulong ReadUInt64()
        {
            return this.ReverseValue(this.reader.ReadUInt64());
        }

        public long Position
        {
            get { return this.reader.BaseStream.Position; }
            set
            {
                this.reader.BaseStream.Position = value;
            }
        }

        private bool ReverseValue(bool value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToBoolean(bytes, 0);
        }

        private double ReverseValue(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        private short ReverseValue(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        private int ReverseValue(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        private long ReverseValue(long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        private ushort ReverseValue(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        private uint ReverseValue(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        private ulong ReverseValue(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        class InternalBinaryReader : BinaryReader
        {
            public InternalBinaryReader(Stream stream)
                : base(stream)
            {

            }
        }
    }

    internal class EndianReverser
    {
        public static bool convert(bool value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToBoolean(bytes, 0);
        }

        public static double convert(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        public static short convert(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        public static int convert(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static long convert(long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        public static ushort convert(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        public static uint convert(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static ulong convert(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public static bool getBoolean(BinaryReader br)
        {
            return convert(br.ReadBoolean());
        }

        public static double getDouble(BinaryReader br)
        {
            return convert(br.ReadDouble());
        }

        public static double[] getDouble(BinaryReader br, int count)
        {
            double[] values = new double[count];
            for (int i = 0; i < count; i++)
            {
                values[i] = convert(br.ReadDouble());
            }
            return values;
        }

        public static short getInt16(BinaryReader br)
        {
            return convert(br.ReadInt16());
        }

        public static int getInt32(BinaryReader br)
        {
            return convert(br.ReadInt32());
        }

        public static long getInt64(BinaryReader br)
        {
            return convert(br.ReadInt64());
        }

        public static ushort getUInt16(BinaryReader br)
        {
            return convert(br.ReadUInt16());
        }

        public static uint getUInt32(BinaryReader br)
        {
            return convert(br.ReadUInt32());
        }

        public static ulong getUInt64(BinaryReader br)
        {
            return convert(br.ReadUInt64());
        }
    }
}

