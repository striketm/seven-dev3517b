package xna.android;

import br.com.R;
import android.graphics.Bitmap;
import android.graphics.Canvas;

public class MySprite {
	
	private static final String TAG = MySprite.class.getSimpleName();

	private Bitmap bitmap;		// imagem
	private int x;				// coord X
	private int y;				// Y
	private boolean touched;	// para o toque
	private Speed speed;
	private boolean visible;
	
	private SoundManager mSoundManager;
	
	public MySprite(Bitmap bitmap, int x, int y, SoundManager sm)
	{
		this.bitmap = bitmap;
		this.x = x;
		this.y = y;
		speed = new Speed(2.0f, 2.0f);
		visible = true;
		this.mSoundManager = sm;

	     mSoundManager.addSound("plim", R.raw.sound);	     
	}
	
	public MySprite(Bitmap bitmap, int x, int y)
	{
		this.bitmap = bitmap;
		this.x = x;
		this.y = y;
		speed = new Speed(2.0f, 2.0f);
		visible = true;
		     
	}

	public Bitmap getBitmap()
	{
		return bitmap;
	}
	
	public void setBitmap(Bitmap bitmap)
	{
		this.bitmap = bitmap;
	}
	
	public int getX()
	{
		return x;
	}
	
	public void setX(int x)
	{
		this.x = x;
	}
	
	public int getY()
	{
		return y;
	}
	
	public void setY(int y)
	{
		this.y = y;
	}
	
	//para o toque
	
	public boolean isTouched()
	{
		return touched;
	}

	public void setTouched(boolean touched)
	{
		this.touched = touched;
	}

	public boolean isVisible() {
		return visible;
	}

	public void setVisible(boolean visible) {
		this.visible = visible;
	}

	public Speed getSpeed() {
		return speed;
	}

	public void setSpeed(Speed speed) {
		this.speed = speed;
	}
	
	public void handleActionDown(int eventX, int eventY)
	{
		if ((eventX >= (x - bitmap.getWidth() / 2))
				&& (eventX <= (x + bitmap.getWidth() / 2))
				&& (eventY >= (y - bitmap.getHeight() / 2))
				&& (eventY <= (y + bitmap.getHeight() / 2)))
		{
			// tocado
			setTouched(true);
			mSoundManager.playSound("plim");
		} 
		else 
		{
			setTouched(false);
		}
	}
	
	//para o desenho
		public void draw(Canvas canvas)
		{
			if(visible)
			canvas.drawBitmap(bitmap, x - (bitmap.getWidth() / 2), y - (bitmap.getHeight() / 2), null);
		}
		
		public boolean intersects(int x, int y, int width, int height)
		{
			if(
					(this.x+this.bitmap.getWidth()<x)//fora pela esquerda
					||
					(this.x>x+width)//fora pela direita
					||
					(this.y+this.bitmap.getHeight()<y)//fora por cima
					||
					(this.y>y+height)//fora por baixo
					)
					{
						return false;
					}
					else
					{
						return true;
					}
		}
		
		public void update(int windowWidth, int windowHeight)
		{
			x+=speed.getXv()*speed.getxDirection();
			y+=speed.getYv()*speed.getyDirection();
			
			if(x<0)
			{
				//Log.d(TAG, "bati no canto esquerdo");
				speed.toggleXDirection();
			}
			
			if(x>windowWidth)
			{
				//Log.d(TAG, "bati no canto direita");
				speed.toggleXDirection();
			}
			
			if(y<0)
			{
				//Log.d(TAG, "bati no canto esquerdo");
				speed.toggleYDirection();
			}
			
			if(y>windowHeight)
			{
				//Log.d(TAG, "bati no canto esquerdo");
				speed.toggleYDirection();
			}
		}		
}