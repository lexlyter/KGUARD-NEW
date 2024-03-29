﻿// Decompiled with JetBrains decompiler
// Type: NetFwTypeLib.INetFwService
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFwTypeLib
{
  [Guid("79FD57C8-908E-4A36-9888-D5B3F0A444CF")]
  [TypeLibType(4160)]
  [ComImport]
  public interface INetFwService
  {
    [DispId(1)]
    string Name { [DispId(1), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

    [DispId(2)]
    NET_FW_SERVICE_TYPE_ Type { [DispId(2), MethodImpl(MethodImplOptions.InternalCall)] get; }

    [DispId(3)]
    bool Customized { [DispId(3), MethodImpl(MethodImplOptions.InternalCall)] get; }

    [DispId(4)]
    NET_FW_IP_VERSION_ IpVersion { [DispId(4), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(4), MethodImpl(MethodImplOptions.InternalCall)] set; }

    [DispId(5)]
    NET_FW_SCOPE_ Scope { [DispId(5), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(5), MethodImpl(MethodImplOptions.InternalCall)] set; }

    [DispId(6)]
    string RemoteAddresses { [DispId(6), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(6), MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.BStr)] set; }

    [DispId(7)]
    bool Enabled { [DispId(7), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(7), MethodImpl(MethodImplOptions.InternalCall)] set; }

    [DispId(8)]
    INetFwOpenPorts GloballyOpenPorts { [DispId(8), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
  }
}
