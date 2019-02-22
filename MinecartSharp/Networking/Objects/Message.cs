using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MinecartSharp.Networking.Objects
{
    class Message
    {
        private readonly List<byte> _buffer = new List<byte>();
        private readonly NetworkStream _stream;
        private int _lastByte;

        public byte[] Data = new byte[4096];

        public Message(NetworkStream stream)
        {
            _stream = stream;
        }

        public int ReadByte()
        {
            byte returnData = Data[_lastByte];
            _lastByte++;

            return returnData;
        }

        private byte[] Read(int length)
        {
            byte[] buffered = new byte[length];

            Array.Copy(Data, _lastByte, buffered, 0, length);
            _lastByte += length;

            return buffered;
        }

        public float ReadFloat()
        {
            byte[] data = Read(4);

            return BitConverter.ToSingle(data, 0);
        }

        public bool ReadBool()
        {
            int data = ReadByte();

            if (data == 1)
                return true;
            else
                return false;
        }

        public double ReadDouble()
        {
            byte[] data = Read(8);

            return BitConverter.ToDouble(data, 0);
        }

        public int ReadVarInt()
        {
            int value = 0;
            int size = 0;
            int b;

            while (((b = ReadByte()) & 0x80) == 0x80)
            {
                value |= (b & 0x7F) << (size++ * 7);
                if (size > 5)
                {
                    throw new IOException("VarInt is too long.");
                }
            }

            return value | ((b & 0x7F) << (size * 7));
        }

        public long ReadVarLong()
        {
            int value = 0;
            int size = 0;
            int b;

            while (((b = ReadByte()) & 0x80) == 0x80)
            {
                value |= (b & 0x7F) << (size++ * 7);
                if (size > 10)
                {
                    throw new IOException("VarLong is too long.");
                }
            }

            return value | ((b & 0x7F) << (size * 7));
        }

        public short ReadShort()
        {
            int o = ReadByte();
            int i = ReadByte();

            if (BitConverter.IsLittleEndian)
                return BitConverter.ToInt16(new byte[2] { (byte)i, (byte)o }, 0);
            else
                return BitConverter.ToInt16(new byte[2] { (byte)o, (byte)i }, 0);
        }

        public string ReadString()
        {
            int length = ReadVarInt();
            byte[] value = Read(length);

            return Encoding.UTF8.GetString(value);
        }

        public string ReadUsername()
        {
            byte[] username = Encoding.UTF8.GetBytes(ReadString());
            List<byte> t = new List<byte>();

            int D = 0;
            foreach (byte i in username)
            {
                if (D > 1)
                {
                    t.Add(i);
                }
                D++;
            }
            return Encoding.UTF8.GetString(t.ToArray());
        }

        public void Write(byte[] data, int offset, int length)
        {
            for (int i = 0; i < length; i++)
            {
                _buffer.Add(Data[i + offset]);
            }
        }

        public void Write(byte[] data)
        {
            foreach (byte i in data)
            {
                _buffer.Add(i);
            }
        }

        public void WriteVarInt(int integer)
        {
            while ((integer & -128) != 0)
            {
                _buffer.Add((byte)(integer & 127 | 128));
                integer = (int)(((uint)integer) >> 7);
            }

            _buffer.Add((byte)integer);
        }

        public void WriteVarLong(long i)
        {
            long data = i;
            while ((data & ~0x7F) != 0)
            {
                _buffer.Add((byte)((data & 0x7F) | 0x80));
                data >>= 7;
            }
            _buffer.Add((byte)data);
        }

        public void WriteInt(int data)
        {
            byte[] buffer = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data));

            Write(buffer);
        }

        public void WriteString(string value)
        {
            byte[] data = Encoding.UTF8.GetBytes(value);

            WriteVarInt(data.Length);
            Write(data);
        }

        public void WriteShort(short value)
        {
            byte[] data = BitConverter.GetBytes(value);

            Write(data);
        }

        public void WriteByte(byte data)
        {
            _buffer.Add(data);
        }

        public void WriteBool(bool data)
        {
            Write(BitConverter.GetBytes(data));
        }

        public void WriteDouble(double data)
        {
            Write(BitConverter.GetBytes(data));
        }

        public void WriteFloat(float data)
        {
            Write(BitConverter.GetBytes(data));
        }

        public void WriteLong(long data)
        {
            Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data)));
        }

        public void FlushData()
        {
            try
            {
                byte[] data = _buffer.ToArray();
                _buffer.Clear();

                WriteVarInt(data.Length);
                byte[] buffer = _buffer.ToArray();

                _stream.Write(buffer, 0, buffer.Length);
                _stream.Write(data, 0, data.Length);
                _buffer.Clear();
            }
            catch
            {
                // ignored
            }
        }

        public void Dispose()
        {
            Data = null;
            _lastByte = 0;
        }
    }
}
