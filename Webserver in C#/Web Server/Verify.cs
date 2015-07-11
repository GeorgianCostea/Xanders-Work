/*
 * File Name    : Verify.cs
 * Project      : Web Server
 * Date         : 09/12/2012
 * Programmer   : Georgian Costea
 * Description  :  Contains the  vefiry class which is a static class. In the
 * Verify class there is a method that check the extension of the file.
 */





using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;



namespace Web_Server
{
	/// <summary>
	/// The Verify class will be used to verify all file extensions, that are requested by 
	/// the user.
	/// </summary>
    public static class Verify
    {



        public const string SspExtension = ".ssp";
        public const int NumOfCharacters = 4;


		/// <summary>
		/// Checks for ".ssp" extension and if its a match than it will return true, otherwise it will
		/// return false.
		/// </summary>
		/// <param name="request">this reprezents the requestStructs</param>
		/// <returns>returns true if was found and false if it didnt.</returns>
        public static bool verifyFileExtension(RequestStruct request)
        {
            string parse = request.url;

            string extension = parse.Substring(Math.Max(0, parse.Length - NumOfCharacters));

            if (extension == SspExtension)
            {
                return true;
            }

            return false;
        }



    }
}
