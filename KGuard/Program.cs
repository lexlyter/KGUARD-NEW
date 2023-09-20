// Decompiled with JetBrains decompiler
// Type: KGuard.Program
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KGuard
{
  internal static class Program
  {
    public static SqlConnection sql = new SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=True;MultipleActiveResultSets=True;");
    public static string WebUrl = "http://www.k-guard.org/Lic/";
    public static string LocalIP = "";
    public static string Ver = "";
    public static string Days = "";
    public static string RegDate = "";
    public static string Email = "";
    public static string ProgVer = "5.09";
    public static WebClient ReqWebClient = new WebClient();
    public static string OzelIPAdresin = "25.152.5.158";
    private static string bast2;

    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.Run((Form) new Initial());
      if (Program.sql.State.ToString() != "Open")
        Application.Run((Form) new SetSQL());
      if (Program.sql.State.ToString() == "Open")
        Application.Run((Form) new MainForm());
      Application.Run((Form) new Closing());
    }

    public static string GetTableQuery(string Query)
    {
      try
      {
        Program.ReqWebClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        return Program.AsciitoTR(Program.ReqWebClient.DownloadString(Program.WebUrl + "Default.aspx?Query=" + Query));
      }
      catch
      {
        return "select 'Problem'";
      }
    }

    public static string GetLc()
    {
      try
      {
        string str = "";
        foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
          if (address.AddressFamily == AddressFamily.InterNetwork)
            str = address.ToString();
        }
        Program.LocalIP = str;
        Program.LocalIP = Program.OzelIPAdresin;
        return "<IP>" + Program.LocalIP + "</IP> <TIME></TIME> <VER>6</VER> <REGDATE>" + Program.bast2 + "</REGDATE> <EMAIL>By Punisher</EMAIL>";
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "PRG.GetLc");
        return "";
      }
    }

    public static string AddLic(string Epin)
    {
      try
      {
        Program.ReqWebClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        return Program.AsciitoTR(Program.ReqWebClient.DownloadString(Program.WebUrl + "AddLicense.aspx?Epin=" + Epin));
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
        return "";
      }
    }

    public static string TransferUser(string Mail)
    {
      try
      {
        Program.ReqWebClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        return Program.AsciitoTR(Program.ReqWebClient.DownloadString(Program.WebUrl + "TransferUser.aspx?Mail=" + Mail));
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
        return "";
      }
    }

    public static string AsciitoTR(string str)
    {
      str = Encoding.UTF8.GetString(Encoding.GetEncoding(1254).GetBytes(str));
      return str;
    }

    public static void RefreshLicense()
    {
      try
      {
        string lc = Program.GetLc();
        if (lc != "NoLicance" && lc.Contains("<EMAIL>"))
        {
          Program.LocalIP = new Regex("<IP>.*</IP>").Match(lc).Value.Replace("<IP>", "").Replace("</IP>", "");
          Program.Days = new Regex("<TIME>.*</TIME>").Match(lc).Value.Replace("<TIME>", "").Replace("</TIME>", "");
          Program.Ver = new Regex("<VER>.*</VER>").Match(lc).Value.Replace("<VER>", "").Replace("</VER>", "");
          Program.RegDate = new Regex("<REGDATE>.*</REGDATE>").Match(lc).Value.Replace("<REGDATE>", "").Replace("</REGDATE>", "");
          Program.Email = new Regex("<EMAIL>.*</EMAIL>").Match(lc).Value.Replace("<EMAIL>", "").Replace("</EMAIL>", "");
        }
        else
          Program.WriteError(lc, "Lisans");
      }
      catch
      {
      }
    }

    public static void WriteError(string msg, string kaynak)
    {
      try
      {
        StreamWriter streamWriter = new StreamWriter((Stream) new FileStream("KGUARD_ErrorLog.txt", FileMode.OpenOrCreate, FileAccess.Write));
        streamWriter.BaseStream.Seek(0L, SeekOrigin.End);
        streamWriter.WriteLine(">>> " + DateTime.Now.ToString() + " KAYNAK: [" + kaynak + "]\n{");
        streamWriter.WriteLine(msg);
        streamWriter.WriteLine("}\n\n");
        streamWriter.Close();
      }
      catch
      {
      }
    }

    public static void CheckHst()
    {
      try
      {
        FileStream fileStream = new FileStream("C:\\Windows\\System32\\drivers\\etc\\hosts", FileMode.Open, FileAccess.Read);
        StreamReader streamReader = new StreamReader((Stream) fileStream);
        string end = streamReader.ReadToEnd();
        streamReader.Close();
        fileStream.Close();
        if (!end.Contains("k-guard") && !end.Contains("ömerçolak") && !end.Contains("xn--merolak-wxa5k"))
          return;
        System.IO.File.WriteAllText("C:\\Windows\\System32\\drivers\\etc\\hosts", end.Replace("k-guard", "kguard").Replace("ömerçolak", "ömarçolak").Replace("xn--merolak-wxa5k", "xn--merolak-wwa5k"));
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "PCKHST");
      }
    }
  }
}
