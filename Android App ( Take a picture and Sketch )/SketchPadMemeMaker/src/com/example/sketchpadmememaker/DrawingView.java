package com.example.sketchpadmememaker;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.graphics.AvoidXfermode.Mode;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Path;
import android.graphics.Typeface;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.ImageView;

public class DrawingView extends ImageView {

    public int width;
    public  int height;
    private Bitmap  mBitmap;
    private Canvas  mCanvas;
    private Canvas mTest;
    private Path    mPath;
    private Paint   mBitmapPaint;
    private Context context;
    private Paint circlePaint;
    private Path circlePath;
    private Paint mPaint;
    private Paint mWritePaint;
    private String inputString= "";
    private float mX, mY;
    private static final float TOUCH_TOLERANCE = 1;
    
    public DrawingView(Context c,AttributeSet attrs ) {
    super(c,attrs);
	    context=c;
	    mPath = new Path();
	    mBitmapPaint = new Paint(Paint.DITHER_FLAG);  
	    circlePaint = new Paint();
	    circlePath = new Path();
	    circlePaint.setAntiAlias(true);
	    circlePaint.setColor(Color.BLUE);
	    circlePaint.setStyle(Paint.Style.STROKE);
	    circlePaint.setStrokeJoin(Paint.Join.MITER);
	    circlePaint.setStrokeWidth(4f); 
	   
	    mPaint = new Paint();
	    mPaint.setAntiAlias(true);
	    mPaint.setDither(true);
	    mPaint.setColor(Color.GREEN);
	    mPaint.setStyle(Paint.Style.STROKE);
	    mPaint.setStrokeJoin(Paint.Join.ROUND);
	    mPaint.setStrokeCap(Paint.Cap.ROUND);
	    mPaint.setStrokeWidth(12); 
	    
	    Typeface tf = Typeface.create("Helvetica",Typeface.BOLD);
	    mWritePaint = new Paint();
	    mWritePaint.setAntiAlias(true);
	    mWritePaint.setColor(Color.BLACK);
	    mWritePaint.setTypeface(tf);
	    mWritePaint.setTextSize((float) 50.0);

    }

    @Override
    protected void onSizeChanged(int w, int h, int oldw, int oldh) {
    super.onSizeChanged(w, h, oldw, oldh);
    
   
    	mBitmap = Bitmap.createBitmap(w, h, Bitmap.Config.ARGB_8888);
    	mCanvas = new Canvas(mBitmap);
    	mTest = new Canvas(mBitmap);

    }
    @Override
    protected void onDraw(Canvas canvas) {
    super.onDraw(canvas);

    	canvas.drawBitmap( mBitmap, 0, 0, mBitmapPaint);

    	canvas.drawPath( mPath,  mPaint);

    	canvas.drawPath( circlePath,  circlePaint);
    	
    
    }
    
   
  

    private void touch_start(float x, float y) 
    {
	    mPath.reset();
	    mPath.moveTo(x, y);
	    mX =x;
	    mY =y;
    }
    private void touch_move(float x, float y) 
    {
	    float dx = Math.abs(x - mX);
	    float dy = Math.abs(y - mY);
	    if (dx >= TOUCH_TOLERANCE || dy >= TOUCH_TOLERANCE) {
	         mPath.quadTo(mX, mY, (x + mX)/2, (y + mY)/2);
	        mX = x;
	        mY = y;
	
	        circlePath.reset();
	        circlePath.addCircle(mX, mY, 30, Path.Direction.CW);
	
	    }
	    else
	    {
	    	drawtext();
	    }
	    
    }
    
    private void drawtext()
    {
    	
    	
    	LayoutInflater li = LayoutInflater.from(context);
		View promptsView = li.inflate(R.layout.prompts, null);

		AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(context);

			// set prompts.xml to alertdialog builder
		alertDialogBuilder.setView(promptsView);

		final EditText userInput = (EditText) promptsView.findViewById(R.id.editTextDialogUserInput);

			// set dialog message
		alertDialogBuilder
			.setCancelable(false)
			.setPositiveButton("OK",
					new DialogInterface.OnClickListener() {
					    public void onClick(DialogInterface dialog,int id) {
						
					    	inputString = userInput.getText().toString();
					    	mTest.drawText(inputString, mX, mY, mWritePaint);
					    	invalidate();
					    }
				  })
		    .setNegativeButton("Cancel",
		    		new DialogInterface.OnClickListener() {
					    public void onClick(DialogInterface dialog,int id) {
						dialog.cancel();
					    }
				  });

			// create alert dialog
			AlertDialog alertDialog = alertDialogBuilder.create();

			// show it
			alertDialog.show();

    	
    }
    
    private void touch_up() 
    {
    	mPath.lineTo(mX, mY);
    	circlePath.reset();
    	// commit the path to our offscreen
    	mCanvas.drawPath(mPath,  mPaint);
    	// kill this so we don't double draw
    	mPath.reset();
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) 
    {
    	float x = event.getX();
    	float y = event.getY();

        switch (event.getAction()) 
        {
            case MotionEvent.ACTION_DOWN:
                touch_start(x, y);
                invalidate();
                break;
            case MotionEvent.ACTION_MOVE:
                touch_move(x, y);
                invalidate();
                break;
            case MotionEvent.ACTION_UP:
            	touch_up();
            	invalidate();
            	
                break;
        }
        return true;
    }
    
    public Bitmap getBitmap()
    {
        
        this.setDrawingCacheEnabled(true);  
        this.buildDrawingCache();
        Bitmap bmp = Bitmap.createBitmap(this.getDrawingCache());   
        this.setDrawingCacheEnabled(false);
        return bmp;
    }
    public void clear()
    {
    	DrawingView d = (DrawingView)findViewById(R.id.ViewImages);
        mBitmap = Bitmap.createBitmap(d.getWidth(),d.getHeight() ,Bitmap.Config.ARGB_8888);

        mCanvas = new Canvas(mBitmap);
        mTest = new Canvas(mBitmap);
        mPath = new Path();
        mBitmapPaint = new Paint(Paint.DITHER_FLAG);

        //Added later..
        mPaint = new Paint();
        mPaint.setAntiAlias(true);
        mPaint.setDither(true);
        mPaint.setColor(Color.GREEN);
        mPaint.setStyle(Paint.Style.STROKE);
        mPaint.setStrokeJoin(Paint.Join.ROUND);
        mPaint.setStrokeCap(Paint.Cap.ROUND);
        mPaint.setStrokeWidth(12);
        invalidate();
    } 
}