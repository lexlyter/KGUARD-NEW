// Decompiled with JetBrains decompiler
// Type: KGuard.ClientsGateway
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
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KGuard
{
  public class ClientsGateway
  {
    private Socket C_Socket;
    private Socket S_Socket;
    private SilkroadSecurityApi.Security C_Security;
    private SilkroadSecurityApi.Security S_Security;
    private TransferBuffer RecvBuffer_Local;
    private TransferBuffer RecvBuffer_Remote;
    private List<Packet> RecvPackets_Local;
    private List<Packet> RecvPackets_Remote;
    private List<KeyValuePair<TransferBuffer, Packet>> SendBuffers_Local;
    private List<KeyValuePair<TransferBuffer, Packet>> SendBuffers_Remote;
    private bool Local = true;
    private bool Remote = false;
    private string IP = "";
    private int PacketCount = 0;
    private bool Sended6102 = false;

    public ClientsGateway(Socket tSocket)
    {
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
          lock (MainForm.GatewayClients)
            MainForm.GatewayClients.Add(this);
          lock (MainForm.GatewayIPList)
            MainForm.GatewayIPList.Add(this.IP);
          if (this.GatewayIPCount() >= 20)
          {
            MainForm.WriteConsole("Gateway IP bazlı bağlantı sınırı olan 20 aşılmaya çalışılıyor. IP: " + this.IP);
            MainForm.Block(this.IP);
            lock (MainForm.GatewayClients)
              MainForm.GatewayClients.Remove(this);
            lock (MainForm.GatewayIPList)
              MainForm.GatewayIPList.Remove(this.IP);
            tSocket.Disconnect(false);
            GC.SuppressFinalize((object) this);
          }
          else
          {
            this.C_Socket = tSocket;
            this.C_Socket.Blocking = false;
            this.C_Socket.NoDelay = true;
            (this.C_Security = new SilkroadSecurityApi.Security()).GenerateSecurity(true, true, true);
            this.C_Security.ChangeIdentity("SR_Client", (byte) 0);
            this.RecvBuffer_Local = new TransferBuffer(65536, 0, 0);
            this.S_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.S_Security = new SilkroadSecurityApi.Security();
            this.RecvBuffer_Remote = new TransferBuffer(65536, 0, 0);
            new Thread(new ThreadStart(this.GuardPool_Local)).Start();
            new Thread(new ThreadStart(this.GuardPool_Remote)).Start();
          }
        }
      }
      catch (Exception ex)
      {
        this.CleanClient(1);
        MainForm.WriteConsole(this.IP + " adresiyle senkronizasyon kurulamadı. Hata metin dosyasına kaydedildi.");
        Program.WriteError(ex.ToString(), "Gateway (Start)");
      }
    }

    private void GuardPool_Local()
    {
      try
      {
        while (this.Local)
        {
          Thread.Sleep(1);
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
          this.RecvPackets_Local = this.C_Security.TransferIncoming();
          if (this.RecvPackets_Local != null)
          {
            this.PacketCount += this.RecvPackets_Local.Count;
            if (this.PacketCount > 3000)
            {
              MainForm.WriteConsole(this.IP + " adresinden şüpheli Gateway saldırısı.", Color.Red);
              MainForm.Block(this.IP);
              break;
            }
            foreach (Packet packet in this.RecvPackets_Local)
            {
              byte[] bytes = packet.GetBytes();
              switch (packet.Opcode)
              {
                case 8193:
                case 20480:
                case 36864:
                  if (!this.S_Socket.Connected)
                  {
                    this.S_Socket.Connect(MainForm.ServerIP, MainForm.LocalGateway);
                    this.S_Socket.Blocking = false;
                    this.S_Socket.NoDelay = true;
                    this.Remote = true;
                    continue;
                  }
                  continue;
                case 8194:
                  if ((uint) bytes.Length <= 0U)
                    goto case 24832;
                  else
                    continue;
                case 24832:
                case 24836:
                case 24838:
                  this.S_Security.Send(packet);
                  continue;
                case 24833:
                  if ((uint) bytes.Length <= 0U)
                    goto case 24832;
                  else
                    continue;
                case 24834:
                  if (packet.ReadUInt8() == (byte) 22)
                  {
                    string UserID = packet.ReadAscii().Replace("'", "");
                    string PassW = packet.ReadAscii();
                    if (this.UserExists(UserID, PassW))
                    {
                      int num1 = this.AgentIPCount();
                      if (MainForm.IPLimit)
                      {
                        ListViewItem listViewItem = new ListViewItem();
                        int num2 = MainForm.IPLimitValue;
                        try
                        {
                          num2 = (int) Convert.ToInt16(Globals.MainWindow.listView_CustomLimit.FindItemWithText(this.IP).SubItems[1].Text) * MainForm.IPLimitValue;
                        }
                        catch
                        {
                        }
                        if (num1 >= num2 && MainForm.AgentClients.Count > 5)
                        {
                          this.SendIPLimit();
                          continue;
                        }
                      }
                      if (this.IsConnect())
                      {
                        if (MainForm.GMYetkiliIP && this.IsGM(UserID) && !MainForm.YetkiliIPList.Contains(this.IP))
                        {
                          this.SendGMError();
                          continue;
                        }
                        if (((double) MainForm.AgentClients.Count + MainForm.EklenecekOnline >= (double) MainForm.ServerKapasite && !MainForm.MultipleOnlineCount || (double) MainForm.AgentClients.Count * MainForm.EklenecekOnline >= (double) MainForm.ServerKapasite && MainForm.MultipleOnlineCount) && (MainForm.LimitOnline && MainForm.OnlineKontrol && !MainForm.YetkiliIPList.Contains(this.IP)) && (!this.isHavePremium(UserID) || !MainForm.PremiumAl) && !this.IsGM(UserID))
                        {
                          Thread.Sleep(500);
                          this.SendServerCrowded();
                          continue;
                        }
                      }
                      else
                        continue;
                    }
                    goto case 24832;
                  }
                  else
                    goto case 24832;
                case 25379:
                  string str = packet.ReadAscii();
                  lock (MainForm.AutoImageValue)
                  {
                    MainForm.AutoImageValue = str;
                    MainForm.AutoImage = true;
                  }
                  new SqlCommand("exec KGUARD.._SetSetting 'AutoImage_Value', '" + MainForm.AutoImageValue + "'", Program.sql).ExecuteNonQuery();
                  new SqlCommand("exec KGUARD.._SetSetting 'AutoImage_Enabled', '" + MainForm.AutoImage.ToString() + "'", Program.sql).ExecuteNonQuery();
                  goto case 24832;
                default:
                  continue;
              }
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
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "GatewayPool_Local");
      }
      this.CleanClient(0);
    }

    private void GuardPool_Remote()
    {
      try
      {
        while (this.Local)
        {
          Thread.Sleep(1);
          if (this.Remote)
          {
            this.RecvBuffer_Remote.Offset = 0;
            SocketError errorCode;
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
            this.RecvPackets_Remote = this.S_Security.TransferIncoming();
            if (this.RecvPackets_Remote != null)
            {
              foreach (Packet packet1 in this.RecvPackets_Remote)
              {
                packet1.GetBytes();
                switch (packet1.Opcode)
                {
                  case 8994:
                    lock (MainForm.AutoImageValue)
                    {
                      if (MainForm.AutoImage)
                      {
                        Packet packet2 = new Packet((ushort) 25379);
                        packet2.WriteAscii(MainForm.AutoImageValue);
                        this.S_Security.Send(packet2);
                        continue;
                      }
                      this.C_Security.Send(packet1);
                      break;
                    }
                  case 41217:
                    uint num1 = (uint) packet1.ReadUInt8();
                    if (num1 == 1U)
                    {
                      if (MainForm.OnlineKontrol)
                      {
                        uint num2 = (uint) packet1.ReadUInt8();
                        packet1.ReadAscii();
                        uint num3 = packet1.ReadUInt32();
                        string str = packet1.ReadAscii();
                        ushort num4 = packet1.ReadUInt16();
                        packet1.ReadUInt16();
                        uint num5 = (uint) packet1.ReadUInt8();
                        ushort num6 = packet1.ReadUInt16();
                        Packet packet2 = new Packet((ushort) 41217);
                        packet2.WriteUInt8((object) num1);
                        packet2.WriteUInt8((object) num2);
                        packet2.WriteAscii("PROTECTING_BY_KGUARD");
                        packet2.WriteUInt32(num3);
                        packet2.WriteAscii(str);
                        if (MainForm.MultipleOnlineCount)
                          packet2.WriteUInt16((object) Convert.ToInt16((double) num4 * MainForm.EklenecekOnline));
                        else
                          packet2.WriteUInt16((object) Convert.ToInt16((double) num4 + MainForm.EklenecekOnline));
                        packet2.WriteUInt16((object) MainForm.ServerKapasite);
                        if (this.IsConnect())
                          packet2.WriteUInt8((object) num5);
                        else
                          packet2.WriteUInt8((byte) 0);
                        packet2.WriteUInt16(num6);
                        this.C_Security.Send(packet2);
                      }
                      else if (MainForm.OnlyAuthorized)
                      {
                        uint num2 = (uint) packet1.ReadUInt8();
                        packet1.ReadAscii();
                        uint num3 = packet1.ReadUInt32();
                        string str = packet1.ReadAscii();
                        ushort num4 = packet1.ReadUInt16();
                        ushort num5 = packet1.ReadUInt16();
                        uint num6 = (uint) packet1.ReadUInt8();
                        ushort num7 = packet1.ReadUInt16();
                        Packet packet2 = new Packet((ushort) 41217);
                        packet2.WriteUInt8((object) num1);
                        packet2.WriteUInt8((object) num2);
                        packet2.WriteAscii("PROTECTING_BY_KGUARD");
                        packet2.WriteUInt32(num3);
                        packet2.WriteAscii(str);
                        packet2.WriteUInt16(num4);
                        packet2.WriteUInt16(num5);
                        if (this.IsConnect())
                          packet2.WriteUInt8((object) num6);
                        else
                          packet2.WriteUInt8((byte) 0);
                        packet2.WriteUInt16(num7);
                        this.C_Security.Send(packet2);
                      }
                      else
                        this.C_Security.Send(packet1);
                      break;
                    }
                    break;
                  case 41218:
                    byte num8 = packet1.ReadUInt8();
                    if (num8 == (byte) 1)
                    {
                      uint num2 = packet1.ReadUInt32();
                      string str1 = packet1.ReadAscii();
                      packet1.ReadUInt16();
                      string localIp = Program.LocalIP;
                      string str2 = MainForm.PublicAgent.ToString();
                      if (str1 != MainForm.ServerIP && MainForm.MultiAgent)
                        str2 = MainForm.PublicAgent2.ToString();
                      Packet packet2 = new Packet((ushort) 41218, true);
                      packet2.WriteUInt8(num8);
                      packet2.WriteUInt32(num2);
                      packet2.WriteAscii(localIp);
                      packet2.WriteUInt16((object) str2);
                      this.C_Security.Send(packet2);
                      break;
                    }
                    this.C_Security.Send(packet1);
                    break;
                  case 41763:
                    if (packet1.ReadUInt8() == (byte) 2)
                    {
                      MainForm.AutoImage = false;
                      new SqlCommand("exec KGUARD.._SetSetting 'AutoImage_Enabled', '" + MainForm.AutoImage.ToString() + "'", Program.sql).ExecuteNonQuery();
                    }
                    this.C_Security.Send(packet1);
                    break;
                  default:
                    this.C_Security.Send(packet1);
                    break;
                }
              }
            }
            this.SendBuffers_Remote = this.S_Security.TransferOutgoing();
            if (this.SendBuffers_Remote != null)
            {
              foreach (KeyValuePair<TransferBuffer, Packet> keyValuePair in this.SendBuffers_Remote)
              {
                if (keyValuePair.Value.Opcode == (ushort) 24834)
                {
                  if (!this.Sended6102)
                  {
                    lock (MainForm.LoginObj)
                      Thread.Sleep(250);
                    this.Sended6102 = true;
                  }
                  else
                    goto label_66;
                }
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
        Program.WriteError(ex.ToString(), "GatewayPool_Remote");
      }
label_66:
      this.CleanClient(0);
    }

    private void CleanClient(int i)
    {
      this.Local = false;
      this.Remote = false;
      try
      {
        lock (MainForm.GatewayClients)
          MainForm.GatewayClients.Remove(this);
        lock (MainForm.GatewayIPList)
          MainForm.GatewayIPList.Remove(this.IP);
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
        Program.WriteError(ex.ToString(), "GatewayCleanFnc");
      }
    }

    private bool IsConnect() => MainForm.YetkiliIPList.Contains(this.IP) || !MainForm.OnlyAuthorized;

    private bool IsGM(string UserID)
    {
      bool flag;
      using (SqlDataReader sqlDataReader = new SqlCommand("exec _IsGM '" + UserID + "'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          int int16_1 = (int) Convert.ToInt16(sqlDataReader[0]);
          int int16_2 = (int) Convert.ToInt16(sqlDataReader[1]);
          flag = int16_1 != 3 || int16_2 != 3;
        }
        else
          flag = false;
        sqlDataReader.Close();
      }
      return flag;
    }

    private void SendGMError()
    {
      Packet packet = new Packet((ushort) 41218);
      packet.WriteUInt16((ushort) 2562);
      this.C_Security.Send(packet);
    }

    private void SendIPLimit()
    {
      Packet packet = new Packet((ushort) 41218);
      packet.WriteUInt16((ushort) 2050);
      this.C_Security.Send(packet);
    }

    private void SendServerCrowded()
    {
      Packet packet = new Packet((ushort) 41218);
      packet.WriteUInt16((ushort) 1282);
      this.C_Security.Send(packet);
    }

    private string MD5yapUTF8(string text)
    {
      byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(text));
      StringBuilder stringBuilder = new StringBuilder();
      foreach (byte num in hash)
        stringBuilder.Append(num.ToString("x2").ToLower());
      return stringBuilder.ToString();
    }

    private bool UserExists(string UserID, string PassW)
    {
      PassW = this.MD5yapUTF8(PassW);
      bool boolean;
      using (SqlDataReader sqlDataReader = new SqlCommand("exec _IsExists '" + UserID + "', '" + PassW + "'", Program.sql).ExecuteReader())
      {
        sqlDataReader.Read();
        boolean = Convert.ToBoolean(sqlDataReader[0]);
        sqlDataReader.Close();
      }
      return boolean;
    }

    private bool isHavePremium(string UserID)
    {
      List<int> intList = new List<int>();
      bool flag = false;
      using (SqlDataReader sqlDataReader = new SqlCommand("exec _IsHavePremium '" + UserID + "'", Program.sql).ExecuteReader())
      {
        while (sqlDataReader.Read())
          intList.Add(Convert.ToInt32(sqlDataReader[0]));
        sqlDataReader.Close();
      }
      if (intList.Count < 1)
        return false;
      foreach (int num in intList)
      {
        if (MainForm.PremiumIDleri.Contains(num))
          flag = true;
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

    private int GatewayIPCount()
    {
      int num = 0;
      foreach (string str in new List<string>((IEnumerable<string>) MainForm.GatewayIPList))
      {
        if (str == this.IP)
          ++num;
      }
      return num;
    }
  }
}
