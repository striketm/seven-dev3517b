package xna.android;

import android.graphics.Canvas;

public abstract class GameElement {
	
    protected int x;
	
    protected int y;
	
    protected int width;
	
    protected int height;
	
	
	
	public void SetX(int x) {
    	this.x = x;
    	
    }
    
    public void SetY(int y) {
    	this.y = y;
    
    }
    
    public void SetWidth(int width) {
    	this.width = width;
    	
    }
    
    public void SetHeight(int height) {
    	this.height = height;
    	
    }
	
	public void MoveByX(int x) {
		
		this.x += x;
		
	}
	
    public void MoveByY(int y) {
		
		this.y += y;
		
	}
    
    
    public void setBounds(int x, int y, int width, int height) {
    	this.x = x;
    	this.y = y;
    	this.width = width;
    	this.height = height;
    	
    }
    
    public int getX() { return x; }
    
    public int getY() { return y; }
    
    public int getWidth() { return width; }
    
    public int getHeight() { return height; }
    
    public abstract void Draw(Canvas canvas);
    
    public boolean isTouch(int posx, int posy) {
    	
    	if( (posx >= x) && (posx <= x + width)
    	&& (posy >= y) && (posy <= y + height))
    		return true;
    	else    		 	
    	    return false;
    }
    
    

}
