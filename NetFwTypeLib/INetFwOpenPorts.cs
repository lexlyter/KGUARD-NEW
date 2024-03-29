﻿// Decompiled with JetBrains decompiler
// Type: NetFwTypeLib.INetFwOpenPorts
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFwTypeLib
{
  [Guid("C0E9D7FA-E07E-430A-B19A-090CE82D92E2")]
  [TypeLibType(4160)]
  [ComImport]
  public interface INetFwOpenPorts : IEnumerable
  {
    [DispId(1)]
    int Count { [DispId(1), MethodImpl(MethodImplOptions.InternalCall)] get; }

    [DispId(2)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    void Add([MarshalAs(UnmanagedType.Interface), In] INetFwOpenPort Port);

    [DispId(3)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    void Remove([In] int portNumber, [In] NET_FW_IP_PROTOCOL_ ipProtocol);

    [DispId(4)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [return: MarshalAs(UnmanagedType.Interface)]
    INetFwOpenPort Item([In] int portNumber, [In] NET_FW_IP_PROTOCOL_ ipProtocol);

    [DispId(-4)]
    [TypeLibFunc(1)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler")]
    new IEnumerator GetEnumerator();
  }
}
