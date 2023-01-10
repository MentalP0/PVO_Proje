namespace Personel_Vardiya_Otomasyonu
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnYoneticiGirisi = new System.Windows.Forms.Button();
            this.btnKullaniciGirisi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnYoneticiGirisi
            // 
            this.btnYoneticiGirisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYoneticiGirisi.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYoneticiGirisi.Image = ((System.Drawing.Image)(resources.GetObject("btnYoneticiGirisi.Image")));
            this.btnYoneticiGirisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnYoneticiGirisi.Location = new System.Drawing.Point(12, 12);
            this.btnYoneticiGirisi.Name = "btnYoneticiGirisi";
            this.btnYoneticiGirisi.Size = new System.Drawing.Size(193, 46);
            this.btnYoneticiGirisi.TabIndex = 0;
            this.btnYoneticiGirisi.Text = "Yönetici Girişi";
            this.btnYoneticiGirisi.UseVisualStyleBackColor = true;
            this.btnYoneticiGirisi.Click += new System.EventHandler(this.btnYoneticiGirisi_Click);
            // 
            // btnKullaniciGirisi
            // 
            this.btnKullaniciGirisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKullaniciGirisi.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKullaniciGirisi.Image = ((System.Drawing.Image)(resources.GetObject("btnKullaniciGirisi.Image")));
            this.btnKullaniciGirisi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKullaniciGirisi.Location = new System.Drawing.Point(211, 12);
            this.btnKullaniciGirisi.Name = "btnKullaniciGirisi";
            this.btnKullaniciGirisi.Size = new System.Drawing.Size(193, 46);
            this.btnKullaniciGirisi.TabIndex = 1;
            this.btnKullaniciGirisi.Text = "Kullanıcı Girişi";
            this.btnKullaniciGirisi.UseVisualStyleBackColor = true;
            this.btnKullaniciGirisi.Click += new System.EventHandler(this.btnKullaniciGirisi_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(418, 72);
            this.Controls.Add(this.btnKullaniciGirisi);
            this.Controls.Add(this.btnYoneticiGirisi);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Personel Vardiya Otomasyonu";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnYoneticiGirisi;
        private System.Windows.Forms.Button btnKullaniciGirisi;
    }
}

