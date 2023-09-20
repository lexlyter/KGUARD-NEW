// Decompiled with JetBrains decompiler
// Type: SilkroadSecurityApi.Packet
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System;
using System.IO;
using System.Text;

namespace SilkroadSecurityApi
{
  public class Packet
  {
    private ushort m_opcode;
    private PacketWriter m_writer;
    private PacketReader m_reader;
    private bool m_encrypted;
    private bool m_massive;
    private bool m_locked;
    private byte[] m_reader_bytes;
    private object m_lock;

    public ushort Opcode => this.m_opcode;

    public bool Encrypted => this.m_encrypted;

    public bool Massive => this.m_massive;

    public Packet(Packet rhs)
    {
      lock (rhs.m_lock)
      {
        this.m_lock = new object();
        this.m_opcode = rhs.m_opcode;
        this.m_encrypted = rhs.m_encrypted;
        this.m_massive = rhs.m_massive;
        this.m_locked = rhs.m_locked;
        if (!this.m_locked)
        {
          this.m_writer = new PacketWriter();
          this.m_reader = (PacketReader) null;
          this.m_reader_bytes = (byte[]) null;
          this.m_writer.Write(rhs.m_writer.GetBytes());
        }
        else
        {
          this.m_writer = (PacketWriter) null;
          this.m_reader_bytes = rhs.m_reader_bytes;
          this.m_reader = new PacketReader(this.m_reader_bytes);
        }
      }
    }

    public Packet(ushort opcode)
    {
      this.m_lock = new object();
      this.m_opcode = opcode;
      this.m_encrypted = false;
      this.m_massive = false;
      this.m_writer = new PacketWriter();
      this.m_reader = (PacketReader) null;
      this.m_reader_bytes = (byte[]) null;
    }

    public Packet(ushort opcode, bool encrypted)
    {
      this.m_lock = new object();
      this.m_opcode = opcode;
      this.m_encrypted = encrypted;
      this.m_massive = false;
      this.m_writer = new PacketWriter();
      this.m_reader = (PacketReader) null;
      this.m_reader_bytes = (byte[]) null;
    }

    public Packet(ushort opcode, bool encrypted, bool massive)
    {
      if (encrypted & massive)
        throw new Exception("[Packet::Packet] Packets cannot both be massive and encrypted!");
      this.m_lock = new object();
      this.m_opcode = opcode;
      this.m_encrypted = encrypted;
      this.m_massive = massive;
      this.m_writer = new PacketWriter();
      this.m_reader = (PacketReader) null;
      this.m_reader_bytes = (byte[]) null;
    }

    public Packet(ushort opcode, bool encrypted, bool massive, byte[] bytes)
    {
      if (encrypted & massive)
        throw new Exception("[Packet::Packet] Packets cannot both be massive and encrypted!");
      this.m_lock = new object();
      this.m_opcode = opcode;
      this.m_encrypted = encrypted;
      this.m_massive = massive;
      this.m_writer = new PacketWriter();
      this.m_writer.Write(bytes);
      this.m_reader = (PacketReader) null;
      this.m_reader_bytes = (byte[]) null;
    }

    public Packet(
      ushort opcode,
      bool encrypted,
      bool massive,
      byte[] bytes,
      int offset,
      int length)
    {
      if (encrypted & massive)
        throw new Exception("[Packet::Packet] Packets cannot both be massive and encrypted!");
      this.m_lock = new object();
      this.m_opcode = opcode;
      this.m_encrypted = encrypted;
      this.m_massive = massive;
      this.m_writer = new PacketWriter();
      this.m_writer.Write(bytes, offset, length);
      this.m_reader = (PacketReader) null;
      this.m_reader_bytes = (byte[]) null;
    }

    public byte[] GetBytes()
    {
      lock (this.m_lock)
        return this.m_locked ? this.m_reader_bytes : this.m_writer.GetBytes();
    }

    public void Lock()
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          return;
        this.m_reader_bytes = this.m_writer.GetBytes();
        this.m_reader = new PacketReader(this.m_reader_bytes);
        this.m_writer.Close();
        this.m_writer = (PacketWriter) null;
        this.m_locked = true;
      }
    }

    public long SeekRead(long offset, SeekOrigin orgin)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot SeekRead on an unlocked Packet.");
        return this.m_reader.BaseStream.Seek(offset, orgin);
      }
    }

    public int RemainingRead()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot SeekRead on an unlocked Packet.");
        return (int) (this.m_reader.BaseStream.Length - this.m_reader.BaseStream.Position);
      }
    }

    public byte ReadUInt8()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        return this.m_reader.ReadByte();
      }
    }

    public sbyte ReadInt8()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        return this.m_reader.ReadSByte();
      }
    }

    public ushort ReadUInt16()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        return this.m_reader.ReadUInt16();
      }
    }

    public short ReadInt16()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        return this.m_reader.ReadInt16();
      }
    }

    public uint ReadUInt32()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        return this.m_reader.ReadUInt32();
      }
    }

    public int ReadInt32()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        return this.m_reader.ReadInt32();
      }
    }

    public ulong ReadUInt64()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        return this.m_reader.ReadUInt64();
      }
    }

    public long ReadInt64()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        return this.m_reader.ReadInt64();
      }
    }

    public float ReadSingle()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        return this.m_reader.ReadSingle();
      }
    }

    public double ReadDouble()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        return this.m_reader.ReadDouble();
      }
    }

    public string ReadAscii() => this.ReadAscii(1254).Replace("'", "");

    public string ReadAscii(int codepage)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        byte[] bytes = this.m_reader.ReadBytes((int) this.m_reader.ReadUInt16());
        return Encoding.GetEncoding(codepage).GetString(bytes);
      }
    }

    public string ReadUnicode()
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        return Encoding.Unicode.GetString(this.m_reader.ReadBytes((int) this.m_reader.ReadUInt16() * 2));
      }
    }

    public byte[] ReadUInt8Array(int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        byte[] numArray = new byte[count];
        for (int index = 0; index < count; ++index)
          numArray[index] = this.m_reader.ReadByte();
        return numArray;
      }
    }

    public sbyte[] ReadInt8Array(int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        sbyte[] numArray = new sbyte[count];
        for (int index = 0; index < count; ++index)
          numArray[index] = this.m_reader.ReadSByte();
        return numArray;
      }
    }

    public ushort[] ReadUInt16Array(int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        ushort[] numArray = new ushort[count];
        for (int index = 0; index < count; ++index)
          numArray[index] = this.m_reader.ReadUInt16();
        return numArray;
      }
    }

    public short[] ReadInt16Array(int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        short[] numArray = new short[count];
        for (int index = 0; index < count; ++index)
          numArray[index] = this.m_reader.ReadInt16();
        return numArray;
      }
    }

    public uint[] ReadUInt32Array(int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        uint[] numArray = new uint[count];
        for (int index = 0; index < count; ++index)
          numArray[index] = this.m_reader.ReadUInt32();
        return numArray;
      }
    }

    public int[] ReadInt32Array(int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        int[] numArray = new int[count];
        for (int index = 0; index < count; ++index)
          numArray[index] = this.m_reader.ReadInt32();
        return numArray;
      }
    }

    public ulong[] ReadUInt64Array(int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        ulong[] numArray = new ulong[count];
        for (int index = 0; index < count; ++index)
          numArray[index] = this.m_reader.ReadUInt64();
        return numArray;
      }
    }

    public long[] ReadInt64Array(int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        long[] numArray = new long[count];
        for (int index = 0; index < count; ++index)
          numArray[index] = this.m_reader.ReadInt64();
        return numArray;
      }
    }

    public float[] ReadSingleArray(int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        float[] numArray = new float[count];
        for (int index = 0; index < count; ++index)
          numArray[index] = this.m_reader.ReadSingle();
        return numArray;
      }
    }

    public double[] ReadDoubleArray(int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        double[] numArray = new double[count];
        for (int index = 0; index < count; ++index)
          numArray[index] = this.m_reader.ReadDouble();
        return numArray;
      }
    }

    public string[] ReadAsciiArray(int count) => this.ReadAsciiArray(1252);

    public string[] ReadAsciiArray(int codepage, int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        string[] strArray = new string[count];
        for (int index = 0; index < count; ++index)
        {
          byte[] bytes = this.m_reader.ReadBytes((int) this.m_reader.ReadUInt16());
          strArray[index] = Encoding.UTF7.GetString(bytes);
        }
        return strArray;
      }
    }

    public string[] ReadUnicodeArray(int count)
    {
      lock (this.m_lock)
      {
        if (!this.m_locked)
          throw new Exception("Cannot Read from an unlocked Packet.");
        string[] strArray = new string[count];
        for (int index = 0; index < count; ++index)
        {
          byte[] bytes = this.m_reader.ReadBytes((int) this.m_reader.ReadUInt16() * 2);
          strArray[index] = Encoding.Unicode.GetString(bytes);
        }
        return strArray;
      }
    }

    public long SeekWrite(long offset, SeekOrigin orgin)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot SeekWrite on a locked Packet.");
        return this.m_writer.BaseStream.Seek(offset, orgin);
      }
    }

    public void WriteUInt8(byte value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(value);
      }
    }

    public void WriteInt8(sbyte value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(value);
      }
    }

    public void WriteUInt16(ushort value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(value);
      }
    }

    public void WriteInt16(short value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(value);
      }
    }

    public void WriteUInt32(uint value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(value);
      }
    }

    public void WriteInt32(int value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(value);
      }
    }

    public void WriteUInt64(ulong value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(value);
      }
    }

    public void WriteInt64(long value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(value);
      }
    }

    public void WriteSingle(float value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(value);
      }
    }

    public void WriteDouble(double value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(value);
      }
    }

    public void WriteAscii(string value) => this.WriteAscii(value, 1254);

    public void WriteAscii(string value, int code_page)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        byte[] bytes = Encoding.GetEncoding(code_page).GetBytes(value);
        this.m_writer.Write((ushort) bytes.Length);
        this.m_writer.Write(bytes);
      }
    }

    public void WriteUnicode(string value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        byte[] bytes = Encoding.Unicode.GetBytes(value);
        this.m_writer.Write((ushort) value.ToString().Length);
        this.m_writer.Write(bytes);
      }
    }

    public void WriteUInt8(object value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write((byte) (Convert.ToUInt64(value) & (ulong) byte.MaxValue));
      }
    }

    public void WriteInt8(object value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write((sbyte) (Convert.ToInt64(value) & (long) byte.MaxValue));
      }
    }

    public void WriteUInt16(object value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write((ushort) (Convert.ToUInt64(value) & (ulong) ushort.MaxValue));
      }
    }

    public void WriteInt16(object value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write((ushort) ((ulong) Convert.ToInt64(value) & (ulong) ushort.MaxValue));
      }
    }

    public void WriteUInt32(object value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write((uint) (Convert.ToUInt64(value) & (ulong) uint.MaxValue));
      }
    }

    public void WriteInt32(object value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write((int) (Convert.ToInt64(value) & (long) uint.MaxValue));
      }
    }

    public void WriteUInt64(object value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(Convert.ToUInt64(value));
      }
    }

    public void WriteInt64(object value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(Convert.ToInt64(value));
      }
    }

    public void WriteSingle(object value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(Convert.ToSingle(value));
      }
    }

    public void WriteDouble(object value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        this.m_writer.Write(Convert.ToDouble(value));
      }
    }

    public void WriteAscii(object value) => this.WriteAscii(value, 1252);

    public void WriteAscii(object value, int code_page)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        byte[] bytes = Encoding.Default.GetBytes(Encoding.UTF7.GetString(Encoding.GetEncoding(code_page).GetBytes(value.ToString())));
        this.m_writer.Write((ushort) bytes.Length);
        this.m_writer.Write(bytes);
      }
    }

    public void WriteUnicode(object value)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        byte[] bytes = Encoding.Unicode.GetBytes(value.ToString());
        this.m_writer.Write((ushort) value.ToString().Length);
        this.m_writer.Write(bytes);
      }
    }

    public void WriteUInt8Array(byte[] values)
    {
      if (this.m_locked)
        throw new Exception("Cannot Write to a locked Packet.");
      this.m_writer.Write(values);
    }

    public void WriteUInt8Array(byte[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.m_writer.Write(values[index1]);
      }
    }

    public void WriteUInt16Array(ushort[] values) => this.WriteUInt16Array(values, 0, values.Length);

    public void WriteUInt16Array(ushort[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.m_writer.Write(values[index1]);
      }
    }

    public void WriteInt16Array(short[] values) => this.WriteInt16Array(values, 0, values.Length);

    public void WriteInt16Array(short[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.m_writer.Write(values[index1]);
      }
    }

    public void WriteUInt32Array(uint[] values) => this.WriteUInt32Array(values, 0, values.Length);

    public void WriteUInt32Array(uint[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.m_writer.Write(values[index1]);
      }
    }

    public void WriteInt32Array(int[] values) => this.WriteInt32Array(values, 0, values.Length);

    public void WriteInt32Array(int[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.m_writer.Write(values[index1]);
      }
    }

    public void WriteUInt64Array(ulong[] values) => this.WriteUInt64Array(values, 0, values.Length);

    public void WriteUInt64Array(ulong[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.m_writer.Write(values[index1]);
      }
    }

    public void WriteInt64Array(long[] values) => this.WriteInt64Array(values, 0, values.Length);

    public void WriteInt64Array(long[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.m_writer.Write(values[index1]);
      }
    }

    public void WriteSingleArray(float[] values) => this.WriteSingleArray(values, 0, values.Length);

    public void WriteSingleArray(float[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.m_writer.Write(values[index1]);
      }
    }

    public void WriteDoubleArray(double[] values) => this.WriteDoubleArray(values, 0, values.Length);

    public void WriteDoubleArray(double[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.m_writer.Write(values[index1]);
      }
    }

    public void WriteAsciiArray(string[] values, int codepage) => this.WriteAsciiArray(values, 0, values.Length, codepage);

    public void WriteAsciiArray(string[] values, int index, int count, int codepage)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteAscii(values[index1], codepage);
      }
    }

    public void WriteAsciiArray(string[] values) => this.WriteAsciiArray(values, 0, values.Length, 1252);

    public void WriteAsciiArray(string[] values, int index, int count) => this.WriteAsciiArray(values, index, count, 1252);

    public void WriteUnicodeArray(string[] values) => this.WriteUnicodeArray(values, 0, values.Length);

    public void WriteUnicodeArray(string[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteUnicode(values[index1]);
      }
    }

    public void WriteUInt8Array(object[] values) => this.WriteUInt8Array(values, 0, values.Length);

    public void WriteUInt8Array(object[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteUInt8(values[index1]);
      }
    }

    public void WriteInt8Array(object[] values) => this.WriteInt8Array(values, 0, values.Length);

    public void WriteInt8Array(object[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteInt8(values[index1]);
      }
    }

    public void WriteUInt16Array(object[] values) => this.WriteUInt16Array(values, 0, values.Length);

    public void WriteUInt16Array(object[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteUInt16(values[index1]);
      }
    }

    public void WriteInt16Array(object[] values) => this.WriteInt16Array(values, 0, values.Length);

    public void WriteInt16Array(object[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteInt16(values[index1]);
      }
    }

    public void WriteUInt32Array(object[] values) => this.WriteUInt32Array(values, 0, values.Length);

    public void WriteUInt32Array(object[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteUInt32(values[index1]);
      }
    }

    public void WriteInt32Array(object[] values) => this.WriteInt32Array(values, 0, values.Length);

    public void WriteInt32Array(object[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteInt32(values[index1]);
      }
    }

    public void WriteUInt64Array(object[] values) => this.WriteUInt64Array(values, 0, values.Length);

    public void WriteUInt64Array(object[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteUInt64(values[index1]);
      }
    }

    public void WriteInt64Array(object[] values) => this.WriteInt64Array(values, 0, values.Length);

    public void WriteInt64Array(object[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteInt64(values[index1]);
      }
    }

    public void WriteSingleArray(object[] values) => this.WriteSingleArray(values, 0, values.Length);

    public void WriteSingleArray(object[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteSingle(values[index1]);
      }
    }

    public void WriteDoubleArray(object[] values) => this.WriteDoubleArray(values, 0, values.Length);

    public void WriteDoubleArray(object[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteDouble(values[index1]);
      }
    }

    public void WriteAsciiArray(object[] values, int codepage) => this.WriteAsciiArray(values, 0, values.Length, codepage);

    public void WriteAsciiArray(object[] values, int index, int count, int codepage)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteAscii(values[index1].ToString(), codepage);
      }
    }

    public void WriteAsciiArray(object[] values) => this.WriteAsciiArray(values, 0, values.Length, 1252);

    public void WriteAsciiArray(object[] values, int index, int count) => this.WriteAsciiArray(values, index, count, 1252);

    public void WriteUnicodeArray(object[] values) => this.WriteUnicodeArray(values, 0, values.Length);

    public void WriteUnicodeArray(object[] values, int index, int count)
    {
      lock (this.m_lock)
      {
        if (this.m_locked)
          throw new Exception("Cannot Write to a locked Packet.");
        for (int index1 = index; index1 < index + count; ++index1)
          this.WriteUnicode(values[index1].ToString());
      }
    }
  }
}
