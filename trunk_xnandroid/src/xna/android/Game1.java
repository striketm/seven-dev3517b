package xna.android;

import br.com.R;
import android.app.Activity;
import android.content.Context;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Typeface;
import android.opengl.GLSurfaceView;
import android.util.Log;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.Window;
import android.view.WindowManager;
import xna.android.Explosion;
import android.content.res.AssetManager;

public class Game1 extends GLSurfaceView implements
		SurfaceHolder.Callback {
	
	private static final String TAG = Game1.class.getSimpleName();;
	
	private MainThread thread;
	
	private MySprite sprite;
//	
//	private Sprite sprite2;
//	
	private MySprite aviao;
//	
//	private SpriteAnimated sprite3;
//	
//	private SoundManager mSoundManager;
//	
//	private SpriteFont spriteFont; // a fonte	
//	
//	private Explosion explosion;
//		
//	private static final int EXPLOSION_SIZE = 200;
	
	public Game1(Context Content) {
		super(Content);

		getHolder().addCallback(this);
		
		// cria o loop principal
		//thread = new MainThread();
		thread = new MainThread(getHolder(), this);
		//passando o holder e o painel para a thread

				setFocusable(true);
		
//		mSoundManager = new SoundManager(Content);
		
		// a imagem:
		sprite = new MySprite(BitmapFactory.decodeResource(getResources(),R.drawable.android), 50, 50);
		
		//sprite2 = new Sprite(BitmapFactory.decodeResource(getResources(),
			//	R.drawable.android), 150, 150, mSoundManager);
		
		//sprite3 = new SpriteAnimated(BitmapFactory.decodeResource(
			//	getResources(), R.drawable.megamantransparente), 0, 0
																		
//				, 50, 50 //largura e altura
	//			, 8, 8); //FPS e numero de quadros
		
		aviao = new MySprite(BitmapFactory.decodeResource(getResources(), R.drawable.aviao_01), 150, 150);
		
		//sprite3.setX(200);
		//sprite3.setY(200);
		
		
		//this.spriteFont = new SpriteFont(BitmapFactory.decodeResource(getResources(), R.drawable.fonte_branca));
		//http://developer.android.com/index.html
		//http://developer.android.com/guide/index.html
		//http://stackoverflow.com/questions/1077357/can-the-android-drawable-directory-contain-subdirectories
		
		
	}
	
	//mover aviao
	public boolean onKeyDown(int KeyCode, KeyEvent event){
		
		switch(KeyCode)
		{
		case KeyEvent.KEYCODE_DPAD_RIGHT:
			aviao.setX(aviao.getX() + 2);
			
			if((aviao.getX()+ 45)  > getWidth())//
			{
				aviao.setX(aviao.getX() - 2 );
			}
			break;
		
		case KeyEvent.KEYCODE_DPAD_LEFT:
			aviao.setX((aviao.getX()) - 2);
			if(aviao.getX() < 48)
			{
				aviao.setX(aviao.getX() + 2 );
			}
			break;
			
		case KeyEvent.KEYCODE_DPAD_UP:
			aviao.setY(aviao.getY() - 2);
			break;
			
		case KeyEvent.KEYCODE_DPAD_DOWN:
			aviao.setY(aviao.getY() + 2);
			break;
		}
		return true;
	}

	@Override
	public void surfaceChanged(SurfaceHolder holder, int format, int width,	int height)
	{

	}

	@Override
	public void surfaceCreated(SurfaceHolder holder)
	{
		thread.setRunning(true);
		thread.start();
	}

	@Override
	public void surfaceDestroyed(SurfaceHolder holder)
	{
		boolean retry = true;
		
		while (retry)
		{
			try 
			{
				thread.join();
				retry = false;
			} 
			catch (InterruptedException e)
			{
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
			//sprite.handleActionDown((int) event.getX(), (int) event.getY());
			//sprite2.handleActionDown((int) event.getX(), (int) event.getY());

			// checagem do toque embaixo da tela
			if (event.getY() > getHeight() - 50)
			{
				thread.setRunning(false);
				((Activity) getContext()).finish();
			}
			else
			{
				Log.d(TAG, "Coords: x=" + event.getX() + ",y=" + event.getY());
			}
		}
		
		if (event.getAction() == MotionEvent.ACTION_MOVE)
		{
			// gestos
			if (true)
			{//sprite.isTouched()) {
				// tocado!
				//sprite.setX((int) event.getX());
				//sprite.setY((int) event.getY());
			}
			if (true)
			{//sprite2.isTouched()) {
				// tocado!
				//sprite2.setX((int) event.getX());
				//sprite2.setY((int) event.getY());
			}
		}
		if (event.getAction() == MotionEvent.ACTION_UP) {
			// larguei
//			if (sprite.isTouched())
//			{
//				sprite.setTouched(false);
//			}
//			if (sprite2.isTouched())
//			{
//				sprite2.setTouched(false);
//			}
		}
		return true;
	}
	
	
	

	// sobrecarregando o m�todo onDraw
	@Override
	protected void onDraw(Canvas spriteBatch) {
		// o bitmap factory deve ser importado
		// o resources drawable android � criado automaticamente com o nome da
		// imagem
		// o 10 10 � a posicao
		// o null � a mascara
		// canvas.drawBitmap(BitmapFactory.decodeResource(getResources(),
		// R.drawable.android), 10, 10, null);

		// preenchimento
		spriteBatch.drawColor(Color.BLACK);
		sprite.draw(spriteBatch);
//		sprite2.draw(spriteBatch);
//		sprite3.draw_rotate(spriteBatch, 0);
		aviao.draw(spriteBatch);
        //seta a cor e a fonte do texto		
		Paint p = new Paint();
		
		//Typeface plain = Typeface.createFromAsset(am, "fonts/comic.ttf");
		//Typeface tp = Typeface.create(plain, Typeface.NORMAL);
				
		//p.setColor(Color.RED);
		//p.setTypeface(tp);
		
//		spriteFont.drawString(spriteBatch, "Escrevendo: X " + ((int)getWidth()-250) + " Y " + ((int)getHeight()-100), ((int)getWidth()-250), ((int)getHeight()-100));
//		spriteFont.drawString(spriteBatch, "COR!!!", 200, 50, 1000, p);
		
//		if (explosion != null)
//		{
//			explosion.draw(spriteBatch);
//		}
		
	}

	public void Update() {

//		sprite.update(getWidth(), getHeight());
//		sprite2.update(getWidth(), getHeight());
//
//		if (sprite.intersects(sprite2.getX(), sprite2.getY(), sprite2
//				.getBitmap().getWidth(), sprite2.getBitmap().getHeight())&&sprite.isVisible())
//		{
//			sprite.setVisible(false);
//			
//			if (explosion == null || explosion.getState() == Explosion.State.DEAD)
//			{
//				explosion = new Explosion(EXPLOSION_SIZE, sprite.getX(), sprite.getY() );
//			}
//		}
//
//		sprite3.update(System.currentTimeMillis());
//		
//		if (explosion != null && explosion.isAlive())
//		{
//			explosion.update(getHolder().getSurfaceFrame());
//		}
		
	}
	
	public void Draw(Canvas canvas)
	{
		onDraw(canvas);
	}
}
