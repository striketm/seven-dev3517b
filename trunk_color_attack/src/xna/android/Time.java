public class Time
{
	
	private long _previousTime;
	private long _currentTime;
	
	public Time()
	{
		_previousTime = System.currentTimeMillis();
		_currentTime  = _previousTime;
	}
	
	public void update()
	{
		_previousTime = _currentTime;
		_currentTime  = System.currentTimeMillis();
	}
	
	public long deltaTime() { return _currentTime - _previousTime; }
	
	public float smoothDeltaTime() { return deltaTime() / 1000.0f; }
	
}