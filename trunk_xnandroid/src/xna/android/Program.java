package xna.android;

import android.app.Activity;
import android.opengl.GLSurfaceView;
import android.os.Bundle;
import android.util.Log;
import android.view.Window;
import android.view.WindowManager;

public class Program extends Activity
{
	private static final String TAG = Program.class.getSimpleName();//

	private GLSurfaceView glSurfaceView;
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
        // desligando o titulo
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        // tela cheia
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        
        glSurfaceView = new GLSurfaceView(this);
        
        glSurfaceView.setRenderer(new GLRenderer());
        //setContentView(glSurfaceView);
        
        // main panel na view
        setContentView(new Game1(this));//TO DO colocar este GL pra render no game 1
        Log.d(TAG, "OK!");
        
	     
    }

    //ondestroy e onstop sobrecarregados apenas para fins de log...
    
 @Override
 protected void onDestroy() 
 {
  Log.d(TAG, "onDestroy.");
  super.onDestroy();
  //
 }

 @Override
 protected void onStop()
 {
  Log.d(TAG, "onStop.");
  super.onStop();
 }
 
@Override
protected void onResume() {
	super.onResume();
	glSurfaceView.onResume();
}

@Override
protected void onPause() {
	super.onPause();
	glSurfaceView.onPause();
}
	
 
}
