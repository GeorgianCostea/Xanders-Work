namespace SETPaint
{
   partial class frmAbout
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
         this.lblAbout = new System.Windows.Forms.Label();
         this.btnOK = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // lblAbout
         // 
         this.lblAbout.AutoSize = true;
         this.lblAbout.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblAbout.Location = new System.Drawing.Point(24, 47);
         this.lblAbout.Name = "lblAbout";
         this.lblAbout.Size = new System.Drawing.Size(85, 60);
         this.lblAbout.TabIndex = 0;
         this.lblAbout.Text = "Developers:\r\n\r\nAlex C.\r\nBrad K.";
         // 
         // btnOK
         // 
         this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.btnOK.Location = new System.Drawing.Point(207, 187);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(75, 23);
         this.btnOK.TabIndex = 1;
         this.btnOK.Text = "OK";
         this.btnOK.UseVisualStyleBackColor = true;
         // 
         // frmAbout
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(294, 222);
         this.Controls.Add(this.btnOK);
         this.Controls.Add(this.lblAbout);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "frmAbout";
         this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
         this.Text = "About SETPaint";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label lblAbout;
      private System.Windows.Forms.Button btnOK;
   }
}