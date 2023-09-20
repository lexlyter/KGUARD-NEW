// Decompiled with JetBrains decompiler
// Type: KGuard.SideGateway
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
using System.Windows.Forms;

namespace KGuard
{
  public class SideGateway
  {
    private Socket C_Socket = (Socket) null;
    private Socket S_Socket = (Socket) null;
    private SilkroadSecurityApi.Security C_Security = new SilkroadSecurityApi.Security();
    private SilkroadSecurityApi.Security S_Security = new SilkroadSecurityApi.Security();
    private byte[] C_Buffer = new byte[8192];
    private byte[] S_Buffer = new byte[8192];
    private AsyncServer.Engine_ServerType HandlerType;
    private AsyncServer.delClientDisconnect m_delDisconnect;
    private object LockObj = new object();
    private string IP = "";
    private int PacketCount = 0;

    public SideGateway(Socket ClientSocket, AsyncServer.delClientDisconnect delDisconnect)
    {
      this.m_delDisconnect = delDisconnect;
      this.C_Socket = ClientSocket;
      this.HandlerType = AsyncServer.Engine_ServerType.GatewayServer;
      this.S_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      try
      {
        this.IP = IPAddress.Parse(((IPEndPoint) ClientSocket.RemoteEndPoint).Address.ToString()).ToString();
        lock (MainForm.AsyncGatewayClients)
          MainForm.AsyncGatewayClients.Add(this);
        lock (MainForm.GatewayIPList)
          MainForm.GatewayIPList.Add(this.IP);
        this.S_Socket.Connect((EndPoint) new IPEndPoint(IPAddress.Parse(Program.LocalIP), MainForm.LocalGateway));
        this.C_Security.GenerateSecurity(true, true, true);
        if (this.GatewayIPCount() >= 40)
        {
          MainForm.WriteConsole("Gateway IP bazlı bağlantı sınırı olan 40 aşılmaya çalışılıyor. IP: " + this.IP);
          MainForm.Block(this.IP);
          Globals.MainWindow.BlockFirewall(this.IP);
          this.DisconnectServerSocket(1);
          this.m_delDisconnect(ref this.C_Socket, this.HandlerType);
        }
        else if (MainForm.BlockedIP.Contains(this.IP))
        {
          this.DisconnectServerSocket(44);
          this.m_delDisconnect(ref this.C_Socket, this.HandlerType);
        }
        else
        {
          this.DoRecvFromClient();
          this.Send(false);
        }
      }
      catch (Exception ex)
      {
        Log.WriteError(ex, "public SideGateway(Socket ClientSocket, AsyncServer.delClientDisconnect delDisconnect)");
      }
    }

    private void OnReceiveFromClient(IAsyncResult iar)
    {
      lock (this.LockObj)
      {
        try
        {
          int length = this.C_Socket.EndReceive(iar);
          if (length > 0)
          {
            this.C_Security.Recv(this.C_Buffer, 0, length);
            List<Packet> packetList = this.C_Security.TransferIncoming();
            if (packetList != null)
            {
              foreach (Packet packet in packetList)
              {
                ++this.PacketCount;
                if (this.PacketCount > 3000)
                {
                  MainForm.WriteConsole(this.IP + " adresinden şüpheli Gateway saldırısı.", Color.Red);
                  MainForm.Block(this.IP);
                  this.DisconnectServerSocket(2);
                  this.m_delDisconnect(ref this.C_Socket, this.HandlerType);
                  break;
                }
                byte[] bytes = packet.GetBytes();
                switch (packet.Opcode)
                {
                  case 8193:
                    this.DoRecvFromServer();
                    continue;
                  case 8194:
                  case 24833:
                    if ((uint) bytes.Length <= 0U)
                    {
                      this.S_Security.Send(packet);
                      this.Send(true);
                      break;
                    }
                    continue;
                  case 20480:
                  case 36864:
                    this.Send(false);
                    continue;
                  case 24832:
                  case 24836:
                  case 24838:
                    this.S_Security.Send(packet);
                    this.Send(true);
                    break;
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
                          if (num1 >= num2)
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
                          if (((double) MainForm.AsyncAgentClients.Count + MainForm.EklenecekOnline >= (double) MainForm.ServerKapasite && !MainForm.MultipleOnlineCount || (double) MainForm.AsyncAgentClients.Count * MainForm.EklenecekOnline >= (double) MainForm.ServerKapasite && MainForm.MultipleOnlineCount) && (MainForm.LimitOnline && MainForm.OnlineKontrol && !MainForm.YetkiliIPList.Contains(this.IP)) && (!this.isHavePremium(UserID) || !MainForm.PremiumAl) && !this.IsGM(UserID))
                          {
                            this.SendServerCrowded();
                            continue;
                          }
                        }
                        else
                          continue;
                      }
                    }
                    this.S_Security.Send(packet);
                    this.Send(true);
                    break;
                  case 25379:
                    string str = packet.ReadAscii();
                    lock (MainForm.AutoImageValue)
                    {
                      MainForm.AutoImageValue = str;
                      MainForm.AutoImage = true;
                    }
                    new SqlCommand("exec KGUARD.._SetSetting 'AutoImage_Value', '" + MainForm.AutoImageValue + "'", Program.sql).ExecuteNonQuery();
                    new SqlCommand("exec KGUARD.._SetSetting 'AutoImage_Enabled', '" + MainForm.AutoImage.ToString() + "'", Program.sql).ExecuteNonQuery();
                    this.S_Security.Send(packet);
                    this.Send(true);
                    break;
                  default:
                    continue;
                }
              }
            }
            this.DoRecvFromClient();
          }
          else
          {
            this.DisconnectServerSocket(3);
            this.m_delDisconnect(ref this.C_Socket, this.HandlerType);
          }
        }
        catch (Exception)
                {
          this.DisconnectServerSocket(4);
          this.m_delDisconnect(ref this.C_Socket, this.HandlerType);
        }
      }
    }

    private void OnReceiveFromServer(IAsyncResult iar)
    {
      lock (this.LockObj)
      {
        try
        {
          int length = this.S_Socket.EndReceive(iar);
          if (length > 0)
          {
            this.S_Security.Recv(this.S_Buffer, 0, length);
            List<Packet> packetList = this.S_Security.TransferIncoming();
            if (packetList != null)
            {
              foreach (Packet packet1 in packetList)
              {
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
                        this.Send(true);
                        continue;
                      }
                      this.C_Security.Send(packet1);
                      this.Send(false);
                      break;
                    }
                  case 20480:
                  case 36864:
                    this.Send(true);
                    continue;
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
                        this.Send(false);
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
                        this.Send(false);
                      }
                      else
                      {
                        this.C_Security.Send(packet1);
                        this.Send(false);
                      }
                    }
                    this.Send(false);
                    break;
                  case 41218:
                    if (packet1.ReadUInt8() == (byte) 1)
                    {
                      uint num2 = packet1.ReadUInt32();
                      packet1.ReadAscii();
                      packet1.ReadUInt16();
                      Packet packet2 = new Packet((ushort) 41218, true);
                      packet2.WriteUInt8((byte) 1);
                      packet2.WriteUInt32(num2);
                      packet2.WriteAscii(Program.LocalIP);
                      packet2.WriteUInt16((object) MainForm.PublicAgent);
                      this.C_Security.Send(packet2);
                      this.Send(false);
                      continue;
                    }
                    this.C_Security.Send(packet1);
                    this.Send(false);
                    break;
                  case 41763:
                    if (packet1.ReadUInt8() == (byte) 2)
                    {
                      MainForm.AutoImage = false;
                      new SqlCommand("exec KGUARD.._SetSetting 'AutoImage_Enabled', '" + MainForm.AutoImage.ToString() + "'", Program.sql).ExecuteNonQuery();
                    }
                    this.C_Security.Send(packet1);
                    this.Send(false);
                    break;
                  default:
                    this.C_Security.Send(packet1);
                    this.Send(false);
                    break;
                }
              }
            }
            this.DoRecvFromServer();
          }
          else
          {
            this.DisconnectServerSocket(5);
            this.m_delDisconnect(ref this.C_Socket, this.HandlerType);
          }
        }
        catch (Exception)
                {
          this.DisconnectServerSocket(6);
          this.m_delDisconnect(ref this.C_Socket, this.HandlerType);
        }
      }
    }

    private void DoRecvFromClient()
    {
      try
      {
        this.C_Socket.BeginReceive(this.C_Buffer, 0, this.C_Buffer.Length, SocketFlags.None, new AsyncCallback(this.OnReceiveFromClient), (object) null);
      }
      catch (Exception ex)
      {
        Log.WriteError(ex, "(SideGateway) void DoRecvFromClient()");
        this.DisconnectServerSocket(7);
        this.m_delDisconnect(ref this.C_Socket, this.HandlerType);
      }
    }

    private void DoRecvFromServer()
    {
      try
      {
        this.S_Socket.BeginReceive(this.S_Buffer, 0, this.S_Buffer.Length, SocketFlags.None, new AsyncCallback(this.OnReceiveFromServer), (object) null);
      }
      catch (Exception ex)
      {
        Log.WriteError(ex, "(SideGateway) void DoRecvFromServer()");
        this.DisconnectServerSocket(8);
        this.m_delDisconnect(ref this.C_Socket, this.HandlerType);
      }
    }

    public void Send(bool ToHost)
    {
      try
      {
        lock (this.LockObj)
        {
          foreach (KeyValuePair<TransferBuffer, Packet> keyValuePair in (ToHost ? this.S_Security : this.C_Security).TransferOutgoing())
            (ToHost ? this.S_Socket : this.C_Socket).Send(keyValuePair.Key.Buffer);
        }
      }
      catch
      {
      }
    }

    private void DisconnectServerSocket(int Commander)
    {
      try
      {
        if (this.S_Socket != null)
        {
          this.S_Socket.Close();
          lock (MainForm.AsyncGatewayClients)
            MainForm.AsyncGatewayClients.Remove(this);
          lock (MainForm.GatewayIPList)
            MainForm.GatewayIPList.Remove(this.IP);
        }
        this.S_Socket = (Socket) null;
      }
      catch
      {
      }
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
      this.Send(false);
    }

    private void SendIPLimit()
    {
      Packet packet = new Packet((ushort) 41218);
      packet.WriteUInt16((ushort) 2050);
      this.C_Security.Send(packet);
      this.Send(false);
    }

    private void SendServerCrowded()
    {
      Packet packet = new Packet((ushort) 41218);
      packet.WriteUInt16((ushort) 1282);
      this.C_Security.Send(packet);
      this.Send(false);
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
  }
}
