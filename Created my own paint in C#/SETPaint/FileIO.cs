/*
 * Date          : 10/05/2012
 * Programmer    : Georgian Costea
 * Description   : The purpose of this class is to save and load
 * a file .Using BinaryReader/BinaryWriter
 * */






using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SETPaint
{

    /// <summary>
    /// The purpose of this class is to load and save the drawn object
    /// to a file / and load from a file.
    /// </summary>
   class FileIO
   {
      private enum DrawObjectType
      {
         Line = 1,
         Rectangle = 2,
         Ellipse = 3
      }



        /// <summary>
        /// Opening a new file ensure object passed is new and using a try statement
        /// we will try to open the file. If the test pass then it will try to read the file 
        /// and put the data from file into data members ( DtawObjects class).
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="drawObjectHistory"></param>
        /// <returns>status</returns>
      public static bool OpenSETPaintFile(string filename, List<DrawObject> drawObjectHistory)
      {
         BinaryReader openFile;
         byte tempByte = 0;
         DrawObject drawObject = new DrawObject();
         bool status = true;

         drawObjectHistory.Clear();   /*opening a new file ensure object passed is new*/

         try
         {
            openFile = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read));
         }
         catch (Exception)
         {
            return false;
         }

         try
         {
            if(openFile.ReadByte() != 58) /*read and test file magic number*/
            {
               return false;
            }
            /*loop and check if its a line /Rectangle or ellipse and put the values into 
              data members.*/
            while (true)
            {
               tempByte = openFile.ReadByte();
               if(tempByte == (byte)DrawObjectType.Line)
               {
                  drawObject = new Line();
               }
               else if(tempByte == (byte)DrawObjectType.Rectangle)
               {
                  drawObject = new RectangleShape();
               }
               else if(tempByte == (byte)DrawObjectType.Ellipse)
               {
                  drawObject = new Ellipse();
               }

               drawObject.startPoint = new Point(openFile.ReadInt32(), openFile.ReadInt32());
               drawObject.endPoint = new Point(openFile.ReadInt32(), openFile.ReadInt32());
               drawObject.fillColour = Color.FromArgb(openFile.ReadInt32());
               drawObject.lineColour = Color.FromArgb(openFile.ReadInt32());
               drawObject.lineThickness = openFile.ReadInt32();
               drawObjectHistory.Add(drawObject);
            }
         }
         catch (EndOfStreamException)
         {
            status = true;
         }
         catch (Exception)
         {
            status = false;
         }
         finally
         {
            openFile.Close();
         }

         return status;
      }






       /// <summary>
       /// Saves the objects drawn by the user to the file. Looping thru each object in the list
       /// and add it to the file ussing BinaryWriter
       /// </summary>
       /// <param name="filename"></param>
       /// <param name="drawObjectHistory"></param>
       /// <returns></returns>
      public static bool SaveSETPaintFile(string filename, List<DrawObject> drawObjectHistory)
      {
         BinaryWriter saveFile;
         bool status = true;

         try
         {
             /*open the file for write.*/
            saveFile = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write));
         }
         catch (Exception)
         {
            return false;
         }

         try
         {
            saveFile.Write((byte)58); /*add a file magic number (every file needs some magic)*/
            /*loops and save all object in the list .*/
            foreach (DrawObject drawObject in drawObjectHistory)
            {
               if(drawObject is Line)
               {
                  saveFile.Write((byte)DrawObjectType.Line);
               }
               else if(drawObject is RectangleShape)
               {
                  saveFile.Write((byte)DrawObjectType.Rectangle);
               }
               else if(drawObject is Ellipse)
               {
                  saveFile.Write((byte)DrawObjectType.Ellipse);
               }

               saveFile.Write(drawObject.startPoint.X);
               saveFile.Write(drawObject.startPoint.Y);
               saveFile.Write(drawObject.endPoint.X);
               saveFile.Write(drawObject.endPoint.Y);
               saveFile.Write(drawObject.fillColour.ToArgb());
               saveFile.Write(drawObject.lineColour.ToArgb());
               saveFile.Write(drawObject.lineThickness);
            }
         }
         catch (Exception)
         {
            status = false;
         }
         finally
         {
            saveFile.Close();
         }

         return status;
      }
   }
}
