<?php
/*
 FileName    : pad.php
 Project     : Assignment 5 web dev
 Date        : 25/11/2012
 Programmer  : Georgian Costea
 Description :The following file open, appends and closes the files that user
 is working with, also it read all files that are saved by the user from "C:/temp/GeorgianCostea"
 This is an assumption that i`ve made. If the folder "temp" is not there then i will create one on C drive
 same with the GeorgianCostea folder.
 
 */



$q = $_GET["q"];
/*parse the request into different elements.*/
$elements= split("!",$q);
$folder_path="C:/temp/GeorgianCostea";
$temp ="C:/temp";
$command = $elements[0];
$fileName =$elements[1];
$body = $elements[2];



	
	/*create the directory if it does not exists.*/
	 if (!file_exists($temp)) 
	 { 
		mkdir($folder_temp) or die("Can`t create folder");
   
	 }  
	 else
	 {
	 	if(!file_exists($folder_path))
		{
	    	mkdir($folder_path) or die("Can`t create folder"); 
	    }
	 }	


	
	switch($command)
	{
		case "open":
		/*open a file read that file , and send the 
		content back to the client side.*/
			$filePath = $folder_path ."/" .$fileName;
			
			if(file_exists($filePath))
			{
				$pFile = fopen($filePath,"r") or exit("enable to open file!");
				
				if($pFile)
				{
					while(!feof($pFile))
					{
						$line = fgets($pFile,1024);
						$buffer = $buffer.$line ;
							
					}	
				}
				else
				{
					echo "Can`t open file";
				}
				
			}
			else
			{
				echo "The File does not exist <br />";
			}
			fclose($pFile);
			echo "open&".$buffer;
			
		break;
		case "save":
		
		/*open a file write to that file , and send the 
		a comfirmation back to the client side.*/

			$filePath = $folder_path ."/" .$fileName;
						
			if(file_exists($filePath))
			{
				$pFile = fopen($filePath,"w") or exit("Enable to open file for writing!");
				
				if($pFile)
				{
						fwrite($pFile,$body);
												
				}
				else
				{
					echo "Can`t open file";
				}
				
			}
			else
			{
				echo "The File does not exist <br />";
			}
			fclose($pFile);
			
			/*read all files in this folder*/
			if($handle = opendir($folder_path))
			{
				while (false !== ($entry = readdir($handle)))
				{
					if(($entry != "." )&&( $entry !=".."))
					{
						$listOfFiles= $listOfFiles.$entry."%";
		        				
		        	}
		        		
		    	}
			}
			echo "save&".$listOfFiles;
			
			
		break;
		case "saveas":
			
			/*create the file write to that file , and send the 
		a comfirmation back to the client side.*/

			$filePath = $folder_path ."/" .$fileName;
			
			$pFile = fopen($filePath,"w") or exit("Enable to open file for writing!");
				
			if($pFile)
			{
					fwrite($pFile,$body);
												
			}
			else
			{
				echo "Can`t open file";
			}
			
			/*read all files in this folder*/
			if($handle = opendir($folder_path))
			{
				while (false !== ($entry = readdir($handle)))
				{
					if(($entry != "." )&&( $entry !=".."))
					{
						$listOfFiles= $listOfFiles.$entry."%";
		        				
		        	}
		        		
		    	}
			}
			
			
			echo "saveas&".$listOfFiles;
			
		break;
		
	}
	
	

?>