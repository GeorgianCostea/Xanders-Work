/*
 * Date         : 10/05/2012
 * Programmers  :Georgian Costea
 * Description  : The purpose of this class is to write
 * to the windows form ( solid ellipse and rubber band effect
 * ellipse) and it inherits from DrawObject all data members 
 * and overwrites the  virtual functions in DrawObject.
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
    /// The purpose of this class is to write
    /// to the windows form ( solid ellipse and rubber band heeffect
    /// ellipse) and it inherits from DrawObject all data members 
    /// and overwrites the  virtual functions in DrawObject.
    /// </summary>
    class Ellipse : DrawObject
    {




        /// <summary>
        /// Default C-tor initializing data members to fixed values
        /// when the program is first started.
        /// </summary>
       public Ellipse()
       {
          startPoint = new Point(0, 0);
          endPoint = new Point(0, 0);
          lineColour = Color.Black;
          lineThickness = 2;
          fillColour = Color.Black;
       }



        /// <summary>
        /// C-tor that sets the values defined by the user into data members.
        /// </summary>
        /// <param name="newLineColour"></param>
        /// <param name="newFillColour"></param>
        /// <param name="newLineThickness"></param>
       public Ellipse(Color newLineColour, Color newFillColour, int newLineThickness)
       {
          startPoint = new Point(0, 0);
          endPoint = new Point(0, 0);
          lineColour = newLineColour;
          lineThickness = newLineThickness;
          fillColour = newFillColour;
       }


        /// <summary>
        /// Overwrites the parrent method and calls the function Draw() 
        /// </summary>
        /// <param name="g"></param>
        public override void DrawSolid(Graphics g)
        {
            Draw(g, new Pen(lineColour), true);
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

           Draw(g, dashedPen, false);
        }



        /// <summary>
        /// Draws the ellipse, and depending by which method was called then i will
        /// draw solid or dashed. Also we are setting the Width and Height to draw the Ellipse.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="ellipsePen"></param>
        /// <param name="fillEllipse"></param>
        private void Draw(Graphics g, Pen ellipsePen, bool fillEllipse)
        {
           Rectangle rect = new Rectangle(startPoint.X, startPoint.Y, (endPoint.X - startPoint.X), (endPoint.Y - startPoint.Y));
            /* check if the start of point x is greated then end point and if 
             its true the rect.x and rect.width is set.*/
           if (startPoint.X > endPoint.X)
           {
              rect.X = endPoint.X;
              rect.Width = (startPoint.X - endPoint.X);
           }

            /*check if startpoint Y is grated then the endpoint of Y and if its 
             true the rect.y and rect.hight is set*/
           if (startPoint.Y > endPoint.Y)
           {
              rect.Y = endPoint.Y;
              rect.Height = (startPoint.Y - endPoint.Y);
           }

           ellipsePen.Width = lineThickness;
           g.DrawEllipse(ellipsePen, rect);

           if(fillEllipse == true)
           {
              g.FillEllipse(new SolidBrush(fillColour), rect);
           }
        }

    }
}
