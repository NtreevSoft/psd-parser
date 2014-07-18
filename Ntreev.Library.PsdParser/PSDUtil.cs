namespace Ntreev.Library.PsdParser
{
    using System;
    using System.IO;
    using System.Text;

    public sealed class PSDUtil
    {
        public static void decodeRLE(byte[] src, byte[] dst, int packedLength, int unpackedLength)
        {
            int index = 0;
            int num2 = 0;
            int num3 = 0;
            byte num4 = 0;
            int num5 = unpackedLength;
            int num6 = packedLength;
            while ((num5 > 0) && (num6 > 0))
            {
                num3 = src[index++];
                num6--;
                if (num3 != 0x80)
                {
                    if (num3 > 0x80)
                    {
                        num3 -= 0x100;
                    }
                    if (num3 < 0)
                    {
                        num3 = 1 - num3;
                        if (num6 == 0)
                        {
                            throw new Exception("Input buffer exhausted in replicate");
                        }
                        if (num3 > num5)
                        {
                            throw new Exception(string.Format("Overrun in packbits replicate of {0} chars", num3 - num5));
                        }
                        num4 = src[index];
                        while (num3 > 0)
                        {
                            if (num5 == 0)
                            {
                                break;
                            }
                            dst[num2++] = num4;
                            num5--;
                            num3--;
                        }
                        if (num5 > 0)
                        {
                            index++;
                            num6--;
                        }
                        continue;
                    }
                    num3++;
                    while (num3 > 0)
                    {
                        if (num6 == 0)
                        {
                            throw new Exception("Input buffer exhausted in copy");
                        }
                        if (num5 == 0)
                        {
                            throw new Exception("Output buffer exhausted in copy");
                        }
                        dst[num2++] = src[index++];
                        num5--;
                        num6--;
                        num3--;
                    }
                }
            }
            if (num5 > 0)
            {
                for (num3 = 0; num3 < num6; num3++)
                {
                    dst[num2++] = 0;
                }
            }
        }

        public static string readAscii(BinaryReader br, int length)
        {
            return Encoding.ASCII.GetString(br.ReadBytes(length));
        }

        public static string readPascalString(BinaryReader br, int modLength)
        {
            byte count = br.ReadByte();
            string str = "";
            if (count == 0)
            {
                Stream baseStream = br.BaseStream;
                baseStream.Position += modLength - 1;
                return str;
            }
            byte[] bytes = br.ReadBytes(count);
            str = Encoding.UTF8.GetString(bytes);
            for (int i = count + 1; (i % modLength) != 0; i++)
            {
                Stream stream2 = br.BaseStream;
                stream2.Position += 1L;
            }
            return str;
        }

        public static string readUnicodeString(BinaryReader br)
        {
            int num = 4;
            int num2 = EndianReverser.getInt32(br);
            string str = "";
            if (num2 == 0)
            {
                Stream baseStream = br.BaseStream;
                baseStream.Position += num - 1;
                return str;
            }
            byte[] bytes = br.ReadBytes(num2 * 2);
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
                Stream stream2 = br.BaseStream;
                stream2.Position += 1L;
            }
            return str;
        }
    }
}

