// Decompiled with JetBrains decompiler
// Type: KGuard.OpenLicense
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace KGuard
{
  public class OpenLicense : Form
  {
    private const int EM_SETCUEBANNER = 5377;
    private IContainer components = (IContainer) null;
    private PictureBox pictureBox1;
    private TextBox textBox1;
    private Button button1;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

    public OpenLicense() => this.InitializeComponent();

    private void OpenLicense_Load(object sender, EventArgs e)
    {
      OpenLicense.SendMessage(this.textBox1.Handle, 5377, 0, "Epin");
      this.ActiveControl = (Control) this.pictureBox1;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.textBox1.Text.Length != 32)
        return;
      this.button1.Enabled = false;
      new Thread(new ThreadStart(this.GetLc)).Start();
    }

    private void GetLc()
    {
      string text = Program.AddLic(this.textBox1.Text);
      int num = (int) MessageBox.Show(text);
      if (text.Contains("[Success]"))
        this.GetNewLc();
      this.button1.Enabled = true;
    }

    private void GetNewLc()
    {
      string lc = Program.GetLc();
      if (lc != "NoLicance")
      {
        Program.LocalIP = new Regex("<IP>.*</IP>").Match(lc).Value.Replace("<IP>", "").Replace("</IP>", "");
        Program.Days = new Regex("<TIME>.*</TIME>").Match(lc).Value.Replace("<TIME>", "").Replace("</TIME>", "");
        Program.Ver = new Regex("<VER>.*</VER>").Match(lc).Value.Replace("<VER>", "").Replace("</VER>", "");
        Program.RegDate = new Regex("<REGDATE>.*</REGDATE>").Match(lc).Value.Replace("<REGDATE>", "").Replace("</REGDATE>", "");
        Program.Email = new Regex("<EMAIL>.*</EMAIL>").Match(lc).Value.Replace("<EMAIL>", "").Replace("</EMAIL>", "");
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OpenLicense));
      this.pictureBox1 = new PictureBox();
      this.textBox1 = new TextBox();
      this.button1 = new Button();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(30, 220);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(657, 78);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.textBox1.Font = new Font("Arial Narrow", 14.25f, FontStyle.Underline, GraphicsUnit.Point, (byte) 0);
      this.textBox1.Location = new Point(118, 53);
      this.textBox1.MaxLength = 32;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(463, 29);
      this.textBox1.TabIndex = 1;
      this.textBox1.TextAlign = HorizontalAlignment.Center;
      this.button1.Location = new Point(291, 103);
      this.button1.Name = "button1";
      this.button1.Size = new Size(117, 47);
      this.button1.TabIndex = 2;
      this.button1.Text = "Confirm";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.AcceptButton = (IButtonControl) this.button1;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(699, 310);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.pictureBox1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.Name = nameof (OpenLicense);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "License";
      this.Load += new EventHandler(this.OpenLicense_Load);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
