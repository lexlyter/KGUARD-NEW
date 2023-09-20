// Decompiled with JetBrains decompiler
// Type: KGuard.SetSQL
// Assembly: KGuard, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 67A939E1-CFF9-4E38-BEE7-DB528074759F
// Assembly location: C:\Users\Kadir\Desktop\kguard\KGuard.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace KGuard
{
  public class SetSQL : Form
  {
    private const int EM_SETCUEBANNER = 5377;
    private IContainer components = (IContainer) null;
    private TextBox textBox1;
    private TextBox textBox2;
    private TextBox textBox3;
    private PictureBox pictureBox1;
    private Button button1;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

    public SetSQL() => this.InitializeComponent();

    private void SetSQL_Load(object sender, EventArgs e)
    {
      SetSQL.SendMessage(this.textBox1.Handle, 5377, 0, "SQL Server Name");
      SetSQL.SendMessage(this.textBox2.Handle, 5377, 0, "User");
      SetSQL.SendMessage(this.textBox3.Handle, 5377, 0, "Password");
      this.ActiveControl = (Control) this.pictureBox1;
      this.Fill(false);
      new Thread(new ThreadStart(this.SendMessageBox)).Start();
    }

    private void SendMessageBox()
    {
      int num = (int) MessageBox.Show("Login yetkilerinin tam olması gerekir. Master veritabanına ve db_owner yetkisine erişimi kısıtlanmamalıdır.");
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.textBox1.Text.Length < 1 || this.textBox2.Text.Length < 1 || this.textBox3.Text.Length < 1)
        return;
      this.button1.Enabled = false;
      new Thread(new ThreadStart(this.TrySql)).Start();
    }

    private void TrySql()
    {
      Program.sql.ConnectionString = "Data Source=" + this.textBox1.Text + ";Initial Catalog=master;Persist Security Info=True;User ID=" + this.textBox2.Text + "; Password=" + this.textBox3.Text + "; MultipleActiveResultSets=True;";
      Program.sql.OpenAsync();
      Thread.Sleep(100);
      if (Program.sql.State.ToString() != "Open")
        Thread.Sleep(600);
      if (Program.sql.State.ToString() != "Open")
      {
        Program.sql.Close();
        int num = (int) MessageBox.Show("Ayarlarınızı kontrol edin.");
      }
      else
      {
        this.Fill(true);
        this.Close();
      }
      this.button1.Enabled = true;
    }

    private void Fill(bool write)
    {
      try
      {
        if (write)
        {
          FileStream fileStream = new FileStream("KGuardSqlConnection.txt", FileMode.OpenOrCreate, FileAccess.Write);
          StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
          streamWriter.WriteLine(Program.sql.ConnectionString);
          streamWriter.Close();
          fileStream.Close();
        }
        else
        {
          FileStream fileStream = new FileStream("KGuardSqlConnection.txt", FileMode.OpenOrCreate, FileAccess.Read);
          StreamReader streamReader = new StreamReader((Stream) fileStream);
          string input = streamReader.ReadLine();
          if (input != null)
          {
            this.textBox1.Text = new Regex("Data Source=.*?;").Match(input).Value.Replace("Data Source=", "").Replace(";", "");
            this.textBox2.Text = new Regex("User ID=.*?;").Match(input).Value.Replace("User ID=", "").Replace(";", "");
            this.textBox3.Text = new Regex("Password=.*?;").Match(input).Value.Replace("Password=", "").Replace(";", "");
          }
          streamReader.Close();
          fileStream.Close();
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SetSQL));
      this.textBox1 = new TextBox();
      this.textBox2 = new TextBox();
      this.textBox3 = new TextBox();
      this.pictureBox1 = new PictureBox();
      this.button1 = new Button();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.textBox1.Font = new Font("Microsoft Sans Serif", 12.25f);
      this.textBox1.Location = new Point(75, 86);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(247, 26);
      this.textBox1.TabIndex = 0;
      this.textBox1.TextAlign = HorizontalAlignment.Center;
      this.textBox2.Font = new Font("Microsoft Sans Serif", 11.25f);
      this.textBox2.Location = new Point(75, 118);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new Size(247, 24);
      this.textBox2.TabIndex = 2;
      this.textBox2.TextAlign = HorizontalAlignment.Center;
      this.textBox3.Font = new Font("Microsoft Sans Serif", 11.25f);
      this.textBox3.Location = new Point(75, 148);
      this.textBox3.Name = "textBox3";
      this.textBox3.PasswordChar = '*';
      this.textBox3.Size = new Size(247, 24);
      this.textBox3.TabIndex = 3;
      this.textBox3.TextAlign = HorizontalAlignment.Center;
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(75, 32);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(267, 50);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 4;
      this.pictureBox1.TabStop = false;
      this.button1.Location = new Point(121, 178);
      this.button1.Name = "button1";
      this.button1.Size = new Size(152, 38);
      this.button1.TabIndex = 5;
      this.button1.Text = "Connect";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.AcceptButton = (IButtonControl) this.button1;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(397, 264);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.textBox3);
      this.Controls.Add((Control) this.textBox2);
      this.Controls.Add((Control) this.textBox1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.Name = nameof (SetSQL);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "SQL Connection";
      this.Load += new EventHandler(this.SetSQL_Load);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
