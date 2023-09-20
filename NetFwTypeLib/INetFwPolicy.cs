// Decompiled with JetBrains decompiler
// Type: NetFwTypeLib.INetFwPolicy
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetFwTypeLib
{
  [Guid("D46D2478-9AC9-4008-9DC7-5563CE5536CC")]
  [TypeLibType(4160)]
  [ComImport]
  public interface INetFwPolicy
  {
    [DispId(1)]
    INetFwProfile CurrentProfile { [DispId(1), MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

    [DispId(2)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    [return: MarshalAs(UnmanagedType.Interface)]
    INetFwProfile GetProfileByType([In] NET_FW_PROFILE_TYPE_ profileType);
  }
}
