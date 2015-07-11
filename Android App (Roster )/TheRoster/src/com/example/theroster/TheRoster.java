package com.example.theroster;

import java.util.Calendar;


import android.support.v7.app.ActionBarActivity;


import android.app.DatePickerDialog;
import android.app.Dialog;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.RadioGroup;
import android.widget.SeekBar;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.SeekBar.OnSeekBarChangeListener;
import android.widget.Toast;
import android.view.View.OnClickListener;

public class TheRoster extends ActionBarActivity implements OnClickListener {

	private SeekBar shirtSeekBar;
	private SeekBar pantsSeekBar;
	private SeekBar shoeSeekBar;
	private EditText nameEditText;
	private CheckBox checkBox;
	private Spinner spinner;
	private RadioGroup radioGroup;
	private TextView pantsIndex;
	private TextView shoeIndex;
	private TextView shirtIndex;
	
	private ImageButton ib;
	private Calendar cal;
	private int day;
	private int month;
	private int year;
	private TextView et;
	
	
	public static final String PREFS_NAME = "MyPrefsFile";
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_the_roster);
        
        ib = (ImageButton) findViewById(R.id.ImageButton);
		cal = Calendar.getInstance();
		day = cal.get(Calendar.DAY_OF_MONTH);
		month = cal.get(Calendar.MONTH);
		year = cal.get(Calendar.YEAR);
		et = (TextView) findViewById(R.id.BirthDayTxt);
		ib.setOnClickListener(this);
        
		
        shirtSeekBar = (SeekBar)findViewById(R.id.shirtSeekBar);
        pantsSeekBar = (SeekBar)findViewById(R.id.pantsSeekBar);
        shoeSeekBar = (SeekBar)findViewById(R.id.ShoeSeekBar);
        nameEditText = (EditText) findViewById(R.id.txtboxName);
        checkBox = (CheckBox)findViewById(R.id.ThinksAreGoing);
    	spinner = (Spinner)findViewById(R.id.colorEyeSpinner);
        radioGroup =(RadioGroup) findViewById(R.id.radioGroup1);
        shirtIndex = (TextView)findViewById(R.id.shirtSeekBarInfoLbl);
        pantsIndex = (TextView)findViewById(R.id.pantsSeekBarInfoLbl);
        shoeIndex = (TextView)findViewById(R.id.ShoeSeekBarInfoLbl);
        
        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this,
                R.array.EyeColors, android.R.layout.simple_spinner_item);
        // Specify the layout to use when the list of choices appears
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        // Apply the adapter to the spinner
        spinner.setAdapter(adapter);
        
        SharedPreferences prefs = getSharedPreferences(PREFS_NAME,MODE_PRIVATE);
        
        nameEditText.setText(prefs.getString("Name",""));
        checkBox.setChecked(prefs.getBoolean("CheckBox",false));
        spinner.setSelection(prefs.getInt("Spinner",0));
        et.setText(prefs.getString("Date",""));
        radioGroup.check(prefs.getInt("Radio",0));
        pantsIndex.setText(prefs.getString("pants","0"));
        if(pantsIndex.toString() != null && !pantsIndex.toString().isEmpty())
        {
        	shirtSeekBar.setProgress(getInteger(pantsIndex.toString()));
        }
        shoeIndex.setText(prefs.getString("Shoe", "0"));
        if(shoeIndex.toString() != null && !shoeIndex.toString().isEmpty())
        {
        	shoeSeekBar.setProgress(getInteger(shoeIndex.toString()));
        }
        shirtIndex.setText(prefs.getString("shirt","0"));
        if(shirtIndex.toString() != null && !shirtIndex.toString().isEmpty())
        {
        	shirtSeekBar.setProgress(getInteger(shirtIndex.toString()));
        }

        
        shirtSeekBar.setOnSeekBarChangeListener(new OnSeekBarChangeListener() {       

            @Override       
            public void onStopTrackingTouch(SeekBar seekBar) {      
                // TODO Auto-generated method stub      
            }       

            @Override       
            public void onStartTrackingTouch(SeekBar seekBar) {     
                // TODO Auto-generated method stub      
            }       

            @Override       
            public void onProgressChanged(SeekBar seekBar, int progress,boolean fromUser) {     
                // TODO Auto-generated method stub      
            	TextView tv = (TextView)findViewById(R.id.shirtSeekBarInfoLbl);

            	   tv.setText(Integer.toString(progress));
            }
        });
        pantsSeekBar.setOnSeekBarChangeListener(new OnSeekBarChangeListener() {       

            @Override       
            public void onStopTrackingTouch(SeekBar seekBar) {      
                // TODO Auto-generated method stub      
            }       

            @Override       
            public void onStartTrackingTouch(SeekBar seekBar) {     
                // TODO Auto-generated method stub      
            }       

            @Override       
            public void onProgressChanged(SeekBar seekBar, int progress,boolean fromUser) {     
                // TODO Auto-generated method stub      
            	TextView tv = (TextView)findViewById(R.id.pantsSeekBarInfoLbl);

            	   tv.setText(Integer.toString(progress));
            }
        });
        shoeSeekBar.setOnSeekBarChangeListener(new OnSeekBarChangeListener() {       

            @Override       
            public void onStopTrackingTouch(SeekBar seekBar) {      
                // TODO Auto-generated method stub      
            }       

            @Override       
            public void onStartTrackingTouch(SeekBar seekBar) {     
                // TODO Auto-generated method stub      
            }       

            @Override       
            public void onProgressChanged(SeekBar seekBar, int progress,boolean fromUser) {     
                // TODO Auto-generated method stub      
            	TextView tv = (TextView)findViewById(R.id.ShoeSeekBarInfoLbl);

            	   tv.setText(Integer.toString(progress + 4));
            }
        });
 
    }
 
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.the_roster, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
    
    @SuppressWarnings("deprecation")
	@Override
	public void onClick(View v) {
		showDialog(0);
	}

	@Override
	@Deprecated
	protected Dialog onCreateDialog(int id) {
		return new DatePickerDialog(this, datePickerListener, year, month, day);
	}
	private DatePickerDialog.OnDateSetListener datePickerListener = new DatePickerDialog.OnDateSetListener() {
		public void onDateSet(DatePicker view, int selectedYear,
				int selectedMonth, int selectedDay) {
			et.setText(selectedDay + " / " + (selectedMonth + 1) + " / "
					+ selectedYear);
		}
	};
	
	public void UpdateInformation(View V)
	{
		//Save all the settings
		SharedPreferences settings = getSharedPreferences(PREFS_NAME, MODE_PRIVATE);
		SharedPreferences.Editor editor = settings.edit();
		
		editor.putString("Name",nameEditText.toString());
		editor.putBoolean("CheckBox",checkBox.isChecked());
		editor.putInt("Spinner",spinner.getSelectedItemPosition());
		editor.putString("Date",et.toString());
		editor.putInt("Radio", radioGroup.getCheckedRadioButtonId());
		editor.putString("pants",pantsIndex.toString());
		editor.putString("Shoe", shoeIndex.toString());
		editor.putString("shirt",shirtIndex.toString());
		editor.commit();
		
		Toast.makeText(getApplicationContext(), "Information Saved!",Toast.LENGTH_LONG).show();
		
	}
	public int getInteger(String s)
	{
		Integer d =0;
		try
		{
			d= Integer.parseInt(s);
		}
		catch(NumberFormatException nfe)
		{
			d= 0;
		}
		return d;
	}
 
}


