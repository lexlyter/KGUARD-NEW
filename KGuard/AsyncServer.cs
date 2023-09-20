// Decompiled with JetBrains decompiler
// Type: KGuard.AsyncServer
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace KGuard
{
  public sealed class AsyncServer
  {
    private static int ClientCount_Gateway = 0;
    private static int ClientCount_Agent = 0;
    private AsyncServer.Engine_ServerType ServerType;
    private Socket ListenerSocket = (Socket) null;
    private Thread ListenerThread = (Thread) null;
    private ManualResetEvent Waiter = new ManualResetEvent(false);

    public bool Start(string LocalIP, int nPort, AsyncServer.Engine_ServerType ServType)
    {
      bool flag = false;
      if (this.ListenerSocket != null)
        return false;
      this.ServerType = ServType;
      this.ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      try
      {
        this.ListenerSocket.Bind((EndPoint) new IPEndPoint(IPAddress.Parse(LocalIP), nPort));
        this.ListenerSocket.Listen(5);
        this.ListenerThread = new Thread(new ThreadStart(this.Listener));
        this.ListenerThread.Start();
        flag = true;
      }
      catch (Exception ex)
      {
        Log.WriteError(ex, "public bool Start(string BindAddr, int nPort, Engine_ServerType ServType)");
      }
      return flag;
    }

    private void Listener()
    {
      while (this.ListenerSocket != null)
      {
        this.Waiter.Reset();
        try
        {
          this.ListenerSocket.BeginAccept(new AsyncCallback(this.AcceptConnectionCallback), (object) null);
        }
        catch (Exception ex)
        {
          Log.WriteError(ex, "void Listener()");
        }
        this.Waiter.WaitOne();
      }
    }

    private void AcceptConnectionCallback(IAsyncResult iar)
    {
      Socket ClientSocket = (Socket) null;
      this.Waiter.Set();
      try
      {
        ClientSocket = this.ListenerSocket.EndAccept(iar);
      }
      catch (Exception ex)
      {
        Log.WriteError(ex, "void AcceptConnectionCallback(IAsyncResult iar) /1");
      }
      try
      {
        switch (this.ServerType)
        {
          case AsyncServer.Engine_ServerType.GatewayServer:
            SideGateway sideGateway = new SideGateway(ClientSocket, new AsyncServer.delClientDisconnect(this.OnClientDisconnect));
            ++AsyncServer.ClientCount_Gateway;
            break;
          case AsyncServer.Engine_ServerType.AgentServer:
            SideAgent sideAgent = new SideAgent(ClientSocket, new AsyncServer.delClientDisconnect(this.OnClientDisconnect));
            ++AsyncServer.ClientCount_Agent;
            break;
        }
      }
      catch (Exception ex)
      {
        Log.WriteError(ex, "void AcceptConnectionCallback(IAsyncResult iar) /2");
      }
    }

    public void OnClientDisconnect(ref Socket ClientSock, AsyncServer.Engine_ServerType HandlerType)
    {
      if (ClientSock == null)
        return;
      switch (HandlerType)
      {
        case AsyncServer.Engine_ServerType.GatewayServer:
          --AsyncServer.ClientCount_Gateway;
          break;
        case AsyncServer.Engine_ServerType.AgentServer:
          --AsyncServer.ClientCount_Agent;
          break;
      }
      try
      {
        ClientSock.Close();
      }
      catch (Exception ex)
      {
        Log.WriteError(ex, "void OnClientDisconnect(ref Socket ClientSock, Engine_ServerType HandlerType)");
      }
      ClientSock = (Socket) null;
      GC.Collect();
    }

    public enum Engine_ServerType : byte
    {
      GatewayServer,
      AgentServer,
    }

    public delegate void delClientDisconnect(
      ref Socket ClientSocket,
      AsyncServer.Engine_ServerType HandlerType);
  }
}
