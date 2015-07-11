namespace quiz6
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
            this.mapexplr = new GMap.NET.WindowsForms.GMapControl();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mapexplr
            // 
            this.mapexplr.Bearing = 0F;
            this.mapexplr.CanDragMap = true;
            this.mapexplr.GrayScaleMode = false;
            this.mapexplr.LevelsKeepInMemmory = 5;
            this.mapexplr.Location = new System.Drawing.Point(2, 1);
            this.mapexplr.MarkersEnabled = true;
            this.mapexplr.MaxZoom = 2;
            this.mapexplr.MinZoom = 2;
            this.mapexplr.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mapexplr.Name = "mapexplr";
            this.mapexplr.NegativeMode = false;
            this.mapexplr.PolygonsEnabled = true;
            this.mapexplr.RetryLoadTile = 0;
            this.mapexplr.RoutesEnabled = true;
            this.mapexplr.ShowTileGridLines = false;
            this.mapexplr.Size = new System.Drawing.Size(1836, 1086);
            this.mapexplr.TabIndex = 0;
            this.mapexplr.Zoom = 0D;
            this.mapexplr.Load += new System.EventHandler(this.mapexplr_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 624);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mapexplr);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl mapexplr;
        private System.Windows.Forms.Label label1;
    }
}

