// Decompiled with JetBrains decompiler
// Type: KGuard.Init.vSroForumCom
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KGuard.Init
{
  public class vSroForumCom : Form
  {
    private IContainer components = (IContainer) null;
    private Label label1;

    public vSroForumCom() => this.InitializeComponent();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(38, 99);
      this.label1.Name = "label1";
      this.label1.Size = new Size(189, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "vsroforum.com tarafından paylaşılmıştır.";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(284, 261);
      this.Controls.Add((Control) this.label1);
      this.Name = nameof (vSroForumCom);
      this.Text = "vSroForum.Com";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
