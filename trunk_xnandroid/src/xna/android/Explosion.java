package xna.android;

import android.graphics.Canvas;
import android.graphics.Rect;

public class Explosion
{
	
	public enum State { ALIVE, DEAD };

	private State state;
	
	private Particle[] particles;
	
	private int x, y;						
	
	public Explosion(int particleNr, int x, int y) 
	{
		
		this.state = State.ALIVE;
		this.particles = new Particle[particleNr];
	 	for (int i = 0; i < this.particles.length; i++)
	 	{
			Particle p = new Particle(x, y);
			this.particles[i] = p;
		}
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
	
	public State getState()
	{
		return state;
	}

	public void setState(State state) 
	{
		this.state = state;
	}

	public boolean isAlive() 
	{
		return this.state == State.ALIVE;
	}
	public boolean isDead()
	{
		return this.state == State.DEAD;
	}

	public void update()
	{
		if (this.state != State.DEAD)
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
				this.state = State.DEAD; 
		}
	}
	
	public void update(Rect container)
	{
		if (this.state != State.DEAD)
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
				this.state = State.DEAD; 
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
