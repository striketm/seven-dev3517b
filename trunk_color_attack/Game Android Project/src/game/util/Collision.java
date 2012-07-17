package game.util;

public class Collision {
	
	public static boolean Check(GameElement obj1, GameElement obj2) {
		
		if( ((obj1.getX() + obj1.getWidth()) >= obj2.getX()) &&
    			
	    		((obj1.getX() <= (obj2.getX() + obj2.getWidth() )) &&
	    		
	    		((obj1.getY() + obj1.getHeight()) >= obj2.getY()) &&
	        			
	    	    ((obj1.getY() <= (obj2.getY() + obj2.getHeight() )))))		
		
		  return true;
		else
		  return false;	
				
		
	}

}
