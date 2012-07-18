package xna.android;

import android.graphics.Canvas;

public interface SceneBase
{
	
	void start();
	
	void update(Time time);
	
	void draw(Canvas canvas);
	
	void terminate();
	
}