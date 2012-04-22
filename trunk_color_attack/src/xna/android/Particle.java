package xna.android;

import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Rect;
import android.util.Log;

public class Particle {
	
	public enum State { ALIVE, DEAD };

	private State state;
	
	public static final int DEFAULT_LIFETIME 	= 200;	
	public static final int MAX_DIMENSION		= 5;	
	public static final int MAX_SPEED			= 10;	
			
	private float widht;		
	private float height;		 
	private float x, y;			
	private double xv, yv;		
	private int age;			
	private int lifetime;		
	private int color;			
	private Paint paint;		
	
	
	public State getState()
	{
		return state;
	}

	public void setState(State state)
	{
		this.state = state;
	}

	public float getWidht()
	{
		return widht;
	}

	public void setWidht(float widht)
	{
		this.widht = widht;
	}

	public float getHeight()
	{
		return height;
	}

	public void setHeight(float height)
	{
		this.height = height;
	}

	public float getX() 
	{
		return x;
	}

	public void setX(float x)
	{
		this.x = x;
	}

	public float getY()
	{
		return y;		
	}

	public void setY(float y)
	{
		this.y = y;
	}

	public double getXv()
	{
		return xv;
	}

	public void setXv(double xv)
	{
		this.xv = xv;
	}

	public double getYv()
	{
		return yv;
	}

	public void setYv(double yv)
	{
		this.yv = yv;
	}

	public int getAge()
	{
		return age;
	}

	public void setAge(int age)
	{
		this.age = age;
	}

	public int getLifetime()
	{
		return lifetime;
	}

	public void setLifetime(int lifetime)
	{
		this.lifetime = lifetime;
	}

	public int getColor() 
	{
		return color;
	}

	public void setColor(int color)
	{
		this.color = color;
	}
	
	public boolean isAlive()
	{
		return this.state == State.ALIVE;
	}
	
	public boolean isDead()
	{
		return this.state == State.DEAD;
	}

	public Particle(int x, int y)
	{
		this.x = x;
		this.y = y;
		this.state = Particle.State.ALIVE;
		this.widht = rndInt(1, MAX_DIMENSION);
		this.height = this.widht;
		this.lifetime = DEFAULT_LIFETIME;
		this.age = 0;
		this.xv = (rndDbl(0, MAX_SPEED * 2) - MAX_SPEED);
		this.yv = (rndDbl(0, MAX_SPEED * 2) - MAX_SPEED);
		if (xv * xv + yv * yv > MAX_SPEED * MAX_SPEED)
		{
			xv *= 0.7;
			yv *= 0.7;
		}
		this.color = Color.argb(255, rndInt(0, 255), rndInt(0, 255), rndInt(0, 255));
		this.paint = new Paint(this.color);
		
		Log.d("oi", "oi");
	}
	
	public void reset(float x, float y)
	{
		this.state = Particle.State.ALIVE;
		this.x = x;
		this.y = y;
		this.age = 0;
	}
	
	static int rndInt(int min, int max)
	{
		return (int) (min + Math.random() * (max - min + 1));
	}

	static double rndDbl(double min, double max)
	{
		return min + (max - min) * Math.random();
	}
	
	public void update()
	{
		if (this.state != State.DEAD)
		{
			this.x += this.xv;
			this.y += this.yv;
			
			int a = this.color >>> 24;//atribui um valor ao a
			
			a -= 2;
			
			if (a <= 0)
			{						
				this.state = State.DEAD;
			} 
			else 
			{
				this.color = (this.color & 0x00ffffff) + (a << 24);
				
				//this.paint.setAlpha(a);
				this.age++;						
			}
			
			if (this.age >= this.lifetime)
			{	
				this.state = State.DEAD;
			}			
		}
	}
	
	public void update(Rect container)
	{
		
		if (this.isAlive())
		{
			if (this.x <= container.left || this.x >= container.right - this.widht)
			{
				this.xv *= -1;
			}
			
			if (this.y <= container.top || this.y >= container.bottom - this.height)
			{
				this.yv *= -1;
			}
		}
		update();
	}

	public void draw(Canvas canvas)
	{
		paint.setColor(this.color);
		canvas.drawRect(this.x, this.y, this.x + this.widht, this.y + this.height, paint);
	}

}
