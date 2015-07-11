package com.example.gpslatlong;

import java.io.BufferedWriter;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.Socket;

import android.os.AsyncTask;
import android.widget.TextView;

public class senderClass extends AsyncTask{

private Socket socket;

	
	
	//port being used for connection
	private static final int SERVERPORT = 2380;

	//ip address used by server
	private static final String SERVER_IP = " 169.254.238.40";

	 
	@Override
	protected Object doInBackground(Object... params) {
		String msg = params[0].toString();
		
		MainActivity me = (MainActivity)params[1];
		try{
		InetAddress serverAddr = InetAddress.getByName(SERVER_IP);
		socket = new Socket(SERVER_IP, SERVERPORT);
		PrintWriter out = new PrintWriter(new BufferedWriter(
				new OutputStreamWriter(socket.getOutputStream())),true);
		out.println(msg);
		
		}
		catch(Exception e)
		{
			me.sayIt(e.toString());
		}
		return null;
	}

}
