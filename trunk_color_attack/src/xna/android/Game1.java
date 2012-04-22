package xna.android;


import br.com.R;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import br.com.R;


public class Game1 extends SurfaceView implements
		SurfaceHolder.Callback {

	private static final String TAG = Game1.class.getSimpleName();;

	private MainThread thread;
	
	private SoundManager mSoundManager;
	
	private SpriteFont spriteFont; // a fonte	
	
	private Sprite border;
	
	private Player sprite;
	
	private int initX, finalX;
	
	private Bitmap dBackground;
	
	private Explosion explosion;
		
	private static final int EXPLOSION_SIZE = 200;
	
	public Game1(Context Content) {
		super(Content);

		getHolder().addCallback(this);
		
		mSoundManager = new SoundManager(Content);
		
		// a imagem:
		sprite = new Player(BitmapFactory.decodeResource(getResources(),
				R.drawable.player), 24, 432);
		
		border =  new Sprite(BitmapFactory.decodeResource(getResources(),
				R.drawable.selection), Button.selectedButton*80+40, 40, mSoundManager);
		
		Button.createButton(BitmapFactory.decodeResource(getResources(), R.drawable.button_red), 40 , 40, mSoundManager);
		Button.createButton(BitmapFactory.decodeResource(getResources(), R.drawable.button_green), 120 , 40, mSoundManager);
		Button.createButton(BitmapFactory.decodeResource(getResources(), R.drawable.button_blue), 200 , 40, mSoundManager);
		Button.createButton(BitmapFactory.decodeResource(getResources(), R.drawable.button_yellow), 280 , 40, mSoundManager);
		
		dBackground = BitmapFactory.decodeResource(getResources(), R.drawable.background);
	
		this.spriteFont = new SpriteFont(BitmapFactory.decodeResource(getResources(), R.drawable.fonte_verde));
		//http://developer.android.com/index.html
		//http://developer.android.com/guide/index.html
		//http://stackoverflow.com/questions/1077357/can-the-android-drawable-directory-contain-subdirectories
		
		// cria o loop principal
		// thread = new MainThread();
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
	public boolean onTouchEvent(MotionEvent event) {
		if (event.getAction() == MotionEvent.ACTION_DOWN) {
			// controle de evento com o sprite
			sprite.handleActionDown((int) event.getX(), (int) event.getY());
			
			initX = (int) event.getX();
			
			if(((int) event.getX() >= 0) && ((int) event.getX() <= 80) && ((int) event.getY() <= 80))
			{
				Button.setSelectedButton(0);
			}
			
			if(((int) event.getX() >= 80) && ((int) event.getX() <= 160) && ((int) event.getY() <= 80))
			{
				Button.setSelectedButton(1);
			}
			
			if(((int) event.getX() >= 160) && ((int) event.getX() <= 240) && ((int) event.getY() <= 80))
			{
				Button.setSelectedButton(2);
			}
			
			if(((int) event.getX() >= 240) && ((int) event.getX() <= 320) && ((int) event.getY() <= 80))
			{
				Button.setSelectedButton(3);
			}
			//sprite2.handleActionDown((int) event.getX(), (int) event.getY());

			// checagem do toque embaixo da tela
			//if (event.getY() > getHeight() - 50) {
				//thread.setRunning(false);
			//	((Activity) getContext()).finish();
			//} else {
			//	Log.d(TAG, "Coords: x=" + event.getX() + ",y=" + event.getY());
			//}
		}
		if (event.getAction() == MotionEvent.ACTION_MOVE) {
		
			
		}
		if (event.getAction() == MotionEvent.ACTION_UP) {
			finalX = (int) event.getX();
			if (finalX - initX >= 80 )
			{
				if(sprite.getX() < 264)
				{
					sprite.setCurrentLane(sprite.getCurrentLane()+1);
					sprite.setX(sprite.getX() + 80);
					
					generateBullet();
				
				}
				
				//Enemy.createEnemy("red", 1, BitmapFactory.decodeResource(getResources(), R.drawable.android));
			}
			if(finalX - initX <= -80)
			{
				if(sprite.getX() > 40)
				{
				sprite.setCurrentLane(sprite.getCurrentLane()-1);
				sprite.setX(sprite.getX() - 80);
				generateBullet();
				}
			}
		}
		return true;
	}

	// sobrecarregando o método onDraw
	@Override
	protected void onDraw(Canvas spriteBatch) 
	{
		// o bitmap factory deve ser importado
		// o resources drawable android é criado automaticamente com o nome da
		// imagem
		// o 10 10 é a posicao
		// o null é a mascara
		// canvas.drawBitmap(BitmapFactory.decodeResource(getResources(),
		// R.drawable.android), 10, 10, null);

		// preenchimento
		spriteBatch.drawColor(Color.BLACK);
		
		spriteBatch.drawBitmap(dBackground, 0, 0, null);
		
		for (int i = 0; i < Bullet.bulletList.size(); i++)
		{
			Bullet.bulletList.get(i).Draw(spriteBatch);
		}
		
		spriteFont.drawString(spriteBatch, " X i " + initX + " X f " + finalX + " Bullets "+Bullet.bulletList.size(), ((int)getWidth()-350), ((int)getHeight()-100));
		
		Enemy.Draw(spriteBatch);
		
		if (explosion != null)
		{
			explosion.draw(spriteBatch);
		}
		
		for (int i = 0; i < Button.buttonList.size(); i++)
		{
			Button.buttonList.get(i).draw(spriteBatch);
		}
		
		border.setX(Button.buttonList.get(Button.selectedButton).getX());
		border.setY(Button.buttonList.get(Button.selectedButton).getY());
		border.draw(spriteBatch);
		
		sprite.draw(spriteBatch);
		
	}

	public void Update()
	{

		for (int i = 0; i < Bullet.bulletList.size(); i++)
		{
			Bullet.bulletList.get(i).Update();
		}
		
		if (explosion != null && explosion.isAlive())
		{
			explosion.update(getHolder().getSurfaceFrame());
		}
	
		Enemy.Update(System.currentTimeMillis());
		
	}
	
	public void Draw(Canvas canvas)
	{
		onDraw(canvas);
	}
	
	public void generateBullet()
	{
		Bullet leBullet = null;
		switch(Button.getSelectedButton())
		{
		case 0:
			leBullet = new Bullet(BitmapFactory.decodeResource(getResources(), R.drawable.red_bullet), 
					sprite.getCurrentLane()*80 + 40, 448, "red");
			break;
		case 1:
			leBullet = new Bullet(BitmapFactory.decodeResource(getResources(), R.drawable.green_bullet), 
					sprite.getCurrentLane()*80 + 40, 448, "green");
			break;
		case 2:
			leBullet = new Bullet(BitmapFactory.decodeResource(getResources(), R.drawable.blue_bullet), 
					sprite.getCurrentLane()*80 + 40, 448, "blue");
			break;
		case 3:
			leBullet = new Bullet(BitmapFactory.decodeResource(getResources(), R.drawable.yellow_bullet), 
					sprite.getCurrentLane()*80 + 40, 448, "yellow");
			break;
		}
		Bullet.bulletList.add(leBullet);
	}
}