/*
 * Date         : 10/05/2012
 * Programmers  : Georgian Costea
 * Description  : This file holds the code for the form; the 
 * list where the objects will be stored; and all the events calls.
 * 
 * */



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SETPaint
{

    /// <summary>
    /// Partial class what inherits from form which is a library.
    /// </summary>
    public partial class frmSETPaint : Form
    {
        /* list where the objects drawn by the user are stored.*/
        private List<DrawObject> drawObjectHistory = new List<DrawObject>();
        private DrawObject currentDrawObject = null;
        private Color currentLineColour = Color.Black;
        private Color currentFillColour = Color.Black;
        private int currentLineThickness = 2;

        /// <summary>
        /// initialize all the components of the form
        /// </summary>
        public frmSETPaint()
        {
            InitializeComponent();
        }



        /// <summary>
        /// When the mouse move and the left click is pressed then display the
        /// coordonates and if the currentDrawObject is not null the set the end point
        /// to the e.location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSETPaint_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                lblMouseStatus.Text = (e.X.ToString() + ", " + e.Y.ToString());
            }
            else if(lblMouseStatus.Text != "")
            {
                lblMouseStatus.Text = "";
            }
            /*get the end location*/
            if(currentDrawObject != null)
            {
               currentDrawObject.endPoint = e.Location;
               this.Refresh();
            }
        }




        /// <summary>
        /// On paint event draw and redraw all the objects, therefor the objects will aprear
        /// until the user closes the program or deletes the current page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSETPaint_Paint(object sender, PaintEventArgs e)
        {
            foreach (DrawObject Drawobj in drawObjectHistory)
            {
               Drawobj.DrawSolid(e.Graphics);
            }

           if (currentDrawObject != null)
           {
              currentDrawObject.DrawDashed(e.Graphics);
           }
        }




        /// <summary>
        /// when mouse was presses the create on of the following shapes depending
        /// on what the user desided to draw.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSETPaint_MouseDown(object sender, MouseEventArgs e)
        {
            /* checks to see which of the options was clicked.*/
            if(tsbLine.Checked == true)
            {
               currentDrawObject = new Line(currentLineColour, currentLineThickness);
            }
            else if(tsbRectangle.Checked == true)
            {
               currentDrawObject = new RectangleShape(currentLineColour, currentFillColour, currentLineThickness);
            }
            else if(tsbEllipse.Checked == true)
            {
               currentDrawObject = new Ellipse(currentLineColour, currentFillColour, currentLineThickness);
            }

            if (currentDrawObject != null)
            {
               currentDrawObject.startPoint = e.Location;
               currentDrawObject.endPoint = e.Location;
            }
        }





        /// <summary>
        /// When the mouse was released, the program can go ahead and add the object to the list .
        /// Also there is no need for cast because its already taken care by the compiler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSETPaint_MouseUp(object sender, MouseEventArgs e)
        {
           if(currentDrawObject != null)
           {
              if(currentDrawObject.startPoint != currentDrawObject.endPoint)
              {
                  drawObjectHistory.Add(currentDrawObject);
              }
              
              currentDrawObject = null;
           }

            this.Refresh();
        }




        /// <summary>
        /// when the button is clicked then it askes the user if they want to delete 
        /// and if they press yes then a new page is created.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbErase_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show("Do you want to erase Image?", "Erase Image", MessageBoxButtons.YesNo) == DialogResult.Yes)
           {
              drawObjectHistory.Clear();
              this.Refresh();
           }
        }




        /// <summary>
        /// if this option was choosen then the other 2 option are shut off.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbLine_Click(object sender, EventArgs e)
        {
           tsbRectangle.Checked = false;
           tsbEllipse.Checked = false;
        }




        /// <summary>
        /// if this option was choosen then the other 2 option are shut off.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbRectangle_Click(object sender, EventArgs e)
        {
           tsbLine.Checked = false;
           tsbEllipse.Checked = false;
        }




        /// <summary>
        /// if this option was choosen then the other 2 option are shut off.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbEllipse_Click(object sender, EventArgs e)
        {
           tsbLine.Checked = false;
           tsbRectangle.Checked = false;
        }




        /// <summary>
        /// user selects a color and the sellected colour is set to currentLineColour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbLineColour_Click(object sender, EventArgs e)
        {
           ColorDialog colourDialog = new ColorDialog();

           colourDialog.Color = currentLineColour;
           if(colourDialog.ShowDialog() == DialogResult.OK)
           {
              currentLineColour = colourDialog.Color;
           }
        }




        /// <summary>
        /// user selects the desired color to fill the shape.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbFillColour_Click(object sender, EventArgs e)
        {
           ColorDialog colourDialog = new ColorDialog();

           colourDialog.Color = currentFillColour;
           if (colourDialog.ShowDialog() == DialogResult.OK)
           {
              currentFillColour = colourDialog.Color;
           }
        }



        /// <summary>
        /// sets the line thickness to 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbThickness1_Click(object sender, EventArgs e)
        {
           tsbThickness1.Checked = true;
           tsbThickness2.Checked = false;
           tsbThickness3.Checked = false;
           currentLineThickness = 1;
        }




        /// <summary>
        /// sets the line thickness to 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbThickness2_Click(object sender, EventArgs e)
        {
           tsbThickness1.Checked = false;
           tsbThickness2.Checked = true;
           tsbThickness3.Checked = false;
           currentLineThickness = 2;
        }




        /// <summary>
        /// sets the line thickness to 4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbThickness3_Click(object sender, EventArgs e)
        {
           tsbThickness1.Checked = false;
           tsbThickness2.Checked = false;
           tsbThickness3.Checked = true;
           currentLineThickness = 4;
        }




        /// <summary>
        /// open a fileDialog allowing to load a new file into the system,
        /// the filter is for any file of .spf file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbOpen_Click(object sender, EventArgs e)
        {
           OpenFileDialog openDialog = new OpenFileDialog();

           openDialog.Title = "Open SETPaint File";
           openDialog.Filter = "SETPaint Files (*.spf)|*.spf";

            /* if the ok button was pressed then is checks if the file is ok*/
           if(openDialog.ShowDialog() == DialogResult.OK)
           {
              if(!FileIO.OpenSETPaintFile(openDialog.FileName, drawObjectHistory))
              {
                 MessageBox.Show("Error: Unable to open file", "File Not Opened");
              }
           }

           this.Refresh();
        }




       
        /// <summary>
        /// allows the user to save their work to a file with .spf extension
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSave_Click(object sender, EventArgs e)
        {
           SaveFileDialog saveDialog = new SaveFileDialog();

           saveDialog.Title = "Save SETPaint File";
           saveDialog.Filter = "SETPaint Files (*.spf)|*.spf";
           if (saveDialog.ShowDialog() == DialogResult.OK)
           {
              if(!FileIO.SaveSETPaintFile(saveDialog.FileName, drawObjectHistory))
              {
                 MessageBox.Show("Error: Unable to save file", "File Not Saved");
              }
           }
        }





        /// <summary>
        /// the about box will pop up 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbAbout_Click(object sender, EventArgs e)
        {
           frmAbout about = new frmAbout();
           about.ShowDialog();
        }


    }
}
