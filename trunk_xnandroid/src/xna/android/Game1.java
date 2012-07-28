package xna.android;

import android.app.Activity;
import android.content.Context;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.opengl.GLSurfaceView;
import android.util.Log;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import br.com.R;

public class Game1 extends GLSurfaceView implements SurfaceHolder.Callback
{
	
	private static final String TAG = Game1.class.getSimpleName();
	
	private MainThread thread;
	
	private Sprite bola;
	
	private Sprite barra;
	public Game1(Context Content, GLSurfaceView glSurfaceView) {
		super(Content);

		getHolder().addCallback(this);
		
		// a imagem:
		bola = new Sprite(BitmapFactory.decodeResource(getResources(),
				R.drawable.bola), 50, 50);
		barra = new Sprite(BitmapFactory.decodeResource(getResources(),
				R.drawable.barra),
				getWidth()/2,
				430);
		
				
		// cria o loop principal
		thread = new MainThread(getHolder(), this);
		// passando o holder e o painel para a thread

		setFocusable(true);
	}

	// @Override
	public void surfaceChanged(SurfaceHolder holder, int format, int width,
			int height) {

	}

	// @Override
	public void surfaceCreated(SurfaceHolder holder) {
		thread.setRunning(true);
		
		thread.start();
	}

	// @Override
	public void surfaceDestroyed(SurfaceHolder holder) {
		boolean retry = true;
		while (retry) {
			try {
				thread.join();
				retry = false;
			} catch (InterruptedException e) {
				// tenta de novo interromper
			}
		}
	}

	@Override
	public boolean onTouchEvent(MotionEvent event)
	{
		if (event.getAction() == MotionEvent.ACTION_DOWN)
		{
			// controle de evento com o sprite
			//bola.handleActionDown((int) event.getX(), (int) event.getY());
			barra.handleActionDown((int) event.getX(), (int) event.getY());

			// checagem do toque embaixo da tela
//			if (event.getY() > getHeight() - 50)
//			{
//				thread.setRunning(false);
//				((Activity) getContext()).finish();
//				
//				//TODO checar erro aqui
//			}
//			else
//			{
//				Log.d(TAG, "Coords: x=" + event.getX() + ",y=" + event.getY());
//			}
		}
		
		if (event.getAction() == MotionEvent.ACTION_MOVE)
		{
			// gestos
//			if (bola.isTouched())
//			{
//				// tocado!
//				bola.setX((int) event.getX());
//				bola.setY((int) event.getY());
//			}
			
			if (barra.isTouched())
			{
				// tocado!
				barra.setX((int) event.getX());
				//barra.setY((int) event.getY());
			}
			
		}
		
		if (event.getAction() == MotionEvent.ACTION_UP)
		{
			// larguei
//			if (bola.isTouched())
//			{
//				bola.setTouched(false);
//			}
			
			if (barra.isTouched())
			{
				barra.setTouched(false);
			}
		}
		
		return true;
	}

	// sobrecarregando o método onDraw
	@Override
	protected void onDraw(Canvas spriteBatch)
	{
		
		// preenchimento
		spriteBatch.drawColor(Color.BLACK);
		sprite.draw(spriteBatch);
//		sprite2.draw(spriteBatch);
//		sprite3.draw_rotate(spriteBatch, 0);
		aviao.draw(spriteBatch);
		//aviao.draw(gl)
        //seta a cor e a fonte do texto		
		Paint p = new Paint();
		
		bola.draw(spriteBatch);
		barra.draw(spriteBatch);
        	
	}

	public void Update()
	{

		bola.update(getWidth(), getHeight());
		
		//barra.update(getWidth(), getHeight());

		if (bola.intersects(barra.getX(), barra.getY(), barra
				.getBitmap().getWidth(), barra.getBitmap().getHeight())&&bola.isVisible())
		{
			bola.setVisible(false);
			
			
		}

		
		
	}
	
	public void Draw(Canvas canvas)
	{
		onDraw(canvas);
	}
}
