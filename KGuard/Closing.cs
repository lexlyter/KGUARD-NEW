// Decompiled with JetBrains decompiler
// Type: KGuard.Closing
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace KGuard
{
  public class Closing : Form
  {
    private IContainer components = (IContainer) null;
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;

    public Closing() => this.InitializeComponent();

    private void Closing_Load(object sender, EventArgs e) => new Thread(new ThreadStart(this.CloseProg)).Start();

    private void CloseProg()
    {
      Thread.Sleep(1000);
      Environment.Exit(0);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Closing));
      this.pictureBox1 = new PictureBox();
      this.pictureBox2 = new PictureBox();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      this.SuspendLayout();
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(0, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(449, 358);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox2.Image = (Image) componentResourceManager.GetObject("pictureBox2.Image");
      this.pictureBox2.Location = new Point(0, 376);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(449, 111);
      this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox2.TabIndex = 1;
      this.pictureBox2.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.ActiveCaptionText;
      this.ClientSize = new Size(447, 485);
      this.Controls.Add((Control) this.pictureBox2);
      this.Controls.Add((Control) this.pictureBox1);
      this.Cursor = Cursors.WaitCursor;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (Closing);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (Closing);
      this.Load += new EventHandler(this.Closing_Load);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      this.ResumeLayout(false);
    }
  }
}
