package xna.android;

import android.graphics.Canvas;

public class SceneManager
{
	
	private SceneBase _current;
	
	public SceneBase getCurrent() { return _current; }
	
	public void setCurrent(SceneBase value)
	{
		if (_current != null) _current.terminate();
		_current = value;
		if (_current != null) _current.start();
	}
	
	public boolean update(Time time)
	{
		if (_current == null) return false;
		_current.update(time);
		return true;
	}
	
	public boolean draw(Canvas canvas)
	{
		if (_current == null) return false;
		_current.draw(canvas);
		return true;
	}
	
}