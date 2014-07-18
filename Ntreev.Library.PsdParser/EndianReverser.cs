using System;
using System.IO;

namespace Ntreev.Library.PsdParser
{
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

