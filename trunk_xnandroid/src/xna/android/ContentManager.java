package xna.android;

import java.util.Map;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Typeface;

/**
 * ContentManager
 * @author Bruno Miguel de Oliveira Tamer <bmotamer@gmail.com>
 * @version 2012.07.09
 */
public class ContentManager
{
	
	/**
	 * Main Activity
	 */
	protected Activity _Activity;
	
	/**
	 * Data
	 */
	protected Map<String, MapPair<Object, Integer>> _Data;
	
	/**
	 * Constructor
	 * @param Activity
	 * @throws Error if given activity is null
	 */
	public ContentManager(Activity activity) throws Exception
	{
		_Activity = activity;
		if (_Activity == null)
			throw new Exception("You can't set activity as null!");
	}
	
	/**
	 * Grabs a loaded asset
	 * @param Path
	 * @return Asset
	 */
	@SuppressWarnings("unchecked")
	public <T> T Grab(String path)
	{
		MapPair<Object, Integer> pair = _Data.get(path);
		if (pair == null)
			return null;
		pair.Value++;
		return (T)pair.Key;
	}
	
	/**
	 * Stores a loaded asset
	 * @param Path (or Nickname)
	 * @param Asset
	 * @return Asset (same as given)
	 * @throws Error in case there is an object with same path stored
	 */
	public <T> T Store(String path, T object) throws Exception
	{
		if (_Data.get(path) != null)
			throw new Exception("Path '" + path + "' already exists!");
		_Data.put(path, new MapPair<Object, Integer>(object, 1));
		return object;
	}
	
	/**
	 * Releases a loaded asset
	 * @param Path
	 * @throws Error 
	 */
	public void Release(String path)
	{
		MapPair<Object, Integer> pair = Grab(path);
		if (pair != null)
		{
			pair.Value--;
			if (pair.Value <= 0)
				Unload(path);
		}
	}
	
	/**
	 * Unloads all loaded assets
	 * @throws Exception
	 */
	public void Unload()
	{
		for (MapPair<Object, Integer> pair : _Data.values())
			Recycle(pair.Key);
		_Data.clear();
	}
	
	/**
	 * Unload a certain asset (through path)
	 * @param Path
	 */
	public void Unload(String path)
	{
		MapPair<Object, Integer> pair = _Data.get(path);
		if (pair == null)
			return;
		Recycle(pair.Key);
		_Data.remove(path);
	}
	
	/**
	 * Unload a certain asset (through object)
	 * @param Asset
	 */
	public void Unload(Object object)
	{
		for (String key : _Data.keySet())
		{
			MapPair<Object, Integer> pair = _Data.get(key);
			if (pair.Key == object)
			{
				Recycle(pair.Key);
				_Data.remove(key);
				break;
			}
		}
	}
	
	/**
	 * Destroys an asset
	 * @param pair
	 * @throws Exception
	 */
	protected void Recycle(Object object)
	{
		if (object instanceof Bitmap)
			((Bitmap)object).recycle();
	}
	
	/**
	 * Loads a Bitmap
	 * @param path
	 * @return
	 * @throws Exception 
	 */
	public Bitmap LoadTexture2D(String path) throws Exception
	{
		Bitmap bitmap = Grab(path);
		if (bitmap == null)
			return Store(path, BitmapFactory.decodeFile(path));
		return bitmap;
	}
	
	/**
	 * Loads a Typeface
	 * @param path
	 * @return
	 * @throws Exception 
	 */
	public Typeface LoadSpriteFont(String path) throws Exception
	{
		Typeface typeFace = Grab(path);
		if (typeFace == null)
			return Store(path, Typeface.createFromAsset(_Activity.getAssets(), path));
		return typeFace;
	}
	
}
