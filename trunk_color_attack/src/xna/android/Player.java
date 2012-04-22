package xna.android;

import android.R.string;
import android.graphics.Bitmap;
import android.graphics.Canvas;

public class Player extends SpriteAnimated
{
	int currentLane = 0;
	
	string currentColor;
	
	boolean alive;
	
	public Player(Bitmap texture, int x, int y)
	{
		super(texture, x, y, 32, 32, 1, 1);
	}
	
	public void update(long gameTime)
	{
		super.update(gameTime);
	}
	
	public int getCurrentLane() {
		return currentLane;
	}

	public void setCurrentLane(int currentLane) {
		this.currentLane = currentLane;
	}

	public void draw(Canvas spriteBatch)
	{
		super.draw(spriteBatch);
	}
	
	public void handleActionDown(int eventX, int eventY)
	{
		if ((eventX >= (getX() - 32 / 2))
				&& (eventX <= (getX() + 32 / 2))
				&& (eventY >= (getY() - 32 / 2))
				&& (eventY <= (getY() + 32 / 2)))
		{
			// tocado
			setTouched(true);
		} 
		else 
		{
			setTouched(false);
		}
	}
	
}
