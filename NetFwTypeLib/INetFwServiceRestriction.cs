﻿// Decompiled with JetBrains decompiler
// Type: NetFwTypeLib.INetFwServiceRestriction
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFwTypeLib
{
  [Guid("8267BBE3-F890-491C-B7B6-2DB1EF0E5D2B")]
  [TypeLibType(4160)]
  [ComImport]
  public interface INetFwServiceRestriction
  {
    [DispId(3)]
    INetFwRules Rules { [DispId(3), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(1)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    void RestrictService(
      [MarshalAs(UnmanagedType.BStr), In] string serviceName,
      [MarshalAs(UnmanagedType.BStr), In] string appName,
      [In] bool RestrictService,
      [In] bool serviceSidRestricted);

    [DispId(2)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    bool ServiceRestricted([MarshalAs(UnmanagedType.BStr), In] string serviceName, [MarshalAs(UnmanagedType.BStr), In] string appName);
  }
}
