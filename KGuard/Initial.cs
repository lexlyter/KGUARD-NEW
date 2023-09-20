// Decompiled with JetBrains decompiler
// Type: KGuard.Initial
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace KGuard
{
  public class Initial : Form
  {
    public static SqlConnection SqlCustom = new SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=True; MultipleActiveResultSets=True;");
    public static SqlConnection LocalSQL = new SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=True; MultipleActiveResultSets=True;");
    public static SqlConnection LocalSQLExpress = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True; MultipleActiveResultSets=True;");
    private IContainer components = (IContainer) null;
    private PictureBox pictureBox1;
    private ProgressBar progressBar1;

    public Initial()
    {
      this.InitializeComponent();
      Control.CheckForIllegalCrossThreadCalls = false;
    }

    private void Initial_Load(object sender, EventArgs e)
    {
      new Thread(new ThreadStart(this.PB)).Start();
      new Thread(new ThreadStart(this.Connect)).Start();
    }

    private void PB()
    {
      for (; this.progressBar1.Value < 100; ++this.progressBar1.Value)
        Thread.Sleep(15);
    }

    private void Connect()
    {
      while (true)
      {
        Program.CheckHst();
        bool flag = false;
        try
        {
          FileStream fileStream = new FileStream("KGuardSqlConnection.txt", FileMode.Open, FileAccess.Read);
          StreamReader streamReader = new StreamReader((Stream) fileStream);
          Initial.SqlCustom.ConnectionString = streamReader.ReadLine();
          Initial.SqlCustom.OpenAsync();
          streamReader.Close();
          fileStream.Close();
          flag = true;
        }
        catch
        {
        }
        Initial.LocalSQL.OpenAsync();
        Initial.LocalSQLExpress.OpenAsync();
        do
          ;
        while (this.progressBar1.Value < 100);
        ConnectionState state = Initial.SqlCustom.State;
        string connectionString;
        if (state.ToString() == "Open")
          connectionString = Initial.SqlCustom.ConnectionString;
        else if (!flag)
        {
          state = Initial.LocalSQL.State;
          if (state.ToString() == "Open")
          {
            connectionString = Initial.LocalSQL.ConnectionString;
          }
          else
          {
            state = Initial.LocalSQLExpress.State;
            if (state.ToString() == "Open")
              connectionString = Initial.LocalSQLExpress.ConnectionString;
            else
              break;
          }
        }
        else
          break;
        Program.sql.ConnectionString = connectionString;
        try
        {
          Program.sql.Open();
          break;
        }
        catch
        {
        }
      }
      Initial.SqlCustom.Close();
      Initial.LocalSQL.Close();
      Initial.LocalSQLExpress.Close();
      string lc = Program.GetLc();
      if (lc != "NoLicance")
      {
        Program.LocalIP = new Regex("<IP>.*</IP>").Match(lc).Value.Replace("<IP>", "").Replace("</IP>", "");
        Program.Days = new Regex("<TIME>.*</TIME>").Match(lc).Value.Replace("<TIME>", "").Replace("</TIME>", "");
        Program.Ver = new Regex("<VER>.*</VER>").Match(lc).Value.Replace("<VER>", "").Replace("</VER>", "");
        Program.RegDate = new Regex("<REGDATE>.*</REGDATE>").Match(lc).Value.Replace("<REGDATE>", "").Replace("</REGDATE>", "");
        Program.Email = new Regex("<EMAIL>.*</EMAIL>").Match(lc).Value.Replace("<EMAIL>", "").Replace("</EMAIL>", "");
        if (Program.Ver.Length < 1)
        {
          int num = (int) MessageBox.Show("Lisans servera erişim sağlayamıyorsunuz gibi görünüyor. www.k-guard.org adresinden bize ulaşıp bilgilendirme yapabilir çözüm isteyebilirsiniz.");
          Environment.Exit(0);
        }
        else
        {
          int int16 = (int) Convert.ToInt16(Program.ProgVer.Replace(".", ""));
          if ((int) Convert.ToInt16(Program.Ver.Replace(".", "")) > int16)
          {
            int num = (int) MessageBox.Show("Yeni bir güncelleme mevcut. Lütfen www.k-guard.org adresine girip v" + Program.Ver + " güncellemesini indirin.");
            Environment.Exit(0);
          }
        }
      }
      else
        Program.LocalIP = "";
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Initial));
      this.pictureBox1 = new PictureBox();
      this.progressBar1 = new ProgressBar();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(609, 181);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.UseWaitCursor = true;
      this.progressBar1.Location = new Point(12, 187);
      this.progressBar1.MarqueeAnimationSpeed = 1;
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(576, 16);
      this.progressBar1.TabIndex = 1;
      this.progressBar1.UseWaitCursor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Black;
      this.ClientSize = new Size(600, 215);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.pictureBox1);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (Initial);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "K-Guard Opening";
      this.UseWaitCursor = true;
      this.Load += new EventHandler(this.Initial_Load);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
    }
  }
}
