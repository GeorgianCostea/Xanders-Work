/*
 * File Name    : SspProcessor.cs
 * Project      : Web Server
 * Date         : 09/12/2012
 * Programmer   : Georgian Costea
 * Description  : Contains the SspProcessor class and the Tokens strucks. The SspProcessor
 * class will be used to parse the file, and send back a response based on the file input.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace SspDLL
{
    public  struct Tokens
	{
        public static string multiplication;
        public static string additions;
        public static string substraction;
        public static string contatenation;
        public static string endOfLine;
        public static string print;
        public static string quote;
        public static string quoteCommand;
        public static string date;
        public static string time;
		public static string startTok;
		public static string nextTok;
		public static string endTok;
		public static string startScriptTag;
		public static string endScriptTag;

	};


	/// <summary>
	/// SspProcessor is used to parse the file content and send back a response .
	/// </summary>
	public static class SspProcessor
	{
		
		public const int zero = 0;
		public const int one = 1;
		public const int two = 2;
		public const string add = "Add";
		public const string substract ="Subtract";
		public const string multiply = "Multiply";
		
		/// <summary>
		/// initialize all tokens.
		/// </summary>
        public static void init()
        {
            Tokens.additions = "+";
            Tokens.contatenation = "&";
            Tokens.multiplication = "*";
            Tokens.substraction = "-";
            Tokens.endOfLine = ";";
            Tokens.print = "pr";
            Tokens.quote = "\"";
            Tokens.quoteCommand = "Cot";
            Tokens.date = "date()";
            Tokens.time = "time()";
			Tokens.startTok = "<";
			Tokens.nextTok = "%";
			Tokens.endTok = ">";
			Tokens.startScriptTag = "<%";
			Tokens.endScriptTag = "%>";
        }


		/// <summary>
		/// This method will loop thru every character in the string . It will exctract
		/// any values that are between " <%" "%>" in order to process it.
		/// </summary>
		/// <param name="input">The file requested by the user</param>
		/// <returns>returns the result or null if the request is invalid.</returns>
		public static string extractScript(string  input)
		{
			
			
			
			init();
			input = input.Trim('\0');

			string tokenStart = Tokens.startTok;
			string tokenNext = Tokens.nextTok;
			string tokenEnd = Tokens.endTok;
			string outText = String.Empty;
			string token = String.Empty;
			string replacement = String.Empty;
			string tok;
			string character = string.Empty;
			string tok2;
			string stringToParse = string.Empty;
			bool flag = true;
			int count = 0;
			int i = 0;
			int len = input.Length;
			
			

           
			while (i < len)
			{
				tok = input[i].ToString();
				character = string.Empty;

				/*if the tok is < and the next character is %*/
				if (tok == tokenStart && input[i + one].ToString() == tokenNext)
				{
					i++;

					tok2 = input[i].ToString();
					if (tok2 == tokenNext)
					{
						flag = false;
						i++;
						while (i < len)
						{	
							/* loop until the end tag was reached or until end of file .*/
							tok2 = input[i].ToString();
							flag = false;
							if ((tok2 == tokenNext) && (input[i + one].ToString() == tokenEnd))
							{
								i = i + two;
								flag = true;
								break;
							}

							token += tok2;
							i++;

						}

						/*the end tag does not exists return null , 
						 file is invalid.*/
						if (!flag)
						{
							return null;
						}

						/*search for any other tags in the string*/
						int tag = token.IndexOf(Tokens.startScriptTag);

						if (tag != -one)
						{
							return null;
						}

						/*parse the script even more*/
						while (count < token.Length)
						{
							tok2 = token[count].ToString();

							/*puts the values int a temporary string until end of line is hit in our case ";" */
							if (tok2 == Tokens.endOfLine)
							{


								string checkReturn = string.Empty;


								checkReturn = parseTokens(stringToParse);

								if (checkReturn == null)
								{
									return null;
								}
								outText += checkReturn;
								stringToParse = string.Empty;
								count++;
								tok2 = token[count].ToString();


							}

							stringToParse += tok2;
							count++;
						}

						stringToParse = stringToParse.Trim('\n', '\r', '\0');
						if (!checkCharacter(stringToParse))
						{
							return null;
						}

					}


					tok = input[i].ToString();//i++;
					character = tok2;
					
				}
				else if ((tok == tokenNext && input[i + one].ToString() == tokenEnd))
				{
					return null;
				}

				outText += tok;
				outText += character;
				i++;


			}

			outText = outText.Trim('\n', '\r', '\0');

			return outText;

		}




		/// <summary>
		/// The purpose of this function is to parse the line that was send
		/// and see if its valid.( if valid a response will be send )
		/// </summary>
		/// <param name="newString">string to parse </param>
		/// <returns>if all good than the result will be send otherwise null will be send.</returns>
        public static string parseTokens(string newString)
		{

			
            newString = newString.Replace(Tokens.endOfLine, "");
			
			string[] stringSep = new string[] { Tokens.contatenation };
			string[] result;
			string outputString = string.Empty;
			/*spliting the string into smaller pieces in order to process it. 
			 this code looks for "&" and splits that string.*/
			result = newString.Split(stringSep, StringSplitOptions.None);
			int index = result[zero].IndexOf(Tokens.print);
			bool flag = false;


			/*check in the first string has pr.*/
			if (index != -one)
			{
				result[0] = result[zero].Substring(index + two);
				flag = true;
			}


			foreach (string s in result)
			{

                string returnVal = checkindex(s, Tokens.quote, Tokens.quote, Tokens.quoteCommand);
				
				if (returnVal == null)
				{
					
					
                    string dateTimeD = GettingDateTime(s, Tokens.date, Tokens.time);

					if (dateTimeD == null)
					{
						return null;
						//file is invalid send 500 error.
					}
					else
					{
						//check for pr
						if (flag != false)
						{
							outputString += dateTimeD;
						}

					}
				}
				else
				{
                    //check for pr
					if (flag != false)
					{
                        //text so you need to save it.
						outputString += returnVal;
					}
					
				}
			}
			return outputString;

		}





		/// <summary>
		/// This method checks if there  are quotation marks and check there is a set of quotation marks ("")
		/// because otherwise its an error.
		/// </summary>
		/// <param name="inputString">the string that needs to be checked</param>
		/// <param name="FrtToken">the first quotation mark</param>
		/// <param name="ScdToken"> the second quotation mark</param>
		/// <param name="command"> and command.</param>
		/// <returns>return the values between quotation marks or null</returns>
        public static string checkindex(string inputString, string FrtToken, string ScdToken, string command)
		{
			int indexFound = inputString.IndexOf(FrtToken);
			string check = string.Empty;
			check = inputString.Substring(indexFound + one);
			int indexFnd = check.LastIndexOf(ScdToken);
			string ret = string.Empty;



			//check for "" 
			if (command == Tokens.quoteCommand)
			{
				if ((indexFound != -one) && (indexFnd != -one))
				{
					string invalidChars = inputString.Substring(zero, indexFound);
					invalidChars += check.Substring(indexFnd + one);

					invalidChars = invalidChars.Trim('\n', '\r', '\a');

					if (!checkCharacter(invalidChars))
					{
						return null;
					}

					ret = inputString.Replace("\"", "");
					return ret;
				}
				else
				{
					ret = checkOperation(inputString);

					if (ret == null)
					{
						return null;
					}
					else
					{
						ret = ret.Trim('\n', '\r', ' ');
						return ret;
					}

				}
			}


			ret = ret.Trim('\n', '\r', ' ');
			return ret;


		}




		/// <summary>
		/// Check if in the input string there is either time() or date() 
		/// </summary>
		/// <param name="input">input string to be checked</param>
		/// <param name="FrtToken"> time()</param>
		/// <param name="ScdToken">date()</param>
		/// <returns>return the date or time if valid otherwise return null.</returns>
        public static string GettingDateTime(string input, string FrtToken, string ScdToken)
		{


			int indexFound = input.IndexOf(FrtToken);
			int ScdIndexFound = input.IndexOf(ScdToken);
			int indexFnd = input.LastIndexOf(ScdToken);
			string ret = string.Empty;

			
			// check for date() and check for time();
			if ((indexFound == -one) && (ScdIndexFound == -one))
			{

				return null;
			}
			else if ((indexFound != -one) && (ScdIndexFound == -one))
			{
                ret = input.Replace(Tokens.date, DateTime.Now.ToString("yyyy-MM-dd"));
				string checkString = input.Substring(indexFound + 6);
				checkString += input.Substring(zero, indexFound);
				checkString = checkString.Trim('\n', '\r', ' ');


				bool allGood = checkCharacter(checkString);

				if (allGood == false)
				{
					return null;
				}


			}
			else if ((ScdIndexFound != -one) && (indexFound == -one))
			{

                ret = input.Replace(Tokens.time, DateTime.Now.ToString("HH:mm:ss tt"));
				string checkString = input.Substring(ScdIndexFound + 6);
				checkString += input.Substring(zero, ScdIndexFound);
				checkString = checkString.Trim('\n', '\r', ' ');

				bool allGood = checkCharacter(checkString);

				if (allGood == false)
				{
					return null;
				}
			}
			ret = ret.Trim('\n', '\r', ' ');
			return ret;
		}




		/// <summary>
		/// This method check if there is any operation in the string ( + - *). 
		/// </summary>
		/// <param name="inputStr">input string to be checked</param>
		/// <returns> return result if any operation was found and the values are valid and null otherwise.</returns>
        public static string checkOperation(string inputStr)
		{
            inputStr = inputStr.Trim(' ','\n','\r');
            int firstIndexFnd = inputStr.IndexOf(Tokens.additions);
            int secondIndexFnd = inputStr.IndexOf(Tokens.substraction);
            int thirdIndexFnd = inputStr.IndexOf(Tokens.multiplication);
			string ret = string.Empty;

			/*check for addition*/
			if ((firstIndexFnd != -one) && (secondIndexFnd == -one) && (thirdIndexFnd == -one))
			{
				string[] addVal = new string[] { Tokens.additions };
				string[] values = inputStr.Split(addVal, StringSplitOptions.None);

				ret = parseValuesAndCalculate(values[zero], values[one], add);

				if (ret == null)
				{
					return null;
				}


			} /*check for subtraction*/
			else if ((firstIndexFnd == -one) && (secondIndexFnd != -one) && (thirdIndexFnd == -one))
			{

                string[] subtractVal = new string[] { Tokens.substraction };
				string[] values = inputStr.Split(subtractVal, StringSplitOptions.None);

                ret = parseValuesAndCalculate(values[zero], values[one],substract);

				if (ret == null)
				{
					return null;
				}
			}  /*check for multiplication.*/
			else if ((firstIndexFnd == -one) && (secondIndexFnd == -one) && (thirdIndexFnd != -one))
			{

                string[] multiplyVal = new string[] { Tokens.multiplication };
				string[] values = inputStr.Split(multiplyVal, StringSplitOptions.None);

				ret = parseValuesAndCalculate(values[zero], values[one], multiply);

				if (ret == null)
				{
					return null;
				}
			}
			else
			{
				return null;
			}



			return ret;
		}


		/// <summary>
		/// This method takes the string values and tries to extract the values ( if ints ) otherwise will return
		/// null
		/// </summary>
		/// <param name="valueF">first integer</param>
		/// <param name="valueS">second integer</param>
		/// <param name="command"> command in our case add/substract/multiplication</param>
		/// <returns></returns>
        public static string parseValuesAndCalculate(string valueF, string valueS, string command)
		{
			Int64 var = 0;
			Int64 var2 = 0;
			Int64 result = 0;
			string ret = string.Empty;
			if (!Int64.TryParse(valueF, out  var))
			{
				return null;
			}
			if (!Int64.TryParse(valueS, out  var2))
			{
				return null;
			}



			switch (command)
			{
				case add:
					result = var + var2;
					break;

				case substract:
					result = var - var2;
					break;

				case multiply:
					result = var * var2;
					break;

				default:
					break;
			}

			ret = result.ToString();

			return ret;
		}







		/// <summary>
		/// check the input string for spaces , if there is anything alse but spaces
		/// will return false;
		/// </summary>
		/// <param name="value">input string to check</param>
		/// <returns>true if all values are spaces other wise will return false.</returns>
        public static bool checkCharacter(string value)
		{
			if (value == null)
			{
				return true;
			}

			for (int i = 0; i < value.Length; i++)
			{

				if (!char.IsWhiteSpace(value[i]))
				{
					return false;
				}

			}
			return true;
		}

	}

}
