// Decompiled with JetBrains decompiler
// Type: KGuard.Log
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using SilkroadSecurityApi;
using System;
using System.IO;

namespace KGuard
{
  internal static class Log
  {
    public static void PacketLog(string txtname, string note, Packet packet)
    {
      byte[] bytes = packet.GetBytes();
      Log.LogPck(txtname, DateTime.Now.ToShortTimeString() + " [" + note + "][{0:X4}][{1} bytes]{2}{3}{4}{5}{6}", (object) packet.Opcode, (object) bytes.Length, packet.Encrypted ? (object) "[Encrypted]" : (object) "", packet.Massive ? (object) "[Massive]" : (object) "", (object) Environment.NewLine, (object) Utility.HexDump(bytes), (object) Environment.NewLine);
    }

    private static void LogPck(string txtname, string msg, params object[] values)
    {
      msg = string.Format(msg, values);
      StreamWriter streamWriter = new StreamWriter((Stream) new FileStream(txtname + ".txt", FileMode.OpenOrCreate, FileAccess.Write));
      streamWriter.BaseStream.Seek(0L, SeekOrigin.End);
      streamWriter.WriteLine(msg);
      streamWriter.WriteLine();
      streamWriter.Close();
    }

    public static void WriteError(Exception excpt, string kaynak)
    {
      try
      {
        StreamWriter streamWriter = new StreamWriter((Stream) new FileStream("KGuard_ErrorLog.txt", FileMode.OpenOrCreate, FileAccess.Write));
        streamWriter.BaseStream.Seek(0L, SeekOrigin.End);
        streamWriter.WriteLine(">>> " + DateTime.Now.ToString() + " KAYNAK: [" + kaynak + "]\n{");
        streamWriter.WriteLine(excpt.ToString());
        streamWriter.WriteLine("}\n");
        streamWriter.Close();
      }
      catch
      {
      }
    }
  }
}
