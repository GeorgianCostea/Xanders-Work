namespace SETPaint
{
    partial class frmSETPaint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSETPaint));
            this.tsToolbar = new System.Windows.Forms.ToolStrip();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbErase = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawObjects = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbLine = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbEllipse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbLineColour = new System.Windows.Forms.ToolStripButton();
            this.tsbFillColour = new System.Windows.Forms.ToolStripButton();
            this.tsbLineThickness = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbThickness1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbThickness2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbThickness3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.ssStatusbar = new System.Windows.Forms.StatusStrip();
            this.lblMouseStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsToolbar.SuspendLayout();
            this.ssStatusbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsToolbar
            // 
            this.tsToolbar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.tsToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpen,
            this.tsbSave,
            this.tsbErase,
            this.tsbDrawObjects,
            this.tsbLineColour,
            this.tsbFillColour,
            this.tsbLineThickness,
            this.tsbAbout});
            this.tsToolbar.Location = new System.Drawing.Point(0, 0);
            this.tsToolbar.Name = "tsToolbar";
            this.tsToolbar.Size = new System.Drawing.Size(555, 25);
            this.tsToolbar.TabIndex = 0;
            this.tsToolbar.Text = "toolStrip1";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = global::SETPaint.Resources.bmpOpen;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbOpen.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.ToolTipText = "Open File";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::SETPaint.Resources.bmpSave;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.ToolTipText = "Save File";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbErase
            // 
            this.tsbErase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbErase.Image = global::SETPaint.Resources.newpage;
            this.tsbErase.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tsbErase.Name = "tsbErase";
            this.tsbErase.Size = new System.Drawing.Size(23, 22);
            this.tsbErase.ToolTipText = "Erase Image";
            this.tsbErase.Click += new System.EventHandler(this.tsbErase_Click);
            // 
            // tsbDrawObjects
            // 
            this.tsbDrawObjects.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDrawObjects.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLine,
            this.tsbRectangle,
            this.tsbEllipse});
            this.tsbDrawObjects.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawObjects.Margin = new System.Windows.Forms.Padding(60, 1, 0, 2);
            this.tsbDrawObjects.Name = "tsbDrawObjects";
            this.tsbDrawObjects.Size = new System.Drawing.Size(47, 22);
            this.tsbDrawObjects.Text = "Draw";
            this.tsbDrawObjects.ToolTipText = "Draw Objects";
            // 
            // tsbLine
            // 
            this.tsbLine.CheckOnClick = true;
            this.tsbLine.Name = "tsbLine";
            this.tsbLine.Size = new System.Drawing.Size(152, 22);
            this.tsbLine.Text = "Line";
            this.tsbLine.ToolTipText = "Draw a Line";
            this.tsbLine.Click += new System.EventHandler(this.tsbLine_Click);
            // 
            // tsbRectangle
            // 
            this.tsbRectangle.CheckOnClick = true;
            this.tsbRectangle.Name = "tsbRectangle";
            this.tsbRectangle.Size = new System.Drawing.Size(152, 22);
            this.tsbRectangle.Text = "Rectangle";
            this.tsbRectangle.ToolTipText = "Draw a Rectangle";
            this.tsbRectangle.Click += new System.EventHandler(this.tsbRectangle_Click);
            // 
            // tsbEllipse
            // 
            this.tsbEllipse.CheckOnClick = true;
            this.tsbEllipse.Name = "tsbEllipse";
            this.tsbEllipse.Size = new System.Drawing.Size(152, 22);
            this.tsbEllipse.Text = "Ellipse";
            this.tsbEllipse.ToolTipText = "Draw a Ellipse";
            this.tsbEllipse.Click += new System.EventHandler(this.tsbEllipse_Click);
            // 
            // tsbLineColour
            // 
            this.tsbLineColour.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLineColour.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLineColour.Name = "tsbLineColour";
            this.tsbLineColour.Size = new System.Drawing.Size(85, 22);
            this.tsbLineColour.Text = "Border Colour";
            this.tsbLineColour.ToolTipText = "Line/Border Colour";
            this.tsbLineColour.Click += new System.EventHandler(this.tsbLineColour_Click);
            // 
            // tsbFillColour
            // 
            this.tsbFillColour.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbFillColour.Image = ((System.Drawing.Image)(resources.GetObject("tsbFillColour.Image")));
            this.tsbFillColour.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFillColour.Name = "tsbFillColour";
            this.tsbFillColour.Size = new System.Drawing.Size(64, 22);
            this.tsbFillColour.Text = "Fill Object";
            this.tsbFillColour.ToolTipText = "Fill Colour";
            this.tsbFillColour.Click += new System.EventHandler(this.tsbFillColour_Click);
            // 
            // tsbLineThickness
            // 
            this.tsbLineThickness.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLineThickness.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbThickness1,
            this.tsbThickness2,
            this.tsbThickness3});
            this.tsbLineThickness.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLineThickness.Name = "tsbLineThickness";
            this.tsbLineThickness.Size = new System.Drawing.Size(94, 22);
            this.tsbLineThickness.Text = "Line thickness";
            this.tsbLineThickness.ToolTipText = "Line Thickness";
            // 
            // tsbThickness1
            // 
            this.tsbThickness1.Name = "tsbThickness1";
            this.tsbThickness1.Size = new System.Drawing.Size(152, 22);
            this.tsbThickness1.Text = "1 px";
            this.tsbThickness1.ToolTipText = "Line Thickness 1";
            this.tsbThickness1.Click += new System.EventHandler(this.tsbThickness1_Click);
            // 
            // tsbThickness2
            // 
            this.tsbThickness2.Checked = true;
            this.tsbThickness2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbThickness2.Name = "tsbThickness2";
            this.tsbThickness2.Size = new System.Drawing.Size(152, 22);
            this.tsbThickness2.Text = "2 px";
            this.tsbThickness2.ToolTipText = "Line Thickness 2";
            this.tsbThickness2.Click += new System.EventHandler(this.tsbThickness2_Click);
            // 
            // tsbThickness3
            // 
            this.tsbThickness3.Name = "tsbThickness3";
            this.tsbThickness3.Size = new System.Drawing.Size(152, 22);
            this.tsbThickness3.Text = "3 px";
            this.tsbThickness3.ToolTipText = "Line Thickness 3";
            this.tsbThickness3.Click += new System.EventHandler(this.tsbThickness3_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbAbout.BackgroundImage = global::SETPaint.Resources.images;
            this.tsbAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Margin = new System.Windows.Forms.Padding(80, 1, 0, 2);
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(23, 22);
            this.tsbAbout.ToolTipText = "About SETPaint";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // ssStatusbar
            // 
            this.ssStatusbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMouseStatus});
            this.ssStatusbar.Location = new System.Drawing.Point(0, 340);
            this.ssStatusbar.Name = "ssStatusbar";
            this.ssStatusbar.Size = new System.Drawing.Size(555, 22);
            this.ssStatusbar.SizingGrip = false;
            this.ssStatusbar.TabIndex = 1;
            this.ssStatusbar.Text = "statusStrip1";
            // 
            // lblMouseStatus
            // 
            this.lblMouseStatus.Name = "lblMouseStatus";
            this.lblMouseStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // frmSETPaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(555, 362);
            this.Controls.Add(this.ssStatusbar);
            this.Controls.Add(this.tsToolbar);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSETPaint";
            this.Text = "SETPaint";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmSETPaint_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmSETPaint_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSETPaint_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmSETPaint_MouseUp);
            this.tsToolbar.ResumeLayout(false);
            this.tsToolbar.PerformLayout();
            this.ssStatusbar.ResumeLayout(false);
            this.ssStatusbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsToolbar;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripDropDownButton tsbDrawObjects;
        private System.Windows.Forms.ToolStripButton tsbLineColour;
        private System.Windows.Forms.ToolStripButton tsbFillColour;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripMenuItem tsbLine;
        private System.Windows.Forms.ToolStripMenuItem tsbRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsbEllipse;
        private System.Windows.Forms.StatusStrip ssStatusbar;
        private System.Windows.Forms.ToolStripStatusLabel lblMouseStatus;
        private System.Windows.Forms.ToolStripButton tsbErase;
        private System.Windows.Forms.ToolStripDropDownButton tsbLineThickness;
        private System.Windows.Forms.ToolStripMenuItem tsbThickness1;
        private System.Windows.Forms.ToolStripMenuItem tsbThickness2;
        private System.Windows.Forms.ToolStripMenuItem tsbThickness3;
    }
}

