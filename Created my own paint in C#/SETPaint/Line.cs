/*

 * Date           : 10/05/2012
 * Programmers    : Georgian Costea
 * Description    : The purpose of this class is to write
 * to the windows form ( solid line and rubber band effect
 * line) and it inherits from DrawObject all data members 
 * and overwrites the  virtual functions in DrawObject.
 *
 **/





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
    /// The purpose of this class is to write
    /// to the windows form ( solid line and rubber band effect
    /// line) and it inherits from DrawObject all data members 
    /// and overwrites the  virtual functions in DrawObject.
    /// </summary>
    class Line : DrawObject
    {

        /// <summary>
        /// Default C-tor initializing data members to fixed values
        /// when the program is first started.
        /// </summary>
       public Line()
       {
          startPoint = new Point(0, 0);
          endPoint = new Point(0, 0);
          lineColour = Color.Black;
          lineThickness = 2;
       }


        /// <summary>
       /// C-tor that sets the values defined by the user into data members.
        /// </summary>
        /// <param name="newLineColour"></param>
        /// <param name="newLineThickness"></param>
       public Line(Color newLineColour, int newLineThickness)
       {
          startPoint = new Point(0, 0);
          endPoint = new Point(0, 0);
          lineColour = newLineColour;
          lineThickness = newLineThickness;
       }



        /// <summary>
        /// Overwrites the parrent method and calls the function Draw() 
        /// </summary>
        /// <param name="g"></param>
        public override void DrawSolid(Graphics g)
        {
            Draw(g, new Pen(lineColour));
        }




        /// <summary>
        /// OverWrites the parrent method and creates a new pen with the 
        /// desired thickness and color and calls the function Draw.
        /// </summary>
        /// <param name="g"></param>
        public override void DrawDashed(Graphics g)
        {
           Pen dashedPen = new Pen(lineColour, lineThickness);

           dashedPen.DashStyle = DashStyle.Dash;
           Draw(g, dashedPen);
        }




        /// <summary>
        /// Draws the ellipse, and depending by which method was called then i will
        /// draw solid or dashed. Also we are setting the Width and Height to draw the Ellipse.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="linePen"></param>
        private void Draw(Graphics g, Pen linePen)
        {
           linePen.Width = lineThickness;
           g.DrawLine(linePen, startPoint, endPoint);
        }
    }
}
