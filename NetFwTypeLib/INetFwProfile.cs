﻿// Decompiled with JetBrains decompiler
// Type: NetFwTypeLib.INetFwProfile
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFwTypeLib
{
  [Guid("174A0DDA-E9F9-449D-993B-21AB667CA456")]
  [TypeLibType(4160)]
  [ComImport]
  public interface INetFwProfile
  {
    [DispId(1)]
    NET_FW_PROFILE_TYPE_ Type { [DispId(1), MethodImpl(MethodImplOptions.InternalCall)] get; }

    [DispId(2)]
    bool FirewallEnabled { [DispId(2), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(2), MethodImpl(MethodImplOptions.InternalCall)] set; }

    [DispId(3)]
    bool ExceptionsNotAllowed { [DispId(3), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(3), MethodImpl(MethodImplOptions.InternalCall)] set; }

    [DispId(4)]
    bool NotificationsDisabled { [DispId(4), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(4), MethodImpl(MethodImplOptions.InternalCall)] set; }

    [DispId(5)]
    bool UnicastResponsesToMulticastBroadcastDisabled { [DispId(5), MethodImpl(MethodImplOptions.InternalCall)] get; [DispId(5), MethodImpl(MethodImplOptions.InternalCall)] set; }

    [DispId(6)]
    INetFwRemoteAdminSettings RemoteAdminSettings { [DispId(6), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(7)]
    INetFwIcmpSettings IcmpSettings { [DispId(7), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(8)]
    INetFwOpenPorts GloballyOpenPorts { [DispId(8), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(9)]
    INetFwServices Services { [DispId(9), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(10)]
    INetFwAuthorizedApplications AuthorizedApplications { [DispId(10), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }
  }
}
