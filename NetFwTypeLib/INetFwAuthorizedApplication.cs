// Decompiled with JetBrains decompiler
// Type: NetFwTypeLib.INetFwAuthorizedApplication
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFwTypeLib
{
  [Guid("B5E64FFA-C2C5-444E-A301-FB5E00018050")]
  [TypeLibType(4160)]
  [ComImport]
  public interface INetFwAuthorizedApplication
  {
    [DispId(1)]
    string Name { [DispId(1), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(1), MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

    [DispId(2)]
    string ProcessImageFileName { [DispId(2), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(2), MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

    [DispId(3)]
    NET_FW_IP_VERSION_ IpVersion { [DispId(3), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(3), MethodImpl(MethodImplOptions.InternalCall)] set; }

    [DispId(4)]
    NET_FW_SCOPE_ Scope { [DispId(4), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(4), MethodImpl(MethodImplOptions.InternalCall)] set; }

    [DispId(5)]
    string RemoteAddresses { [DispId(5), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(5), MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

    [DispId(6)]
    bool Enabled { [DispId(6), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(6), MethodImpl(MethodImplOptions.InternalCall)] set; }
  }
}
