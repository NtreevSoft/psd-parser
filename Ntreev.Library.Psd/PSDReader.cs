using System;
using System.IO;
using System.Text;

namespace Ntreev.Library.Psd
{
    class PSDReader : IDisposable
    {
        private readonly BinaryReader reader;
        private int version = 1;

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
            return ReadUnicodeString2();
            int num = 4;
            int num2 = this.ReadInt32();
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
            //for (int j = num2 + 1; (j % num) != 0; j++)
            //{
            //    Stream stream2 = this.reader.BaseStream;
            //    stream2.Position += 1L;
            //}
            return str;
        }

        public string ReadUnicodeString2()
        {
            int length = this.ReadInt32();
            if (length == 0)
                return string.Empty;

            byte[] bytes = this.ReadBytes(length * 2);
            for (int i = 0; i < length; i++)
            {
                int index = i * 2;
                byte b = bytes[index];
                bytes[index] = bytes[index + 1];
                bytes[index + 1] = b;
            }

            if (bytes[bytes.Length - 1] == 0 && bytes[bytes.Length - 2] == 0)
            {
                length--;
            }

            return Encoding.Unicode.GetString(bytes, 0, length * 2);
        }

        public string ReadStringOrKey()
        {
            int length = this.ReadInt32();
            length = (length > 0) ? length : 4;
            return this.ReadAscii(length);
        }

        public int Read(byte[] buffer, int index, int count)
        {
            return this.reader.Read(buffer, index, count);
        }

        public byte ReadByte()
        {
            return this.reader.ReadByte();
        }

        public byte[] ReadBytes(int count)
        {
            return this.reader.ReadBytes(count);
        }

        public bool ReadBoolean()
        {
            return this.ReverseValue(this.reader.ReadBoolean());
        }

        public double ReadDouble()
        {
            return this.ReverseValue(this.reader.ReadDouble());
        }

        public double[] ReadDoubles(int count)
        {
            double[] values = new double[count];
            for (int i = 0; i < count; i++)
            {
                values[i] = this.ReadDouble();
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

        public int ReadLength()
        {
            if (this.version == 1)
                return this.ReadInt32();
            return (int)this.ReadInt64();
        }

        public long Position
        {
            get { return this.reader.BaseStream.Position; }
            set
            {
                this.reader.BaseStream.Position = value;
            }
        }

        public long Length
        {
            get { return this.reader.BaseStream.Length; }
        }

        public int Version
        {
            get { return this.version; }
            set { this.version = value; }
        }

        internal BinaryReader InternalReader
        {
            get { return this.reader; }
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
}


