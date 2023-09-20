// Decompiled with JetBrains decompiler
// Type: NetFwTypeLib.INetFwRemoteAdminSettings
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFwTypeLib
{
  [Guid("D4BECDDF-6F73-4A83-B832-9C66874CD20E")]
  [TypeLibType(4160)]
  [ComImport]
  public interface INetFwRemoteAdminSettings
  {
    [DispId(1)]
    NET_FW_IP_VERSION_ IpVersion { [DispId(1), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(1), MethodImpl(MethodImplOptions.InternalCall)] set; }

    [DispId(2)]
    NET_FW_SCOPE_ Scope { [DispId(2), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(2), MethodImpl(MethodImplOptions.InternalCall)] set; }

    [DispId(3)]
    string RemoteAddresses { [DispId(3), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(3), MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

    [DispId(4)]
    bool Enabled { [DispId(4), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(4), MethodImpl(MethodImplOptions.InternalCall)] set; }
  }
}
