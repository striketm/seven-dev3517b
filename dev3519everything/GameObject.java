package game.util;

import android.widget.*;
import android.view.*;

public class GameObject {
	
	public static void MoveTo(View gameObject, int Left, int Top) {
		
	   gameObject.setLayoutParams(new AbsoluteLayout.LayoutParams(gameObject.getWidth(), 
	   gameObject.getHeight(), Left, Top ));
		
	}
	
   public static void MoveBy(View gameObject, int Left, int Top) {
	   
	   int l = gameObject.getLeft();
	   
	   int t = gameObject.getTop();
	   
	   l+=Left;
	   
	   t+=Top;
		
	   gameObject.setLayoutParams(new AbsoluteLayout.LayoutParams(gameObject.getWidth(), 
	   gameObject.getHeight(), l, t ));
		
	}

   public static void MoveToX(View gameObject, int Left) {
	   
	   gameObject.setLayoutParams(new AbsoluteLayout.LayoutParams(gameObject.getWidth(), 
	   gameObject.getHeight(), Left, gameObject.getTop() ));
	   
   }
   
   public static void MoveByX(View gameObject, int Left) {
	 
	   int l = gameObject.getLeft();
	   
	   l+=Left;
	   
	   gameObject.setLayoutParams(new AbsoluteLayout.LayoutParams(gameObject.getWidth(), 
	   gameObject.getHeight(), l, gameObject.getTop() ));
	   
   }
   
   public static void MoveToY(View gameObject, int Top) {
	   
	   gameObject.setLayoutParams(new AbsoluteLayout.LayoutParams(gameObject.getWidth(), 
	   gameObject.getHeight(), gameObject.getLeft(), Top ));
	   
   }
   
   public static void MoveByY(View gameObject, int Top) {
	 
	   int t = gameObject.getTop();
	   
	   t+=Top;
	   
	   gameObject.setLayoutParams(new AbsoluteLayout.LayoutParams(gameObject.getWidth(), 
	   gameObject.getHeight(), gameObject.getLeft(), t ));
	   
   }
   
   
   public static boolean CheckCollision(View gameObject1, View gameObject2) {
	   
		   if( ((gameObject1.getLeft() + gameObject1.getWidth()) >= gameObject2.getLeft()) &&
		   ((gameObject1.getLeft() <= (gameObject2.getLeft() + gameObject2.getWidth() )) &&
		   ((gameObject1.getTop() + gameObject1.getHeight()) >= gameObject2.getTop()) &&
		   ((gameObject1.getTop() <= (gameObject2.getTop() + gameObject2.getHeight() )))))
			   
			   
		   return true;
		   else
		   return false;
		   
   }
   
   
   
}
