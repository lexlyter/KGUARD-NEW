// Decompiled with JetBrains decompiler
// Type: SilkroadSecurityApi.PacketReader
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System.IO;

namespace SilkroadSecurityApi
{
  internal class PacketReader : BinaryReader
  {
    private byte[] m_input;

    public PacketReader(byte[] input)
      : base((Stream) new MemoryStream(input, false))
      => this.m_input = input;

    public PacketReader(byte[] input, int index, int count)
      : base((Stream) new MemoryStream(input, index, count, false))
      => this.m_input = input;
  }
}
