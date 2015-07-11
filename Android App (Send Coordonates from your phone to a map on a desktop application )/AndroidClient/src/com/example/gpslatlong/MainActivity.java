package com.example.gpslatlong;

import java.net.InetAddress;

import android.os.Bundle;
import android.location.Location; 
import android.location.LocationListener; 
import android.location.LocationManager; 
import android.os.Bundle; 
import android.app.Activity; 
import android.content.Context; 
import android.content.Intent;
import android.widget.TextView;
import android.app.Activity;
import android.view.Menu;
import android.view.View;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.Socket;
import java.net.SocketException;
import java.net.UnknownHostException;
import java.util.Enumeration;



import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.Switch;
import android.widget.Toast;
import android.widget.ToggleButton;
import android.app.Activity;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.provider.MediaStore;
import android.text.format.Formatter;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnTouchListener;
import android.widget.EditText;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.app.Activity;
import android.content.Context;
import android.widget.TextView;
public class MainActivity extends Activity {

	private Socket socket;

	
	
	//port being used for connection
	private static final int SERVERPORT = 2380;

	//ip address used by server
	private static final String SERVER_IP = " 169.254.238.40";
	
	 
	LocationManager myLocationManager1;
	String PROVIDER1 = LocationManager.GPS_PROVIDER;
	private WifiManager wifiManager;
	
	TextView testViewStatus; 
	TextView textViewLatitude; 
	TextView textViewLongitude; 
	 
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		testViewStatus = (TextView)findViewById(R.id.status); 
		 textViewLatitude = (TextView)findViewById(R.id.latitude); 
		 textViewLongitude = (TextView)findViewById(R.id.longitude); 
		 
		 myLocationManager1 = (LocationManager)getSystemService(Context.LOCATION_SERVICE); 
		 
		 //get last known location, if available 
		 Location location = myLocationManager1.getLastKnownLocation(PROVIDER1); 
		 showMyLocation(location);	
		 Button d = (Button) findViewById(R.id.button1);
		 d.setOnClickListener(new View.OnClickListener() {

	            @Override
	            public void onClick(View v) {
	                //Intent cameraIntent = new Intent(android.provider.MediaStore.ACTION_IMAGE_CAPTURE); 
	               // startActivityForResult(cameraIntent, CAMERA_REQUEST); 
	           	goLatLong(v);
	            }
	        });
	}
	
	
	@Override 
	 protected void onPause() { 
	 // TODO Auto-generated method stub 
	 super.onPause(); 
	 myLocationManager1.removeUpdates(myLocationListener); 
	 } 

	private LocationListener myLocationListener 
	 = new LocationListener(){ 
	 
	 @Override 
	 public void onLocationChanged(Location location) { 
	 showMyLocation(location); 
	 } 
	 
	 @Override 
	 public void onProviderDisabled(String provider) { 
	 // TODO Auto-generated method stub 
	 
	 } 
	 
	 @Override 
	 public void onProviderEnabled(String provider) { 
	 // TODO Auto-generated method stub 
	 
	 } 
	 
	 @Override 
	 public void onStatusChanged(String provider, int status, Bundle extras) { 
	 // TODO Auto-generated method stub 
	 
	 }};
	
	@Override 
	 protected void onResume() { 
	 // TODO Auto-generated method stub 
	 super.onResume(); 
	 myLocationManager1.requestLocationUpdates( 
	 PROVIDER1, //provider 
	 0, //minTime 
	 0, //minDistance 
	 myLocationListener); //LocationListener 
	 }
	
	private void showMyLocation(Location l){ 
		
		 if(l == null) 
		 { 
		 testViewStatus.setText("No Location!"); 
		 } 
		
		 else 
		 { 
			 testViewStatus.setText("Connected");
		 textViewLatitude.setText("" + l.getLatitude()); 
		 textViewLongitude.setText("" + l.getLongitude()); 
		 } 
		 
		 }

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}
	
	
	public void goLatLong(View view)
	{
		try {

			/*InetAddress serverAddr = InetAddress.getByName(SERVER_IP);
			socket = new Socket(SERVER_IP, SERVERPORT);
			*/
			TextView Lat = (TextView) findViewById(R.id.latitude);

			TextView Lon = (TextView) findViewById(R.id.longitude);

			TextView message = (TextView)findViewById(R.id.status);
			
			//store the GPS coordinates in a string from the textview
			String str2 = Lat.getText().toString();
			String strLong = Lon.getText().toString();

			//variable to write to the server by geting the socket both are communicating on
			String msg = "$Nothing to report$"+str2+"$"+strLong;
			new senderClass().execute(msg, this);
			
		/*	PrintWriter out = new PrintWriter(new BufferedWriter(
			new OutputStreamWriter(socket.getOutputStream())),true);

			//send the information to the server
			out.println();
			message.setText("Nothing To Report");
*/
			
			
			} catch (Exception e) {
				TextView message = (TextView)findViewById(R.id.status);
				message.setText(e.toString());
			} 
			
	}
	
	public void sayIt(String msg)
	{
		TextView message = (TextView)findViewById(R.id.status);
		message.setText(msg);
	}
	
	
}



