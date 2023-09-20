// Decompiled with JetBrains decompiler
// Type: KGuard.Checks
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace KGuard
{
  internal static class Checks
  {
    private static SqlDataReader dr;

    public static void CheckMtx()
    {
      bool createdNew;
      Mutex mutex = new Mutex(true, "Program", out createdNew);
      if (!createdNew)
      {
        if (MessageBox.Show("Program zaten açık. Kapatılıp yeniden açılmasını ister misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          Process[] processesByName = Process.GetProcessesByName("KGuard");
          string str = Process.GetCurrentProcess().Id.ToString();
          foreach (Process process in processesByName)
          {
            if (process.Id.ToString() != str)
              process.Kill();
          }
        }
        else
          Environment.Exit(0);
      }
      else
      {
        Process[] processesByName = Process.GetProcessesByName("KGuard");
        string str = Process.GetCurrentProcess().Id.ToString();
        foreach (Process process in processesByName)
        {
          if (process.Id.ToString() != str)
            process.Kill();
        }
      }
    }
  }
}
