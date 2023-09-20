// Decompiled with JetBrains decompiler
// Type: SilkroadSecurityApi.PacketWriter
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System.IO;

namespace SilkroadSecurityApi
{
  internal class PacketWriter : BinaryWriter
  {
    private MemoryStream m_ms;

    public PacketWriter()
    {
      this.m_ms = new MemoryStream();
      this.OutStream = (Stream) this.m_ms;
    }

    public byte[] GetBytes() => this.m_ms.ToArray();
  }
}
