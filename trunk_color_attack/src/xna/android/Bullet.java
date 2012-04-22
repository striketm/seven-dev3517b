package xna.android;

import java.util.ArrayList;
import java.util.List;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import br.com.R.string;

public class Bullet extends SpriteAnimated {
	
	public Bullet(Bitmap texture, int x, int y, String color) 
	{
		super(texture, x, y , 4, 4, 1, 1);
		// TODO Auto-generated constructor stub
		speed = new Speed(5,5);
		this.currentColor = color;
	}

	String currentColor;
	
	Speed speed;
	
	int damage;
	
	boolean alive;
	
	public static List<Bullet> bulletList = new ArrayList<Bullet>();
	
	public static void CreateBullet(int currentLane, Bitmap texture, int x, int y, String color)
	{
		Bullet bullet = new Bullet(texture, x, y, color);
		bulletList.add(bullet);		
	}
	
	public void Update()
	{
		setY((int)(getY() - speed.getYv()));
	}
	
	public void Draw(Canvas spriteBatch)
	{
		super.draw(spriteBatch);
	}

}
