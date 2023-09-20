// Decompiled with JetBrains decompiler
// Type: KGuard.Kurulum
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KGuard
{
  public class Kurulum : Form
  {
    private const int EM_SETCUEBANNER = 5377;
    private IContainer components = (IContainer) null;
    private OpenFileDialog openFileDialog1;
    private ListBox listBox1;
    private Button button1;
    private RichTextBox richTextBox1;
    private OpenFileDialog openFileDialog2;
    private TextBox textBox1;
    private Button button3;
    private RichTextBox richTextBox2;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

    public Kurulum() => this.InitializeComponent();

    private void Kurulum_Load(object sender, EventArgs e) => Kurulum.SendMessage(this.textBox1.Handle, 5377, 0, "Clientlerdeki Port");

    private void button1_Click(object sender, EventArgs e)
    {
      int num = (int) this.openFileDialog1.ShowDialog();
      if (this.openFileDialog1.FileName.Length < 5)
        return;
      this.richTextBox1.Text = this.openFileDialog1.FileName;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      this.listBox1.Items.Clear();
      if (this.openFileDialog1.FileName.Length < 5)
        this.listBox1.Items.Add((object) "srNodeData.ini dosyasını seçin.");
      else if (this.textBox1.Text.Length < 1)
      {
        this.listBox1.Items.Add((object) "Clientlerin Login portunu girin.");
      }
      else
      {
        this.listBox1.Items.Add((object) "Dosyalar kontrol ediliyor...");
        this.EnableDisable(false);
        new Task((Action) (() => this.ReadAllData())).Start();
      }
    }

    private async void ReadAllData()
    {
      await Task.Delay(5);
      try
      {
        string[] srNodeData = File.ReadAllLines(this.openFileDialog1.FileName);
        List<string> BlockPorts = new List<string>();
        List<string> OpenPorts = new List<string>();
        string RealGateway = "33001";
        string RealAgent = "33002";
        string FakeGateway = this.textBox1.Text;
        string FakeAgent = "15884";
        string i = "";
        this.listBox1.Items.Clear();
        bool isSended = false;
        string[] strArray = srNodeData;
        for (int index = 0; index < strArray.Length; ++index)
        {
          string Node = strArray[index];
          if (Node.Contains("entry"))
            i = new Regex("\\d+").Match(Node).Value;
          else if (Node.Contains("port"))
          {
            string port = new Regex("\\d+").Match(Node).Value;
            if (i == "4")
              OpenPorts.Add(port);
            else if (i == "2")
            {
              if (this.textBox1.Text == port)
              {
                this.listBox1.Items.Add((object) "srNodeData.ini Entry2 portunu 33001 yapın.");
                this.listBox1.Items.Add((object) "srNodeData.ini Entry7 portunu 33002 yapın.");
                this.listBox1.Items.Add((object) "Bir üst klasöründeki Compile.bat'ı çalıştırın.");
                this.listBox1.Items.Add((object) "Cert de dahil tüm uygulamaları yeniden başlatın.");
                this.listBox1.Items.Add((object) " ");
                isSended = true;
              }
              else
                RealGateway = port;
            }
            else if (i == "7")
            {
              if (!isSended && port != "33002")
              {
                this.listBox1.Items.Add((object) "srNodeData.ini Entry7 portunu 33002 yapın.");
                this.listBox1.Items.Add((object) "Bir üst klasöründeki Compile.bat'ı çalıştırın.");
                this.listBox1.Items.Add((object) "Cert de dahil tüm uygulamaları yeniden başlatın.");
                this.listBox1.Items.Add((object) " ");
              }
            }
            else
              BlockPorts.Add(port);
            port = (string) null;
          }
          Node = (string) null;
        }
        strArray = (string[]) null;
        this.listBox1.Items.Add((object) ">> Açılacak portlar");
        OpenPorts.Add(FakeGateway);
        OpenPorts.Add(FakeAgent);
        BlockPorts.Add(RealGateway);
        BlockPorts.Add(RealAgent);
        foreach (string str in OpenPorts)
        {
          string port = str;
          this.listBox1.Items.Add((object) port);
          port = (string) null;
        }
        this.listBox1.Items.Add((object) " ");
        this.listBox1.Items.Add((object) ">> Kapatılacak portlar");
        foreach (string str in BlockPorts)
        {
          string port = str;
          this.listBox1.Items.Add((object) port);
          port = (string) null;
        }
        this.EnableDisable(true);
        this.listBox1.Items.Add((object) " ");
        this.listBox1.Items.Add((object) "Server.cfg'de IP Limit varsa kaldırın.");
        this.listBox1.Items.Add((object) ("Veritabanına GM IP yerine " + Program.LocalIP + " adresini girin."));
        this.listBox1.Items.Add((object) " ");
        this.listBox1.Items.Add((object) ">>>> Guard Ayarları");
        this.listBox1.Items.Add((object) ("Remote Gateway : " + FakeGateway));
        this.listBox1.Items.Add((object) ("Remote Agent : " + FakeAgent));
        this.listBox1.Items.Add((object) ("Local Gateway : " + RealGateway));
        this.listBox1.Items.Add((object) ("Local Agent : " + RealAgent));
        this.listBox1.Items.Add((object) " ");
        this.listBox1.Items.Add((object) "Tarama ve analiz tamamlandı.");
        srNodeData = (string[]) null;
        BlockPorts = (List<string>) null;
        OpenPorts = (List<string>) null;
        RealGateway = (string) null;
        RealAgent = (string) null;
        FakeGateway = (string) null;
        FakeAgent = (string) null;
        i = (string) null;
      }
      catch (Exception ex)
      {
        this.EnableDisable(true);
        this.listBox1.Items.Add((object) ">");
        this.listBox1.Items.Add((object) "Tarama ve analiz sırasında hata oluştu.");
        Program.WriteError(ex.ToString(), nameof (ReadAllData));
      }
    }

    private void EnableDisable(bool En)
    {
      if (!En)
      {
        this.button1.Enabled = false;
        this.textBox1.Enabled = false;
        this.button3.Enabled = false;
      }
      else
      {
        this.button1.Enabled = true;
        this.textBox1.Enabled = true;
        this.button3.Enabled = true;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Kurulum));
      this.openFileDialog1 = new OpenFileDialog();
      this.listBox1 = new ListBox();
      this.button1 = new Button();
      this.richTextBox1 = new RichTextBox();
      this.openFileDialog2 = new OpenFileDialog();
      this.textBox1 = new TextBox();
      this.button3 = new Button();
      this.richTextBox2 = new RichTextBox();
      this.SuspendLayout();
      this.openFileDialog1.Filter = "srNodeData (srNodeData.ini)|srNodeData.ini";
      this.openFileDialog1.Title = "srNodeData.ini dosyasını seçin.";
      this.listBox1.FormattingEnabled = true;
      this.listBox1.Location = new Point(12, 77);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new Size(473, 394);
      this.listBox1.TabIndex = 0;
      this.button1.Location = new Point(12, 12);
      this.button1.Name = "button1";
      this.button1.Size = new Size(154, 32);
      this.button1.TabIndex = 1;
      this.button1.Text = "srNodeData.ini";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.richTextBox1.BackColor = SystemColors.ActiveBorder;
      this.richTextBox1.BorderStyle = BorderStyle.None;
      this.richTextBox1.ForeColor = SystemColors.Info;
      this.richTextBox1.Location = new Point(172, 12);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.ReadOnly = true;
      this.richTextBox1.Size = new Size(153, 59);
      this.richTextBox1.TabIndex = 3;
      this.richTextBox1.Text = "";
      this.openFileDialog2.Filter = "Server.cfg (Server.cfg)|Server.cfg";
      this.openFileDialog2.Title = "Server.cfg dosyasını seçin.";
      this.textBox1.Font = new Font("Microsoft Sans Serif", 9.25f);
      this.textBox1.Location = new Point(12, 50);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(154, 21);
      this.textBox1.TabIndex = 5;
      this.textBox1.TextAlign = HorizontalAlignment.Center;
      this.button3.Location = new Point(331, 12);
      this.button3.Name = "button3";
      this.button3.Size = new Size(154, 59);
      this.button3.TabIndex = 6;
      this.button3.Text = "Tara";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.richTextBox2.BackColor = SystemColors.Info;
      this.richTextBox2.BorderStyle = BorderStyle.None;
      this.richTextBox2.Cursor = Cursors.Arrow;
      this.richTextBox2.Font = new Font("Microsoft Sans Serif", 9.25f);
      this.richTextBox2.Location = new Point(491, 12);
      this.richTextBox2.Name = "richTextBox2";
      this.richTextBox2.ReadOnly = true;
      this.richTextBox2.Size = new Size(355, 459);
      this.richTextBox2.TabIndex = 7;
      this.richTextBox2.Text = componentResourceManager.GetString("richTextBox2.Text");
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(858, 481);
      this.Controls.Add((Control) this.richTextBox2);
      this.Controls.Add((Control) this.button3);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.richTextBox1);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.listBox1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.Name = nameof (Kurulum);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Kurulum Yardımcısı";
      this.Load += new EventHandler(this.Kurulum_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
