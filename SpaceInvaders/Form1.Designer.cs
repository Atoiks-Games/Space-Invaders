namespace SpaceInvaders
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imglist = new System.Windows.Forms.ImageList(this.components);
            this.player1 = new System.Windows.Forms.PictureBox();
            this.atkTimer = new System.Windows.Forms.Timer(this.components);
            this.btnResume = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.player1)).BeginInit();
            this.SuspendLayout();
            // 
            // imglist
            // 
            this.imglist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglist.ImageStream")));
            this.imglist.TransparentColor = System.Drawing.Color.Transparent;
            this.imglist.Images.SetKeyName(0, "attacker.png");
            this.imglist.Images.SetKeyName(1, "player.png");
            this.imglist.Images.SetKeyName(2, "bullet.png");
            // 
            // player1
            // 
            this.player1.Location = new System.Drawing.Point(190, 256);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(100, 50);
            this.player1.TabIndex = 0;
            this.player1.TabStop = false;
            // 
            // atkTimer
            // 
            this.atkTimer.Tick += new System.EventHandler(this.atkTimer_Tick);
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(359, 12);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(126, 23);
            this.btnResume.TabIndex = 1;
            this.btnResume.Text = "Continue / Resume";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(359, 41);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(126, 23);
            this.btnRestart.TabIndex = 1;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 318);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.player1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "SpaceInvaders";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.player1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imglist;
        private System.Windows.Forms.PictureBox player1;
        private System.Windows.Forms.Timer atkTimer;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnRestart;
    }
}

