// Decompiled with JetBrains decompiler
// Type: KGuard.ClientsAgent
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using SilkroadSecurityApi;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace KGuard
{
  public class ClientsAgent
  {
    private Socket C_Socket;
    private Socket S_Socket;
    private Security C_Security;
    private Security S_Security;
    private TransferBuffer RecvBuffer_Local;
    private TransferBuffer RecvBuffer_Remote;
    private List<Packet> RecvPackets_FromClient;
    private List<Packet> RecvPackets_FromServer;
    private List<KeyValuePair<TransferBuffer, Packet>> SendBuffers_Local;
    private List<KeyValuePair<TransferBuffer, Packet>> SendBuffers_Remote;
    private bool Local = true;
    private bool Remote = false;
    private string IP;
    private DateTime PacketTimer = DateTime.Now;
    private int PacketCount = 0;
    private List<int> Exploits = new List<int>()
    {
      0,
      30583,
      25373,
      4101,
      8197,
      8202,
      40963,
      4097,
      4098,
      4099,
      4100,
      4102,
      4103,
      8977,
      8710,
      8713,
      8714,
      8718,
      8719,
      24579,
      24581,
      24582,
      24837,
      25101,
      25344,
      24848,
      25095,
      25352,
      25362,
      25347,
      26885,
      26898,
      25365,
      26882,
      40966,
      40968,
      41734,
      41480,
      41472,
      41475,
      17476,
      13489,
      13522,
      14431,
      13584,
      13499
    };
    private List<string> Avatar_Mail = new List<string>()
    {
      "MATTR_AVATAR_INT",
      "MATTR_AVATAR_STR",
      "MATTR_AVATAR_HPRG",
      "MATTR_AVATAR_HR",
      "MATTR_AVATAR_DRUA",
      "MATTR_AVATAR_HP",
      "MATTR_AVATAR_INT_2",
      "MATTR_AVATAR_INT_3",
      "MATTR_AVATAR_INT_4",
      "MATTR_AVATAR_LUCK",
      "MATTR_AVATAR_LUCK_2",
      "MATTR_AVATAR_LUCK_3",
      "MATTR_AVATAR_LUCK_4",
      "MATTR_AVATAR_MDIA",
      "MATTR_AVATAR_MDIA_2",
      "MATTR_AVATAR_MDIA_3",
      "MATTR_AVATAR_MDIA_4",
      "MATTR_AVATAR_STR_2",
      "MATTR_AVATAR_STR_3",
      "MATTR_AVATAR_STR_4"
    };
    private List<string> Avatar_Helm = new List<string>()
    {
      "MATTR_AVATAR_INT",
      "MATTR_AVATAR_STR",
      "MATTR_AVATAR_DARA",
      "MATTR_AVATAR_ER",
      "MATTR_AVATAR_MP",
      "MATTR_AVATAR_MPRG"
    };
    private List<string> Avatar_Addon = new List<string>()
    {
      "MATTR_AVATAR_HPRG",
      "MATTR_AVATAR_HR"
    };
    public string CharName = "Bilinmiyor";
    private bool Sended = false;
    private bool Teleported = false;
    private int RealAgentPort = 0;
    private int FakeAgentPort = 0;
    private string RealAgentIP = "";
    private int TotalPacketCount = 0;
    private DateTime LoginDate;
    private int SleepTime = 10;
    private bool DisableMoving = false;

    public ClientsAgent(Socket tSocket, int RealAP, string RealIP)
    {
      this.RealAgentPort = RealAP;
      this.RealAgentIP = RealIP;
      this.LoginDate = DateTime.Now;
      try
      {
        this.IP = IPAddress.Parse(((IPEndPoint) tSocket.RemoteEndPoint).Address.ToString()).ToString();
        if (MainForm.BlockedIP.Contains(this.IP))
        {
          Thread.Sleep(20);
          tSocket.Disconnect(false);
          GC.SuppressFinalize((object) this);
        }
        else
        {
          lock (MainForm.AgentClients)
            MainForm.AgentClients.Add(this);
          lock (MainForm.AgentIPList)
            MainForm.AgentIPList.Add(this.IP);
          ListViewItem listViewItem = new ListViewItem();
          int num = MainForm.IPLimitValue;
          try
          {
            num = (int) Convert.ToInt16(Globals.MainWindow.listView_CustomLimit.FindItemWithText(this.IP).SubItems[1].Text) * MainForm.IPLimitValue;
          }
          catch
          {
          }
          if (!MainForm.IPLimit)
            num = 50;
          if (this.AgentIPCount() >= num + 1)
          {
            MainForm.WriteConsole("Agent IP bazlı bağlantı sınırı olan " + num.ToString() + " aşılmaya çalışılıyor. IP: " + this.IP);
            MainForm.Block(this.IP);
            lock (MainForm.AgentClients)
              MainForm.AgentClients.Remove(this);
            lock (MainForm.AgentIPList)
              MainForm.AgentIPList.Remove(this.IP);
            tSocket.Disconnect(false);
            GC.SuppressFinalize((object) this);
          }
          else
          {
            this.C_Socket = tSocket;
            this.C_Socket.Blocking = false;
            this.C_Socket.NoDelay = true;
            (this.C_Security = new Security()).GenerateSecurity(true, true, true);
            this.C_Security.ChangeIdentity("SR_Client", (byte) 0);
            this.RecvBuffer_Local = new TransferBuffer(65536, 0, 0);
            this.S_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.S_Security = new Security();
            this.RecvBuffer_Remote = new TransferBuffer(65536, 0, 0);
            this.SleepTime = MainForm.DinnerTime;
            new Thread(new ThreadStart(this.GuardAsyncPool)).Start();
          }
        }
      }
      catch (Exception ex)
      {
        this.CleanClient(1);
        MainForm.WriteConsole(this.IP + " adresiyle senkronizasyon kurulamadı. Hata metin dosyasına kaydedildi.");
        Program.WriteError(ex.ToString(), "Agent (Start)");
      }
    }

    private void GuardAsyncPool()
    {
      try
      {
        while (this.Local)
        {
          this.RecvBuffer_Local.Offset = 0;
          SocketError errorCode;
          this.RecvBuffer_Local.Size = this.C_Socket.Receive(this.RecvBuffer_Local.Buffer, 0, this.RecvBuffer_Local.Buffer.Length, SocketFlags.None, out errorCode);
          if ((uint) errorCode > 0U)
          {
            if (errorCode != SocketError.WouldBlock)
              break;
          }
          else if (this.C_Socket.Connected && this.RecvBuffer_Local.Size > 0)
            this.C_Security.Recv(this.RecvBuffer_Local);
          else
            break;
          this.RecvPackets_FromClient = this.C_Security.TransferIncoming();
          if (this.RecvPackets_FromClient != null)
          {
            if (MainForm.MaxPacketLimit)
            {
              this.TotalPacketCount += this.RecvPackets_FromClient.Count;
              if (this.TotalPacketCount >= 100000)
              {
                MainForm.WriteConsole("Paket limiti aşıldığı için DC verildi. Gönderilen paket sayısı: " + this.TotalPacketCount.ToString() + " , IP: " + this.IP + " , Karakter Adı: " + this.CharName + " , Oynama Süresi: " + Convert.ToInt16((DateTime.Now - this.LoginDate).TotalMinutes).ToString() + " dakika.", Color.OrangeRed);
                break;
              }
            }
            if (DateTime.Now > this.PacketTimer)
            {
              this.PacketTimer = DateTime.Now.AddSeconds(1.0);
              this.PacketCount = 0;
            }
            else
            {
              this.PacketCount += this.RecvPackets_FromClient.Count;
              if (this.PacketCount >= 200)
              {
                MainForm.WriteConsole(this.IP + " adresinden şüpheli paket saldırısı. Son 1 saniye içinde gönderilen paket sayısı:  [" + this.PacketCount.ToString() + "] , Algılanma süresi: [" + (DateTime.Now - this.PacketTimer.AddSeconds(-1.0)).Milliseconds.ToString() + " ms]", Color.Red);
                MainForm.Block(this.IP);
                break;
              }
            }
            foreach (Packet packet1 in this.RecvPackets_FromClient)
            {
              switch (packet1.Opcode)
              {
                case 8193:
                case 20480:
                case 36864:
                  if (!this.S_Socket.Connected)
                  {
                    this.S_Socket.Connect(this.RealAgentIP, this.RealAgentPort);
                    this.S_Socket.Blocking = false;
                    this.S_Socket.NoDelay = true;
                    this.Remote = true;
                    continue;
                  }
                  continue;
                case 12306:
                  if (!this.Sended)
                  {
                    new SqlCommand("exec __Dev_AddCharIP '" + this.CharName + "','" + this.IP + "'", Program.sql).ExecuteNonQuery();
                    if (MainForm.Dev_LoginLogoutLog)
                      new SqlCommand("exec __Dev_Log_LoginLogout '" + this.CharName + "','" + this.IP + "',1", Program.sql).ExecuteNonQuery();
                    if (MainForm.WelcomeMsg)
                      this.SendNotice(MainForm.WelcomeMsg1 + " " + this.CharName + ", " + MainForm.WelcomeMsg2);
                    this.Sended = true;
                  }
                  using (SqlDataReader sqlDataReader = new SqlCommand("exec _GetFortressPetStatus '" + this.CharName + "'", Program.sql).ExecuteReader())
                  {
                    sqlDataReader.Read();
                    int int16 = (int) Convert.ToInt16(sqlDataReader[0]);
                    sqlDataReader.Close();
                    if (int16 == 1)
                      this.DisableMoving = true;
                  }
                  this.Teleported = true;
                  break;
                case 13481:
                  int num1 = (int) packet1.ReadUInt8();
                  string str1 = packet1.ReadAscii();
                  SqlDataReader sqlDataReader1 = new SqlCommand("exec _GetAvatarType '" + this.CharName + "'," + num1.ToString(), Program.sql).ExecuteReader();
                  sqlDataReader1.Read();
                  int int16_1 = (int) Convert.ToInt16(sqlDataReader1[0]);
                  sqlDataReader1.Close();
                  if ((int16_1 != 1 || !this.Avatar_Helm.Contains(str1)) && (int16_1 != 2 || !this.Avatar_Mail.Contains(str1)) && (int16_1 != 3 || !this.Avatar_Addon.Contains(str1)))
                  {
                    this.SendAcademy("KGuard", "Do not even try. Lmao.");
                    continue;
                  }
                  break;
                case 28673:
                  string str2 = packet1.ReadAscii();
                  if (this.CharName == "Bilinmiyor")
                  {
                    this.CharName = str2;
                    break;
                  }
                  if (this.CharName != str2)
                  {
                    this.SendAcademy("KGuard", "Do not even try. Lmao.");
                    break;
                  }
                  break;
                case 28677:
                  Thread.Sleep(20);
                  int num2;
                  switch (packet1.ReadUInt8())
                  {
                    case 1:
                    case 2:
                      if (MainForm.AntiBugEsc)
                      {
                        num2 = !this.Teleported ? 1 : 0;
                        break;
                      }
                      goto default;
                    default:
                      num2 = 0;
                      break;
                  }
                  if (num2 != 0)
                  {
                    this.SendNotice("Use teleport once before exit.");
                    continue;
                  }
                  this.Teleported = false;
                  break;
                case 28705:
                case 28869:
                  if (MainForm.DisablePetsInFTW && this.DisableMoving)
                  {
                    using (SqlDataReader sqlDataReader2 = new SqlCommand("exec _GetFortressPetStatus '" + this.CharName + "'", Program.sql).ExecuteReader())
                    {
                      sqlDataReader2.Read();
                      int int16_2 = (int) Convert.ToInt16(sqlDataReader2[0]);
                      sqlDataReader2.Close();
                      if (int16_2 != 1)
                      {
                        this.DisableMoving = false;
                        break;
                      }
                      this.SendNotice("You can not use any pet in that area. You must close them.");
                      continue;
                    }
                  }
                  else
                    break;
                case 28709:
                  Thread.Sleep(20);
                  switch (packet1.ReadUInt8())
                  {
                    case 1:
                      int num3 = (int) packet1.ReadUInt8();
                      if (this.WordIsSwear(packet1.ReadAscii().ToLower()))
                      {
                        this.SendNotice("Insult word detected.");
                        continue;
                      }
                      break;
                    case 2:
                      int num4 = (int) packet1.ReadUInt8();
                      string str3 = packet1.ReadAscii();
                      string lower1 = packet1.ReadAscii().ToLower();
                      if (this.WordIsSwear(lower1))
                      {
                        this.SendNotice("Insult word detected.");
                        continue;
                      }
                      if (MainForm.Dev_LogPM)
                        new SqlCommand("exec __Dev_PM '" + this.CharName + "','" + str3 + "','" + lower1 + "'", Program.sql).ExecuteNonQueryAsync();
                      if (MainForm.Dev_VerifyPM && MainForm.PMVerifyNames.Contains(str3))
                      {
                        Packet packet2 = new Packet((ushort) 45093);
                        packet2.WriteUInt8((byte) 1);
                        packet2.WriteUInt8((byte) 2);
                        packet2.WriteUInt8((object) num4);
                        this.C_Security.Send(packet2);
                        continue;
                      }
                      break;
                    case 4:
                      int num5 = (int) packet1.ReadUInt8();
                      if (this.WordIsSwear(packet1.ReadAscii().ToLower()))
                      {
                        this.SendNotice("Insult word detected.");
                        continue;
                      }
                      break;
                  }
                  break;
                case 28724:
                  Thread.Sleep(5);
                  int num6 = (int) packet1.ReadUInt8();
                  if (MainForm.Dev_LockSlot && num6 == 0)
                  {
                    int num7 = (int) packet1.ReadUInt8();
                    int num8 = (int) packet1.ReadUInt8();
                    bool flag = false;
                    SqlDataReader sqlDataReader2 = new SqlCommand("exec _SlotIsLocked '" + this.CharName + "'," + num7.ToString() + "," + num8.ToString(), Program.sql).ExecuteReader();
                    if (sqlDataReader2.Read())
                      flag = Convert.ToBoolean(sqlDataReader2[0]);
                    sqlDataReader2.Close();
                    if (flag)
                    {
                      this.SendNotice("This is not possible. The slot is locked.");
                      continue;
                    }
                    break;
                  }
                  if (MainForm.AntiDropInTown && (num6 == 7 || num6 == 10))
                  {
                    SqlDataReader sqlDataReader2 = new SqlCommand("exec _IsBattleField '" + this.CharName + "'", Program.sql).ExecuteReader();
                    sqlDataReader2.Read();
                    bool boolean = Convert.ToBoolean(sqlDataReader2[0]);
                    sqlDataReader2.Close();
                    if (!boolean)
                    {
                      this.SendNotice("You can not drop the item or gold in town.");
                      continue;
                    }
                    break;
                  }
                  break;
                case 28748:
                  Thread.Sleep(20);
                  int num9 = (int) packet1.ReadUInt8();
                  ushort num10 = packet1.ReadUInt16();
                  int num11;
                  switch (num10)
                  {
                    case 6636:
                    case 6637:
                      num11 = MainForm.JobRevRestrc ? 1 : 0;
                      break;
                    default:
                      num11 = 0;
                      break;
                  }
                  if (num11 != 0)
                  {
                    SqlDataReader sqlDataReader2 = new SqlCommand("EXEC _JobCheck '" + this.CharName + "'", Program.sql).ExecuteReader();
                    if (sqlDataReader2.Read())
                    {
                      long int64 = Convert.ToInt64(sqlDataReader2["ItemID"]);
                      sqlDataReader2.Close();
                      if ((ulong) int64 > 0UL)
                      {
                        this.SendNotice("You can't use Reverse Scroll during job.");
                        continue;
                      }
                      break;
                    }
                    sqlDataReader2.Close();
                  }
                  else if ((num10 == (ushort) 10732 || num10 == (ushort) 10733) && MainForm.SwearFilter)
                  {
                    string lower2 = packet1.ReadAscii().ToLower();
                    if (this.WordIsSwear(lower2))
                    {
                      this.SendNotice("Insult word detected.");
                      continue;
                    }
                    if (MainForm.Dev_LogGlo)
                      new SqlCommand("exec __Dev_GLOBAL '" + this.CharName + "','" + lower2 + "'", Program.sql).ExecuteNonQueryAsync();
                  }
                  else if (num10 == (ushort) 4588 || num10 == (ushort) 4589)
                  {
                    Thread.Sleep(500);
                    if (this.RecvPackets_FromClient.Contains(new Packet((ushort) 28762)) || this.RecvPackets_FromClient.Contains(new Packet((ushort) 13494)) || this.RecvPackets_FromClient.Contains(new Packet((ushort) 12306)) || this.RecvPackets_FromClient.Contains(new Packet((ushort) 29966)))
                      continue;
                  }
                  if (num10 == (ushort) 4588 || num10 == (ushort) 4589 || num10 == (ushort) 2253 || num10 == (ushort) 4301)
                  {
                    this.DisableMoving = true;
                    break;
                  }
                  break;
                case 28762:
                  Thread.Sleep(350);
                  if (!this.RecvPackets_FromClient.Contains(new Packet((ushort) 28748)))
                    break;
                  continue;
                case 28766:
                  Thread.Sleep(10);
                  int num12 = (int) packet1.ReadUInt32();
                  uint num13 = (uint) packet1.ReadUInt8();
                  if (MainForm.AntiTaxRateChange && num13 == 1U)
                  {
                    this.SendNotice("You can not change tax.");
                    continue;
                  }
                  if (MainForm.LimitOfFortressWarEnabled && num13 == 7U)
                  {
                    int num7 = (int) packet1.ReadUInt8();
                    int num8 = 0;
                    using (SqlDataReader sqlDataReader2 = new SqlCommand("exec _GetFortressRequestCount " + num7.ToString(), Program.sql).ExecuteReader())
                    {
                      sqlDataReader2.Read();
                      num8 = (int) Convert.ToInt16(sqlDataReader2[0]);
                      sqlDataReader2.Close();
                    }
                    if (num8 >= MainForm.LimitOfFortressWarValue)
                    {
                      this.SendNotice("The Fortress War's member limit is " + MainForm.LimitOfFortressWarValue.ToString() + " and we have " + (object) num8 + " requested members. You can not make a request for this fortress war.");
                      continue;
                    }
                    break;
                  }
                  break;
                case 28788:
                  if (MainForm.AntiSkillBug)
                  {
                    Thread.Sleep(100);
                    break;
                  }
                  break;
                case 28801:
                  Thread.Sleep(20);
                  if (MainForm.AntiBugExc && !this.Teleported)
                  {
                    this.SendNotice("Use teleport once before exchange.");
                    continue;
                  }
                  this.Teleported = false;
                  break;
                case 28839:
                  int num14 = (int) packet1.ReadUInt8();
                  if (num14 >= 2 & num14 <= 4)
                  {
                    this.SendAcademy("KGuard", "Do not even try. Lmao.");
                    continue;
                  }
                  break;
                case 28849:
                  Thread.Sleep(20);
                  if (MainForm.AntiBugStall && !this.Teleported)
                  {
                    this.SendNotice("Use teleport once before open stalls.");
                    continue;
                  }
                  this.Teleported = false;
                  break;
                case 28901:
                  Thread.Sleep(20);
                  if (MainForm.ThiefRewardRestrc)
                  {
                    this.SendNotice("Your access to this section is blocked by the KGuard.");
                    continue;
                  }
                  break;
                case 28915:
                  Thread.Sleep(20);
                  if (MainForm.GuildLimitEnabled)
                  {
                    int guildLimitValue = MainForm.GuildLimitValue;
                    SqlDataReader sqlDataReader2 = new SqlCommand("exec _GetGuildMemberCount '" + this.CharName + "'", Program.sql).ExecuteReader();
                    sqlDataReader2.Read();
                    int int16_2 = (int) Convert.ToInt16(sqlDataReader2[0]);
                    sqlDataReader2.Close();
                    if (int16_2 >= guildLimitValue)
                    {
                      this.SendNotice("Guild member limit is " + guildLimitValue.ToString());
                      continue;
                    }
                    break;
                  }
                  break;
                case 28923:
                  Thread.Sleep(20);
                  if (MainForm.UnionLimitEnabled)
                  {
                    int unionLimitValue = MainForm.UnionLimitValue;
                    SqlDataReader sqlDataReader2 = new SqlCommand("exec _GetUnionMemberCount '" + this.CharName + "'", Program.sql).ExecuteReader();
                    sqlDataReader2.Read();
                    int int16_2 = (int) Convert.ToInt16(sqlDataReader2[0]);
                    sqlDataReader2.Close();
                    if (int16_2 >= unionLimitValue)
                    {
                      this.SendNotice("Union member limit is " + unionLimitValue.ToString());
                      continue;
                    }
                    break;
                  }
                  break;
                case 29008:
                  Thread.Sleep(100);
                  if (MainForm.PlusLimit && packet1.ReadUInt8() == (byte) 2 && packet1.ReadUInt8() == (byte) 3)
                  {
                    int num7 = (int) packet1.ReadUInt8();
                    SqlDataReader sqlDataReader2 = new SqlCommand("EXEC _GetOptLvBySlot '" + this.CharName + "' , '" + ((int) packet1.ReadUInt8()).ToString() + "'", Program.sql).ExecuteReader();
                    sqlDataReader2.Read();
                    int int16_2 = (int) Convert.ToInt16(sqlDataReader2["OptLevel"]);
                    sqlDataReader2.Close();
                    if (int16_2 >= MainForm.PlusLimitValue)
                    {
                      this.SendNotice("The item has reached the limit of plus +" + MainForm.PlusLimitValue.ToString());
                      Packet packet2 = new Packet((ushort) 45392);
                      packet2.WriteUInt8((byte) 1);
                      packet2.WriteUInt8((byte) 1);
                      this.C_Security.Send(packet2);
                      continue;
                    }
                    break;
                  }
                  break;
                case 29023:
                  Thread.Sleep(20);
                  if (MainForm.JobRevRestrc)
                  {
                    int num7 = (int) packet1.ReadUInt8();
                    int num8 = (int) packet1.ReadUInt8();
                    if (packet1.ReadUInt8() == (byte) 25)
                    {
                      SqlDataReader sqlDataReader2 = new SqlCommand("EXEC _JobCheck '" + this.CharName + "'", Program.sql).ExecuteReader();
                      if (sqlDataReader2.Read())
                      {
                        long int64 = Convert.ToInt64(sqlDataReader2["ItemID"]);
                        sqlDataReader2.Close();
                        if ((ulong) int64 > 0UL)
                        {
                          this.SendNotice("You can't use Reverse Scroll during job.");
                          continue;
                        }
                        break;
                      }
                      sqlDataReader2.Close();
                    }
                    break;
                  }
                  break;
                case 29808:
                  if (MainForm.AntiAcademyCreate)
                  {
                    this.SendNotice("Academy closed in this game.");
                    continue;
                  }
                  break;
              }
              if (!this.Exploits.Contains((int) packet1.Opcode))
                this.S_Security.Send(packet1);
            }
          }
          this.SendBuffers_Local = this.C_Security.TransferOutgoing();
          if (this.SendBuffers_Local != null)
          {
            foreach (KeyValuePair<TransferBuffer, Packet> keyValuePair in this.SendBuffers_Local)
            {
              Packet packet = keyValuePair.Value;
              TransferBuffer key = keyValuePair.Key;
              this.C_Socket.Blocking = true;
              while (key.Offset != key.Size)
                key.Offset += this.C_Socket.Send(key.Buffer, key.Offset, key.Size - key.Offset, SocketFlags.None);
              this.C_Socket.Blocking = false;
            }
          }
          Thread.Sleep(this.SleepTime);
          if (this.Remote)
          {
            this.RecvBuffer_Remote.Offset = 0;
            this.RecvBuffer_Remote.Size = this.S_Socket.Receive(this.RecvBuffer_Remote.Buffer, 0, this.RecvBuffer_Remote.Buffer.Length, SocketFlags.None, out errorCode);
            if ((uint) errorCode > 0U)
            {
              if (errorCode != SocketError.WouldBlock)
                break;
            }
            else if (this.S_Socket.Connected && this.RecvBuffer_Remote.Size > 0)
              this.S_Security.Recv(this.RecvBuffer_Remote);
            else
              break;
            this.RecvPackets_FromServer = this.S_Security.TransferIncoming();
            if (this.RecvPackets_FromServer != null)
            {
              foreach (Packet packet in this.RecvPackets_FromServer)
                this.C_Security.Send(packet);
            }
            this.SendBuffers_Remote = this.S_Security.TransferOutgoing();
            if (this.SendBuffers_Remote != null)
            {
              foreach (KeyValuePair<TransferBuffer, Packet> keyValuePair in this.SendBuffers_Remote)
              {
                Packet packet = keyValuePair.Value;
                TransferBuffer key = keyValuePair.Key;
                this.S_Socket.Blocking = true;
                while (key.Offset != key.Size)
                  key.Offset += this.S_Socket.Send(key.Buffer, key.Offset, key.Size - key.Offset, SocketFlags.None);
                this.S_Socket.Blocking = false;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "AgentPool");
      }
      this.CleanClient(0);
    }

    public void CleanClient(int i)
    {
      this.Local = false;
      this.Remote = false;
      try
      {
        if (MainForm.Dev_LoginLogoutLog)
          new SqlCommand("exec __Dev_Log_LoginLogout '" + this.CharName + "','" + this.IP + "',0", Program.sql).ExecuteNonQuery();
        lock (MainForm.AgentClients)
          MainForm.AgentClients.Remove(this);
        lock (MainForm.AgentIPList)
          MainForm.AgentIPList.Remove(this.IP);
        if (this.C_Socket.Connected)
          this.C_Socket.Disconnect(false);
        if (this.S_Socket.Connected)
          this.S_Socket.Disconnect(false);
        try
        {
          this.C_Socket.Close();
          this.S_Socket.Close();
        }
        catch
        {
        }
        GC.SuppressFinalize((object) this);
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "AgentCleanFnc");
      }
    }

    private bool WordIsSwear(string msg)
    {
      bool flag = false;
      if (!MainForm.SwearFilter)
        return false;
      foreach (string swearWord in MainForm.SwearWords)
      {
        if (msg.Contains(swearWord))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    private int AgentIPCount()
    {
      int num = 0;
      foreach (string str in new List<string>((IEnumerable<string>) MainForm.AgentIPList))
      {
        if (str == this.IP)
          ++num;
      }
      return num;
    }

    public void ChangeSleepTime(int i) => this.SleepTime = i;

    public void SendPM(string SenderName, string Msg)
    {
      Packet packet = new Packet((ushort) 12326);
      packet.WriteUInt8((byte) 2);
      packet.WriteAscii(SenderName);
      packet.WriteAscii(Msg);
      this.C_Security.Send(packet);
    }

    public void SendPMask(string SenderName, string Msg)
    {
      Packet packet = new Packet((ushort) 12326);
      packet.WriteUInt8((byte) 10);
      packet.WriteAscii(SenderName);
      packet.WriteAscii(Msg);
      this.C_Security.Send(packet);
    }

    public void SendNotice(string msg)
    {
      Packet packet = new Packet((ushort) 12326);
      packet.WriteUInt8((byte) 7);
      packet.WriteAscii(msg);
      this.C_Security.Send(packet);
    }

    public void SendGlobal(string SenderName, string Msg)
    {
      Packet packet = new Packet((ushort) 12326);
      packet.WriteUInt8((byte) 6);
      packet.WriteAscii(SenderName);
      packet.WriteAscii(Msg);
      this.C_Security.Send(packet);
    }

    public void SendAcademy(string SenderName, string Msg)
    {
      Packet packet = new Packet((ushort) 12326);
      packet.WriteUInt8((byte) 16);
      packet.WriteAscii(SenderName);
      packet.WriteAscii(Msg);
      this.C_Security.Send(packet);
    }
  }
}
