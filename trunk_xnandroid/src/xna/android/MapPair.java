package xna.android;

/**
 * 
 * @author Bruno Miguel
 * @version 2012.07.09
 * 
 * @param Key
 * @param Value
 */
public class MapPair<K, V>
{
	
	/**
	 * Key
	 */
    public K Key;
    
    /**
     * Value
     */
    public V Value;
    
    /**
     * Constructor
     * @param Key
     * @param Value
     */
    public MapPair(K key, V value)
    {
        Key = key;
        Value = value;
    }
    
}