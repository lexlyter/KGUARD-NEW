// Decompiled with JetBrains decompiler
// Type: KGuard.MainForm
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KGuard
{
  public class MainForm : Form
  {
    private static Kurulum KurulumForm = new Kurulum();
    public static List<ClientsGateway> GatewayClients = new List<ClientsGateway>();
    public static List<string> GatewayIPList = new List<string>();
    public static List<ClientsAgent> AgentClients = new List<ClientsAgent>();
    public static List<string> AgentIPList = new List<string>();
    public static List<SideGateway> AsyncGatewayClients = new List<SideGateway>();
    public static List<SideAgent> AsyncAgentClients = new List<SideAgent>();
    public static bool OnlyAuthorized = false;
    public static bool MultiAgent = false;
    public static int LocalGateway;
    public static int LocalAgent;
    public static int PublicGateway;
    public static int PublicAgent;
    public static int LocalAgent2;
    public static int PublicAgent2;
    public static string ServerIP;
    public static string ServerIP2;
    public static bool OnlineKontrol = false;
    public static bool LimitOnline = false;
    public static bool PremiumAl = false;
    public static bool RastgeleDC = false;
    public static List<int> PremiumIDleri = new List<int>();
    public static double EklenecekOnline = 0.0;
    public static int ServerKapasite = 1000;
    public static bool MultipleOnlineCount = false;
    public static List<string> YetkiliIPList = new List<string>();
    public static bool GMYetkiliIP = false;
    public static bool IPLimit = false;
    public static int IPLimitValue = 100;
    public static bool WelcomeMsg = false;
    public static string WelcomeMsg1 = "";
    public static string WelcomeMsg2 = "";
    public static bool AutoImage = false;
    public static string AutoImageValue = "";
    private static Thread HandleGateway;
    private static Thread HandleAgent;
    private static readonly Thread HandleAgent2;
    private static Socket SocketGatewayServer;
    private static Socket SocketAgentServer;
    private static Socket SocketAgentServer2;
    public static object LoginObj = new object();
    public static object FirewallBlockLocker = new object();
    private static object ListLocker = new object();
    public static List<string> BlockedIP = new List<string>();
    private static int Suspect = 0;
    public static bool JobRevRestrc = false;
    public static bool ThiefRewardRestrc = false;
    public static bool AntiBugExc = false;
    public static bool AntiBugStall = false;
    public static bool AntiBugEsc = false;
    public static bool SwearFilter = false;
    public static bool PlusLimit = false;
    public static int PlusLimitValue = 12;
    public static List<string> SwearWords = new List<string>();
    public static bool Dev_NoticeThr = false;
    public static bool Dev_GlobalThr = false;
    public static bool Dev_SendMsg = false;
    public static bool Dev_VerifyPM = false;
    public static bool Dev_LogPM = false;
    public static bool Dev_LogGlo = false;
    public static bool Dev_GiveDC = false;
    public static List<string> PMVerifyNames = new List<string>();
    public static bool Dev_LockSlot = false;
    public static bool Dev_LoginLogoutLog = false;
    public static bool Dev_DoEverySecond = false;
    public static bool GuildLimitEnabled = false;
    public static bool UnionLimitEnabled = false;
    public static int GuildLimitValue = 50;
    public static int UnionLimitValue = 8;
    public static bool AntiSkillBug = false;
    public static bool AntiTaxRateChange = false;
    public static bool AntiAcademyCreate = false;
    public static bool AntiDropInTown = false;
    public static bool MaxPacketLimit = false;
    public static int DinnerTime = 10;
    public static bool LimitOfFortressWarEnabled = false;
    public static int LimitOfFortressWarValue = 200;
    public static bool DisablePetsInFTW = false;
    private const int EM_SETCUEBANNER = 5377;
    private IContainer components = (IContainer) null;
    private Label label1;
    private Label label3;
    private Label label5;
    private Label label6;
    private Label label7;
    private Label labelThread;
    private Label labelMEMORY;
    private Label label_LoginClient;
    private Label label_AgentClient;
    private Label label_Suspect;
    private ListView listView1;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem toolStripMenuItem1;
    private ContextMenuStrip contextMenuStrip2;
    private ToolStripMenuItem toolStripMenuItem2;
    private ContextMenuStrip contextMenuStrip3;
    private ToolStripMenuItem toolStripMenuItem3;
    private Button button_GetSettings;
    private ContextMenuStrip contextMenuStrip4;
    private ToolStripMenuItem toolStripMenuItem4;
    private TabPage tabPage5;
    private GroupBox groupBox16;
    private CheckBox checkBox_GiveDC;
    private GroupBox groupBox15;
    private ListBox listBox_VerifyNames;
    private TextBox textBox_IncPMSendedVerify;
    private CheckBox checkBox_VerifyPM;
    private GroupBox groupBox14;
    private Label label25;
    private CheckBox checkBox_Devs_Notice;
    private CheckBox checkBox_Devs_SendMsg;
    private GroupBox groupBox13;
    private CheckBox checkBox_LogUserGlobal;
    private CheckBox checkBox_LogUserPM;
    private TabPage tabPage4;
    private GroupBox groupBox12;
    private TextBox textBox_InsuiltChat;
    private ListBox listBox_InsuiltChat;
    private GroupBox groupBox10;
    private CheckBox checkBox_ThiefRewardRestrc;
    private TextBox textBox_PlusLimit;
    private CheckBox checkBox_PlusLimit;
    private CheckBox checkBox_Swear;
    private CheckBox checkBox_BugStall;
    private CheckBox checkBox_BugEsc;
    private CheckBox checkBox_BugExc;
    private CheckBox checkBox_JobRev;
    private GroupBox groupBox11;
    private TextBox textBox_Welcome2;
    private Label label27;
    private Label label26;
    private TextBox textBox_Welcome1;
    private CheckBox checkBox_Welcome;
    private TabPage tabPage2;
    private GroupBox groupBox7;
    private Button button_OnlineSave;
    private Button button_OnlineEdit;
    private Panel panel_OnlineYonetim;
    private Button button_RemoveJobID;
    private Button button_AddJobID;
    private Label label20;
    private TextBox textBox_JobID;
    private ListBox listBox_JobID;
    private CheckBox checkBox_Premiums;
    private CheckBox checkBox_RandomDC;
    private CheckBox checkBox_LimitCapac;
    private Label label19;
    private CheckBox checkBox_AddOnline;
    private TextBox textBox_OnlineCap;
    private TextBox textBox_OnlineCount;
    private Label label18;
    private GroupBox groupBox17;
    private Label label31;
    private Label label30;
    private Label label29;
    private Label label28;
    private TextBox textBox_SecondAgentFakePort;
    private TextBox textBox_SecondAgentPort;
    private TextBox textBox_SecondAgentIP;
    private CheckBox checkBox_MultiAgent;
    private TabPage tabPage1;
    private GroupBox groupBox1;
    private Label label4;
    private Label label8;
    private Label label_LisansUser;
    private Label label_LisansIP;
    private GroupBox groupBox9;
    private Button button_AddCustomIP;
    private Label label24;
    private TextBox textBox_CustomLimitValue;
    private Label label22;
    public ListView listView_CustomLimit;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader4;
    private TextBox textBox_CustomLimitIP;
    private GroupBox groupBox6;
    private Button button_YetkiliIPSave;
    private Button button_YetkiliIPEdit;
    private Panel panel_YetkiliIP;
    private Button button_RemoveIP;
    private TextBox textBox_YetkiliIP;
    private Button button_AddIP;
    private ListBox listBox_YetkiliIP;
    private CheckBox checkBox_GMIPKontrol;
    private GroupBox groupBox8;
    private Label label21;
    private TextBox textBox_IPLimit;
    private CheckBox checkBox_IPLimit;
    private GroupBox groupBox4;
    private GroupBox groupBox_StartServer;
    private Label label15;
    private Button button_StartServer;
    private ComboBox comboBox_Service;
    private GroupBox groupBox5;
    private Button button_PortSave;
    private Button button_PortEdit;
    private Panel panel_PortSetting;
    private TextBox textBox_ServIP;
    private Label label16;
    private Label label14;
    private TextBox textBox_Port1;
    private TextBox textBox_Port3;
    private TextBox textBox_Port4;
    private Label label12;
    private Label label11;
    private Label label13;
    private TextBox textBox_Port2;
    private Button button_OpenHelper;
    private TabControl tabControl1;
    private TextBox textBox_UnionLimit;
    private TextBox textBox_GuildLimit;
    private CheckBox checkBox_UnionLimit;
    private CheckBox checkBox_GuildLimit;
    private TabPage tabPage3;
    private GroupBox groupBox18;
    private TextBox textBox_IPC_IP;
    private TextBox textBox_IPC_CharName;
    private ListView listView_IPC;
    private ColumnHeader columnHeader5;
    private ColumnHeader columnHeader6;
    private ColumnHeader columnHeader7;
    private ContextMenuStrip contextMenuStrip5;
    private ToolStripMenuItem karakteriBanlaToolStripMenuItem;
    private ToolStripMenuItem ıPBlockAtToolStripMenuItem;
    private GroupBox groupBox22;
    private GroupBox groupBox19;
    private GroupBox groupBox21;
    private Button button_FirewallBan;
    private TextBox textBox_IPBlock_IP;
    private GroupBox groupBox20;
    private Button button_LoginBan;
    private RichTextBox richTextBox_Ban_Guide;
    private TextBox textBox_LoginBan_DAY;
    private TextBox textBox_LoginBan_CN;
    private Button button_BanHistory_Firewall;
    private Button button_BanHistory_Login;
    private ListView listView_BanHistory;
    private ColumnHeader Ban_ColHeader1;
    private ColumnHeader Ban_ColHeader2;
    private ColumnHeader Ban_ColHeader3;
    private CheckBox checkBox_LockSlot;
    private ContextMenuStrip contextMenuStrip6;
    private ToolStripMenuItem kaldırToolStripMenuItem;
    private CheckBox checkBox_SkillBug;
    private CheckBox checkBox_AcademyCreate;
    private CheckBox checkBox_TaxRate;
    private CheckBox checkBox_DropInTown;
    private CheckBox checkBox_Multiplier;
    private GroupBox groupBox23;
    private GroupBox groupBox24;
    private CheckBox checkBox_MaxPacketLimit;
    private CheckBox checkBox_Devs_GlobalSw;
    private CheckBox checkBox_DoEverySecond;
    private CheckBox checkBox_LoginLogoutLog;
    private GroupBox groupBox25;
    private Label label2;
    private ComboBox comboBox_DinnerTime;
    private Label label23;
    private Button button_HelpForFTWLimit;
    private CheckBox checkBox_LimitOfFortressWar;
    private TextBox textBox_LimitOfFortressWar;
    private CheckBox checkBox_DisablePetsInFTW;
        private readonly object state;
        private readonly int _003C_003E1__state;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

    public MainForm() => this.InitializeComponent();

    private void MainForm_Load(object sender, EventArgs e)
    {
      Globals.MainWindow = this;
      new Task(new Action(this.RightContent)).Start();
      new Task(new Action(this.CheckSQL)).Start();
      new Task(new Action(this.StartProg)).Start();
      MainForm.SendMessage(this.textBox_Welcome1.Handle, 5377, 0, "Merhaba");
      MainForm.SendMessage(this.textBox_Welcome2.Handle, 5377, 0, "Hoşgeldin");
      MainForm.SendMessage(this.textBox_InsuiltChat.Handle, 5377, 0, "Filtrelenecek Sözcük");
      MainForm.SendMessage(this.textBox_IncPMSendedVerify.Handle, 5377, 0, "Sahte Karakter Adı");
      MainForm.SendMessage(this.textBox_IPC_CharName.Handle, 5377, 0, "Karakter Adı");
      MainForm.SendMessage(this.textBox_IPC_IP.Handle, 5377, 0, "IP Adresi");
      MainForm.SendMessage(this.textBox_LoginBan_CN.Handle, 5377, 0, "Karakter Adı");
      MainForm.SendMessage(this.textBox_LoginBan_DAY.Handle, 5377, 0, "Gün");
      MainForm.SendMessage(this.textBox_IPBlock_IP.Handle, 5377, 0, "IP Adresi");
      MainForm.HandleGateway = new Thread(new ThreadStart(this.HandleServerGateway));
      MainForm.HandleAgent = new Thread(new ThreadStart(this.HandleServerAgent));
    }

    private void GetAllData()
    {
      this.GetPortSettings();
      this.GetOnlineSettings();
      this.GetYetkiliIP();
      this.GetSpecialLimits();
      this.GetWelcomeMsgSetting();
      this.GetAutoImageSettings();
      this.GetRestrcSettings();
      this.GetSwearList();
      this.GetDeveloperSettings();
      this.GetMultiAgentSettings();
      this.GetGuildLimits();
      this.GetExtraSecuritySettings();
      this.GetDinnerTimeSetting();
    }

    private void StartProg()
    {
      while (true)
      {
        MainForm.WriteConsole("Veritabanı kontrolleri sağlanıyor...");
        this.FillLicensePanel();
        new Task(new Action(this.GetLc)).Start();
        try
        {
          bool boolean;
          using (SqlDataReader sqlDataReader = new SqlCommand("SELECT COUNT(name) FROM master..sysdatabases where name = 'KGUARD'", Program.sql).ExecuteReader())
          {
            sqlDataReader.Read();
            boolean = Convert.ToBoolean(sqlDataReader[0]);
            sqlDataReader.Close();
          }
          if (!boolean)
          {
            new SqlCommand(Program.GetTableQuery("DBCreate"), Program.sql).ExecuteNonQuery();
            MainForm.WriteConsole("KGUARD veritabanı oluşturuldu.", Color.Green);
          }
          else
            MainForm.WriteConsole("KGUARD veritabanı mevcut.");
          new SqlCommand("USE [KGUARD]", Program.sql).ExecuteNonQuery();
          this.CreateTables();
          this.CreateProcedures();
          this.GetAllData();
          new SqlCommand("exec __Dev_DoWhileStart", Program.sql).ExecuteNonQueryAsync();
          new Task(new Action(this.CleanBlocks)).Start();
          new Task(new Action(this.ChatThread)).Start();
          new Task(new Action(this.ExecEverySecond)).Start();
          MainForm.WriteConsole("Tüm kontroller tamamlandı.", Color.Green);
          break;
        }
        catch (Exception ex)
        {
          MainForm.WriteConsole("Algoritma akışında bir hata oldu. Bir kaç saniye içinde tekrar başlatılacak. Hata metin dosyasına kaydedildi.", Color.Red);
          MainForm.WriteConsole("SQL ile alakalı hatalar alıyorsanız KGUARD veritabanını silmeyi deneyin.", Color.Red);
          Program.WriteError(ex.ToString(), nameof (StartProg));
          Thread.Sleep(5000);
        }
      }
      this.tabControl1.Enabled = true;
    }

    private void CreateTables()
    {
      List<string> stringList = new List<string>();
      using (SqlDataReader sqlDataReader = new SqlCommand("select name from KGUARD..sysobjects where xtype = 'U'", Program.sql).ExecuteReader())
      {
        while (sqlDataReader.Read())
          stringList.Add(sqlDataReader[0].ToString());
        sqlDataReader.Close();
      }
      foreach (string str in new List<string>()
      {
        "_Settings",
        "_PremiumID",
        "_YetkiliIP",
        "_SpecialLimit",
        "_SwearWord",
        "__Dev_CharLastIP",
        "__Dev_SendMsg",
        "__Dev_Notice",
        "__Dev_GiveDC",
        "_VerifyNames",
        "_FirewallBlocks",
        "__Dev_LockSlot",
        "__Dev_SendGlobal"
      })
      {
        if (!stringList.Contains(str))
        {
          new SqlCommand(Program.GetTableQuery("TB" + str).Replace("GO", "--GO"), Program.sql).ExecuteNonQuery();
          MainForm.WriteConsole(str + " tablosu oluşturuldu.", Color.Green);
        }
      }
    }

    private void CreateProcedures()
    {
      List<string> stringList = new List<string>();
      using (SqlDataReader sqlDataReader = new SqlCommand("select name from KGUARD..sysobjects where xtype = 'P'", Program.sql).ExecuteReader())
      {
        while (sqlDataReader.Read())
          stringList.Add(sqlDataReader[0].ToString());
        sqlDataReader.Close();
      }
      foreach (string str in new List<string>()
      {
        "_SetSetting",
        "_JobCheck",
        "_GetOptLvBySlot",
        "__Dev_AddCharIP",
        "_LastNotice",
        "_LastDC",
        "__Dev_GLOBAL",
        "__Dev_PM",
        "_GetGuildMemberCount",
        "_GetUnionMemberCount",
        "_GetIPHistory",
        "_GiveBan",
        "_GetBanHistory",
        "_SlotIsLocked",
        "_RemoveBan",
        "_IsBattleField",
        "_IsGM",
        "_IsExists",
        "_IsHavePremium",
        "_LastGlobal",
        "__Dev_Log_LoginLogout",
        "__Dev_DoWhileStart",
        "__Dev_DoEverySecond",
        "_GetAvatarType",
        "_GetFortressRequestCount",
        "_GetFortressPetStatus"
      })
      {
        if (!stringList.Contains(str))
        {
          new SqlCommand(Program.GetTableQuery("PRC" + str), Program.sql).ExecuteNonQuery();
          MainForm.WriteConsole(str + " prosedürü oluşturuldu.", Color.Blue);
        }
      }
    }

        private void HandleServerGateway()
        {
            MainForm.WriteConsole("HandleServer Gateway başlatıldı. (" + MainForm.PublicGateway.ToString() + ")", Color.DarkCyan);
            while (true)
            {
                try
                {
                    Socket tSocket = MainForm.SocketGatewayServer.Accept();
                    ClientsGateway clientsGateway = new ClientsGateway(tSocket);
                    tSocket = (Socket)null;
                }
                catch (Exception ex)
                {
                    Program.WriteError(ex.ToString(), nameof(HandleServerGateway));
                }
            }
        }

        private void HandleServerAgent()
        {
            MainForm.WriteConsole("HandleServer Agent başlatıldı. (" + MainForm.PublicAgent.ToString() + ")", Color.DarkCyan);
            while (true)
            {
                try
                {
                    Socket tSocket = MainForm.SocketAgentServer.Accept();
                    ClientsAgent clientsAgent = new ClientsAgent(tSocket, MainForm.LocalAgent, MainForm.ServerIP);
                    tSocket = (Socket)null;
                }
                catch (Exception ex)
                {
                    Program.WriteError(ex.ToString(), nameof(HandleServerAgent));
                }
            }
        }

        private void HandleServerAgent_2()
        {
            MainForm.WriteConsole("HandleServer Agent 2 başlatıldı. (" + MainForm.PublicAgent2.ToString() + ")", Color.DarkCyan);
            while (true)
            {
                try
                {
                    Socket tSocket = MainForm.SocketAgentServer2.Accept();
                    ClientsAgent clientsAgent = new ClientsAgent(tSocket, MainForm.LocalAgent2, MainForm.ServerIP2);
                    tSocket = (Socket)null;
                }
                catch (Exception ex)
                {
                    Program.WriteError(ex.ToString(), "HandleServerAgent 2");
                }
                Thread.Sleep(1);
            }
        }

        public static void WriteConsole(string msg)
    {
      try
      {
        lock (MainForm.ListLocker)
        {
          ListViewItem listViewItem = new ListViewItem(new string[2]
          {
            DateTime.Now.ToLongTimeString(),
            msg
          });
          Globals.MainWindow.listView1.Items.Add(listViewItem);
          Globals.MainWindow.listView1.Items[Globals.MainWindow.listView1.Items.Count - 1].EnsureVisible();
          Globals.MainWindow.listView1.Columns[Globals.MainWindow.listView1.Columns.Count - 1].Width = -2;
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "AddList");
      }
    }

    public static void WriteConsole(string msg, Color color)
    {
      try
      {
        lock (MainForm.ListLocker)
        {
          Globals.MainWindow.listView1.Items.Add(new ListViewItem(new string[2]
          {
            DateTime.Now.ToLongTimeString(),
            msg
          })
          {
            ForeColor = color
          });
          Globals.MainWindow.listView1.Items[Globals.MainWindow.listView1.Items.Count - 1].EnsureVisible();
          Globals.MainWindow.listView1.Columns[Globals.MainWindow.listView1.Columns.Count - 1].Width = -2;
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "AddListColored");
      }
    }

    private void RightContent()
    {
      while (true)
      {
        try
        {
          PerformanceCounter performanceCounter = new PerformanceCounter("Process", "Working Set - Private", Process.GetCurrentProcess().ProcessName);
          this.label_LoginClient.Text = MainForm.AsyncGatewayClients.Count.ToString();
          this.label_AgentClient.Text = MainForm.AsyncAgentClients.Count.ToString();
          this.label_Suspect.Text = MainForm.Suspect.ToString();
          this.labelThread.Text = Process.GetCurrentProcess().Threads.Count.ToString();
          this.labelMEMORY.Text = (Convert.ToInt32(performanceCounter.NextValue()) / 1024 / 1024).ToString();
          Thread.Sleep(1000);
        }
        catch (Exception ex)
        {
          Program.WriteError(ex.ToString(), nameof (RightContent));
        }
      }
    }

    private void CheckSQL()
    {
      while (true)
      {
        Thread.Sleep(1000);
        try
        {
          ConnectionState state = Program.sql.State;
          if (state.ToString() != "Open")
          {
            state = Program.sql.State;
            MainForm.WriteConsole("SQL Bağlantısı koptu. Programı yeniden başlatın. [SQL State: " + state.ToString() + "]", Color.Red);
            state = Program.sql.State;
            int num = (int) MessageBox.Show("SQL Bağlantısı koptu. Programı yeniden başlatın. [SQL State: " + state.ToString() + "]");
            break;
          }
        }
        catch (Exception ex)
        {
          Program.WriteError(ex.ToString(), nameof (CheckSQL));
        }
      }
    }

    private void FillLicensePanel()
    {
      Program.RefreshLicense();
      this.label_LisansIP.Text = Program.LocalIP;
      this.label_LisansUser.Text = Program.Email;
    }

    public static void Block(string ip)
    {
      try
      {
        lock (MainForm.BlockedIP)
          MainForm.BlockedIP.Add(ip);
        MainForm.WriteConsole(ip + " blocked.", Color.RosyBrown);
        ++MainForm.Suspect;
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "MainFormBlock");
      }
    }

    private void RemoveLoginBan(string CharName)
    {
      try
      {
        new SqlCommand("exec _RemoveBan '" + CharName + "'", Program.sql).ExecuteNonQuery();
        this.Button_BanHistory_Login_Click((object) null, (EventArgs) null);
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), nameof (RemoveLoginBan));
      }
    }

    public void BlockFirewall(string blockip)
    {
      try
      {
        lock (MainForm.FirewallBlockLocker)
        {
          using (SqlDataReader sqlDataReader = new SqlCommand("select * from _FirewallBlocks where IP = '" + blockip + "'", Program.sql).ExecuteReader())
          {
            if (sqlDataReader.Read())
            {
              sqlDataReader.Close();
              return;
            }
            sqlDataReader.Close();
          }
          new Task((Action) (() =>
          {
            try
            {
              System.Type typeFromClsid1 = System.Type.GetTypeFromCLSID(new Guid("{E2B3C97F-6AE1-41AC-817A-F6F92166D7DD}"));
              System.Type typeFromClsid2 = System.Type.GetTypeFromCLSID(new Guid("{2C5BC43E-3369-4C33-AB0C-BE9469677AF4}"));
              INetFwPolicy2 instance1 = (INetFwPolicy2) Activator.CreateInstance(typeFromClsid1);
              INetFwRule instance2 = (INetFwRule) Activator.CreateInstance(typeFromClsid2);
              instance2.Name = "KGUARD " + blockip;
              instance2.Description = "Blocked from KGUARD.";
              instance2.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
              instance2.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
              instance2.Enabled = true;
              instance2.InterfaceTypes = "All";
              instance2.RemoteAddresses = blockip;
              ((INetFwPolicy2) Activator.CreateInstance(System.Type.GetTypeFromProgID("HNetCfg.FwPolicy2"))).Rules.Add(instance2);
              MainForm.WriteConsole(blockip + " adresinin tüm portlara erişimi engellendi.", Color.IndianRed);
              new SqlCommand("insert into _FirewallBlocks (IP) values ('" + blockip + "')", Program.sql).ExecuteNonQuery();
            }
            catch
            {
            }
          })).Start();
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), nameof (BlockFirewall));
      }
    }

    private void RemoveBlockFirewall(string blockip)
    {
      try
      {
        ((INetFwPolicy2) Activator.CreateInstance(System.Type.GetTypeFromProgID("HNetCfg.FwPolicy2"))).Rules.Remove("KGUARD " + blockip);
        new SqlCommand("delete from _FirewallBlocks where IP = '" + blockip.Replace("'", "") + "'", Program.sql).ExecuteNonQuery();
        this.button_BanHistory_Firewall_Click((object) null, (EventArgs) null);
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), nameof (RemoveBlockFirewall));
      }
    }

        private void CleanBlocks()
        {
            // ISSUE: reference to a compiler-generated field
            int num = _003C_003E1__state;
            while (true)
            {
                try
                {
                    Thread.Sleep(20000);
                    if (MainForm.BlockedIP.Count >= 1)
                    {
                        lock (MainForm.BlockedIP)
                            MainForm.BlockedIP.Clear();
                        MainForm.WriteConsole("Blocklar kaldırıldı.", Color.RosyBrown);
                    }
                }
                catch
                {
                }
            }
        }

        private void GetLc()
    {
      bool flag = false;
      while (true)
      {
        Thread.Sleep(10000);
        try
        {
          this.CheckHst();
          string lc = Program.GetLc();
          if (lc != "NoLicance" && lc.Contains("<REGDATE>"))
          {
            Program.LocalIP = new Regex("<IP>.*</IP>").Match(lc).Value.Replace("<IP>", "").Replace("</IP>", "");
            Program.Days = new Regex("<TIME>.*</TIME>").Match(lc).Value.Replace("<TIME>", "").Replace("</TIME>", "");
            Program.Ver = new Regex("<VER>.*</VER>").Match(lc).Value.Replace("<VER>", "").Replace("</VER>", "");
            Program.RegDate = new Regex("<REGDATE>.*</REGDATE>").Match(lc).Value.Replace("<REGDATE>", "").Replace("</REGDATE>", "");
            Program.Email = new Regex("<EMAIL>.*</EMAIL>").Match(lc).Value.Replace("<EMAIL>", "").Replace("</EMAIL>", "");
            if (Convert.ToInt16(Program.Days) < (short) 2 && !flag)
            {
              flag = true;
              int num = (int) MessageBox.Show("Lisansınızın son günü. Süreniz bittiğinde oyuna girişler sistem tarafından kapatılacaktır. Lütfen k-guard.org adresinden yeni lisans satın alın.");
            }
          }
          else if (lc == "NoLicance")
          {
            Program.LocalIP = "";
            Program.Days = "NoLic";
            MainForm.SocketGatewayServer.Dispose();
            MainForm.SocketAgentServer.Dispose();
            MainForm.HandleGateway.Abort();
            MainForm.HandleAgent.Abort();
            int num = (int) MessageBox.Show("Lisansınız sona ermiş görünüyor. Oyuna girişler şu andan itibaren kapalı. Lütfen k-guard.org adresinden yeni bir lisans satın alın.");
            break;
          }
          this.FillLicensePanel();
        }
        catch
        {
        }
      }
    }

    private void CheckHst()
    {
      string str = "";
      try
      {
        FileStream fileStream = new FileStream("C:\\Windows\\System32\\drivers\\etc\\hosts", FileMode.Open, FileAccess.Read);
        StreamReader streamReader = new StreamReader((Stream) fileStream);
        str = streamReader.ReadToEnd();
        streamReader.Close();
        fileStream.Close();
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), nameof (CheckHst));
      }
      if (!str.Contains("k-guard") && !str.Contains("ömerçolak") && !str.Contains("xn--merolak-wxa5k"))
        return;
      File.WriteAllText("C:\\Windows\\System32\\drivers\\etc\\hosts", str.Replace("k-guard", "kguard").Replace("ömerçolak", "ömarçolak").Replace("xn--merolak-wxa5k", "xn--merolak-wwa5k"));
    }

    private bool SendMessageByCharName(string Sender, string CharName, string Msg, int MsgType)
    {
      bool flag = false;
      List<ClientsAgent> clientsAgentList = new List<ClientsAgent>((IEnumerable<ClientsAgent>) MainForm.AgentClients);
      if (clientsAgentList.Count > 0)
      {
        foreach (ClientsAgent clientsAgent in clientsAgentList)
        {
          if (!flag)
          {
            try
            {
              if (!(clientsAgent.CharName != CharName))
              {
                flag = true;
                switch (MsgType)
                {
                  case 1:
                    clientsAgent.SendPM(Sender, Msg);
                    break;
                  case 2:
                    clientsAgent.SendPMask(Sender, Msg);
                    break;
                  case 3:
                    clientsAgent.SendNotice(Msg);
                    break;
                  case 4:
                    clientsAgent.SendGlobal(Sender, Msg);
                    break;
                  case 5:
                    clientsAgent.SendAcademy(Sender, Msg);
                    break;
                }
              }
            }
            catch (Exception ex)
            {
              Program.WriteError(ex.ToString(), "SendNoticeToServer");
            }
          }
          else
            break;
        }
      }
      return flag;
    }

    private void GiveDCByCharName(string CharName)
    {
      List<ClientsAgent> clientsAgentList = new List<ClientsAgent>((IEnumerable<ClientsAgent>) MainForm.AgentClients);
      if (clientsAgentList.Count <= 0)
        return;
      foreach (ClientsAgent clientsAgent in clientsAgentList)
      {
        try
        {
          if (clientsAgent.CharName == CharName)
          {
            clientsAgent.CleanClient(2);
            MainForm.WriteConsole(clientsAgent.CharName + " is disconnected.");
            break;
          }
        }
        catch (Exception ex)
        {
          Program.WriteError(ex.ToString(), nameof (GiveDCByCharName));
        }
      }
    }

    private void SendNoticeToServer(string NoticeMsg)
    {
      List<ClientsAgent> clientsAgentList = new List<ClientsAgent>((IEnumerable<ClientsAgent>) MainForm.AgentClients);
      if (clientsAgentList.Count < 1)
        return;
      foreach (ClientsAgent clientsAgent in clientsAgentList)
      {
        try
        {
          clientsAgent.SendNotice(NoticeMsg);
        }
        catch (Exception ex)
        {
          Program.WriteError(ex.ToString(), nameof (SendNoticeToServer));
        }
      }
    }

    private void SendGlobalToServer(string SenderName, string GlobalMsg)
    {
      List<ClientsAgent> clientsAgentList = new List<ClientsAgent>((IEnumerable<ClientsAgent>) MainForm.AgentClients);
      if (clientsAgentList.Count < 1)
        return;
      foreach (ClientsAgent clientsAgent in clientsAgentList)
      {
        try
        {
          clientsAgent.SendGlobal(SenderName, GlobalMsg);
        }
        catch (Exception ex)
        {
          Program.WriteError(ex.ToString(), "SendNoticeToServer");
        }
      }
    }

        private void ChatThread()
        {
            // ISSUE: reference to a compiler-generated field
            int num = _003C_003E1__state;
            while (true)
            {
                Thread.Sleep(10);
                try
                {
                    if (MainForm.Dev_NoticeThr)
                    {
                        using (SqlDataReader dr = new SqlCommand("exec _LastNotice", Program.sql).ExecuteReader())
                        {
                            if (dr.Read())
                                this.SendNoticeToServer(dr[0].ToString());
                            dr.Close();
                        }
                    }
                    if (MainForm.Dev_GlobalThr)
                    {
                        using (SqlDataReader dr = new SqlCommand("exec _LastGlobal", Program.sql).ExecuteReader())
                        {
                            if (dr.Read())
                                this.SendGlobalToServer(dr[0].ToString(), dr[1].ToString());
                            dr.Close();
                        }
                    }
                    if (MainForm.Dev_SendMsg)
                    {
                        using (SqlDataReader dr = new SqlCommand("select top 1 * from __Dev_SendMsg where Sended = 0 order by nDate", Program.sql).ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                int MsgID = (int)Convert.ToInt16(dr[0]);
                                int MsgType = (int)Convert.ToInt16(dr[1]);
                                string CharName = dr[2].ToString();
                                string Sender = dr[3].ToString();
                                string Msg = dr[4].ToString();
                                if (MsgType < 1 || MsgType > 5)
                                {
                                    new SqlCommand("update __Dev_SendMsg set Sended = 4 where ID = " + MsgID.ToString(), Program.sql).ExecuteNonQuery();
                                }
                                else
                                {
                                    try
                                    {
                                        if (this.SendMessageByCharName(Sender, CharName, Msg, MsgType))
                                            new SqlCommand("update __Dev_SendMsg set Sended = 1 where ID = " + MsgID.ToString(), Program.sql).ExecuteNonQuery();
                                        else
                                            new SqlCommand("update __Dev_SendMsg set Sended = 3 where ID = " + MsgID.ToString(), Program.sql).ExecuteNonQuery();
                                    }
                                    catch (Exception ex)
                                    {
                                        Program.WriteError(ex.ToString(), nameof(ChatThread));
                                        new SqlCommand("update __Dev_SendMsg set Sended = 2 where ID = " + MsgID.ToString(), Program.sql).ExecuteNonQuery();
                                    }
                                }
                                CharName = (string)null;
                                Sender = (string)null;
                                Msg = (string)null;
                            }
                            dr.Close();
                        }
                    }
                    if (MainForm.Dev_GiveDC)
                    {
                        using (SqlDataReader dr = new SqlCommand("exec _LastDC", Program.sql).ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                string CharName = dr[0].ToString();
                                this.GiveDCByCharName(CharName);
                                CharName = (string)null;
                            }
                            dr.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.WriteError(ex.ToString(), nameof(ChatThread));
                }
            }
        }

        private void ExecEverySecond()
        {
            while (true)
            {
                Thread.Sleep(1000);
                try
                {
                    if (MainForm.Dev_DoEverySecond)
                        new SqlCommand("exec __Dev_DoEverySecond", Program.sql).ExecuteNonQueryAsync();
                }
                catch
                {
                }
            }
        }

        private void Button_BanHistory_Login_Click(object sender, EventArgs e)
    {
      this.Ban_ColHeader1.Text = "Karakter Adı";
      this.Ban_ColHeader2.Text = "Kalan Gün";
      this.Ban_ColHeader3.Text = "Eklenme Tarihi";
      this.listView_BanHistory.Items.Clear();
      try
      {
        using (SqlDataReader sqlDataReader = new SqlCommand("exec _GetBanHistory", Program.sql).ExecuteReader())
        {
          while (sqlDataReader.Read())
          {
            this.listView_BanHistory.Items.Add(new ListViewItem(new string[3]
            {
              sqlDataReader[0].ToString(),
              sqlDataReader[2].ToString(),
              Convert.ToDateTime(sqlDataReader[1]).ToShortDateString()
            }));
            this.listView_BanHistory.Columns[this.listView_BanHistory.Columns.Count - 1].Width = -2;
          }
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), nameof (Button_BanHistory_Login_Click));
        MainForm.WriteConsole("Ban verileri çekilirken hata!", Color.Red);
      }
    }

    private void button_BanHistory_Firewall_Click(object sender, EventArgs e)
    {
      this.Ban_ColHeader1.Text = "IP Adresi";
      this.Ban_ColHeader2.Text = "Benzersiz ID";
      this.Ban_ColHeader3.Text = "Eklenme Tarihi";
      this.listView_BanHistory.Items.Clear();
      try
      {
        using (SqlDataReader sqlDataReader = new SqlCommand("select * from _FirewallBlocks", Program.sql).ExecuteReader())
        {
          while (sqlDataReader.Read())
          {
            this.listView_BanHistory.Items.Add(new ListViewItem(new string[3]
            {
              sqlDataReader[1].ToString(),
              sqlDataReader[0].ToString(),
              Convert.ToDateTime(sqlDataReader[2]).ToShortDateString()
            }));
            this.listView_BanHistory.Columns[this.listView_BanHistory.Columns.Count - 1].Width = -2;
          }
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "button_BanHistory_Login_Click");
        MainForm.WriteConsole("Ban verileri çekilirken hata!", Color.Red);
      }
    }

    private void Button_LoginBan_Click(object sender, EventArgs e)
    {
      if (this.textBox_LoginBan_CN.Text.Length < 1 || this.textBox_LoginBan_DAY.Text.Length < 1)
      {
        MainForm.WriteConsole("Verileri eksiksiz doldurun!", Color.Red);
      }
      else
      {
                if (!short.TryParse(this.textBox_LoginBan_DAY.Text, out short result))
                {
                    MainForm.WriteConsole("Gün değeri bir sayı olmalı!", Color.Red);
                }
                else
                {
                    string str1 = this.textBox_LoginBan_CN.Text.Replace("'", "");
                    string str2 = this.richTextBox_Ban_Guide.Text.Replace("'", "");
                    using (SqlDataReader sqlDataReader = new SqlCommand("exec _GiveBan '" + str1 + "'," + result.ToString() + ",'" + str2 + "'", Program.sql).ExecuteReader())
                    {
                        sqlDataReader.Read();
                        bool boolean = Convert.ToBoolean(sqlDataReader[0]);
                        sqlDataReader.Close();
                        if (!boolean)
                        {
                            MainForm.WriteConsole("Karakter mevcut değil.", Color.Red);
                        }
                        else
                        {
                            MainForm.WriteConsole(str1 + " isimli karakter ve yan karakterleri banlandı.", Color.RosyBrown);
                            this.textBox_LoginBan_CN.Text = "";
                            this.Button_BanHistory_Login_Click((object)null, (EventArgs)null);
                        }
                    }
                }
            }
    }

    private void Button_FirewallBan_Click(object sender, EventArgs e)
    {
      if (this.textBox_IPBlock_IP.Text.Length < 7 || this.textBox_IPBlock_IP.Text.Length > 15)
        return;
      this.BlockFirewall(this.textBox_IPBlock_IP.Text.Replace("'", ""));
      this.textBox_IPBlock_IP.Text = "";
    }

    private void Button_Transfer_Click(object sender, EventArgs e)
    {
    }

    private void button_AddLicense_Click(object sender, EventArgs e)
    {
    }

    private void Button_OpenHelper_Click(object sender, EventArgs e)
    {
      if (!MainForm.KurulumForm.Visible)
        (MainForm.KurulumForm = new Kurulum()).Show();
      else
        MainForm.KurulumForm.BringToFront();
    }

    private void button_PortEdit_Click(object sender, EventArgs e)
    {
      if (this.button_PortSave.Visible)
      {
        this.button_PortSave.Visible = false;
        this.button_PortEdit.Text = "Düzenle";
        this.panel_PortSetting.Enabled = false;
        this.GetPortSettings();
      }
      else
      {
        this.button_PortSave.Visible = true;
        this.button_PortEdit.Text = "Vazgeç";
        this.panel_PortSetting.Enabled = true;
      }
    }

    private void Button_PortSave_Click(object sender, EventArgs e)
    {
      try
      {
        MainForm.ServerIP = this.textBox_ServIP.Text;
        MainForm.PublicGateway = Convert.ToInt32(this.textBox_Port1.Text);
        MainForm.PublicAgent = Convert.ToInt32(this.textBox_Port2.Text);
        MainForm.LocalGateway = Convert.ToInt32(this.textBox_Port3.Text);
        MainForm.LocalAgent = Convert.ToInt32(this.textBox_Port4.Text);
      }
      catch
      {
        MainForm.WriteConsole("Değerleri kontrol edin.", Color.Red);
        return;
      }
      new SqlCommand("delete from KGUARD.._Settings where Name in ('Port_ServIP','Port_PblcGw','Port_PblcAg','Port_LclGw','Port_LclAg')", Program.sql).ExecuteNonQuery();
      new SqlCommand("insert into KGUARD.._Settings values ('Port_ServIP','" + MainForm.ServerIP + "')", Program.sql).ExecuteNonQuery();
      new SqlCommand("insert into KGUARD.._Settings values ('Port_PblcGw','" + MainForm.PublicGateway.ToString() + "')", Program.sql).ExecuteNonQuery();
      new SqlCommand("insert into KGUARD.._Settings values ('Port_PblcAg','" + MainForm.PublicAgent.ToString() + "')", Program.sql).ExecuteNonQuery();
      new SqlCommand("insert into KGUARD.._Settings values ('Port_LclGw','" + MainForm.LocalGateway.ToString() + "')", Program.sql).ExecuteNonQuery();
      new SqlCommand("insert into KGUARD.._Settings values ('Port_LclAg','" + MainForm.LocalAgent.ToString() + "')", Program.sql).ExecuteNonQuery();
      MainForm.WriteConsole("Port ayarları kaydedildi.", Color.Green);
      this.button_PortEdit_Click((object) null, (EventArgs) null);
    }

    private void button_OnlineEdit_Click(object sender, EventArgs e)
    {
      if (this.button_OnlineSave.Visible)
      {
        this.button_OnlineSave.Visible = false;
        this.button_OnlineEdit.Text = "Düzenle";
        this.panel_OnlineYonetim.Enabled = false;
        this.GetOnlineSettings();
      }
      else
      {
        this.button_OnlineSave.Visible = true;
        this.button_OnlineEdit.Text = "Vazgeç";
        this.panel_OnlineYonetim.Enabled = true;
      }
    }

    private void button_OnlineSave_Click(object sender, EventArgs e)
    {
      try
      {
        MainForm.OnlineKontrol = this.checkBox_AddOnline.Checked;
        MainForm.LimitOnline = this.checkBox_LimitCapac.Checked;
        MainForm.PremiumAl = this.checkBox_Premiums.Checked;
        MainForm.RastgeleDC = this.checkBox_RandomDC.Checked;
        MainForm.EklenecekOnline = Convert.ToDouble(this.textBox_OnlineCount.Text);
        MainForm.ServerKapasite = (int) Convert.ToInt16(this.textBox_OnlineCap.Text);
        MainForm.PremiumIDleri.Clear();
        if (this.listBox_JobID.Items.Count > 0)
        {
          foreach (string str in this.listBox_JobID.Items)
            MainForm.PremiumIDleri.Add(Convert.ToInt32(str));
        }
      }
      catch
      {
        MainForm.WriteConsole("Değerleri kontrol edin. Eksiksiz ve hatasız doldurmalısınız.", Color.Red);
        return;
      }
      new SqlCommand("delete from KGUARD.._Settings where Name in ('Online_Add','Online_Limit','Online_Premiums','Online_RandomDC','Online_AddCount','Online_ServCapac')", Program.sql).ExecuteNonQuery();
      new SqlCommand("insert into KGUARD.._Settings values ('Online_Add','" + MainForm.OnlineKontrol.ToString() + "')", Program.sql).ExecuteNonQuery();
      new SqlCommand("insert into KGUARD.._Settings values ('Online_Limit','" + MainForm.LimitOnline.ToString() + "')", Program.sql).ExecuteNonQuery();
      new SqlCommand("insert into KGUARD.._Settings values ('Online_Premiums','" + MainForm.PremiumAl.ToString() + "')", Program.sql).ExecuteNonQuery();
      new SqlCommand("insert into KGUARD.._Settings values ('Online_RandomDC','" + MainForm.RastgeleDC.ToString() + "')", Program.sql).ExecuteNonQuery();
      new SqlCommand("insert into KGUARD.._Settings values ('Online_AddCount','" + MainForm.EklenecekOnline.ToString() + "')", Program.sql).ExecuteNonQuery();
      new SqlCommand("insert into KGUARD.._Settings values ('Online_ServCapac','" + MainForm.ServerKapasite.ToString() + "')", Program.sql).ExecuteNonQuery();
      new SqlCommand("truncate table KGUARD.._PremiumID", Program.sql).ExecuteNonQuery();
      if (this.listBox_JobID.Items.Count > 0)
      {
        foreach (string str in this.listBox_JobID.Items)
          new SqlCommand("insert into KGUARD.._PremiumID values (" + str + ")", Program.sql).ExecuteNonQuery();
      }
      MainForm.WriteConsole("Online yönetim ayarları kaydedildi.", Color.Green);
      this.button_OnlineEdit_Click((object) null, (EventArgs) null);
    }

    private void button_AddJobID_Click(object sender, EventArgs e)
    {
      if (this.textBox_JobID.Text.Length < 0)
        return;
      bool flag = int.TryParse(this.textBox_JobID.Text, out int _);
      if (this.listBox_JobID.Items.Contains((object) this.textBox_JobID.Text))
        MainForm.WriteConsole("Zaten mevcut.", Color.Red);
      else if (flag)
      {
        this.listBox_JobID.Items.Add((object) this.textBox_JobID.Text);
      }
      else
      {
        MainForm.WriteConsole("Girdiğiniz değer bir sayı olmalı.", Color.Red);
        return;
      }
      this.textBox_JobID.Text = "";
    }

    private void button_RemoveJobID_Click(object sender, EventArgs e)
    {
      this.listBox_JobID.Items.RemoveAt(this.listBox_JobID.SelectedIndex);
      this.button_RemoveJobID.Enabled = false;
    }

    private void button_StartServer_Click(object sender, EventArgs e)
    {
      if (MainForm.HandleGateway.ThreadState.ToString() != "Unstarted" && MainForm.HandleGateway.ThreadState.ToString() != "Aborted")
        return;
      for (int count = this.tabControl1.TabPages.Count; count >= 0; --count)
        this.tabControl1.SelectedIndex = count;
      if (MainForm.LocalGateway <= 0 || MainForm.LocalAgent <= 0 || (MainForm.PublicGateway <= 0 || MainForm.PublicAgent <= 0) || MainForm.ServerIP.Length <= 7)
        return;
      try
      {
        this.button_StartServer.Enabled = false;
        if (new AsyncServer().Start(Program.LocalIP, MainForm.PublicGateway, AsyncServer.Engine_ServerType.GatewayServer))
          MainForm.WriteConsole("Gateway Başlatıldı. [" + Program.LocalIP + ":" + MainForm.PublicGateway.ToString() + "]", Color.Aqua);
        if (new AsyncServer().Start(Program.LocalIP, MainForm.PublicAgent, AsyncServer.Engine_ServerType.AgentServer))
          MainForm.WriteConsole("Agent Başlatıldı. [" + Program.LocalIP + ":" + MainForm.PublicAgent.ToString() + "]", Color.Aqua);
      }
      catch (Exception ex)
      {
        this.button_StartServer.Enabled = true;
        MainForm.WriteConsole("Guard Server başlatılamadı. Hata metin dosyasına yazıldı.", Color.Red);
        Program.WriteError(ex.ToString(), "StartServer");
      }
    }

    private void button_YetkiliIPSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (!MainForm.GMYetkiliIP && this.checkBox_GMIPKontrol.Checked)
        {
          int num = (int) MessageBox.Show("Yetkili IP listesinde bulunmayan IP'ler giriş yapmayı denedikleri zaman +18 Only Adults hatası alacaklar.");
        }
        MainForm.GMYetkiliIP = this.checkBox_GMIPKontrol.Checked;
        new SqlCommand("delete from KGUARD.._Settings where Name = 'YetkiliIP_GM' TRUNCATE TABLE KGUARD.._YetkiliIP insert into KGUARD.._Settings values ('YetkiliIP_GM','" + MainForm.GMYetkiliIP.ToString() + "')", Program.sql).ExecuteNonQuery();
        if (this.listBox_YetkiliIP.Items.Count > 0)
        {
          foreach (string str in this.listBox_YetkiliIP.Items)
            new SqlCommand("insert into KGUARD.._YetkiliIP values ('" + str + "')", Program.sql).ExecuteNonQuery();
        }
      }
      catch
      {
        MainForm.WriteConsole("Değerleri kontrol edin. Eksiksiz ve hatasız doldurmalısınız.", Color.Red);
        return;
      }
      MainForm.WriteConsole("Yetkili IP ayarları kaydedildi.", Color.Green);
      this.button_YetkiliIPEdit_Click((object) null, (EventArgs) null);
    }

    private void button_YetkiliIPEdit_Click(object sender, EventArgs e)
    {
      if (this.button_YetkiliIPSave.Visible)
      {
        this.button_YetkiliIPSave.Visible = false;
        this.button_YetkiliIPEdit.Text = "Düzenle";
        this.panel_YetkiliIP.Enabled = false;
        this.GetYetkiliIP();
      }
      else
      {
        this.button_YetkiliIPSave.Visible = true;
        this.button_YetkiliIPEdit.Text = "Vazgeç";
        this.panel_YetkiliIP.Enabled = true;
      }
    }

    private void button_AddIP_Click(object sender, EventArgs e)
    {
      if (this.textBox_YetkiliIP.Text.Length < 7)
        return;
      if (this.listBox_YetkiliIP.Items.Contains((object) this.textBox_YetkiliIP.Text))
        MainForm.WriteConsole("Zaten mevcut.", Color.Red);
      else
        this.listBox_YetkiliIP.Items.Add((object) this.textBox_YetkiliIP.Text);
      this.textBox_YetkiliIP.Text = "";
    }

    private void button_RemoveIP_Click(object sender, EventArgs e)
    {
      this.listBox_YetkiliIP.Items.RemoveAt(this.listBox_YetkiliIP.SelectedIndex);
      this.button_RemoveIP.Enabled = false;
    }

    private void button_AddCustomIP_Click(object sender, EventArgs e)
    {
      if (this.textBox_CustomLimitIP.Text.Length < 7 || this.textBox_CustomLimitValue.Text.Length < 1)
        MainForm.WriteConsole("Değerleri kontrol edin.", Color.Red);
      else if (!int.TryParse(this.textBox_CustomLimitValue.Text, out int _))
      {
        MainForm.WriteConsole("Limit değeri rakamlardan oluşmalı.", Color.Red);
      }
      else
      {
        try
        {
          new SqlCommand("insert into KGUARD.._SpecialLimit values ('" + this.textBox_CustomLimitIP.Text + "'," + this.textBox_CustomLimitValue.Text + ")", Program.sql).ExecuteNonQuery();
          this.GetSpecialLimits();
          this.textBox_CustomLimitIP.Text = "";
          this.textBox_CustomLimitValue.Text = "";
        }
        catch
        {
        }
      }
    }

    private void button_GetSettings_Click(object sender, EventArgs e)
    {
      try
      {
        this.GetAllData();
        MainForm.WriteConsole("Ayarlar veritabanındakiler ile senkronize edildi.", Color.Green);
      }
      catch (Exception ex)
      {
        MainForm.WriteConsole("Ayarlar senkronize edilirken hata oluştu. Metin dosyasına yazıldı.", Color.Red);
        Program.WriteError(ex.ToString(), "GetSettings");
      }
    }

    private void button_HelpForFTWLimit_Click(object sender, EventArgs e)
    {
      int num = (int) MessageBox.Show("Kayıtlı olan guildlerin sahip olduğu toplam oyuncuyu baz alarak çalışır. Fortress Haritası başına toplam kaç oyuncu izin alabileceğini yazın. Tüm oyuncuların online olmayacağını hesaba katmayı unutmayın.");
    }

    private void textBox_IPLimit_TextChanged(object sender, EventArgs e)
    {
      if (!this.checkBox_IPLimit.Checked)
        return;
      this.checkBox_IPLimit.Checked = false;
    }

    private void textBox_Welcome1_TextChanged(object sender, EventArgs e) => this.checkBox_Welcome.Checked = false;

    private void textBox_Welcome2_TextChanged(object sender, EventArgs e) => this.checkBox_Welcome.Checked = false;

    private void textBox_PlusLimit_TextChanged(object sender, EventArgs e) => this.checkBox_PlusLimit.Checked = false;

    private void textBox_SecondAgentIP_TextChanged(object sender, EventArgs e) => this.checkBox_MultiAgent.Checked = false;

    private void textBox_SecondAgentPort_TextChanged(object sender, EventArgs e) => this.checkBox_MultiAgent.Checked = false;

    private void textBox_SecondAgentFakePort_TextChanged(object sender, EventArgs e) => this.checkBox_MultiAgent.Checked = false;

    private void textBox_GuildLimit_TextChanged(object sender, EventArgs e) => this.checkBox_GuildLimit.Checked = false;

    private void textBox_UnionLimit_TextChanged(object sender, EventArgs e) => this.checkBox_UnionLimit.Checked = false;

    private void textBox_IPC_CharName_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.listView_IPC.Items.Clear();
      this.textBox_IPC_IP.Text = "";
      if (e.KeyChar != '\r')
        return;
      try
      {
        using (SqlDataReader sqlDataReader = new SqlCommand("exec _GetIPHistory '" + this.textBox_IPC_CharName.Text.Replace("'", "") + "','" + this.textBox_IPC_IP.Text.Replace("'", "") + "'", Program.sql).ExecuteReader())
        {
          while (sqlDataReader.Read())
          {
            this.listView_IPC.Items.Add(new ListViewItem(new string[3]
            {
              sqlDataReader[0].ToString(),
              sqlDataReader[1].ToString(),
              sqlDataReader[2].ToString()
            }));
            this.listView_IPC.Columns[this.listView_IPC.Columns.Count - 1].Width = -2;
          }
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), nameof (textBox_IPC_CharName_KeyPress));
        MainForm.WriteConsole("İşlem sırasında bir hata oluştu. Metin belgesine kaydedildi.", Color.Red);
      }
    }

    private void textBox_IPC_IP_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.listView_IPC.Items.Clear();
      this.textBox_IPC_CharName.Text = "";
      if (e.KeyChar != '\r')
        return;
      try
      {
        using (SqlDataReader sqlDataReader = new SqlCommand("exec _GetIPHistory '" + this.textBox_IPC_CharName.Text.Replace("'", "") + "','" + this.textBox_IPC_IP.Text.Replace("'", "") + "'", Program.sql).ExecuteReader())
        {
          while (sqlDataReader.Read())
          {
            this.listView_IPC.Items.Add(new ListViewItem(new string[3]
            {
              sqlDataReader[0].ToString(),
              sqlDataReader[1].ToString(),
              sqlDataReader[2].ToString()
            }));
            this.listView_IPC.Columns[this.listView_IPC.Columns.Count - 1].Width = -2;
          }
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), nameof (textBox_IPC_IP_KeyPress));
        MainForm.WriteConsole("İşlem sırasında bir hata oluştu. Metin belgesine kaydedildi.", Color.Red);
      }
    }

    private void textBox_LimitOfFortressWar_TextChanged(object sender, EventArgs e) => this.checkBox_LimitOfFortressWar.Checked = false;

    private void checkBox_MaxPacketLimit_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.MaxPacketLimit = this.checkBox_MaxPacketLimit.Checked;
      this.SetSetting("MaxPacketLimit", MainForm.MaxPacketLimit.ToString());
    }

    private void checkBox_Multiplier_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.MultipleOnlineCount = this.checkBox_Multiplier.Checked;
      this.SetSetting("MultipleOnlineCount", MainForm.MultipleOnlineCount.ToString());
    }

    private void checkBox_SkillBug_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.AntiSkillBug = this.checkBox_SkillBug.Checked;
      this.SetSetting("AntiSkillBug", MainForm.AntiSkillBug.ToString());
    }

    private void checkBox_TaxRate_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.AntiTaxRateChange = this.checkBox_TaxRate.Checked;
      this.SetSetting("AntiTaxRateChange", MainForm.AntiTaxRateChange.ToString());
    }

    private void checkBox_AcademyCreate_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.AntiAcademyCreate = this.checkBox_AcademyCreate.Checked;
      this.SetSetting("AntiAcademyCreate", MainForm.AntiAcademyCreate.ToString());
    }

    private void checkBox_DropInTown_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.AntiDropInTown = this.checkBox_DropInTown.Checked;
      this.SetSetting("AntiDropInTown", MainForm.AntiDropInTown.ToString());
    }

    private void checkBox_LockSlot_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.Dev_LockSlot = this.checkBox_LockSlot.Checked;
      this.SetSetting("Dev_LockSlot", MainForm.Dev_LockSlot.ToString());
    }

    private void checkBox_LoginLogoutLog_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.Dev_LoginLogoutLog = this.checkBox_LoginLogoutLog.Checked;
      this.SetSetting("Dev_LoginLogoutLog", MainForm.Dev_LoginLogoutLog.ToString());
    }

    private void checkBox_DoEverySecond_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.Dev_DoEverySecond = this.checkBox_DoEverySecond.Checked;
      this.SetSetting("Dev_DoEverySecond", MainForm.Dev_DoEverySecond.ToString());
    }

    private void checkBox_IPLimit_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.IPLimit = this.checkBox_IPLimit.Checked;
      int result;
      bool flag = int.TryParse(this.textBox_IPLimit.Text, out result);
      if (this.checkBox_IPLimit.Checked && !flag)
      {
        MainForm.WriteConsole("IP Limit için geçerli olacak bir değer girmelisiniz.", Color.Red);
        this.checkBox_IPLimit.Checked = false;
      }
      else
      {
        try
        {
          new SqlCommand("exec KGUARD.._SetSetting 'IPLimit_Enabled', '" + MainForm.IPLimit.ToString() + "'", Program.sql).ExecuteNonQuery();
          new SqlCommand("exec KGUARD.._SetSetting 'IPLimit_Value', '" + result.ToString() + "'", Program.sql).ExecuteNonQuery();
          this.GetSpecialLimits();
        }
        catch
        {
        }
      }
    }

    private void CheckBox_Welcome_CheckedChanged(object sender, EventArgs e)
    {
      if ((this.textBox_Welcome1.Text.Length < 1 || this.textBox_Welcome2.Text.Length < 1) && this.checkBox_Welcome.Checked)
      {
        MainForm.WriteConsole("Karşılama ve Mesaj bölümlerini doldurun.", Color.Red);
        this.checkBox_Welcome.Checked = false;
      }
      else
      {
        MainForm.WelcomeMsg = this.checkBox_Welcome.Checked;
        MainForm.WelcomeMsg1 = this.textBox_Welcome1.Text.Replace("'", "");
        MainForm.WelcomeMsg2 = this.textBox_Welcome2.Text.Replace("'", "");
        try
        {
          new SqlCommand("exec KGUARD.._SetSetting 'WelcomeMsg_Enabled', '" + MainForm.WelcomeMsg.ToString() + "'", Program.sql).ExecuteNonQuery();
          new SqlCommand("exec KGUARD.._SetSetting 'WelcomeMsg_Value1', '" + MainForm.WelcomeMsg1 + "'", Program.sql).ExecuteNonQuery();
          new SqlCommand("exec KGUARD.._SetSetting 'WelcomeMsg_Value2', '" + MainForm.WelcomeMsg2 + "'", Program.sql).ExecuteNonQuery();
        }
        catch
        {
        }
      }
    }

    private void checkBox_JobRev_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        MainForm.JobRevRestrc = this.checkBox_JobRev.Checked;
        new SqlCommand("exec KGUARD.._SetSetting 'JobRevRestrc_Enabled', '" + MainForm.JobRevRestrc.ToString() + "'", Program.sql).ExecuteNonQuery();
      }
      catch
      {
      }
    }

    private void checkBox_BugExc_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        MainForm.AntiBugExc = this.checkBox_BugExc.Checked;
        new SqlCommand("exec KGUARD.._SetSetting 'AntiBugExc_Enabled', '" + MainForm.AntiBugExc.ToString() + "'", Program.sql).ExecuteNonQuery();
      }
      catch
      {
      }
    }

    private void checkBox_BugStall_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        MainForm.AntiBugStall = this.checkBox_BugStall.Checked;
        new SqlCommand("exec KGUARD.._SetSetting 'AntiBugStall_Enabled', '" + MainForm.AntiBugStall.ToString() + "'", Program.sql).ExecuteNonQuery();
      }
      catch
      {
      }
    }

    private void checkBox_BugEsc_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        MainForm.AntiBugEsc = this.checkBox_BugEsc.Checked;
        new SqlCommand("exec KGUARD.._SetSetting 'AntiBugEsc_Enabled', '" + MainForm.AntiBugEsc.ToString() + "'", Program.sql).ExecuteNonQuery();
      }
      catch
      {
      }
    }

    private void checkBox_Swear_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        MainForm.SwearFilter = this.checkBox_Swear.Checked;
        new SqlCommand("exec KGUARD.._SetSetting 'SwearFilter_Enabled', '" + MainForm.SwearFilter.ToString() + "'", Program.sql).ExecuteNonQuery();
      }
      catch
      {
      }
    }

    private void checkBox_PlusLimit_CheckedChanged(object sender, EventArgs e)
    {
      short result = 0;
      if (!short.TryParse(this.textBox_PlusLimit.Text, out result) && this.checkBox_PlusLimit.Checked)
      {
        this.checkBox_PlusLimit.Checked = false;
        MainForm.PlusLimit = false;
        MainForm.WriteConsole("Artı limiti için geçerli bir değer olmalı.", Color.Red);
        this.textBox_PlusLimit.Focus();
      }
      else
      {
        try
        {
          MainForm.PlusLimitValue = (int) result;
          MainForm.PlusLimit = this.checkBox_PlusLimit.Checked;
          new SqlCommand("exec KGUARD.._SetSetting 'PlusLimit_Enabled', '" + MainForm.PlusLimit.ToString() + "'", Program.sql).ExecuteNonQuery();
          new SqlCommand("exec KGUARD.._SetSetting 'PlusLimit_Value', '" + MainForm.PlusLimitValue.ToString() + "'", Program.sql).ExecuteNonQuery();
        }
        catch
        {
        }
      }
    }

    private void checkBox_Devs_Notice_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.Dev_NoticeThr = this.checkBox_Devs_Notice.Checked;
      this.SetSetting("Dev_NoticeThr", MainForm.Dev_NoticeThr.ToString());
    }

    private void checkBox_Devs_GlobalSw_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.Dev_GlobalThr = this.checkBox_Devs_GlobalSw.Checked;
      this.SetSetting("Dev_GlobalThr", MainForm.Dev_GlobalThr.ToString());
    }

    private void checkBox_Devs_SendMsg_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.Dev_SendMsg = this.checkBox_Devs_SendMsg.Checked;
      this.SetSetting("Dev_SendMsg", MainForm.Dev_SendMsg.ToString());
    }

    private void checkBox_VerifyPM_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.Dev_VerifyPM = this.checkBox_VerifyPM.Checked;
      this.SetSetting("Dev_VerifyPM", MainForm.Dev_VerifyPM.ToString());
    }

    private void checkBox_LogUserPM_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.Dev_LogPM = this.checkBox_LogUserPM.Checked;
      this.SetSetting("Dev_LogPM", MainForm.Dev_LogPM.ToString());
    }

    private void checkBox_LogUserGlobal_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.Dev_LogGlo = this.checkBox_LogUserGlobal.Checked;
      this.SetSetting("Dev_LogGlo", MainForm.Dev_LogGlo.ToString());
    }

    private void checkBox_GiveDC_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.Dev_GiveDC = this.checkBox_GiveDC.Checked;
      this.SetSetting("Dev_GiveDC", MainForm.Dev_GiveDC.ToString());
    }

    private void checkBox_MultiAgent_CheckedChanged(object sender, EventArgs e)
    {
      if ((this.textBox_SecondAgentFakePort.Text.Length < 1 || this.textBox_SecondAgentIP.Text.Length < 1 || this.textBox_SecondAgentPort.Text.Length < 1) && this.checkBox_MultiAgent.Checked)
      {
        MainForm.WriteConsole("İkinci Agent için ayarları eksiksiz doldurun.", Color.Red);
        this.checkBox_MultiAgent.Checked = false;
      }
      else
      {
        MainForm.MultiAgent = this.checkBox_MultiAgent.Checked;
        this.SetSetting("MultiAgent_Enabled", this.checkBox_MultiAgent.Checked.ToString());
        if (!this.checkBox_MultiAgent.Checked)
          return;
        try
        {
          MainForm.ServerIP2 = this.textBox_SecondAgentIP.Text;
          MainForm.LocalAgent2 = Convert.ToInt32(this.textBox_SecondAgentPort.Text);
          MainForm.PublicAgent2 = Convert.ToInt32(this.textBox_SecondAgentFakePort.Text);
          this.SetSetting("MultiAgent_RealIP", MainForm.ServerIP2);
          this.SetSetting("MultiAgent_RealPort", MainForm.LocalAgent2.ToString());
          this.SetSetting("MultiAgent_FakePort", MainForm.PublicAgent2.ToString());
        }
        catch (Exception ex)
        {
          Program.WriteError(ex.ToString(), "MultiAgentChecked");
          MainForm.WriteConsole("Bir hata oluştu. Hata metin dosyasına yazıldı.", Color.Red);
          this.checkBox_MultiAgent.Checked = false;
        }
      }
    }

    private void checkBox_GuildLimit_CheckedChanged(object sender, EventArgs e)
    {
      short result = 0;
      if ((!short.TryParse(this.textBox_GuildLimit.Text, out result) || result < (short) 0 || result > (short) 50) && this.checkBox_GuildLimit.Checked)
      {
        this.textBox_GuildLimit.Text = "50";
        this.checkBox_GuildLimit.Checked = true;
      }
      else
      {
        try
        {
          MainForm.GuildLimitEnabled = this.checkBox_GuildLimit.Checked;
          MainForm.GuildLimitValue = (int) result;
          this.SetSetting("GuildLimitEnabled", MainForm.GuildLimitEnabled.ToString());
          this.SetSetting("GuildLimitValue", result.ToString());
        }
        catch
        {
        }
      }
    }

    private void checkBox_UnionLimit_CheckedChanged(object sender, EventArgs e)
    {
      short result = 0;
      if ((!short.TryParse(this.textBox_UnionLimit.Text, out result) || result < (short) 0 || result > (short) 50) && this.checkBox_UnionLimit.Checked)
      {
        this.textBox_UnionLimit.Text = "8";
        this.checkBox_UnionLimit.Checked = true;
      }
      else
      {
        try
        {
          MainForm.UnionLimitEnabled = this.checkBox_UnionLimit.Checked;
          MainForm.UnionLimitValue = (int) result;
          this.SetSetting("UnionLimitEnabled", MainForm.UnionLimitEnabled.ToString());
          this.SetSetting("UnionLimitValue", result.ToString());
        }
        catch
        {
        }
      }
    }

    private void checkBox_LimitOfFortressWar_CheckedChanged(object sender, EventArgs e)
    {
      short result = 0;
      if ((!short.TryParse(this.textBox_LimitOfFortressWar.Text, out result) || result < (short) 0) && this.checkBox_LimitOfFortressWar.Checked)
      {
        this.textBox_LimitOfFortressWar.Text = "200";
        this.checkBox_LimitOfFortressWar.Checked = true;
      }
      else
      {
        MainForm.LimitOfFortressWarEnabled = this.checkBox_LimitOfFortressWar.Checked;
        MainForm.LimitOfFortressWarValue = (int) result;
        this.SetSetting("LimitOfFortressWarEnabled", MainForm.LimitOfFortressWarEnabled.ToString());
        this.SetSetting("LimitOfFortressWarValue", MainForm.LimitOfFortressWarValue.ToString());
      }
    }

    private void checkBox_DisablePetsInFTW_CheckedChanged(object sender, EventArgs e)
    {
      MainForm.DisablePetsInFTW = this.checkBox_DisablePetsInFTW.Checked;
      this.SetSetting("DisablePetsInFTW", MainForm.DisablePetsInFTW.ToString());
    }

    private void kaldırToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.Ban_ColHeader1.Text == "IP Adresi")
      {
        this.RemoveBlockFirewall(this.listView_BanHistory.FocusedItem.Text);
      }
      else
      {
        if (!(this.Ban_ColHeader1.Text == "Karakter Adı"))
          return;
        this.RemoveLoginBan(this.listView_BanHistory.FocusedItem.Text);
      }
    }

    private void listView_BanHistory_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      this.contextMenuStrip6.Show(Cursor.Position);
    }

    private void karakteriBanlaToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        this.textBox_LoginBan_CN.Text = this.listView_IPC.FocusedItem.Text;
        this.textBox_LoginBan_CN.Focus();
      }
      catch
      {
      }
    }

    private void ıPBlockAtToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        this.textBox_IPBlock_IP.Text = this.listView_IPC.FocusedItem.SubItems[2].Text;
        this.textBox_IPBlock_IP.Focus();
      }
      catch
      {
      }
    }

    private void comboBox_Service_SelectedIndexChanged(object sender, EventArgs e)
    {
      MainForm.OnlyAuthorized = Convert.ToBoolean(this.comboBox_Service.SelectedIndex);
      this.button_StartServer.Visible = true;
    }

    private void listBox_YetkiliIP_SelectedIndexChanged(object sender, EventArgs e) => this.button_RemoveIP.Enabled = true;

    private void textBox_ServIP_Leave(object sender, EventArgs e) => this.textBox_Port1.Focus();

    private void listView1_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      this.contextMenuStrip2.Show(Cursor.Position);
    }

    private void toolStripMenuItem2_Click(object sender, EventArgs e)
    {
      lock (MainForm.ListLocker)
      {
        this.listView1.Items.Clear();
        MainForm.WriteConsole("KGUARD v" + Program.ProgVer + " - Silkroad Server Security", Color.PaleVioletRed);
      }
    }

    private void listBox_JobID_SelectedIndexChanged(object sender, EventArgs e) => this.button_RemoveJobID.Enabled = true;

    private void listView2_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right || !this.listView_CustomLimit.FocusedItem.Bounds.Contains(e.Location))
        return;
      this.contextMenuStrip1.Show(Cursor.Position);
    }

    private void toolStripMenuItem1_Click(object sender, EventArgs e)
    {
      new SqlCommand("delete from KGUARD.._SpecialLimit where IP = '" + this.listView_CustomLimit.FocusedItem.Text + "'", Program.sql).ExecuteNonQuery();
      this.GetSpecialLimits();
    }

    private void checkBox_ThiefRewardRestrc_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        MainForm.ThiefRewardRestrc = this.checkBox_ThiefRewardRestrc.Checked;
        new SqlCommand("exec KGUARD.._SetSetting 'ThiefRewardRestrc_Enabled', '" + MainForm.ThiefRewardRestrc.ToString() + "'", Program.sql).ExecuteNonQuery();
      }
      catch
      {
      }
    }

    private void textBox_InsuiltChat_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      string str = this.textBox_InsuiltChat.Text.ToLower().Replace("'", "");
      if (str.Length < 3)
        MainForm.WriteConsole("En az 3 haneli bir kelime yazın.", Color.Red);
      else if (MainForm.SwearWords.Contains(str))
      {
        MainForm.WriteConsole("Sözcük listede zaten mevcut.", Color.Red);
      }
      else
      {
        MainForm.SwearWords.Add(str);
        this.listBox_InsuiltChat.Items.Add((object) str);
        this.textBox_InsuiltChat.Text = "";
        try
        {
          new SqlCommand("insert into _SwearWord values ('" + str + "')", Program.sql).ExecuteNonQuery();
        }
        catch
        {
        }
      }
    }

    private void toolStripMenuItem3_Click(object sender, EventArgs e)
    {
      if (this.listBox_InsuiltChat.SelectedItems.Count < 1)
        return;
      List<string> stringList = new List<string>();
      foreach (string selectedItem in this.listBox_InsuiltChat.SelectedItems)
        stringList.Add(selectedItem);
      foreach (string str in stringList)
      {
        this.listBox_InsuiltChat.Items.Remove((object) str);
        MainForm.SwearWords.Remove(str);
        new SqlCommand("delete from _SwearWord where Kelime = '" + str + "'", Program.sql).ExecuteNonQuery();
      }
    }

    private void listBox_InsuiltChat_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right || this.listBox_InsuiltChat.SelectedItems.Count <= 0)
        return;
      this.contextMenuStrip3.Show(Cursor.Position);
    }

    private void textBox_IncPMSendedVerify_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      string str = this.textBox_IncPMSendedVerify.Text.Replace("'", "");
      if (str.Length < 1)
        MainForm.WriteConsole("Değer girmediniz.", Color.Red);
      else if (MainForm.PMVerifyNames.Contains(str))
      {
        MainForm.WriteConsole("Listede zaten mevcut.", Color.Red);
      }
      else
      {
        MainForm.PMVerifyNames.Add(str);
        this.listBox_VerifyNames.Items.Add((object) str);
        this.textBox_IncPMSendedVerify.Text = "";
        try
        {
          new SqlCommand("insert into _VerifyNames values ('" + str + "')", Program.sql).ExecuteNonQuery();
        }
        catch
        {
        }
      }
    }

    private void toolStripMenuItem4_Click(object sender, EventArgs e)
    {
      if (this.listBox_VerifyNames.SelectedItems.Count < 1)
        return;
      List<string> stringList = new List<string>();
      foreach (string selectedItem in this.listBox_VerifyNames.SelectedItems)
        stringList.Add(selectedItem);
      foreach (string str in stringList)
      {
        this.listBox_VerifyNames.Items.Remove((object) str);
        MainForm.PMVerifyNames.Remove(str);
        new SqlCommand("delete from _VerifyNames where Name = '" + str + "'", Program.sql).ExecuteNonQuery();
      }
    }

    private void listBox_VerifyNames_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right || this.listBox_VerifyNames.SelectedItems.Count <= 0)
        return;
      this.contextMenuStrip4.Show(Cursor.Position);
    }

    private void listView_IPC_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      this.contextMenuStrip5.Show(Cursor.Position);
    }

    private void comboBox_DinnerTime_SelectedIndexChanged(object sender, EventArgs e)
    {
      new SqlCommand("exec KGUARD.._SetSetting 'DinnerTime', '" + ((this.comboBox_DinnerTime.SelectedIndex + 1) * 2).ToString() + "'", Program.sql).ExecuteNonQuery();
      MainForm.DinnerTime = (this.comboBox_DinnerTime.SelectedIndex + 1) * 2;
      if (MainForm.AgentClients.Count < 1)
        return;
      MainForm.WriteConsole("Dinlenme zamanı " + MainForm.DinnerTime.ToString() + "ms olarak ayarlandı. Bağlı oyunculara da uygulanıyor...", Color.DarkSeaGreen);
      new Thread((ThreadStart) (() =>
      {
        List<ClientsAgent> clientsAgentList = new List<ClientsAgent>();
        lock (MainForm.AgentClients)
          clientsAgentList.AddRange((IEnumerable<ClientsAgent>) MainForm.AgentClients);
        foreach (ClientsAgent clientsAgent in clientsAgentList)
          clientsAgent.ChangeSleepTime(MainForm.DinnerTime);
        MainForm.WriteConsole("İşlem bağlı oyunculara da uygulandı ve tamamlandı.", Color.Green);
      })).Start();
    }

    private void GetPortSettings()
    {
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name in ('Port_ServIP','Port_PblcGw','Port_PblcAg','Port_LclGw','Port_LclAg')", Program.sql).ExecuteReader())
      {
        try
        {
          while (sqlDataReader.Read())
          {
            string str1 = sqlDataReader[1].ToString();
            string str2 = sqlDataReader[0].ToString();
            if (!(str2 == "Port_ServIP"))
            {
              if (!(str2 == "Port_PblcGw"))
              {
                if (!(str2 == "Port_PblcAg"))
                {
                  if (!(str2 == "Port_LclGw"))
                  {
                    if (str2 == "Port_LclAg")
                    {
                      MainForm.LocalAgent = Convert.ToInt32(str1);
                      this.textBox_Port4.Text = str1;
                    }
                  }
                  else
                  {
                    MainForm.LocalGateway = Convert.ToInt32(str1);
                    this.textBox_Port3.Text = str1;
                  }
                }
                else
                {
                  MainForm.PublicAgent = Convert.ToInt32(str1);
                  this.textBox_Port2.Text = str1;
                }
              }
              else
              {
                MainForm.PublicGateway = Convert.ToInt32(str1);
                this.textBox_Port1.Text = str1;
              }
            }
            else
            {
              MainForm.ServerIP = str1;
              this.textBox_ServIP.Text = str1;
            }
          }
        }
        catch (Exception ex)
        {
          MainForm.WriteConsole("Port ayarları alınırken hata oluştu. Hata metin dosyasına yazıldı.", Color.Red);
          Program.WriteError(ex.ToString(), nameof (GetPortSettings));
        }
        sqlDataReader.Close();
      }
    }

    private void GetYetkiliIP()
    {
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._YetkiliIP order by IP", Program.sql).ExecuteReader())
      {
        try
        {
          MainForm.YetkiliIPList.Clear();
          this.listBox_YetkiliIP.Items.Clear();
          while (sqlDataReader.Read())
          {
            this.listBox_YetkiliIP.Items.Add((object) sqlDataReader[0].ToString());
            MainForm.YetkiliIPList.Add(sqlDataReader[0].ToString());
          }
        }
        catch (Exception ex)
        {
          MainForm.WriteConsole("Yetkili IP'ler alınırken hata oluştu. Hata metin dosyasına yazıldı.", Color.Red);
          Program.WriteError(ex.ToString(), nameof (GetYetkiliIP));
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'YetkiliIP_GM'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.GMYetkiliIP = Convert.ToBoolean(sqlDataReader[1]);
          this.checkBox_GMIPKontrol.Checked = MainForm.GMYetkiliIP;
        }
        sqlDataReader.Close();
      }
    }

    private void GetOnlineSettings()
    {
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name in ('Online_Add','Online_Limit','Online_Premiums','Online_RandomDC','Online_AddCount','Online_ServCapac')", Program.sql).ExecuteReader())
      {
        try
        {
          while (sqlDataReader.Read())
          {
            string str1 = sqlDataReader[1].ToString();
            string str2 = sqlDataReader[0].ToString();
            if (!(str2 == "Online_Add"))
            {
              if (!(str2 == "Online_Limit"))
              {
                if (!(str2 == "Online_Premiums"))
                {
                  if (!(str2 == "Online_RandomDC"))
                  {
                    if (!(str2 == "Online_AddCount"))
                    {
                      if (str2 == "Online_ServCapac")
                      {
                        MainForm.ServerKapasite = (int) Convert.ToInt16(str1);
                        this.textBox_OnlineCap.Text = str1;
                      }
                    }
                    else
                    {
                      MainForm.EklenecekOnline = Convert.ToDouble(str1);
                      this.textBox_OnlineCount.Text = str1;
                    }
                  }
                  else
                  {
                    MainForm.RastgeleDC = Convert.ToBoolean(str1);
                    this.checkBox_RandomDC.Checked = Convert.ToBoolean(str1);
                  }
                }
                else
                {
                  MainForm.PremiumAl = Convert.ToBoolean(str1);
                  this.checkBox_Premiums.Checked = Convert.ToBoolean(str1);
                }
              }
              else
              {
                MainForm.LimitOnline = Convert.ToBoolean(str1);
                this.checkBox_LimitCapac.Checked = Convert.ToBoolean(str1);
              }
            }
            else
            {
              MainForm.OnlineKontrol = Convert.ToBoolean(str1);
              this.checkBox_AddOnline.Checked = Convert.ToBoolean(str1);
            }
          }
          this.GetSetting("MultipleOnlineCount", ref MainForm.MultipleOnlineCount, ref this.checkBox_Multiplier);
        }
        catch (Exception ex)
        {
          MainForm.WriteConsole("Online ayarları alınırken hata oluştu. Hata metin dosyasına yazıldı.", Color.Red);
          Program.WriteError(ex.ToString(), nameof (GetOnlineSettings));
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._PremiumID order by ID", Program.sql).ExecuteReader())
      {
        try
        {
          this.listBox_JobID.Items.Clear();
          MainForm.PremiumIDleri.Clear();
          while (sqlDataReader.Read())
          {
            MainForm.PremiumIDleri.Add(Convert.ToInt32(sqlDataReader[0]));
            this.listBox_JobID.Items.Add((object) sqlDataReader[0].ToString());
          }
        }
        catch (Exception ex)
        {
          MainForm.WriteConsole("Online ayarları alınırken hata oluştu. Hata metin dosyasına yazıldı.", Color.Red);
          Program.WriteError(ex.ToString(), nameof (GetOnlineSettings));
        }
        sqlDataReader.Close();
      }
    }

    private void GetSpecialLimits()
    {
      this.listView_CustomLimit.Items.Clear();
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._SpecialLimit order by IP", Program.sql).ExecuteReader())
      {
        while (sqlDataReader.Read())
        {
          this.listView_CustomLimit.Items.Add(new ListViewItem(new string[2]
          {
            sqlDataReader[0].ToString(),
            sqlDataReader[1].ToString()
          }));
          this.listView_CustomLimit.Columns[this.listView_CustomLimit.Columns.Count - 1].Width = -2;
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'IPLimit_Value'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.IPLimitValue = (int) Convert.ToInt16(sqlDataReader[1]);
          this.textBox_IPLimit.Text = MainForm.IPLimitValue.ToString();
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'IPLimit_Enabled'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.IPLimit = Convert.ToBoolean(sqlDataReader[1]);
          this.checkBox_IPLimit.Checked = MainForm.IPLimit;
        }
        sqlDataReader.Close();
      }
    }

    private void GetWelcomeMsgSetting()
    {
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'WelcomeMsg_Value1'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.WelcomeMsg1 = sqlDataReader[1].ToString();
          this.textBox_Welcome1.Text = MainForm.WelcomeMsg1;
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'WelcomeMsg_Value2'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.WelcomeMsg2 = sqlDataReader[1].ToString();
          this.textBox_Welcome2.Text = MainForm.WelcomeMsg2;
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'WelcomeMsg_Enabled'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.WelcomeMsg = Convert.ToBoolean(sqlDataReader[1]);
          this.checkBox_Welcome.Checked = MainForm.IPLimit;
        }
        sqlDataReader.Close();
      }
    }

    private void GetAutoImageSettings()
    {
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'AutoImage_Value'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
          MainForm.AutoImageValue = sqlDataReader[1].ToString();
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'AutoImage_Enabled'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
          MainForm.AutoImage = Convert.ToBoolean(sqlDataReader[1]);
        sqlDataReader.Close();
      }
    }

    private void GetRestrcSettings()
    {
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'JobRevRestrc_Enabled'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.JobRevRestrc = Convert.ToBoolean(sqlDataReader[1]);
          this.checkBox_JobRev.Checked = MainForm.JobRevRestrc;
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'ThiefRewardRestrc_Enabled'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.ThiefRewardRestrc = Convert.ToBoolean(sqlDataReader[1]);
          this.checkBox_ThiefRewardRestrc.Checked = MainForm.ThiefRewardRestrc;
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'AntiBugExc_Enabled'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.AntiBugExc = Convert.ToBoolean(sqlDataReader[1]);
          this.checkBox_BugExc.Checked = MainForm.AntiBugExc;
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'AntiBugStall_Enabled'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.AntiBugStall = Convert.ToBoolean(sqlDataReader[1]);
          this.checkBox_BugStall.Checked = MainForm.AntiBugStall;
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'AntiBugEsc_Enabled'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.AntiBugEsc = Convert.ToBoolean(sqlDataReader[1]);
          this.checkBox_BugEsc.Checked = MainForm.AntiBugEsc;
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'SwearFilter_Enabled'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.SwearFilter = Convert.ToBoolean(sqlDataReader[1]);
          this.checkBox_Swear.Checked = MainForm.SwearFilter;
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'PlusLimit_Value'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.PlusLimitValue = (int) Convert.ToInt16(sqlDataReader[1]);
          this.textBox_PlusLimit.Text = MainForm.PlusLimitValue.ToString();
        }
        sqlDataReader.Close();
      }
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'PlusLimit_Enabled'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.PlusLimit = Convert.ToBoolean(sqlDataReader[1]);
          this.checkBox_PlusLimit.Checked = MainForm.PlusLimit;
        }
        sqlDataReader.Close();
      }
      this.GetSetting("AntiSkillBug", ref MainForm.AntiSkillBug, ref this.checkBox_SkillBug);
      this.GetSetting("AntiTaxRateChange", ref MainForm.AntiTaxRateChange, ref this.checkBox_TaxRate);
      this.GetSetting("AntiAcademyCreate", ref MainForm.AntiAcademyCreate, ref this.checkBox_AcademyCreate);
      this.GetSetting("AntiDropInTown", ref MainForm.AntiDropInTown, ref this.checkBox_DropInTown);
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'LimitOfFortressWarValue'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
        {
          MainForm.LimitOfFortressWarValue = (int) Convert.ToInt16(sqlDataReader[1]);
          this.textBox_LimitOfFortressWar.Text = MainForm.LimitOfFortressWarValue.ToString();
        }
        sqlDataReader.Close();
      }
      this.GetSetting("LimitOfFortressWarEnabled", ref MainForm.LimitOfFortressWarEnabled, ref this.checkBox_LimitOfFortressWar);
      this.GetSetting("DisablePetsInFTW", ref MainForm.DisablePetsInFTW, ref this.checkBox_DisablePetsInFTW);
    }

    private void GetSwearList()
    {
      try
      {
        using (SqlDataReader sqlDataReader = new SqlCommand("select * from _SwearWord order by Kelime", Program.sql).ExecuteReader())
        {
          while (sqlDataReader.Read())
          {
            string str = sqlDataReader[0].ToString();
            MainForm.SwearWords.Add(str);
            this.listBox_InsuiltChat.Items.Add((object) str);
          }
          sqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        MainForm.WriteConsole("Kelime filtresi veritabanından okunurken hata oluştu.", Color.Red);
        Program.WriteError(ex.ToString(), nameof (GetSwearList));
      }
    }

    private void GetDeveloperSettings()
    {
      this.GetSetting("Dev_NoticeThr", ref MainForm.Dev_NoticeThr, ref this.checkBox_Devs_Notice);
      this.GetSetting("Dev_GlobalThr", ref MainForm.Dev_GlobalThr, ref this.checkBox_Devs_GlobalSw);
      this.GetSetting("Dev_LoginLogoutLog", ref MainForm.Dev_LoginLogoutLog, ref this.checkBox_LoginLogoutLog);
      this.GetSetting("Dev_DoEverySecond", ref MainForm.Dev_DoEverySecond, ref this.checkBox_DoEverySecond);
      this.GetSetting("Dev_SendMsg", ref MainForm.Dev_SendMsg, ref this.checkBox_Devs_SendMsg);
      this.GetSetting("Dev_VerifyPM", ref MainForm.Dev_VerifyPM, ref this.checkBox_VerifyPM);
      this.GetSetting("Dev_LogPM", ref MainForm.Dev_LogPM, ref this.checkBox_LogUserPM);
      this.GetSetting("Dev_LogGlo", ref MainForm.Dev_LogGlo, ref this.checkBox_LogUserGlobal);
      this.GetSetting("Dev_GiveDC", ref MainForm.Dev_GiveDC, ref this.checkBox_GiveDC);
      this.GetSetting("Dev_LockSlot", ref MainForm.Dev_LockSlot, ref this.checkBox_LockSlot);
      this.GetVerifyPMList();
    }

    private void GetExtraSecuritySettings() => this.GetSetting("MaxPacketLimit", ref MainForm.MaxPacketLimit, ref this.checkBox_MaxPacketLimit);

    private void GetVerifyPMList()
    {
      try
      {
        using (SqlDataReader sqlDataReader = new SqlCommand("select * from _VerifyNames order by Name", Program.sql).ExecuteReader())
        {
          while (sqlDataReader.Read())
          {
            string str = sqlDataReader[0].ToString();
            MainForm.PMVerifyNames.Add(str);
            this.listBox_VerifyNames.Items.Add((object) str);
          }
          sqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        MainForm.WriteConsole("Gelen PM Alındı Paketi listesi veritabanından okunurken hata oluştu.", Color.Red);
        Program.WriteError(ex.ToString(), nameof (GetVerifyPMList));
      }
    }

    private void GetMultiAgentSettings()
    {
      try
      {
        using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'MultiAgent_RealIP'", Program.sql).ExecuteReader())
        {
          if (sqlDataReader.Read())
          {
            MainForm.ServerIP2 = sqlDataReader[1].ToString();
            this.textBox_SecondAgentIP.Text = MainForm.ServerIP2;
          }
          sqlDataReader.Close();
        }
        using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'MultiAgent_RealPort'", Program.sql).ExecuteReader())
        {
          if (sqlDataReader.Read())
          {
            MainForm.LocalAgent2 = Convert.ToInt32(sqlDataReader[1]);
            this.textBox_SecondAgentPort.Text = MainForm.LocalAgent2.ToString();
          }
          sqlDataReader.Close();
        }
        using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'MultiAgent_FakePort'", Program.sql).ExecuteReader())
        {
          if (sqlDataReader.Read())
          {
            MainForm.PublicAgent2 = Convert.ToInt32(sqlDataReader[1]);
            this.textBox_SecondAgentFakePort.Text = MainForm.PublicAgent2.ToString();
          }
          sqlDataReader.Close();
          this.GetSetting("MultiAgent_Enabled", ref MainForm.MultiAgent, ref this.checkBox_MultiAgent);
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), nameof (GetMultiAgentSettings));
        MainForm.WriteConsole("Multi Agent ayarları alınırken hata oluştu.", Color.Red);
      }
    }

    private void GetDinnerTimeSetting()
    {
      using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = 'DinnerTime'", Program.sql).ExecuteReader())
      {
        if (sqlDataReader.Read())
          MainForm.DinnerTime = (int) Convert.ToInt16(sqlDataReader[1]);
        this.comboBox_DinnerTime.SelectedIndex = MainForm.DinnerTime / 2 - 1;
      }
    }

    private void SetSetting(string SetName, string Value)
    {
      try
      {
        new SqlCommand("exec KGUARD.._SetSetting '" + SetName + "', '" + Value + "'", Program.sql).ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), nameof (SetSetting));
      }
    }

    private void GetSetting(string SetName, ref string rfr, ref TextBox txtbx)
    {
      try
      {
        using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = '" + SetName + "'", Program.sql).ExecuteReader())
        {
          if (sqlDataReader.Read())
          {
            rfr = sqlDataReader[1].ToString();
            txtbx.Text = rfr;
          }
          sqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "GetSetting(string SetName, ref bool rfr)");
      }
    }

    private void GetSetting(string SetName, ref int rfr, ref TextBox txtbx)
    {
      try
      {
        using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = '" + SetName + "'", Program.sql).ExecuteReader())
        {
          if (sqlDataReader.Read())
          {
            rfr = Convert.ToInt32(sqlDataReader[1]);
            txtbx.Text = rfr.ToString();
          }
          sqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "GetSetting(string SetName, ref bool rfr)");
      }
    }

    private void GetSetting(string SetName, ref bool rfr, ref CheckBox cckbx)
    {
      try
      {
        using (SqlDataReader sqlDataReader = new SqlCommand("select * from KGUARD.._Settings where Name = '" + SetName + "'", Program.sql).ExecuteReader())
        {
          if (sqlDataReader.Read())
          {
            rfr = Convert.ToBoolean(sqlDataReader[1]);
            cckbx.Checked = rfr;
          }
          sqlDataReader.Close();
        }
      }
      catch (Exception ex)
      {
        Program.WriteError(ex.ToString(), "GetSetting(string SetName, ref bool rfr, ref CheckBox cckbx)");
      }
    }

    private void GetGuildLimits()
    {
      this.GetSetting("GuildLimitValue", ref MainForm.GuildLimitValue, ref this.textBox_GuildLimit);
      this.GetSetting("UnionLimitValue", ref MainForm.UnionLimitValue, ref this.textBox_UnionLimit);
      this.GetSetting("GuildLimitEnabled", ref MainForm.GuildLimitEnabled, ref this.checkBox_GuildLimit);
      this.GetSetting("UnionLimitEnabled", ref MainForm.UnionLimitEnabled, ref this.checkBox_UnionLimit);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainForm));
      this.label1 = new Label();
      this.label3 = new Label();
      this.label5 = new Label();
      this.label6 = new Label();
      this.label7 = new Label();
      this.labelThread = new Label();
      this.labelMEMORY = new Label();
      this.label_LoginClient = new Label();
      this.label_AgentClient = new Label();
      this.label_Suspect = new Label();
      this.listView1 = new ListView();
      this.columnHeader1 = new ColumnHeader();
      this.columnHeader2 = new ColumnHeader();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.toolStripMenuItem1 = new ToolStripMenuItem();
      this.contextMenuStrip2 = new ContextMenuStrip(this.components);
      this.toolStripMenuItem2 = new ToolStripMenuItem();
      this.contextMenuStrip3 = new ContextMenuStrip(this.components);
      this.toolStripMenuItem3 = new ToolStripMenuItem();
      this.button_GetSettings = new Button();
      this.contextMenuStrip4 = new ContextMenuStrip(this.components);
      this.toolStripMenuItem4 = new ToolStripMenuItem();
      this.tabPage5 = new TabPage();
      this.groupBox16 = new GroupBox();
      this.checkBox_DoEverySecond = new CheckBox();
      this.checkBox_LoginLogoutLog = new CheckBox();
      this.checkBox_LockSlot = new CheckBox();
      this.checkBox_GiveDC = new CheckBox();
      this.groupBox15 = new GroupBox();
      this.listBox_VerifyNames = new ListBox();
      this.textBox_IncPMSendedVerify = new TextBox();
      this.checkBox_VerifyPM = new CheckBox();
      this.groupBox14 = new GroupBox();
      this.checkBox_Devs_GlobalSw = new CheckBox();
      this.label25 = new Label();
      this.checkBox_Devs_Notice = new CheckBox();
      this.checkBox_Devs_SendMsg = new CheckBox();
      this.groupBox13 = new GroupBox();
      this.checkBox_LogUserGlobal = new CheckBox();
      this.checkBox_LogUserPM = new CheckBox();
      this.tabPage4 = new TabPage();
      this.groupBox24 = new GroupBox();
      this.checkBox_SkillBug = new CheckBox();
      this.checkBox_BugExc = new CheckBox();
      this.checkBox_BugEsc = new CheckBox();
      this.checkBox_BugStall = new CheckBox();
      this.groupBox12 = new GroupBox();
      this.textBox_InsuiltChat = new TextBox();
      this.listBox_InsuiltChat = new ListBox();
      this.groupBox10 = new GroupBox();
      this.checkBox_DisablePetsInFTW = new CheckBox();
      this.button_HelpForFTWLimit = new Button();
      this.textBox_LimitOfFortressWar = new TextBox();
      this.checkBox_DropInTown = new CheckBox();
      this.checkBox_LimitOfFortressWar = new CheckBox();
      this.checkBox_AcademyCreate = new CheckBox();
      this.checkBox_TaxRate = new CheckBox();
      this.textBox_UnionLimit = new TextBox();
      this.textBox_GuildLimit = new TextBox();
      this.checkBox_UnionLimit = new CheckBox();
      this.checkBox_GuildLimit = new CheckBox();
      this.checkBox_ThiefRewardRestrc = new CheckBox();
      this.textBox_PlusLimit = new TextBox();
      this.checkBox_PlusLimit = new CheckBox();
      this.checkBox_Swear = new CheckBox();
      this.checkBox_JobRev = new CheckBox();
      this.groupBox11 = new GroupBox();
      this.textBox_Welcome2 = new TextBox();
      this.label27 = new Label();
      this.label26 = new Label();
      this.textBox_Welcome1 = new TextBox();
      this.checkBox_Welcome = new CheckBox();
      this.tabPage2 = new TabPage();
      this.groupBox25 = new GroupBox();
      this.label23 = new Label();
      this.comboBox_DinnerTime = new ComboBox();
      this.label2 = new Label();
      this.groupBox23 = new GroupBox();
      this.checkBox_MaxPacketLimit = new CheckBox();
      this.groupBox7 = new GroupBox();
      this.button_OnlineSave = new Button();
      this.button_OnlineEdit = new Button();
      this.panel_OnlineYonetim = new Panel();
      this.checkBox_Multiplier = new CheckBox();
      this.button_RemoveJobID = new Button();
      this.button_AddJobID = new Button();
      this.label20 = new Label();
      this.textBox_JobID = new TextBox();
      this.listBox_JobID = new ListBox();
      this.checkBox_Premiums = new CheckBox();
      this.checkBox_RandomDC = new CheckBox();
      this.checkBox_LimitCapac = new CheckBox();
      this.label19 = new Label();
      this.checkBox_AddOnline = new CheckBox();
      this.textBox_OnlineCap = new TextBox();
      this.textBox_OnlineCount = new TextBox();
      this.label18 = new Label();
      this.groupBox17 = new GroupBox();
      this.label31 = new Label();
      this.label30 = new Label();
      this.label29 = new Label();
      this.label28 = new Label();
      this.textBox_SecondAgentFakePort = new TextBox();
      this.textBox_SecondAgentPort = new TextBox();
      this.textBox_SecondAgentIP = new TextBox();
      this.checkBox_MultiAgent = new CheckBox();
      this.tabPage1 = new TabPage();
      this.groupBox1 = new GroupBox();
      this.label4 = new Label();
      this.label8 = new Label();
      this.label_LisansUser = new Label();
      this.label_LisansIP = new Label();
      this.groupBox9 = new GroupBox();
      this.button_AddCustomIP = new Button();
      this.label24 = new Label();
      this.textBox_CustomLimitValue = new TextBox();
      this.label22 = new Label();
      this.listView_CustomLimit = new ListView();
      this.columnHeader3 = new ColumnHeader();
      this.columnHeader4 = new ColumnHeader();
      this.textBox_CustomLimitIP = new TextBox();
      this.groupBox6 = new GroupBox();
      this.button_YetkiliIPSave = new Button();
      this.button_YetkiliIPEdit = new Button();
      this.panel_YetkiliIP = new Panel();
      this.button_RemoveIP = new Button();
      this.textBox_YetkiliIP = new TextBox();
      this.button_AddIP = new Button();
      this.listBox_YetkiliIP = new ListBox();
      this.checkBox_GMIPKontrol = new CheckBox();
      this.groupBox8 = new GroupBox();
      this.label21 = new Label();
      this.textBox_IPLimit = new TextBox();
      this.checkBox_IPLimit = new CheckBox();
      this.groupBox4 = new GroupBox();
      this.groupBox_StartServer = new GroupBox();
      this.label15 = new Label();
      this.button_StartServer = new Button();
      this.comboBox_Service = new ComboBox();
      this.groupBox5 = new GroupBox();
      this.button_PortSave = new Button();
      this.button_PortEdit = new Button();
      this.panel_PortSetting = new Panel();
      this.textBox_ServIP = new TextBox();
      this.label16 = new Label();
      this.label14 = new Label();
      this.textBox_Port1 = new TextBox();
      this.textBox_Port3 = new TextBox();
      this.textBox_Port4 = new TextBox();
      this.label12 = new Label();
      this.label11 = new Label();
      this.label13 = new Label();
      this.textBox_Port2 = new TextBox();
      this.button_OpenHelper = new Button();
      this.tabControl1 = new TabControl();
      this.tabPage3 = new TabPage();
      this.groupBox22 = new GroupBox();
      this.button_BanHistory_Firewall = new Button();
      this.button_BanHistory_Login = new Button();
      this.listView_BanHistory = new ListView();
      this.Ban_ColHeader1 = new ColumnHeader();
      this.Ban_ColHeader2 = new ColumnHeader();
      this.Ban_ColHeader3 = new ColumnHeader();
      this.groupBox19 = new GroupBox();
      this.groupBox20 = new GroupBox();
      this.button_LoginBan = new Button();
      this.richTextBox_Ban_Guide = new RichTextBox();
      this.textBox_LoginBan_DAY = new TextBox();
      this.textBox_LoginBan_CN = new TextBox();
      this.groupBox21 = new GroupBox();
      this.button_FirewallBan = new Button();
      this.textBox_IPBlock_IP = new TextBox();
      this.groupBox18 = new GroupBox();
      this.listView_IPC = new ListView();
      this.columnHeader5 = new ColumnHeader();
      this.columnHeader6 = new ColumnHeader();
      this.columnHeader7 = new ColumnHeader();
      this.textBox_IPC_IP = new TextBox();
      this.textBox_IPC_CharName = new TextBox();
      this.contextMenuStrip5 = new ContextMenuStrip(this.components);
      this.karakteriBanlaToolStripMenuItem = new ToolStripMenuItem();
      this.ıPBlockAtToolStripMenuItem = new ToolStripMenuItem();
      this.contextMenuStrip6 = new ContextMenuStrip(this.components);
      this.kaldırToolStripMenuItem = new ToolStripMenuItem();
      this.contextMenuStrip1.SuspendLayout();
      this.contextMenuStrip2.SuspendLayout();
      this.contextMenuStrip3.SuspendLayout();
      this.contextMenuStrip4.SuspendLayout();
      this.tabPage5.SuspendLayout();
      this.groupBox16.SuspendLayout();
      this.groupBox15.SuspendLayout();
      this.groupBox14.SuspendLayout();
      this.groupBox13.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.groupBox24.SuspendLayout();
      this.groupBox12.SuspendLayout();
      this.groupBox10.SuspendLayout();
      this.groupBox11.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.groupBox25.SuspendLayout();
      this.groupBox23.SuspendLayout();
      this.groupBox7.SuspendLayout();
      this.panel_OnlineYonetim.SuspendLayout();
      this.groupBox17.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox9.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.panel_YetkiliIP.SuspendLayout();
      this.groupBox8.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.groupBox_StartServer.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.panel_PortSetting.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.groupBox22.SuspendLayout();
      this.groupBox19.SuspendLayout();
      this.groupBox20.SuspendLayout();
      this.groupBox21.SuspendLayout();
      this.groupBox18.SuspendLayout();
      this.contextMenuStrip5.SuspendLayout();
      this.contextMenuStrip6.SuspendLayout();
      this.SuspendLayout();
      this.label1.Location = new Point(856, 510);
      this.label1.Name = "label1";
      this.label1.Size = new Size(85, 20);
      this.label1.TabIndex = 2;
      this.label1.Text = "Thread :";
      this.label1.TextAlign = ContentAlignment.MiddleRight;
      this.label3.Location = new Point(856, 530);
      this.label3.Name = "label3";
      this.label3.Size = new Size(85, 20);
      this.label3.TabIndex = 4;
      this.label3.Text = "Ram (MB) :";
      this.label3.TextAlign = ContentAlignment.MiddleRight;
      this.label5.Location = new Point(856, 450);
      this.label5.Name = "label5";
      this.label5.Size = new Size(85, 20);
      this.label5.TabIndex = 6;
      this.label5.Text = "Login Client :";
      this.label5.TextAlign = ContentAlignment.MiddleRight;
      this.label6.Location = new Point(856, 470);
      this.label6.Name = "label6";
      this.label6.Size = new Size(85, 20);
      this.label6.TabIndex = 7;
      this.label6.Text = "Agent Client :";
      this.label6.TextAlign = ContentAlignment.MiddleRight;
      this.label7.Location = new Point(856, 490);
      this.label7.Name = "label7";
      this.label7.Size = new Size(85, 20);
      this.label7.TabIndex = 8;
      this.label7.Text = "Suspect :";
      this.label7.TextAlign = ContentAlignment.MiddleRight;
      this.labelThread.Location = new Point(947, 510);
      this.labelThread.Name = "labelThread";
      this.labelThread.Size = new Size(85, 20);
      this.labelThread.TabIndex = 9;
      this.labelThread.Text = "0";
      this.labelThread.TextAlign = ContentAlignment.MiddleLeft;
      this.labelMEMORY.Location = new Point(947, 530);
      this.labelMEMORY.Name = "labelMEMORY";
      this.labelMEMORY.Size = new Size(85, 20);
      this.labelMEMORY.TabIndex = 11;
      this.labelMEMORY.Text = "0";
      this.labelMEMORY.TextAlign = ContentAlignment.MiddleLeft;
      this.label_LoginClient.Location = new Point(947, 450);
      this.label_LoginClient.Name = "label_LoginClient";
      this.label_LoginClient.Size = new Size(85, 20);
      this.label_LoginClient.TabIndex = 12;
      this.label_LoginClient.Text = "0";
      this.label_LoginClient.TextAlign = ContentAlignment.MiddleLeft;
      this.label_AgentClient.Location = new Point(947, 470);
      this.label_AgentClient.Name = "label_AgentClient";
      this.label_AgentClient.Size = new Size(85, 20);
      this.label_AgentClient.TabIndex = 13;
      this.label_AgentClient.Text = "0";
      this.label_AgentClient.TextAlign = ContentAlignment.MiddleLeft;
      this.label_Suspect.Location = new Point(947, 490);
      this.label_Suspect.Name = "label_Suspect";
      this.label_Suspect.Size = new Size(85, 20);
      this.label_Suspect.TabIndex = 14;
      this.label_Suspect.Text = "0";
      this.label_Suspect.TextAlign = ContentAlignment.MiddleLeft;
      this.listView1.BackColor = Color.AliceBlue;
      this.listView1.Columns.AddRange(new ColumnHeader[2]
      {
        this.columnHeader1,
        this.columnHeader2
      });
      this.listView1.ForeColor = SystemColors.ControlDarkDark;
      this.listView1.FullRowSelect = true;
      this.listView1.Location = new Point(12, 411);
      this.listView1.MultiSelect = false;
      this.listView1.Name = "listView1";
      this.listView1.Size = new Size(838, 158);
      this.listView1.TabIndex = 0;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = View.Details;
      this.listView1.MouseClick += new MouseEventHandler(this.listView1_MouseClick);
      this.columnHeader1.Text = "Tarih";
      this.columnHeader2.Text = "Mesaj";
      this.columnHeader2.Width = 758;
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.toolStripMenuItem1
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(87, 26);
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new Size(86, 22);
      this.toolStripMenuItem1.Text = "Sil";
      this.toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem1_Click);
      this.contextMenuStrip2.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.toolStripMenuItem2
      });
      this.contextMenuStrip2.Name = "contextMenuStrip2";
      this.contextMenuStrip2.Size = new Size(116, 26);
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new Size(115, 22);
      this.toolStripMenuItem2.Text = "Temizle";
      this.toolStripMenuItem2.Click += new EventHandler(this.toolStripMenuItem2_Click);
      this.contextMenuStrip3.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.toolStripMenuItem3
      });
      this.contextMenuStrip3.Name = "contextMenuStrip3";
      this.contextMenuStrip3.Size = new Size(143, 26);
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new Size(142, 22);
      this.toolStripMenuItem3.Text = "Seçilenleri Sil";
      this.toolStripMenuItem3.Click += new EventHandler(this.toolStripMenuItem3_Click);
      this.button_GetSettings.Enabled = false;
      this.button_GetSettings.Location = new Point(858, 412);
      this.button_GetSettings.Name = "button_GetSettings";
      this.button_GetSettings.Size = new Size(171, 30);
      this.button_GetSettings.TabIndex = 17;
      this.button_GetSettings.Text = "KGUARD";
      this.button_GetSettings.UseVisualStyleBackColor = true;
      this.button_GetSettings.Click += new EventHandler(this.button_GetSettings_Click);
      this.contextMenuStrip4.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.toolStripMenuItem4
      });
      this.contextMenuStrip4.Name = "contextMenuStrip3";
      this.contextMenuStrip4.Size = new Size(143, 26);
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new Size(142, 22);
      this.toolStripMenuItem4.Text = "Seçilenleri Sil";
      this.toolStripMenuItem4.Click += new EventHandler(this.toolStripMenuItem4_Click);
      this.tabPage5.Controls.Add((Control) this.groupBox16);
      this.tabPage5.Controls.Add((Control) this.groupBox15);
      this.tabPage5.Controls.Add((Control) this.groupBox14);
      this.tabPage5.Controls.Add((Control) this.groupBox13);
      this.tabPage5.Location = new Point(4, 22);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new Padding(3);
      this.tabPage5.Size = new Size(1016, 367);
      this.tabPage5.TabIndex = 4;
      this.tabPage5.Text = "Geliştirici";
      this.tabPage5.UseVisualStyleBackColor = true;
      this.groupBox16.Controls.Add((Control) this.checkBox_DoEverySecond);
      this.groupBox16.Controls.Add((Control) this.checkBox_LoginLogoutLog);
      this.groupBox16.Controls.Add((Control) this.checkBox_LockSlot);
      this.groupBox16.Controls.Add((Control) this.checkBox_GiveDC);
      this.groupBox16.Location = new Point(593, 102);
      this.groupBox16.Name = "groupBox16";
      this.groupBox16.Size = new Size(417, 259);
      this.groupBox16.TabIndex = 4;
      this.groupBox16.TabStop = false;
      this.groupBox16.Text = "Ekstra";
      this.checkBox_DoEverySecond.AutoSize = true;
      this.checkBox_DoEverySecond.Location = new Point(16, 99);
      this.checkBox_DoEverySecond.Name = "checkBox_DoEverySecond";
      this.checkBox_DoEverySecond.Size = new Size(300, 17);
      this.checkBox_DoEverySecond.TabIndex = 3;
      this.checkBox_DoEverySecond.Text = "__Dev_DoEverySecond Prosedürünü Saniyede Bir Çalıştır";
      this.checkBox_DoEverySecond.UseVisualStyleBackColor = true;
      this.checkBox_DoEverySecond.CheckedChanged += new EventHandler(this.checkBox_DoEverySecond_CheckedChanged);
      this.checkBox_LoginLogoutLog.AutoSize = true;
      this.checkBox_LoginLogoutLog.Location = new Point(16, 76);
      this.checkBox_LoginLogoutLog.Name = "checkBox_LoginLogoutLog";
      this.checkBox_LoginLogoutLog.Size = new Size(269, 17);
      this.checkBox_LoginLogoutLog.TabIndex = 2;
      this.checkBox_LoginLogoutLog.Text = "__Dev_Log_LoginLogout Prosedürüne Log Gönder";
      this.checkBox_LoginLogoutLog.UseVisualStyleBackColor = true;
      this.checkBox_LoginLogoutLog.CheckedChanged += new EventHandler(this.checkBox_LoginLogoutLog_CheckedChanged);
      this.checkBox_LockSlot.AutoSize = true;
      this.checkBox_LockSlot.Location = new Point(16, 53);
      this.checkBox_LockSlot.Name = "checkBox_LockSlot";
      this.checkBox_LockSlot.Size = new Size(269, 17);
      this.checkBox_LockSlot.TabIndex = 1;
      this.checkBox_LockSlot.Text = "__Dev_LockSlot Tablo Verilerine Göre Slotları Kilitle";
      this.checkBox_LockSlot.UseVisualStyleBackColor = true;
      this.checkBox_LockSlot.CheckedChanged += new EventHandler(this.checkBox_LockSlot_CheckedChanged);
      this.checkBox_GiveDC.AutoSize = true;
      this.checkBox_GiveDC.Location = new Point(16, 30);
      this.checkBox_GiveDC.Name = "checkBox_GiveDC";
      this.checkBox_GiveDC.Size = new Size(233, 17);
      this.checkBox_GiveDC.TabIndex = 0;
      this.checkBox_GiveDC.Text = "__Dev_GiveDC Tablo Verilerine Göre DC at";
      this.checkBox_GiveDC.UseVisualStyleBackColor = true;
      this.checkBox_GiveDC.CheckedChanged += new EventHandler(this.checkBox_GiveDC_CheckedChanged);
      this.groupBox15.Controls.Add((Control) this.listBox_VerifyNames);
      this.groupBox15.Controls.Add((Control) this.textBox_IncPMSendedVerify);
      this.groupBox15.Controls.Add((Control) this.checkBox_VerifyPM);
      this.groupBox15.Location = new Point(399, 6);
      this.groupBox15.Name = "groupBox15";
      this.groupBox15.Size = new Size(188, 355);
      this.groupBox15.TabIndex = 3;
      this.groupBox15.TabStop = false;
      this.groupBox15.Text = "Gelen PM";
      this.listBox_VerifyNames.FormattingEnabled = true;
      this.listBox_VerifyNames.Location = new Point(6, 85);
      this.listBox_VerifyNames.Name = "listBox_VerifyNames";
      this.listBox_VerifyNames.SelectionMode = SelectionMode.MultiExtended;
      this.listBox_VerifyNames.Size = new Size(176, 264);
      this.listBox_VerifyNames.TabIndex = 2;
      this.listBox_VerifyNames.MouseDown += new MouseEventHandler(this.listBox_VerifyNames_MouseDown);
      this.textBox_IncPMSendedVerify.Location = new Point(6, 59);
      this.textBox_IncPMSendedVerify.Name = "textBox_IncPMSendedVerify";
      this.textBox_IncPMSendedVerify.Size = new Size(176, 20);
      this.textBox_IncPMSendedVerify.TabIndex = 1;
      this.textBox_IncPMSendedVerify.KeyPress += new KeyPressEventHandler(this.textBox_IncPMSendedVerify_KeyPress);
      this.checkBox_VerifyPM.Location = new Point(6, 19);
      this.checkBox_VerifyPM.Name = "checkBox_VerifyPM";
      this.checkBox_VerifyPM.Size = new Size(176, 31);
      this.checkBox_VerifyPM.TabIndex = 0;
      this.checkBox_VerifyPM.Text = "Listedeki isimlere gelen PM'lere alındı paketi gönder";
      this.checkBox_VerifyPM.UseVisualStyleBackColor = true;
      this.checkBox_VerifyPM.CheckedChanged += new EventHandler(this.checkBox_VerifyPM_CheckedChanged);
      this.groupBox14.Controls.Add((Control) this.checkBox_Devs_GlobalSw);
      this.groupBox14.Controls.Add((Control) this.label25);
      this.groupBox14.Controls.Add((Control) this.checkBox_Devs_Notice);
      this.groupBox14.Controls.Add((Control) this.checkBox_Devs_SendMsg);
      this.groupBox14.Location = new Point(6, 6);
      this.groupBox14.Name = "groupBox14";
      this.groupBox14.Size = new Size(387, 355);
      this.groupBox14.TabIndex = 2;
      this.groupBox14.TabStop = false;
      this.groupBox14.Text = "Chat";
      this.checkBox_Devs_GlobalSw.AutoSize = true;
      this.checkBox_Devs_GlobalSw.Location = new Point(16, 56);
      this.checkBox_Devs_GlobalSw.Name = "checkBox_Devs_GlobalSw";
      this.checkBox_Devs_GlobalSw.Size = new Size(286, 17);
      this.checkBox_Devs_GlobalSw.TabIndex = 6;
      this.checkBox_Devs_GlobalSw.Text = "__Dev_SendGlobal Tablo Verilerini Oyunculara Gönder";
      this.checkBox_Devs_GlobalSw.UseVisualStyleBackColor = true;
      this.checkBox_Devs_GlobalSw.CheckedChanged += new EventHandler(this.checkBox_Devs_GlobalSw_CheckedChanged);
      this.label25.Location = new Point(13, 126);
      this.label25.Name = "label25";
      this.label25.Size = new Size(312, 221);
      this.label25.TabIndex = 5;
      this.label25.Text = componentResourceManager.GetString("label25.Text");
      this.checkBox_Devs_Notice.AutoSize = true;
      this.checkBox_Devs_Notice.Location = new Point(16, 33);
      this.checkBox_Devs_Notice.Name = "checkBox_Devs_Notice";
      this.checkBox_Devs_Notice.Size = new Size(262, 17);
      this.checkBox_Devs_Notice.TabIndex = 4;
      this.checkBox_Devs_Notice.Text = "__Dev_Notice Tablo Verilerini Oyunculara Gönder";
      this.checkBox_Devs_Notice.UseVisualStyleBackColor = true;
      this.checkBox_Devs_Notice.CheckedChanged += new EventHandler(this.checkBox_Devs_Notice_CheckedChanged);
      this.checkBox_Devs_SendMsg.AutoSize = true;
      this.checkBox_Devs_SendMsg.Location = new Point(16, 96);
      this.checkBox_Devs_SendMsg.Name = "checkBox_Devs_SendMsg";
      this.checkBox_Devs_SendMsg.Size = new Size(270, 17);
      this.checkBox_Devs_SendMsg.TabIndex = 3;
      this.checkBox_Devs_SendMsg.Text = "__Dev_SendMsg Tablo Verilerini Oyuncuya Gönder";
      this.checkBox_Devs_SendMsg.UseVisualStyleBackColor = true;
      this.checkBox_Devs_SendMsg.CheckedChanged += new EventHandler(this.checkBox_Devs_SendMsg_CheckedChanged);
      this.groupBox13.Controls.Add((Control) this.checkBox_LogUserGlobal);
      this.groupBox13.Controls.Add((Control) this.checkBox_LogUserPM);
      this.groupBox13.Location = new Point(593, 6);
      this.groupBox13.Name = "groupBox13";
      this.groupBox13.Size = new Size(417, 90);
      this.groupBox13.TabIndex = 1;
      this.groupBox13.TabStop = false;
      this.groupBox13.Text = "Chat Log";
      this.checkBox_LogUserGlobal.AutoSize = true;
      this.checkBox_LogUserGlobal.Location = new Point(16, 51);
      this.checkBox_LogUserGlobal.Name = "checkBox_LogUserGlobal";
      this.checkBox_LogUserGlobal.Size = new Size(298, 17);
      this.checkBox_LogUserGlobal.TabIndex = 1;
      this.checkBox_LogUserGlobal.Text = "Global Chat için __Dev_GLOBAL prosedürüne log gönder";
      this.checkBox_LogUserGlobal.UseVisualStyleBackColor = true;
      this.checkBox_LogUserGlobal.CheckedChanged += new EventHandler(this.checkBox_LogUserGlobal_CheckedChanged);
      this.checkBox_LogUserPM.AutoSize = true;
      this.checkBox_LogUserPM.Location = new Point(16, 28);
      this.checkBox_LogUserPM.Name = "checkBox_LogUserPM";
      this.checkBox_LogUserPM.Size = new Size(258, 17);
      this.checkBox_LogUserPM.TabIndex = 0;
      this.checkBox_LogUserPM.Text = "PM Chat için __Dev_PM prosedürüne log gönder";
      this.checkBox_LogUserPM.UseVisualStyleBackColor = true;
      this.checkBox_LogUserPM.CheckedChanged += new EventHandler(this.checkBox_LogUserPM_CheckedChanged);
      this.tabPage4.Controls.Add((Control) this.groupBox24);
      this.tabPage4.Controls.Add((Control) this.groupBox12);
      this.tabPage4.Controls.Add((Control) this.groupBox10);
      this.tabPage4.Controls.Add((Control) this.groupBox11);
      this.tabPage4.Location = new Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new Padding(3);
      this.tabPage4.Size = new Size(1016, 367);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Özelleştirmeler";
      this.tabPage4.UseVisualStyleBackColor = true;
      this.groupBox24.Controls.Add((Control) this.checkBox_SkillBug);
      this.groupBox24.Controls.Add((Control) this.checkBox_BugExc);
      this.groupBox24.Controls.Add((Control) this.checkBox_BugEsc);
      this.groupBox24.Controls.Add((Control) this.checkBox_BugStall);
      this.groupBox24.Location = new Point(689, 156);
      this.groupBox24.Name = "groupBox24";
      this.groupBox24.Size = new Size(321, 205);
      this.groupBox24.TabIndex = 4;
      this.groupBox24.TabStop = false;
      this.groupBox24.Text = "Bug Kısıtlamaları";
      this.checkBox_SkillBug.AutoSize = true;
      this.checkBox_SkillBug.Location = new Point(12, 88);
      this.checkBox_SkillBug.Name = "checkBox_SkillBug";
      this.checkBox_SkillBug.Size = new Size(235, 17);
      this.checkBox_SkillBug.TabIndex = 15;
      this.checkBox_SkillBug.Text = "Skill Bug Önle (1 skill kullanımı 100ms içinde)";
      this.checkBox_SkillBug.UseVisualStyleBackColor = true;
      this.checkBox_SkillBug.CheckedChanged += new EventHandler(this.checkBox_SkillBug_CheckedChanged);
      this.checkBox_BugExc.AutoSize = true;
      this.checkBox_BugExc.Location = new Point(12, 19);
      this.checkBox_BugExc.Name = "checkBox_BugExc";
      this.checkBox_BugExc.Size = new Size(121, 17);
      this.checkBox_BugExc.TabIndex = 4;
      this.checkBox_BugExc.Text = "Exchange Bug Önle";
      this.checkBox_BugExc.UseVisualStyleBackColor = true;
      this.checkBox_BugExc.CheckedChanged += new EventHandler(this.checkBox_BugExc_CheckedChanged);
      this.checkBox_BugEsc.AutoSize = true;
      this.checkBox_BugEsc.Location = new Point(12, 65);
      this.checkBox_BugEsc.Name = "checkBox_BugEsc";
      this.checkBox_BugEsc.Size = new Size(90, 17);
      this.checkBox_BugEsc.TabIndex = 5;
      this.checkBox_BugEsc.Text = "Exit Bug Önle";
      this.checkBox_BugEsc.UseVisualStyleBackColor = true;
      this.checkBox_BugEsc.CheckedChanged += new EventHandler(this.checkBox_BugEsc_CheckedChanged);
      this.checkBox_BugStall.AutoSize = true;
      this.checkBox_BugStall.Location = new Point(12, 42);
      this.checkBox_BugStall.Name = "checkBox_BugStall";
      this.checkBox_BugStall.Size = new Size(93, 17);
      this.checkBox_BugStall.TabIndex = 6;
      this.checkBox_BugStall.Text = "Stall Bug Önle";
      this.checkBox_BugStall.UseVisualStyleBackColor = true;
      this.checkBox_BugStall.CheckedChanged += new EventHandler(this.checkBox_BugStall_CheckedChanged);
      this.groupBox12.Controls.Add((Control) this.textBox_InsuiltChat);
      this.groupBox12.Controls.Add((Control) this.listBox_InsuiltChat);
      this.groupBox12.Location = new Point(508, 6);
      this.groupBox12.Name = "groupBox12";
      this.groupBox12.Size = new Size(175, 355);
      this.groupBox12.TabIndex = 3;
      this.groupBox12.TabStop = false;
      this.groupBox12.Text = "Chat Filtrelenecek Sözcükler";
      this.textBox_InsuiltChat.Location = new Point(6, 19);
      this.textBox_InsuiltChat.Name = "textBox_InsuiltChat";
      this.textBox_InsuiltChat.Size = new Size(163, 20);
      this.textBox_InsuiltChat.TabIndex = 1;
      this.textBox_InsuiltChat.KeyPress += new KeyPressEventHandler(this.textBox_InsuiltChat_KeyPress);
      this.listBox_InsuiltChat.FormattingEnabled = true;
      this.listBox_InsuiltChat.Location = new Point(6, 45);
      this.listBox_InsuiltChat.Name = "listBox_InsuiltChat";
      this.listBox_InsuiltChat.SelectionMode = SelectionMode.MultiExtended;
      this.listBox_InsuiltChat.Size = new Size(163, 303);
      this.listBox_InsuiltChat.TabIndex = 0;
      this.listBox_InsuiltChat.MouseDown += new MouseEventHandler(this.listBox_InsuiltChat_MouseDown);
      this.groupBox10.Controls.Add((Control) this.checkBox_DisablePetsInFTW);
      this.groupBox10.Controls.Add((Control) this.button_HelpForFTWLimit);
      this.groupBox10.Controls.Add((Control) this.textBox_LimitOfFortressWar);
      this.groupBox10.Controls.Add((Control) this.checkBox_DropInTown);
      this.groupBox10.Controls.Add((Control) this.checkBox_LimitOfFortressWar);
      this.groupBox10.Controls.Add((Control) this.checkBox_AcademyCreate);
      this.groupBox10.Controls.Add((Control) this.checkBox_TaxRate);
      this.groupBox10.Controls.Add((Control) this.textBox_UnionLimit);
      this.groupBox10.Controls.Add((Control) this.textBox_GuildLimit);
      this.groupBox10.Controls.Add((Control) this.checkBox_UnionLimit);
      this.groupBox10.Controls.Add((Control) this.checkBox_GuildLimit);
      this.groupBox10.Controls.Add((Control) this.checkBox_ThiefRewardRestrc);
      this.groupBox10.Controls.Add((Control) this.textBox_PlusLimit);
      this.groupBox10.Controls.Add((Control) this.checkBox_PlusLimit);
      this.groupBox10.Controls.Add((Control) this.checkBox_Swear);
      this.groupBox10.Controls.Add((Control) this.checkBox_JobRev);
      this.groupBox10.Location = new Point(12, 6);
      this.groupBox10.Name = "groupBox10";
      this.groupBox10.Size = new Size(490, 355);
      this.groupBox10.TabIndex = 2;
      this.groupBox10.TabStop = false;
      this.groupBox10.Text = "Genel Kısıtlamalar";
      this.checkBox_DisablePetsInFTW.AutoSize = true;
      this.checkBox_DisablePetsInFTW.Location = new Point(12, 259);
      this.checkBox_DisablePetsInFTW.Name = "checkBox_DisablePetsInFTW";
      this.checkBox_DisablePetsInFTW.Size = new Size(234, 17);
      this.checkBox_DisablePetsInFTW.TabIndex = 21;
      this.checkBox_DisablePetsInFTW.Text = "Fortress Haritalarında Pet Kullanımını Engelle";
      this.checkBox_DisablePetsInFTW.UseVisualStyleBackColor = true;
      this.checkBox_DisablePetsInFTW.CheckedChanged += new EventHandler(this.checkBox_DisablePetsInFTW_CheckedChanged);
      this.button_HelpForFTWLimit.Location = new Point(301, 233);
      this.button_HelpForFTWLimit.Name = "button_HelpForFTWLimit";
      this.button_HelpForFTWLimit.Size = new Size(52, 21);
      this.button_HelpForFTWLimit.TabIndex = 19;
      this.button_HelpForFTWLimit.Text = "Bilgi";
      this.button_HelpForFTWLimit.UseVisualStyleBackColor = true;
      this.button_HelpForFTWLimit.Click += new EventHandler(this.button_HelpForFTWLimit_Click);
      this.textBox_LimitOfFortressWar.Location = new Point(234, 234);
      this.textBox_LimitOfFortressWar.MaxLength = 3;
      this.textBox_LimitOfFortressWar.Name = "textBox_LimitOfFortressWar";
      this.textBox_LimitOfFortressWar.Size = new Size(61, 20);
      this.textBox_LimitOfFortressWar.TabIndex = 20;
      this.textBox_LimitOfFortressWar.TextChanged += new EventHandler(this.textBox_LimitOfFortressWar_TextChanged);
      this.checkBox_DropInTown.AutoSize = true;
      this.checkBox_DropInTown.Location = new Point(12, 213);
      this.checkBox_DropInTown.Name = "checkBox_DropInTown";
      this.checkBox_DropInTown.Size = new Size(177, 17);
      this.checkBox_DropInTown.TabIndex = 16;
      this.checkBox_DropInTown.Text = "Şehir İçi Yere Item Atmayı Kapat";
      this.checkBox_DropInTown.UseVisualStyleBackColor = true;
      this.checkBox_DropInTown.CheckedChanged += new EventHandler(this.checkBox_DropInTown_CheckedChanged);
      this.checkBox_LimitOfFortressWar.AutoSize = true;
      this.checkBox_LimitOfFortressWar.Location = new Point(12, 236);
      this.checkBox_LimitOfFortressWar.Name = "checkBox_LimitOfFortressWar";
      this.checkBox_LimitOfFortressWar.Size = new Size(216, 17);
      this.checkBox_LimitOfFortressWar.TabIndex = 18;
      this.checkBox_LimitOfFortressWar.Text = "Fortress Kayıt Limiti (Oyuncu Sayısı Bazlı)";
      this.checkBox_LimitOfFortressWar.UseVisualStyleBackColor = true;
      this.checkBox_LimitOfFortressWar.CheckedChanged += new EventHandler(this.checkBox_LimitOfFortressWar_CheckedChanged);
      this.checkBox_AcademyCreate.AutoSize = true;
      this.checkBox_AcademyCreate.Location = new Point(12, 190);
      this.checkBox_AcademyCreate.Name = "checkBox_AcademyCreate";
      this.checkBox_AcademyCreate.Size = new Size(164, 17);
      this.checkBox_AcademyCreate.TabIndex = 14;
      this.checkBox_AcademyCreate.Text = "Academy Kuruluşunu Engelle";
      this.checkBox_AcademyCreate.UseVisualStyleBackColor = true;
      this.checkBox_AcademyCreate.CheckedChanged += new EventHandler(this.checkBox_AcademyCreate_CheckedChanged);
      this.checkBox_TaxRate.AutoSize = true;
      this.checkBox_TaxRate.Location = new Point(12, 167);
      this.checkBox_TaxRate.Name = "checkBox_TaxRate";
      this.checkBox_TaxRate.Size = new Size(179, 17);
      this.checkBox_TaxRate.TabIndex = 13;
      this.checkBox_TaxRate.Text = "Tax Rate Değiştirilmesini Engelle";
      this.checkBox_TaxRate.UseVisualStyleBackColor = true;
      this.checkBox_TaxRate.CheckedChanged += new EventHandler(this.checkBox_TaxRate_CheckedChanged);
      this.textBox_UnionLimit.Location = new Point(138, 142);
      this.textBox_UnionLimit.Name = "textBox_UnionLimit";
      this.textBox_UnionLimit.Size = new Size(42, 20);
      this.textBox_UnionLimit.TabIndex = 12;
      this.textBox_UnionLimit.TextAlign = HorizontalAlignment.Center;
      this.textBox_UnionLimit.TextChanged += new EventHandler(this.textBox_UnionLimit_TextChanged);
      this.textBox_GuildLimit.Location = new Point(134, 119);
      this.textBox_GuildLimit.Name = "textBox_GuildLimit";
      this.textBox_GuildLimit.Size = new Size(42, 20);
      this.textBox_GuildLimit.TabIndex = 4;
      this.textBox_GuildLimit.TextAlign = HorizontalAlignment.Center;
      this.textBox_GuildLimit.TextChanged += new EventHandler(this.textBox_GuildLimit_TextChanged);
      this.checkBox_UnionLimit.AutoSize = true;
      this.checkBox_UnionLimit.Location = new Point(12, 144);
      this.checkBox_UnionLimit.Name = "checkBox_UnionLimit";
      this.checkBox_UnionLimit.Size = new Size(120, 17);
      this.checkBox_UnionLimit.TabIndex = 11;
      this.checkBox_UnionLimit.Text = "Union Member Sınırı";
      this.checkBox_UnionLimit.UseVisualStyleBackColor = true;
      this.checkBox_UnionLimit.CheckedChanged += new EventHandler(this.checkBox_UnionLimit_CheckedChanged);
      this.checkBox_GuildLimit.AutoSize = true;
      this.checkBox_GuildLimit.Location = new Point(12, 121);
      this.checkBox_GuildLimit.Name = "checkBox_GuildLimit";
      this.checkBox_GuildLimit.Size = new Size(116, 17);
      this.checkBox_GuildLimit.TabIndex = 10;
      this.checkBox_GuildLimit.Text = "Guild Member Sınırı";
      this.checkBox_GuildLimit.UseVisualStyleBackColor = true;
      this.checkBox_GuildLimit.CheckedChanged += new EventHandler(this.checkBox_GuildLimit_CheckedChanged);
      this.checkBox_ThiefRewardRestrc.AutoSize = true;
      this.checkBox_ThiefRewardRestrc.Location = new Point(12, 52);
      this.checkBox_ThiefRewardRestrc.Name = "checkBox_ThiefRewardRestrc";
      this.checkBox_ThiefRewardRestrc.Size = new Size(216, 17);
      this.checkBox_ThiefRewardRestrc.TabIndex = 9;
      this.checkBox_ThiefRewardRestrc.Text = "Thief Haftalık Reward Seçeneğini Kapat";
      this.checkBox_ThiefRewardRestrc.UseVisualStyleBackColor = true;
      this.checkBox_ThiefRewardRestrc.CheckedChanged += new EventHandler(this.checkBox_ThiefRewardRestrc_CheckedChanged);
      this.textBox_PlusLimit.Location = new Point(84, 96);
      this.textBox_PlusLimit.MaxLength = 2;
      this.textBox_PlusLimit.Name = "textBox_PlusLimit";
      this.textBox_PlusLimit.Size = new Size(42, 20);
      this.textBox_PlusLimit.TabIndex = 3;
      this.textBox_PlusLimit.TextAlign = HorizontalAlignment.Center;
      this.textBox_PlusLimit.TextChanged += new EventHandler(this.textBox_PlusLimit_TextChanged);
      this.checkBox_PlusLimit.AutoSize = true;
      this.checkBox_PlusLimit.Location = new Point(12, 98);
      this.checkBox_PlusLimit.Name = "checkBox_PlusLimit";
      this.checkBox_PlusLimit.Size = new Size(66, 17);
      this.checkBox_PlusLimit.TabIndex = 8;
      this.checkBox_PlusLimit.Text = "Artı Sınırı";
      this.checkBox_PlusLimit.UseVisualStyleBackColor = true;
      this.checkBox_PlusLimit.CheckedChanged += new EventHandler(this.checkBox_PlusLimit_CheckedChanged);
      this.checkBox_Swear.AutoSize = true;
      this.checkBox_Swear.Location = new Point(12, 75);
      this.checkBox_Swear.Name = "checkBox_Swear";
      this.checkBox_Swear.Size = new Size(114, 17);
      this.checkBox_Swear.TabIndex = 7;
      this.checkBox_Swear.Text = "Chat Kelime Filtresi";
      this.checkBox_Swear.UseVisualStyleBackColor = true;
      this.checkBox_Swear.CheckedChanged += new EventHandler(this.checkBox_Swear_CheckedChanged);
      this.checkBox_JobRev.AutoSize = true;
      this.checkBox_JobRev.Location = new Point(12, 29);
      this.checkBox_JobRev.Name = "checkBox_JobRev";
      this.checkBox_JobRev.Size = new Size(222, 17);
      this.checkBox_JobRev.TabIndex = 3;
      this.checkBox_JobRev.Text = "Job Sırasında Reverse Kullanımını Engelle";
      this.checkBox_JobRev.UseVisualStyleBackColor = true;
      this.checkBox_JobRev.CheckedChanged += new EventHandler(this.checkBox_JobRev_CheckedChanged);
      this.groupBox11.Controls.Add((Control) this.textBox_Welcome2);
      this.groupBox11.Controls.Add((Control) this.label27);
      this.groupBox11.Controls.Add((Control) this.label26);
      this.groupBox11.Controls.Add((Control) this.textBox_Welcome1);
      this.groupBox11.Controls.Add((Control) this.checkBox_Welcome);
      this.groupBox11.Location = new Point(689, 6);
      this.groupBox11.Name = "groupBox11";
      this.groupBox11.Size = new Size(321, 144);
      this.groupBox11.TabIndex = 1;
      this.groupBox11.TabStop = false;
      this.groupBox11.Text = "Hoşgeldin Mesajı";
      this.textBox_Welcome2.Location = new Point(12, 108);
      this.textBox_Welcome2.Name = "textBox_Welcome2";
      this.textBox_Welcome2.Size = new Size(258, 20);
      this.textBox_Welcome2.TabIndex = 14;
      this.textBox_Welcome2.TextChanged += new EventHandler(this.textBox_Welcome2_TextChanged);
      this.label27.Location = new Point(15, 86);
      this.label27.Name = "label27";
      this.label27.Size = new Size(69, 19);
      this.label27.TabIndex = 11;
      this.label27.Text = "{Mesaj} :";
      this.label27.TextAlign = ContentAlignment.MiddleRight;
      this.label26.Location = new Point(15, 41);
      this.label26.Name = "label26";
      this.label26.Size = new Size(69, 19);
      this.label26.TabIndex = 10;
      this.label26.Text = "{Karşılama} :";
      this.label26.TextAlign = ContentAlignment.MiddleRight;
      this.textBox_Welcome1.Location = new Point(12, 63);
      this.textBox_Welcome1.Name = "textBox_Welcome1";
      this.textBox_Welcome1.Size = new Size(217, 20);
      this.textBox_Welcome1.TabIndex = 1;
      this.textBox_Welcome1.TextChanged += new EventHandler(this.textBox_Welcome1_TextChanged);
      this.checkBox_Welcome.AutoSize = true;
      this.checkBox_Welcome.Location = new Point(223, 22);
      this.checkBox_Welcome.Name = "checkBox_Welcome";
      this.checkBox_Welcome.Size = new Size(47, 17);
      this.checkBox_Welcome.TabIndex = 0;
      this.checkBox_Welcome.Text = "Aktif";
      this.checkBox_Welcome.UseVisualStyleBackColor = true;
      this.checkBox_Welcome.CheckedChanged += new EventHandler(this.CheckBox_Welcome_CheckedChanged);
      this.tabPage2.Controls.Add((Control) this.groupBox25);
      this.tabPage2.Controls.Add((Control) this.groupBox23);
      this.tabPage2.Controls.Add((Control) this.groupBox7);
      this.tabPage2.Controls.Add((Control) this.groupBox17);
      this.tabPage2.Location = new Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(1016, 367);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Ayarlar";
      this.tabPage2.UseVisualStyleBackColor = true;
      this.groupBox25.Controls.Add((Control) this.label23);
      this.groupBox25.Controls.Add((Control) this.comboBox_DinnerTime);
      this.groupBox25.Controls.Add((Control) this.label2);
      this.groupBox25.Location = new Point(719, 133);
      this.groupBox25.Name = "groupBox25";
      this.groupBox25.Size = new Size(291, 228);
      this.groupBox25.TabIndex = 14;
      this.groupBox25.TabStop = false;
      this.groupBox25.Text = "Performans";
      this.label23.Location = new Point(26, 156);
      this.label23.Name = "label23";
      this.label23.Size = new Size(242, 66);
      this.label23.TabIndex = 10;
      this.label23.Text = "Ayarlayacağınız süre her oyuncu için açılan döngünün bir turun ardından dinleneceği süredir. Bu süre milisaniye değerindedir.\r\nDefault Değer: 10";
      this.label23.TextAlign = ContentAlignment.MiddleCenter;
      this.comboBox_DinnerTime.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox_DinnerTime.FormattingEnabled = true;
      this.comboBox_DinnerTime.Items.AddRange(new object[10]
      {
        (object) "2",
        (object) "4",
        (object) "6",
        (object) "8",
        (object) "10",
        (object) "12",
        (object) "14",
        (object) "16",
        (object) "18",
        (object) "20"
      });
      this.comboBox_DinnerTime.Location = new Point(112, 118);
      this.comboBox_DinnerTime.Name = "comboBox_DinnerTime";
      this.comboBox_DinnerTime.Size = new Size(66, 21);
      this.comboBox_DinnerTime.TabIndex = 9;
      this.comboBox_DinnerTime.SelectedIndexChanged += new EventHandler(this.comboBox_DinnerTime_SelectedIndexChanged);
      this.label2.Location = new Point(26, 16);
      this.label2.Name = "label2";
      this.label2.Size = new Size(242, 83);
      this.label2.TabIndex = 8;
      this.label2.Text = "İşlemci Kullanımı / Performans Oranı\r\n\r\nBu ayar çok hassastır. Ayar düştükçe performans ve işlemci kullanımı artar.\r\nİdeal seçenekler 10, 12 ve 14'dür.";
      this.label2.TextAlign = ContentAlignment.MiddleCenter;
      this.groupBox23.Controls.Add((Control) this.checkBox_MaxPacketLimit);
      this.groupBox23.Location = new Point(719, 6);
      this.groupBox23.Name = "groupBox23";
      this.groupBox23.Size = new Size(291, 121);
      this.groupBox23.TabIndex = 13;
      this.groupBox23.TabStop = false;
      this.groupBox23.Text = "Ekstra Güvenlik";
      this.checkBox_MaxPacketLimit.AutoSize = true;
      this.checkBox_MaxPacketLimit.Location = new Point(19, 29);
      this.checkBox_MaxPacketLimit.Name = "checkBox_MaxPacketLimit";
      this.checkBox_MaxPacketLimit.Size = new Size(234, 17);
      this.checkBox_MaxPacketLimit.TabIndex = 17;
      this.checkBox_MaxPacketLimit.Text = "Karakter Başı Maximum Paket Sınırı 100.000";
      this.checkBox_MaxPacketLimit.UseVisualStyleBackColor = true;
      this.checkBox_MaxPacketLimit.CheckedChanged += new EventHandler(this.checkBox_MaxPacketLimit_CheckedChanged);
      this.groupBox7.Controls.Add((Control) this.button_OnlineSave);
      this.groupBox7.Controls.Add((Control) this.button_OnlineEdit);
      this.groupBox7.Controls.Add((Control) this.panel_OnlineYonetim);
      this.groupBox7.Location = new Point(6, 6);
      this.groupBox7.Name = "groupBox7";
      this.groupBox7.Size = new Size(431, 355);
      this.groupBox7.TabIndex = 12;
      this.groupBox7.TabStop = false;
      this.groupBox7.Text = "Online Yönetimi";
      this.button_OnlineSave.Location = new Point(269, 326);
      this.button_OnlineSave.Name = "button_OnlineSave";
      this.button_OnlineSave.Size = new Size(75, 23);
      this.button_OnlineSave.TabIndex = 4;
      this.button_OnlineSave.Text = "Kaydet";
      this.button_OnlineSave.UseVisualStyleBackColor = true;
      this.button_OnlineSave.Visible = false;
      this.button_OnlineSave.Click += new EventHandler(this.button_OnlineSave_Click);
      this.button_OnlineEdit.Location = new Point(350, 326);
      this.button_OnlineEdit.Name = "button_OnlineEdit";
      this.button_OnlineEdit.Size = new Size(75, 23);
      this.button_OnlineEdit.TabIndex = 3;
      this.button_OnlineEdit.Text = "Düzenle";
      this.button_OnlineEdit.UseVisualStyleBackColor = true;
      this.button_OnlineEdit.Click += new EventHandler(this.button_OnlineEdit_Click);
      this.panel_OnlineYonetim.Controls.Add((Control) this.checkBox_Multiplier);
      this.panel_OnlineYonetim.Controls.Add((Control) this.button_RemoveJobID);
      this.panel_OnlineYonetim.Controls.Add((Control) this.button_AddJobID);
      this.panel_OnlineYonetim.Controls.Add((Control) this.label20);
      this.panel_OnlineYonetim.Controls.Add((Control) this.textBox_JobID);
      this.panel_OnlineYonetim.Controls.Add((Control) this.listBox_JobID);
      this.panel_OnlineYonetim.Controls.Add((Control) this.checkBox_Premiums);
      this.panel_OnlineYonetim.Controls.Add((Control) this.checkBox_RandomDC);
      this.panel_OnlineYonetim.Controls.Add((Control) this.checkBox_LimitCapac);
      this.panel_OnlineYonetim.Controls.Add((Control) this.label19);
      this.panel_OnlineYonetim.Controls.Add((Control) this.checkBox_AddOnline);
      this.panel_OnlineYonetim.Controls.Add((Control) this.textBox_OnlineCap);
      this.panel_OnlineYonetim.Controls.Add((Control) this.textBox_OnlineCount);
      this.panel_OnlineYonetim.Controls.Add((Control) this.label18);
      this.panel_OnlineYonetim.Enabled = false;
      this.panel_OnlineYonetim.Location = new Point(6, 19);
      this.panel_OnlineYonetim.Name = "panel_OnlineYonetim";
      this.panel_OnlineYonetim.Size = new Size(419, 301);
      this.panel_OnlineYonetim.TabIndex = 2;
      this.checkBox_Multiplier.AutoSize = true;
      this.checkBox_Multiplier.Location = new Point(18, 33);
      this.checkBox_Multiplier.Name = "checkBox_Multiplier";
      this.checkBox_Multiplier.Size = new Size(207, 17);
      this.checkBox_Multiplier.TabIndex = 22;
      this.checkBox_Multiplier.Tag = (object) "";
      this.checkBox_Multiplier.Text = "Çarpan olarak kullan (Default: Ekleme)";
      this.checkBox_Multiplier.UseVisualStyleBackColor = true;
      this.checkBox_Multiplier.CheckedChanged += new EventHandler(this.checkBox_Multiplier_CheckedChanged);
      this.button_RemoveJobID.Enabled = false;
      this.button_RemoveJobID.Location = new Point(329, 65);
      this.button_RemoveJobID.Name = "button_RemoveJobID";
      this.button_RemoveJobID.Size = new Size(87, 23);
      this.button_RemoveJobID.TabIndex = 21;
      this.button_RemoveJobID.Text = "Kaldır";
      this.button_RemoveJobID.UseVisualStyleBackColor = true;
      this.button_RemoveJobID.Click += new EventHandler(this.button_RemoveJobID_Click);
      this.button_AddJobID.Location = new Point(329, 36);
      this.button_AddJobID.Name = "button_AddJobID";
      this.button_AddJobID.Size = new Size(87, 23);
      this.button_AddJobID.TabIndex = 20;
      this.button_AddJobID.Text = "Ekle";
      this.button_AddJobID.UseVisualStyleBackColor = true;
      this.button_AddJobID.Click += new EventHandler(this.button_AddJobID_Click);
      this.label20.Location = new Point(329, 93);
      this.label20.Name = "label20";
      this.label20.Size = new Size(87, 51);
      this.label20.TabIndex = 19;
      this.label20.Text = "Premium Skill ID";
      this.textBox_JobID.Location = new Point(329, 10);
      this.textBox_JobID.MaxLength = 7;
      this.textBox_JobID.Name = "textBox_JobID";
      this.textBox_JobID.Size = new Size(87, 20);
      this.textBox_JobID.TabIndex = 18;
      this.textBox_JobID.TextAlign = HorizontalAlignment.Center;
      this.listBox_JobID.FormattingEnabled = true;
      this.listBox_JobID.Location = new Point(235, 10);
      this.listBox_JobID.Name = "listBox_JobID";
      this.listBox_JobID.Size = new Size(88, 134);
      this.listBox_JobID.TabIndex = 17;
      this.listBox_JobID.SelectedIndexChanged += new EventHandler(this.listBox_JobID_SelectedIndexChanged);
      this.checkBox_Premiums.AutoSize = true;
      this.checkBox_Premiums.Location = new Point(18, 150);
      this.checkBox_Premiums.Name = "checkBox_Premiums";
      this.checkBox_Premiums.Size = new Size(162, 17);
      this.checkBox_Premiums.TabIndex = 16;
      this.checkBox_Premiums.Text = "Belirli JobID'leri her zaman al.";
      this.checkBox_Premiums.UseVisualStyleBackColor = true;
      this.checkBox_RandomDC.AutoSize = true;
      this.checkBox_RandomDC.Location = new Point(18, 173);
      this.checkBox_RandomDC.Name = "checkBox_RandomDC";
      this.checkBox_RandomDC.Size = new Size(181, 17);
      this.checkBox_RandomDC.TabIndex = 15;
      this.checkBox_RandomDC.Text = "Limitte Rastgele DC (JobID'sizler)";
      this.checkBox_RandomDC.UseVisualStyleBackColor = true;
      this.checkBox_LimitCapac.AutoSize = true;
      this.checkBox_LimitCapac.Location = new Point(18, (int) sbyte.MaxValue);
      this.checkBox_LimitCapac.Name = "checkBox_LimitCapac";
      this.checkBox_LimitCapac.Size = new Size(136, 17);
      this.checkBox_LimitCapac.TabIndex = 14;
      this.checkBox_LimitCapac.Text = "Kapasite Fazlasını Alma";
      this.checkBox_LimitCapac.UseVisualStyleBackColor = true;
      this.label19.Location = new Point(78, 81);
      this.label19.Name = "label19";
      this.label19.Size = new Size(58, 23);
      this.label19.TabIndex = 13;
      this.label19.Text = "Kapasite:";
      this.label19.TextAlign = ContentAlignment.MiddleRight;
      this.checkBox_AddOnline.AutoSize = true;
      this.checkBox_AddOnline.Location = new Point(18, 10);
      this.checkBox_AddOnline.Name = "checkBox_AddOnline";
      this.checkBox_AddOnline.Size = new Size(211, 17);
      this.checkBox_AddOnline.TabIndex = 11;
      this.checkBox_AddOnline.Tag = (object) "";
      this.checkBox_AddOnline.Text = "Online Sistemini KGUARD Kontrol Etsin";
      this.checkBox_AddOnline.UseVisualStyleBackColor = true;
      this.textBox_OnlineCap.Location = new Point(142, 83);
      this.textBox_OnlineCap.MaxLength = 4;
      this.textBox_OnlineCap.Name = "textBox_OnlineCap";
      this.textBox_OnlineCap.Size = new Size(73, 20);
      this.textBox_OnlineCap.TabIndex = 12;
      this.textBox_OnlineCap.TextAlign = HorizontalAlignment.Center;
      this.textBox_OnlineCount.Location = new Point(142, 57);
      this.textBox_OnlineCount.MaxLength = 4;
      this.textBox_OnlineCount.Name = "textBox_OnlineCount";
      this.textBox_OnlineCount.Size = new Size(73, 20);
      this.textBox_OnlineCount.TabIndex = 0;
      this.textBox_OnlineCount.TextAlign = HorizontalAlignment.Center;
      this.label18.Location = new Point(23, 55);
      this.label18.Name = "label18";
      this.label18.Size = new Size(113, 23);
      this.label18.TabIndex = 10;
      this.label18.Text = "Ekleme yada Çarpan";
      this.label18.TextAlign = ContentAlignment.MiddleRight;
      this.groupBox17.Controls.Add((Control) this.label31);
      this.groupBox17.Controls.Add((Control) this.label30);
      this.groupBox17.Controls.Add((Control) this.label29);
      this.groupBox17.Controls.Add((Control) this.label28);
      this.groupBox17.Controls.Add((Control) this.textBox_SecondAgentFakePort);
      this.groupBox17.Controls.Add((Control) this.textBox_SecondAgentPort);
      this.groupBox17.Controls.Add((Control) this.textBox_SecondAgentIP);
      this.groupBox17.Controls.Add((Control) this.checkBox_MultiAgent);
      this.groupBox17.Location = new Point(443, 6);
      this.groupBox17.Name = "groupBox17";
      this.groupBox17.Size = new Size(270, 355);
      this.groupBox17.TabIndex = 5;
      this.groupBox17.TabStop = false;
      this.groupBox17.Text = "Çift Agent";
      this.label31.Location = new Point(6, 167);
      this.label31.Name = "label31";
      this.label31.Size = new Size(242, 174);
      this.label31.TabIndex = 7;
      this.label31.Text = componentResourceManager.GetString("label31.Text");
      this.label31.TextAlign = ContentAlignment.MiddleCenter;
      this.label30.Location = new Point(6, 125);
      this.label30.Name = "label30";
      this.label30.Size = new Size(163, 23);
      this.label30.TabIndex = 6;
      this.label30.Text = "İkinci Agent Sahte Port:";
      this.label30.TextAlign = ContentAlignment.MiddleRight;
      this.label29.Location = new Point(6, 99);
      this.label29.Name = "label29";
      this.label29.Size = new Size(163, 23);
      this.label29.TabIndex = 5;
      this.label29.Text = "İkinci Agent Gerçek Port:";
      this.label29.TextAlign = ContentAlignment.MiddleRight;
      this.label28.Location = new Point(6, 73);
      this.label28.Name = "label28";
      this.label28.Size = new Size(92, 23);
      this.label28.TabIndex = 4;
      this.label28.Text = "İkinci Agent IP:";
      this.label28.TextAlign = ContentAlignment.MiddleRight;
      this.textBox_SecondAgentFakePort.Location = new Point(175, (int) sbyte.MaxValue);
      this.textBox_SecondAgentFakePort.Name = "textBox_SecondAgentFakePort";
      this.textBox_SecondAgentFakePort.Size = new Size(63, 20);
      this.textBox_SecondAgentFakePort.TabIndex = 3;
      this.textBox_SecondAgentFakePort.TextChanged += new EventHandler(this.textBox_SecondAgentFakePort_TextChanged);
      this.textBox_SecondAgentPort.Location = new Point(175, 101);
      this.textBox_SecondAgentPort.Name = "textBox_SecondAgentPort";
      this.textBox_SecondAgentPort.Size = new Size(63, 20);
      this.textBox_SecondAgentPort.TabIndex = 2;
      this.textBox_SecondAgentPort.TextChanged += new EventHandler(this.textBox_SecondAgentPort_TextChanged);
      this.textBox_SecondAgentIP.Location = new Point(104, 75);
      this.textBox_SecondAgentIP.Name = "textBox_SecondAgentIP";
      this.textBox_SecondAgentIP.Size = new Size(134, 20);
      this.textBox_SecondAgentIP.TabIndex = 1;
      this.textBox_SecondAgentIP.TextChanged += new EventHandler(this.textBox_SecondAgentIP_TextChanged);
      this.checkBox_MultiAgent.AutoSize = true;
      this.checkBox_MultiAgent.Location = new Point(16, 34);
      this.checkBox_MultiAgent.Name = "checkBox_MultiAgent";
      this.checkBox_MultiAgent.Size = new Size(111, 17);
      this.checkBox_MultiAgent.TabIndex = 0;
      this.checkBox_MultiAgent.Text = "Çift Agent Desteği";
      this.checkBox_MultiAgent.UseVisualStyleBackColor = true;
      this.checkBox_MultiAgent.CheckedChanged += new EventHandler(this.checkBox_MultiAgent_CheckedChanged);
      this.tabPage1.BackColor = Color.White;
      this.tabPage1.Controls.Add((Control) this.groupBox1);
      this.tabPage1.Controls.Add((Control) this.groupBox9);
      this.tabPage1.Controls.Add((Control) this.groupBox6);
      this.tabPage1.Controls.Add((Control) this.groupBox8);
      this.tabPage1.Controls.Add((Control) this.groupBox4);
      this.tabPage1.Location = new Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(1016, 367);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Server";
      this.groupBox1.Controls.Add((Control) this.label4);
      this.groupBox1.Controls.Add((Control) this.label8);
      this.groupBox1.Controls.Add((Control) this.label_LisansUser);
      this.groupBox1.Controls.Add((Control) this.label_LisansIP);
      this.groupBox1.Location = new Point(742, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(268, 355);
      this.groupBox1.TabIndex = 17;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Lisans";
      this.label4.Location = new Point(6, 16);
      this.label4.Name = "label4";
      this.label4.Size = new Size(83, 20);
      this.label4.TabIndex = 7;
      this.label4.Text = "Lisans IP :";
      this.label4.TextAlign = ContentAlignment.MiddleRight;
      this.label8.Location = new Point(6, 36);
      this.label8.Name = "label8";
      this.label8.Size = new Size(83, 20);
      this.label8.TabIndex = 8;
      this.label8.Text = "Lisans User :";
      this.label8.TextAlign = ContentAlignment.MiddleRight;
      this.label_LisansUser.Location = new Point(95, 36);
      this.label_LisansUser.Name = "label_LisansUser";
      this.label_LisansUser.Size = new Size(205, 20);
      this.label_LisansUser.TabIndex = 13;
      this.label_LisansUser.Text = "0";
      this.label_LisansUser.TextAlign = ContentAlignment.MiddleLeft;
      this.label_LisansIP.Location = new Point(95, 16);
      this.label_LisansIP.Name = "label_LisansIP";
      this.label_LisansIP.Size = new Size(205, 20);
      this.label_LisansIP.TabIndex = 12;
      this.label_LisansIP.Text = "0";
      this.label_LisansIP.TextAlign = ContentAlignment.MiddleLeft;
      this.groupBox9.Controls.Add((Control) this.button_AddCustomIP);
      this.groupBox9.Controls.Add((Control) this.label24);
      this.groupBox9.Controls.Add((Control) this.textBox_CustomLimitValue);
      this.groupBox9.Controls.Add((Control) this.label22);
      this.groupBox9.Controls.Add((Control) this.listView_CustomLimit);
      this.groupBox9.Controls.Add((Control) this.textBox_CustomLimitIP);
      this.groupBox9.Location = new Point(223, 63);
      this.groupBox9.Name = "groupBox9";
      this.groupBox9.Size = new Size(247, 298);
      this.groupBox9.TabIndex = 4;
      this.groupBox9.TabStop = false;
      this.groupBox9.Text = "Özel IP Limit";
      this.button_AddCustomIP.Location = new Point((int) sbyte.MaxValue, 261);
      this.button_AddCustomIP.Name = "button_AddCustomIP";
      this.button_AddCustomIP.Size = new Size(75, 23);
      this.button_AddCustomIP.TabIndex = 5;
      this.button_AddCustomIP.Text = "Ekle";
      this.button_AddCustomIP.UseVisualStyleBackColor = true;
      this.button_AddCustomIP.Click += new EventHandler(this.button_AddCustomIP_Click);
      this.label24.Location = new Point(6, 264);
      this.label24.Name = "label24";
      this.label24.Size = new Size(56, 19);
      this.label24.TabIndex = 8;
      this.label24.Text = "Çarpan :";
      this.label24.TextAlign = ContentAlignment.MiddleRight;
      this.textBox_CustomLimitValue.Location = new Point(68, 263);
      this.textBox_CustomLimitValue.MaxLength = 2;
      this.textBox_CustomLimitValue.Name = "textBox_CustomLimitValue";
      this.textBox_CustomLimitValue.Size = new Size(52, 20);
      this.textBox_CustomLimitValue.TabIndex = 7;
      this.textBox_CustomLimitValue.TextAlign = HorizontalAlignment.Center;
      this.label22.Location = new Point(25, 238);
      this.label22.Name = "label22";
      this.label22.Size = new Size(37, 19);
      this.label22.TabIndex = 6;
      this.label22.Text = "IP :";
      this.label22.TextAlign = ContentAlignment.MiddleRight;
      this.listView_CustomLimit.Columns.AddRange(new ColumnHeader[2]
      {
        this.columnHeader3,
        this.columnHeader4
      });
      this.listView_CustomLimit.FullRowSelect = true;
      this.listView_CustomLimit.Location = new Point(6, 19);
      this.listView_CustomLimit.MultiSelect = false;
      this.listView_CustomLimit.Name = "listView_CustomLimit";
      this.listView_CustomLimit.Size = new Size(235, 212);
      this.listView_CustomLimit.TabIndex = 3;
      this.listView_CustomLimit.UseCompatibleStateImageBehavior = false;
      this.listView_CustomLimit.View = View.Details;
      this.listView_CustomLimit.MouseClick += new MouseEventHandler(this.listView2_MouseClick);
      this.columnHeader3.Text = "IP";
      this.columnHeader3.Width = 148;
      this.columnHeader4.Text = "Limit Çarpanı";
      this.columnHeader4.Width = 73;
      this.textBox_CustomLimitIP.Location = new Point(68, 237);
      this.textBox_CustomLimitIP.MaxLength = 15;
      this.textBox_CustomLimitIP.Name = "textBox_CustomLimitIP";
      this.textBox_CustomLimitIP.Size = new Size(134, 20);
      this.textBox_CustomLimitIP.TabIndex = 5;
      this.textBox_CustomLimitIP.TextAlign = HorizontalAlignment.Center;
      this.groupBox6.Controls.Add((Control) this.button_YetkiliIPSave);
      this.groupBox6.Controls.Add((Control) this.button_YetkiliIPEdit);
      this.groupBox6.Controls.Add((Control) this.panel_YetkiliIP);
      this.groupBox6.Location = new Point(476, 6);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new Size(260, 355);
      this.groupBox6.TabIndex = 13;
      this.groupBox6.TabStop = false;
      this.groupBox6.Text = "Yetkili IP";
      this.button_YetkiliIPSave.Location = new Point(98, 326);
      this.button_YetkiliIPSave.Name = "button_YetkiliIPSave";
      this.button_YetkiliIPSave.Size = new Size(75, 23);
      this.button_YetkiliIPSave.TabIndex = 15;
      this.button_YetkiliIPSave.Text = "Kaydet";
      this.button_YetkiliIPSave.UseVisualStyleBackColor = true;
      this.button_YetkiliIPSave.Visible = false;
      this.button_YetkiliIPSave.Click += new EventHandler(this.button_YetkiliIPSave_Click);
      this.button_YetkiliIPEdit.Location = new Point(179, 326);
      this.button_YetkiliIPEdit.Name = "button_YetkiliIPEdit";
      this.button_YetkiliIPEdit.Size = new Size(75, 23);
      this.button_YetkiliIPEdit.TabIndex = 14;
      this.button_YetkiliIPEdit.Text = "Düzenle";
      this.button_YetkiliIPEdit.UseVisualStyleBackColor = true;
      this.button_YetkiliIPEdit.Click += new EventHandler(this.button_YetkiliIPEdit_Click);
      this.panel_YetkiliIP.Controls.Add((Control) this.button_RemoveIP);
      this.panel_YetkiliIP.Controls.Add((Control) this.textBox_YetkiliIP);
      this.panel_YetkiliIP.Controls.Add((Control) this.button_AddIP);
      this.panel_YetkiliIP.Controls.Add((Control) this.listBox_YetkiliIP);
      this.panel_YetkiliIP.Controls.Add((Control) this.checkBox_GMIPKontrol);
      this.panel_YetkiliIP.Enabled = false;
      this.panel_YetkiliIP.Location = new Point(6, 19);
      this.panel_YetkiliIP.Name = "panel_YetkiliIP";
      this.panel_YetkiliIP.Size = new Size(248, 301);
      this.panel_YetkiliIP.TabIndex = 13;
      this.button_RemoveIP.Enabled = false;
      this.button_RemoveIP.Location = new Point(138, 104);
      this.button_RemoveIP.Name = "button_RemoveIP";
      this.button_RemoveIP.Size = new Size(107, 23);
      this.button_RemoveIP.TabIndex = 24;
      this.button_RemoveIP.Text = "Kaldır";
      this.button_RemoveIP.UseVisualStyleBackColor = true;
      this.button_RemoveIP.Click += new EventHandler(this.button_RemoveIP_Click);
      this.textBox_YetkiliIP.Location = new Point(138, 49);
      this.textBox_YetkiliIP.MaxLength = 15;
      this.textBox_YetkiliIP.Name = "textBox_YetkiliIP";
      this.textBox_YetkiliIP.Size = new Size(107, 20);
      this.textBox_YetkiliIP.TabIndex = 14;
      this.textBox_YetkiliIP.TextAlign = HorizontalAlignment.Center;
      this.button_AddIP.Location = new Point(138, 75);
      this.button_AddIP.Name = "button_AddIP";
      this.button_AddIP.Size = new Size(107, 23);
      this.button_AddIP.TabIndex = 23;
      this.button_AddIP.Text = "Ekle";
      this.button_AddIP.UseVisualStyleBackColor = true;
      this.button_AddIP.Click += new EventHandler(this.button_AddIP_Click);
      this.listBox_YetkiliIP.FormattingEnabled = true;
      this.listBox_YetkiliIP.Location = new Point(12, 49);
      this.listBox_YetkiliIP.Name = "listBox_YetkiliIP";
      this.listBox_YetkiliIP.Size = new Size(120, 238);
      this.listBox_YetkiliIP.TabIndex = 13;
      this.listBox_YetkiliIP.SelectedIndexChanged += new EventHandler(this.listBox_YetkiliIP_SelectedIndexChanged);
      this.checkBox_GMIPKontrol.AutoSize = true;
      this.checkBox_GMIPKontrol.Location = new Point(12, 10);
      this.checkBox_GMIPKontrol.Name = "checkBox_GMIPKontrol";
      this.checkBox_GMIPKontrol.Size = new Size(176, 17);
      this.checkBox_GMIPKontrol.TabIndex = 12;
      this.checkBox_GMIPKontrol.Tag = (object) "";
      this.checkBox_GMIPKontrol.Text = "GM Girişleri Sadece Yetkili IP'ler";
      this.checkBox_GMIPKontrol.UseVisualStyleBackColor = true;
      this.groupBox8.Controls.Add((Control) this.label21);
      this.groupBox8.Controls.Add((Control) this.textBox_IPLimit);
      this.groupBox8.Controls.Add((Control) this.checkBox_IPLimit);
      this.groupBox8.Location = new Point(223, 6);
      this.groupBox8.Name = "groupBox8";
      this.groupBox8.Size = new Size(247, 51);
      this.groupBox8.TabIndex = 2;
      this.groupBox8.TabStop = false;
      this.groupBox8.Text = "IP Limit";
      this.label21.Location = new Point(87, 17);
      this.label21.Name = "label21";
      this.label21.Size = new Size(71, 19);
      this.label21.TabIndex = 3;
      this.label21.Text = "Değer :";
      this.label21.TextAlign = ContentAlignment.MiddleRight;
      this.textBox_IPLimit.Location = new Point(164, 17);
      this.textBox_IPLimit.MaxLength = 2;
      this.textBox_IPLimit.Name = "textBox_IPLimit";
      this.textBox_IPLimit.Size = new Size(38, 20);
      this.textBox_IPLimit.TabIndex = 1;
      this.textBox_IPLimit.TextAlign = HorizontalAlignment.Center;
      this.textBox_IPLimit.TextChanged += new EventHandler(this.textBox_IPLimit_TextChanged);
      this.checkBox_IPLimit.AutoSize = true;
      this.checkBox_IPLimit.Location = new Point(21, 19);
      this.checkBox_IPLimit.Name = "checkBox_IPLimit";
      this.checkBox_IPLimit.Size = new Size(60, 17);
      this.checkBox_IPLimit.TabIndex = 0;
      this.checkBox_IPLimit.Text = "IP Limit";
      this.checkBox_IPLimit.UseVisualStyleBackColor = true;
      this.checkBox_IPLimit.CheckedChanged += new EventHandler(this.checkBox_IPLimit_CheckedChanged);
      this.groupBox4.Controls.Add((Control) this.groupBox_StartServer);
      this.groupBox4.Controls.Add((Control) this.groupBox5);
      this.groupBox4.Controls.Add((Control) this.button_OpenHelper);
      this.groupBox4.Location = new Point(6, 6);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new Size(211, 355);
      this.groupBox4.TabIndex = 0;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Server";
      this.groupBox_StartServer.Controls.Add((Control) this.label15);
      this.groupBox_StartServer.Controls.Add((Control) this.button_StartServer);
      this.groupBox_StartServer.Controls.Add((Control) this.comboBox_Service);
      this.groupBox_StartServer.Location = new Point(6, 240);
      this.groupBox_StartServer.Name = "groupBox_StartServer";
      this.groupBox_StartServer.Size = new Size(199, 109);
      this.groupBox_StartServer.TabIndex = 3;
      this.groupBox_StartServer.TabStop = false;
      this.groupBox_StartServer.Text = "Start";
      this.label15.Location = new Point(6, 43);
      this.label15.Name = "label15";
      this.label15.Size = new Size(187, 29);
      this.label15.TabIndex = 7;
      this.label15.Text = "Değiştirdiğiniz taktirde otomatik aktif olacaktır.";
      this.button_StartServer.Location = new Point(6, 75);
      this.button_StartServer.Name = "button_StartServer";
      this.button_StartServer.Size = new Size(187, 29);
      this.button_StartServer.TabIndex = 1;
      this.button_StartServer.Text = "Sistemi Başlat";
      this.button_StartServer.UseVisualStyleBackColor = true;
      this.button_StartServer.Visible = false;
      this.button_StartServer.Click += new EventHandler(this.button_StartServer_Click);
      this.comboBox_Service.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox_Service.FormattingEnabled = true;
      this.comboBox_Service.Items.AddRange(new object[2]
      {
        (object) "Herkes Girebilir",
        (object) "Sadece Yetkili IP"
      });
      this.comboBox_Service.Location = new Point(6, 19);
      this.comboBox_Service.Name = "comboBox_Service";
      this.comboBox_Service.Size = new Size(187, 21);
      this.comboBox_Service.TabIndex = 1;
      this.comboBox_Service.SelectedIndexChanged += new EventHandler(this.comboBox_Service_SelectedIndexChanged);
      this.groupBox5.Controls.Add((Control) this.button_PortSave);
      this.groupBox5.Controls.Add((Control) this.button_PortEdit);
      this.groupBox5.Controls.Add((Control) this.panel_PortSetting);
      this.groupBox5.Location = new Point(6, 51);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new Size(199, 183);
      this.groupBox5.TabIndex = 1;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Port";
      this.button_PortSave.Location = new Point(28, 154);
      this.button_PortSave.Name = "button_PortSave";
      this.button_PortSave.Size = new Size(75, 23);
      this.button_PortSave.TabIndex = 2;
      this.button_PortSave.Text = "Kaydet";
      this.button_PortSave.UseVisualStyleBackColor = true;
      this.button_PortSave.Visible = false;
      this.button_PortSave.Click += new EventHandler(this.Button_PortSave_Click);
      this.button_PortEdit.Location = new Point(109, 154);
      this.button_PortEdit.Name = "button_PortEdit";
      this.button_PortEdit.Size = new Size(75, 23);
      this.button_PortEdit.TabIndex = 1;
      this.button_PortEdit.Text = "Düzenle";
      this.button_PortEdit.UseVisualStyleBackColor = true;
      this.button_PortEdit.Click += new EventHandler(this.button_PortEdit_Click);
      this.panel_PortSetting.Controls.Add((Control) this.textBox_ServIP);
      this.panel_PortSetting.Controls.Add((Control) this.label16);
      this.panel_PortSetting.Controls.Add((Control) this.label14);
      this.panel_PortSetting.Controls.Add((Control) this.textBox_Port1);
      this.panel_PortSetting.Controls.Add((Control) this.textBox_Port3);
      this.panel_PortSetting.Controls.Add((Control) this.textBox_Port4);
      this.panel_PortSetting.Controls.Add((Control) this.label12);
      this.panel_PortSetting.Controls.Add((Control) this.label11);
      this.panel_PortSetting.Controls.Add((Control) this.label13);
      this.panel_PortSetting.Controls.Add((Control) this.textBox_Port2);
      this.panel_PortSetting.Enabled = false;
      this.panel_PortSetting.Location = new Point(6, 15);
      this.panel_PortSetting.Name = "panel_PortSetting";
      this.panel_PortSetting.Size = new Size(187, 133);
      this.panel_PortSetting.TabIndex = 1;
      this.textBox_ServIP.Location = new Point(61, 3);
      this.textBox_ServIP.MaxLength = 15;
      this.textBox_ServIP.Name = "textBox_ServIP";
      this.textBox_ServIP.Size = new Size(117, 20);
      this.textBox_ServIP.TabIndex = 8;
      this.textBox_ServIP.TextAlign = HorizontalAlignment.Center;
      this.textBox_ServIP.Leave += new EventHandler(this.textBox_ServIP_Leave);
      this.label16.Location = new Point(0, 1);
      this.label16.Name = "label16";
      this.label16.Size = new Size(55, 23);
      this.label16.TabIndex = 9;
      this.label16.Text = "Server IP:";
      this.label16.TextAlign = ContentAlignment.MiddleRight;
      this.label14.Location = new Point(8, 110);
      this.label14.Name = "label14";
      this.label14.Size = new Size(92, 13);
      this.label14.TabIndex = 6;
      this.label14.Text = "Local Agent:";
      this.label14.TextAlign = ContentAlignment.MiddleRight;
      this.textBox_Port1.Location = new Point(106, 29);
      this.textBox_Port1.MaxLength = 5;
      this.textBox_Port1.Name = "textBox_Port1";
      this.textBox_Port1.Size = new Size(72, 20);
      this.textBox_Port1.TabIndex = 1;
      this.textBox_Port3.Location = new Point(106, 81);
      this.textBox_Port3.MaxLength = 5;
      this.textBox_Port3.Name = "textBox_Port3";
      this.textBox_Port3.Size = new Size(72, 20);
      this.textBox_Port3.TabIndex = 3;
      this.textBox_Port4.Location = new Point(106, 107);
      this.textBox_Port4.MaxLength = 5;
      this.textBox_Port4.Name = "textBox_Port4";
      this.textBox_Port4.Size = new Size(72, 20);
      this.textBox_Port4.TabIndex = 7;
      this.label12.Location = new Point(8, 84);
      this.label12.Name = "label12";
      this.label12.Size = new Size(92, 13);
      this.label12.TabIndex = 2;
      this.label12.Text = "Local Gateway:";
      this.label12.TextAlign = ContentAlignment.MiddleRight;
      this.label11.Location = new Point(8, 27);
      this.label11.Name = "label11";
      this.label11.Size = new Size(92, 23);
      this.label11.TabIndex = 1;
      this.label11.Text = "Public Gateway:";
      this.label11.TextAlign = ContentAlignment.MiddleRight;
      this.label13.Location = new Point(8, 58);
      this.label13.Name = "label13";
      this.label13.Size = new Size(92, 13);
      this.label13.TabIndex = 4;
      this.label13.Text = "Public Agent:";
      this.label13.TextAlign = ContentAlignment.MiddleRight;
      this.textBox_Port2.Location = new Point(106, 55);
      this.textBox_Port2.MaxLength = 5;
      this.textBox_Port2.Name = "textBox_Port2";
      this.textBox_Port2.Size = new Size(72, 20);
      this.textBox_Port2.TabIndex = 5;
      this.button_OpenHelper.Location = new Point(6, 19);
      this.button_OpenHelper.Name = "button_OpenHelper";
      this.button_OpenHelper.Size = new Size(199, 26);
      this.button_OpenHelper.TabIndex = 2;
      this.button_OpenHelper.Text = "Kurulum Yardımcısı";
      this.button_OpenHelper.UseVisualStyleBackColor = true;
      this.button_OpenHelper.Click += new EventHandler(this.Button_OpenHelper_Click);
      this.tabControl1.Controls.Add((Control) this.tabPage1);
      this.tabControl1.Controls.Add((Control) this.tabPage2);
      this.tabControl1.Controls.Add((Control) this.tabPage4);
      this.tabControl1.Controls.Add((Control) this.tabPage5);
      this.tabControl1.Controls.Add((Control) this.tabPage3);
      this.tabControl1.Enabled = false;
      this.tabControl1.Location = new Point(12, 12);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(1024, 393);
      this.tabControl1.TabIndex = 16;
      this.tabPage3.Controls.Add((Control) this.groupBox22);
      this.tabPage3.Controls.Add((Control) this.groupBox19);
      this.tabPage3.Controls.Add((Control) this.groupBox18);
      this.tabPage3.Location = new Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new Padding(3);
      this.tabPage3.Size = new Size(1016, 367);
      this.tabPage3.TabIndex = 5;
      this.tabPage3.Text = "IP - Karakter - Ban Yönetimi";
      this.tabPage3.UseVisualStyleBackColor = true;
      this.groupBox22.Controls.Add((Control) this.button_BanHistory_Firewall);
      this.groupBox22.Controls.Add((Control) this.button_BanHistory_Login);
      this.groupBox22.Controls.Add((Control) this.listView_BanHistory);
      this.groupBox22.Location = new Point(650, 6);
      this.groupBox22.Name = "groupBox22";
      this.groupBox22.Size = new Size(360, 355);
      this.groupBox22.TabIndex = 2;
      this.groupBox22.TabStop = false;
      this.groupBox22.Text = "Ban Durumları";
      this.button_BanHistory_Firewall.Location = new Point(184, 19);
      this.button_BanHistory_Firewall.Name = "button_BanHistory_Firewall";
      this.button_BanHistory_Firewall.Size = new Size(163, 38);
      this.button_BanHistory_Firewall.TabIndex = 6;
      this.button_BanHistory_Firewall.Text = "IP Banlar";
      this.button_BanHistory_Firewall.UseVisualStyleBackColor = true;
      this.button_BanHistory_Firewall.Click += new EventHandler(this.button_BanHistory_Firewall_Click);
      this.button_BanHistory_Login.Location = new Point(15, 19);
      this.button_BanHistory_Login.Name = "button_BanHistory_Login";
      this.button_BanHistory_Login.Size = new Size(163, 38);
      this.button_BanHistory_Login.TabIndex = 5;
      this.button_BanHistory_Login.Text = "Login Banlar";
      this.button_BanHistory_Login.UseVisualStyleBackColor = true;
      this.button_BanHistory_Login.Click += new EventHandler(this.Button_BanHistory_Login_Click);
      this.listView_BanHistory.Columns.AddRange(new ColumnHeader[3]
      {
        this.Ban_ColHeader1,
        this.Ban_ColHeader2,
        this.Ban_ColHeader3
      });
      this.listView_BanHistory.FullRowSelect = true;
      this.listView_BanHistory.Location = new Point(15, 60);
      this.listView_BanHistory.Margin = new Padding(0);
      this.listView_BanHistory.MultiSelect = false;
      this.listView_BanHistory.Name = "listView_BanHistory";
      this.listView_BanHistory.Size = new Size(332, 283);
      this.listView_BanHistory.TabIndex = 2;
      this.listView_BanHistory.UseCompatibleStateImageBehavior = false;
      this.listView_BanHistory.View = View.Details;
      this.listView_BanHistory.MouseClick += new MouseEventHandler(this.listView_BanHistory_MouseClick);
      this.Ban_ColHeader1.Text = "Karakter Adı";
      this.Ban_ColHeader1.Width = (int) sbyte.MaxValue;
      this.Ban_ColHeader2.Text = "Kalan Gün";
      this.Ban_ColHeader2.TextAlign = HorizontalAlignment.Center;
      this.Ban_ColHeader2.Width = 78;
      this.Ban_ColHeader3.Text = "Eklenme Tarihi";
      this.Ban_ColHeader3.Width = 119;
      this.groupBox19.Controls.Add((Control) this.groupBox20);
      this.groupBox19.Controls.Add((Control) this.groupBox21);
      this.groupBox19.Location = new Point(400, 6);
      this.groupBox19.Name = "groupBox19";
      this.groupBox19.Size = new Size(244, 355);
      this.groupBox19.TabIndex = 1;
      this.groupBox19.TabStop = false;
      this.groupBox19.Text = "Ban";
      this.groupBox20.Controls.Add((Control) this.button_LoginBan);
      this.groupBox20.Controls.Add((Control) this.richTextBox_Ban_Guide);
      this.groupBox20.Controls.Add((Control) this.textBox_LoginBan_DAY);
      this.groupBox20.Controls.Add((Control) this.textBox_LoginBan_CN);
      this.groupBox20.Location = new Point(6, 19);
      this.groupBox20.Name = "groupBox20";
      this.groupBox20.Size = new Size(232, 202);
      this.groupBox20.TabIndex = 2;
      this.groupBox20.TabStop = false;
      this.groupBox20.Text = "Login Ban";
      this.button_LoginBan.Location = new Point(6, 124);
      this.button_LoginBan.Name = "button_LoginBan";
      this.button_LoginBan.Size = new Size(220, 37);
      this.button_LoginBan.TabIndex = 4;
      this.button_LoginBan.Text = "Banla";
      this.button_LoginBan.UseVisualStyleBackColor = true;
      this.button_LoginBan.Click += new EventHandler(this.Button_LoginBan_Click);
      this.richTextBox_Ban_Guide.Location = new Point(6, 60);
      this.richTextBox_Ban_Guide.Name = "richTextBox_Ban_Guide";
      this.richTextBox_Ban_Guide.Size = new Size(220, 58);
      this.richTextBox_Ban_Guide.TabIndex = 3;
      this.richTextBox_Ban_Guide.Text = "";
      this.textBox_LoginBan_DAY.Location = new Point(165, 34);
      this.textBox_LoginBan_DAY.MaxLength = 3;
      this.textBox_LoginBan_DAY.Name = "textBox_LoginBan_DAY";
      this.textBox_LoginBan_DAY.Size = new Size(61, 20);
      this.textBox_LoginBan_DAY.TabIndex = 2;
      this.textBox_LoginBan_DAY.TextAlign = HorizontalAlignment.Center;
      this.textBox_LoginBan_CN.Location = new Point(6, 34);
      this.textBox_LoginBan_CN.MaxLength = 32;
      this.textBox_LoginBan_CN.Name = "textBox_LoginBan_CN";
      this.textBox_LoginBan_CN.Size = new Size(153, 20);
      this.textBox_LoginBan_CN.TabIndex = 1;
      this.textBox_LoginBan_CN.TextAlign = HorizontalAlignment.Center;
      this.groupBox21.Controls.Add((Control) this.button_FirewallBan);
      this.groupBox21.Controls.Add((Control) this.textBox_IPBlock_IP);
      this.groupBox21.Location = new Point(6, 227);
      this.groupBox21.Name = "groupBox21";
      this.groupBox21.Size = new Size(232, 122);
      this.groupBox21.TabIndex = 3;
      this.groupBox21.TabStop = false;
      this.groupBox21.Text = "IP Block";
      this.button_FirewallBan.Location = new Point(6, 54);
      this.button_FirewallBan.Name = "button_FirewallBan";
      this.button_FirewallBan.Size = new Size(220, 37);
      this.button_FirewallBan.TabIndex = 6;
      this.button_FirewallBan.Text = "Firewall Block At";
      this.button_FirewallBan.UseVisualStyleBackColor = true;
      this.button_FirewallBan.Click += new EventHandler(this.Button_FirewallBan_Click);
      this.textBox_IPBlock_IP.Location = new Point(6, 28);
      this.textBox_IPBlock_IP.MaxLength = 32;
      this.textBox_IPBlock_IP.Name = "textBox_IPBlock_IP";
      this.textBox_IPBlock_IP.Size = new Size(220, 20);
      this.textBox_IPBlock_IP.TabIndex = 5;
      this.textBox_IPBlock_IP.TextAlign = HorizontalAlignment.Center;
      this.groupBox18.Controls.Add((Control) this.listView_IPC);
      this.groupBox18.Controls.Add((Control) this.textBox_IPC_IP);
      this.groupBox18.Controls.Add((Control) this.textBox_IPC_CharName);
      this.groupBox18.Location = new Point(6, 6);
      this.groupBox18.Name = "groupBox18";
      this.groupBox18.Size = new Size(388, 355);
      this.groupBox18.TabIndex = 0;
      this.groupBox18.TabStop = false;
      this.groupBox18.Text = "IP - Karakter Yönetimi";
      this.listView_IPC.Columns.AddRange(new ColumnHeader[3]
      {
        this.columnHeader5,
        this.columnHeader6,
        this.columnHeader7
      });
      this.listView_IPC.FullRowSelect = true;
      this.listView_IPC.Location = new Point(17, 45);
      this.listView_IPC.Margin = new Padding(0);
      this.listView_IPC.MultiSelect = false;
      this.listView_IPC.Name = "listView_IPC";
      this.listView_IPC.Size = new Size(352, 298);
      this.listView_IPC.TabIndex = 1;
      this.listView_IPC.UseCompatibleStateImageBehavior = false;
      this.listView_IPC.View = View.Details;
      this.listView_IPC.MouseClick += new MouseEventHandler(this.listView_IPC_MouseClick);
      this.columnHeader5.Text = "Karakter Adı";
      this.columnHeader5.Width = 119;
      this.columnHeader6.Text = "Level";
      this.columnHeader6.TextAlign = HorizontalAlignment.Center;
      this.columnHeader6.Width = 51;
      this.columnHeader7.Text = "IP Adresi";
      this.columnHeader7.TextAlign = HorizontalAlignment.Center;
      this.columnHeader7.Width = 178;
      this.textBox_IPC_IP.Location = new Point(196, 19);
      this.textBox_IPC_IP.MaxLength = 15;
      this.textBox_IPC_IP.Name = "textBox_IPC_IP";
      this.textBox_IPC_IP.Size = new Size(173, 20);
      this.textBox_IPC_IP.TabIndex = 1;
      this.textBox_IPC_IP.TextAlign = HorizontalAlignment.Center;
      this.textBox_IPC_IP.KeyPress += new KeyPressEventHandler(this.textBox_IPC_IP_KeyPress);
      this.textBox_IPC_CharName.Location = new Point(17, 19);
      this.textBox_IPC_CharName.MaxLength = 32;
      this.textBox_IPC_CharName.Name = "textBox_IPC_CharName";
      this.textBox_IPC_CharName.Size = new Size(173, 20);
      this.textBox_IPC_CharName.TabIndex = 0;
      this.textBox_IPC_CharName.TextAlign = HorizontalAlignment.Center;
      this.textBox_IPC_CharName.KeyPress += new KeyPressEventHandler(this.textBox_IPC_CharName_KeyPress);
      this.contextMenuStrip5.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.karakteriBanlaToolStripMenuItem,
        (ToolStripItem) this.ıPBlockAtToolStripMenuItem
      });
      this.contextMenuStrip5.Name = "contextMenuStrip5";
      this.contextMenuStrip5.Size = new Size(153, 48);
      this.karakteriBanlaToolStripMenuItem.Name = "karakteriBanlaToolStripMenuItem";
      this.karakteriBanlaToolStripMenuItem.Size = new Size(152, 22);
      this.karakteriBanlaToolStripMenuItem.Text = "Karakteri Banla";
      this.karakteriBanlaToolStripMenuItem.Click += new EventHandler(this.karakteriBanlaToolStripMenuItem_Click);
      this.ıPBlockAtToolStripMenuItem.Name = "ıPBlockAtToolStripMenuItem";
      this.ıPBlockAtToolStripMenuItem.Size = new Size(152, 22);
      this.ıPBlockAtToolStripMenuItem.Text = "IP Block At";
      this.ıPBlockAtToolStripMenuItem.Click += new EventHandler(this.ıPBlockAtToolStripMenuItem_Click);
      this.contextMenuStrip6.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.kaldırToolStripMenuItem
      });
      this.contextMenuStrip6.Name = "contextMenuStrip6";
      this.contextMenuStrip6.Size = new Size(105, 26);
      this.kaldırToolStripMenuItem.Name = "kaldırToolStripMenuItem";
      this.kaldırToolStripMenuItem.Size = new Size(104, 22);
      this.kaldırToolStripMenuItem.Text = "Kaldır";
      this.kaldırToolStripMenuItem.Click += new EventHandler(this.kaldırToolStripMenuItem_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.PeachPuff;
      this.ClientSize = new Size(1043, 580);
      this.Controls.Add((Control) this.button_GetSettings);
      this.Controls.Add((Control) this.listView1);
      this.Controls.Add((Control) this.tabControl1);
      this.Controls.Add((Control) this.label_Suspect);
      this.Controls.Add((Control) this.label_AgentClient);
      this.Controls.Add((Control) this.label_LoginClient);
      this.Controls.Add((Control) this.labelMEMORY);
      this.Controls.Add((Control) this.labelThread);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.Name = nameof (MainForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "K-Guard vSRO Server Security and Management";
      this.Load += new EventHandler(this.MainForm_Load);
      this.contextMenuStrip1.ResumeLayout(false);
      this.contextMenuStrip2.ResumeLayout(false);
      this.contextMenuStrip3.ResumeLayout(false);
      this.contextMenuStrip4.ResumeLayout(false);
      this.tabPage5.ResumeLayout(false);
      this.groupBox16.ResumeLayout(false);
      this.groupBox16.PerformLayout();
      this.groupBox15.ResumeLayout(false);
      this.groupBox15.PerformLayout();
      this.groupBox14.ResumeLayout(false);
      this.groupBox14.PerformLayout();
      this.groupBox13.ResumeLayout(false);
      this.groupBox13.PerformLayout();
      this.tabPage4.ResumeLayout(false);
      this.groupBox24.ResumeLayout(false);
      this.groupBox24.PerformLayout();
      this.groupBox12.ResumeLayout(false);
      this.groupBox12.PerformLayout();
      this.groupBox10.ResumeLayout(false);
      this.groupBox10.PerformLayout();
      this.groupBox11.ResumeLayout(false);
      this.groupBox11.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.groupBox25.ResumeLayout(false);
      this.groupBox23.ResumeLayout(false);
      this.groupBox23.PerformLayout();
      this.groupBox7.ResumeLayout(false);
      this.panel_OnlineYonetim.ResumeLayout(false);
      this.panel_OnlineYonetim.PerformLayout();
      this.groupBox17.ResumeLayout(false);
      this.groupBox17.PerformLayout();
      this.tabPage1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox9.ResumeLayout(false);
      this.groupBox9.PerformLayout();
      this.groupBox6.ResumeLayout(false);
      this.panel_YetkiliIP.ResumeLayout(false);
      this.panel_YetkiliIP.PerformLayout();
      this.groupBox8.ResumeLayout(false);
      this.groupBox8.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox_StartServer.ResumeLayout(false);
      this.groupBox5.ResumeLayout(false);
      this.panel_PortSetting.ResumeLayout(false);
      this.panel_PortSetting.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.groupBox22.ResumeLayout(false);
      this.groupBox19.ResumeLayout(false);
      this.groupBox20.ResumeLayout(false);
      this.groupBox20.PerformLayout();
      this.groupBox21.ResumeLayout(false);
      this.groupBox21.PerformLayout();
      this.groupBox18.ResumeLayout(false);
      this.groupBox18.PerformLayout();
      this.contextMenuStrip5.ResumeLayout(false);
      this.contextMenuStrip6.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
