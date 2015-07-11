/*
 * Date       : 10/5/2012
 * Programmers:Georgian Costea
 * Description: This file hold the DrawObject class
 *  that is the parrent of Line | Ellipse | rectangleShape
 *  with specific data members and 2 virtual functions that
 *  will be overwritten by the children.
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
    /// Holds data members and  methods that will be used
    /// by children. The methods will be overritten and  
    /// data members will define the each object. 
    /// </summary>
    class DrawObject
    {
        public Point startPoint
        {
            get;
            set;
        }

        public Point endPoint
        {
            get;
            set;
        }

        public Color fillColour
        {
           get;
           set;
        }

        public Color lineColour
        {
            get;
            set;
        }
        public int lineThickness
        {
            get;
            set;
        }



        /// <summary>
        ///  Virtual function there is no code in this function because
        ///  its going to ve overritten by the children and we did not 
        ///  found a purpose to put any code in here.
        /// </summary>
        /// <param name="g"></param>
        public virtual void DrawSolid(Graphics g){}



        /// <summary>
        ///Virtual function there is no code in this function because
        ///  its going to ve overritten by the children and we did not 
        ///  found a purpose to put any code in here.
        /// </summary>
        /// <param name="g"></param>
        public virtual void DrawDashed(Graphics g){}




    }
}
