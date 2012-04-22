package xna.android;
import java.util.ArrayList;
import java.util.List;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.util.Log;


public class Enemy extends SpriteAnimated 
{
	public static List<Enemy> enemyList = new ArrayList<Enemy>(); 
	
	int currentLane; //Int from 0 to 3, indicating the current vertical lane he is at
	
	String currentColor; //Stores the currentColor... == type? or make an class??
	
	boolean alive; //Guess what
	
	int life;
	
	Speed speed;
	
	public Enemy(String color, int startLane, Bitmap texture)
	{
		//Hardcoded enemy sizes
		
		super(texture, 40 + (startLane * 80), -40, 32, 32, 1, 1);
		
		this.currentColor = color;
		this.currentLane = startLane;
	
		this.alive = true;		
	}
	
	//TODO IA function... random the positions and colors of the enemies
	
	public static void createEnemy(String color, int startLane, Bitmap texture)
	{
		Enemy enemy = new Enemy(color, startLane, texture);
		enemyList.add(enemy);
		Log.d("k","NUM ENEMIES " + enemyList.size());
	} 
	
	public void UpdateEnemy(long gameTime)
	{
		//make the enemies go down
		this.setY(this.getY() + 3);
		
		//TODO remove from the list when y > 480...
		
		//make the animation
		super.update(gameTime);
	}
	
	
	public static void Update(long gameTime)
	{
		for(int i = 0; i < enemyList.size(); i++)
		{
			enemyList.get(i).UpdateEnemy(gameTime);
		}
	}
	
	
	public void DrawEnemy(Canvas spriteBatch)
	{
		super.draw(spriteBatch);
	}

	
	public static void Draw(Canvas spriteBatch)
	{
		for(int i = 0; i < enemyList.size(); i++)
		{
			enemyList.get(i).DrawEnemy(spriteBatch);
		}
	}
}
