
package xna.android;

import android.graphics.Canvas;
import android.graphics.Rect;
import android.util.Log;

public class Explosion {

	private static final String TAG = Explosion.class.getSimpleName();
	
	public static final int STATE_ALIVE 	= 0;	
	public static final int STATE_DEAD 		= 1;	
	
	private Particle[] particles;			
	private int x, y;						
	private float gravity;					
	private float wind;						
	private int size;						
	private int state;						
	
	public Explosion(int particleNr, int x, int y) 
	{
		
		this.state = STATE_ALIVE;
		this.particles = new Particle[particleNr];
	 	for (int i = 0; i < this.particles.length; i++)
	 	{
			Particle p = new Particle(x, y);
			this.particles[i] = p;
		}
	 	this.size = particleNr;
	}
	
	public Particle[] getParticles()
	{
		return particles;
	}
	
	public void setParticles(Particle[] particles)
	{
		this.particles = particles;
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
	
	public float getGravity() 
	{
		return gravity;
	}
	
	public void setGravity(float gravity) 
	{
		this.gravity = gravity;
	}
	
	public float getWind()
	{
		return wind;
	}
	
	public void setWind(float wind)
	{
		this.wind = wind;
	}
	
	public int getSize()
	{
		return size;
	}
	
	public void setSize(int size)
	{
		this.size = size;
	}
	
	public int getState()
	{
		return state;
	}

	public void setState(int state) 
	{
		this.state = state;
	}

	public boolean isAlive() 
	{
		return this.state == STATE_ALIVE;
	}
	public boolean isDead()
	{
		return this.state == STATE_DEAD;
	}

	public void update()
	{
		if (this.state != STATE_DEAD)
		{
			boolean isDead = true;
			for (int i = 0; i < this.particles.length; i++)
			{
				if (this.particles[i].isAlive())
				{
					this.particles[i].update();
					isDead = false;
				}
			}
			if (isDead)
				this.state = STATE_DEAD; 
		}
	}
	
	public void update(Rect container)
	{
		if (this.state != STATE_DEAD)
		{
			boolean isDead = true;
			for (int i = 0; i < this.particles.length; i++)
			{
				if (this.particles[i].isAlive())
				{
					this.particles[i].update(container);
					isDead = false;
				}
			}
			if (isDead)
				this.state = STATE_DEAD; 
		}
	}

	public void draw(Canvas canvas)
	{
		for(int i = 0; i < this.particles.length; i++)
		{
			if (this.particles[i].isAlive())
			{
				this.particles[i].draw(canvas);
			}
		}
	}
}
