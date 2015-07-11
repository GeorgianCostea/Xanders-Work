package com.example.sketchpadmememaker;


import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FilenameFilter;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.Gallery;
import android.widget.ImageView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;


public class MainActivity extends Activity
{

	private static final String JPEG_FILE_PREFIX = "IMG_";
	private static final String JPEG_FILE_SUFFIX = ".jpg";
	private static final int ACTION_TAKE_PHOTO_B = 1;
	private DrawingView myImageView;
    private AlbumStorageDirFactory mAlbumStorageDirFactory = null;
	private String mCurrentPhotoPath;
	@SuppressWarnings("deprecation")
	private Gallery gallery;
	private Uri[] mUrls;  
    private String[] mFiles=null;
    
    
    
    @SuppressWarnings("deprecation")
	@Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        myImageView = (DrawingView)findViewById(R.id.ViewImages);

        gallery = (Gallery) findViewById(R.id.GalleryImages);
        Button picBtn = (Button) findViewById(R.id.btnTakePicture);
		setBtnListenerOrDisable( 
				picBtn, 
				mTakePicOnClickListener,
				MediaStore.ACTION_IMAGE_CAPTURE
		);
        
        mAlbumStorageDirFactory = new BaseAlbumDirFactory();
        
        methodPopulateGaleryView();
        
        gallery.setOnItemClickListener(new OnItemClickListener() {
            public void onItemClick(AdapterView<?> parent, View v, int position, long id) {
                Toast.makeText(MainActivity.this, "Your selected Image " + position, Toast.LENGTH_SHORT).show();
                mCurrentPhotoPath = mFiles[position];
        	    myImageView.clear();
                setPic();
    			methodPopulateGaleryView();
            }
        }); 

    }

    public void saveDrawing(View view)
    {    
    	File folder = new File(Environment.getExternalStorageDirectory().toString());
        boolean success = false;
        if (!folder.exists()) 
        {
        	success = folder.mkdirs();
        }
        
        System.out.println(success+"folder");
        File file = new File(mCurrentPhotoPath);
        if ( !file.exists() )
        {
        	try 
            {
        		success = file.createNewFile();
            } 
            catch (IOException e) 
            {
            	e.printStackTrace();
            }
        }
        System.out.println(success+"file");
        FileOutputStream ostream = null;
             try
             {
            	 ostream = new FileOutputStream(file);

            	 System.out.println(ostream);
            	 Bitmap well = myImageView.getBitmap();
           
	             if(well.compress(Bitmap.CompressFormat.PNG, 100, ostream))
	             {
	            	 Toast.makeText(getApplicationContext(), "Picture Saved", Toast.LENGTH_SHORT).show();
	             }
             
             }
             catch (NullPointerException e) 
             {
                 e.printStackTrace();
                 Toast.makeText(getApplicationContext(), "Null error", Toast.LENGTH_SHORT).show();
             }
             catch (FileNotFoundException e) 
             {
                 e.printStackTrace();
                 Toast.makeText(getApplicationContext(), "File error", Toast.LENGTH_SHORT).show();
             }
             catch (IOException e) 
             {
                 e.printStackTrace();
                 Toast.makeText(getApplicationContext(), "IO error", Toast.LENGTH_SHORT).show();
             }
    }
    
    public void methodPopulateGaleryView()
    {
    
    	File images = getAlbumDir()  ;
        File[] imagelist = images.listFiles(new FilenameFilter(){  
        		 
   	       	public boolean accept(File dir, String name)  
   	        {  
   	            return ((name.endsWith(".jpg"))||(name.endsWith(".png")));
   	        }  
        });  
       
        mFiles = new String[imagelist.length];  
           
    
     
   	    for(int i= 0 ; i< imagelist.length; i++)  
   	    {  
   	    	mFiles[i] = imagelist[i].getAbsolutePath();  
   	    }  
   	    mUrls = new Uri[mFiles.length];  
   	  
   	    for(int i=0; i < mFiles.length; i++)  
   	    {  
   	    	mUrls[i] = Uri.parse(mFiles[i]);     
   	    }         
        gallery.setAdapter(new ImageAdapter(this));
           
    }
    
    public class ImageAdapter extends BaseAdapter
    {  
        
        int mGalleryItemBackground;  
        public ImageAdapter(Context c)  {     
            mContext = c;
        }  
        public int getCount(){  
            return mUrls.length;  
        }  
        public Object getItem(int position){  
            return position;  
        }  
        public long getItemId(int position) {  
            return position;  
        }  
        @SuppressWarnings("deprecation")
		public View getView(int position, View convertView, ViewGroup parent){  
            ImageView i = new ImageView(mContext);  
            i.setImageURI(mUrls[position]);  
            i.setScaleType(ImageView.ScaleType.FIT_XY);  
            i.setLayoutParams(new Gallery.LayoutParams(200, 200));  
            return i;  
                 
        }     
        private Context mContext;        
    } 
    
    @Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
    	if (resultCode == RESULT_OK) {
    		handleBigCameraPhoto();
		} 
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
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
    
    @Override
	protected void onRestoreInstanceState(Bundle savedInstanceState) {
		super.onRestoreInstanceState(savedInstanceState);
		methodPopulateGaleryView();
	}
    public static boolean isIntentAvailable(Context context, String action) {
		final PackageManager packageManager = context.getPackageManager();
		final Intent intent = new Intent(action);
		List<ResolveInfo> list =
			packageManager.queryIntentActivities(intent,
					PackageManager.MATCH_DEFAULT_ONLY);
		return list.size() > 0;
	}
    private void setBtnListenerOrDisable( 
			Button btn, 
			Button.OnClickListener onClickListener,
			String intentName
	) {
		if (isIntentAvailable(this, intentName)) {
			btn.setOnClickListener(onClickListener);        	
		} else {
			btn.setText( 
				getText(R.string.cannot).toString() + " " + btn.getText());
			btn.setClickable(false);
		}
	}
	/* Photo album for this application */
	private String getAlbumName() {
		return getString(R.string.album_name);
	}
	
	private File getAlbumDir() {
		File storageDir = null;

		if (Environment.MEDIA_MOUNTED.equals(Environment.getExternalStorageState())) {
			
			storageDir = mAlbumStorageDirFactory.getAlbumStorageDir(getAlbumName());

			if (storageDir != null) {
				if (! storageDir.mkdirs()) {
					if (! storageDir.exists()){
						Log.d("CameraSample", "failed to create directory");
						return null;
					}
				}
			}
			
		} else {
			Log.v(getString(R.string.app_name), "External storage is not mounted READ/WRITE.");
		}
		
		return storageDir;
	}
	private File createImageFile() throws IOException {
		// Create an image file name
		String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").format(new Date());
		String imageFileName = JPEG_FILE_PREFIX + timeStamp + "_";
		File albumF = getAlbumDir();
		File imageF = File.createTempFile(imageFileName, JPEG_FILE_SUFFIX, albumF);
		return imageF;
	}
	private File setUpPhotoFile() throws IOException {
			
			File f = createImageFile();
			mCurrentPhotoPath = f.getAbsolutePath();
			
			return f;
	}
	
	private void setPic() {

		/* There isn't enough memory to open up more than a couple camera photos */
		/* So pre-scale the target bitmap into which the file is decoded */

		/* Get the size of the ImageView */
		int targetW = myImageView.getWidth();
		int targetH = myImageView.getHeight();

		/* Get the size of the image */
		BitmapFactory.Options bmOptions = new BitmapFactory.Options();
		bmOptions.inJustDecodeBounds = true;
		BitmapFactory.decodeFile(mCurrentPhotoPath, bmOptions);
		int photoW = bmOptions.outWidth;
		int photoH = bmOptions.outHeight;
		
		/* Figure out which way needs to be reduced less */
		int scaleFactor = 1;
		if ((targetW > 0) || (targetH > 0)) {
			scaleFactor = Math.min(photoW/targetW, photoH/targetH);	
		}

		/* Set bitmap options to scale the image decode target */
		bmOptions.inJustDecodeBounds = false;
		bmOptions.inSampleSize = scaleFactor;
		bmOptions.inPurgeable = true;

		/* Decode the JPEG file into a Bitmap */
		Bitmap bitmap = BitmapFactory.decodeFile(mCurrentPhotoPath, bmOptions);
		/* Associate the Bitmap to the ImageView */
		myImageView.setImageBitmap(bitmap);
		myImageView.setVisibility(View.VISIBLE);

	}
	
	
	private void galleryAddPic() {
	    Intent mediaScanIntent = new Intent("android.intent.action.MEDIA_SCANNER_SCAN_FILE");
		File f = new File(mCurrentPhotoPath);
	    Uri contentUri = Uri.fromFile(f);
	    mediaScanIntent.setData(contentUri);
	    this.sendBroadcast(mediaScanIntent);
	}
	private void dispatchTakePictureIntent() {

		Intent takePictureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);

			File f = null;
			
			try {
				f = setUpPhotoFile();
				mCurrentPhotoPath = f.getAbsolutePath();
				takePictureIntent.putExtra(MediaStore.EXTRA_OUTPUT, Uri.fromFile(f));
			} catch (IOException e) {
				e.printStackTrace();
				f = null;
				mCurrentPhotoPath = null;
			}
			
		startActivityForResult(takePictureIntent,ACTION_TAKE_PHOTO_B);
	}
	private void handleBigCameraPhoto() {

		if (mCurrentPhotoPath != null) {
			setPic();
			galleryAddPic();
			methodPopulateGaleryView();
			mCurrentPhotoPath = null;
		}

	}
	Button.OnClickListener mTakePicOnClickListener = 
			new Button.OnClickListener() {
			@Override
			public void onClick(View v) {
				dispatchTakePictureIntent();
			}
		};
}
