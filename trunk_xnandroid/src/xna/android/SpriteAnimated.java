/**
 * 
 */
package xna.android;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Rect;

public class SpriteAnimated {
	
	private static final String TAG = SpriteAnimated.class.getSimpleName();

	private Bitmap bitmap;		// a imagem
	private Rect sourceRect;	// o retângulo de destino
	private int frameNr;		// número de frames
	private int currentFrame;	// frame atual
	private long frameTicker;	// tempo do último update
	private int framePeriod;	// milisegundos entre cada frame 1000/fps
	
	private int spriteWidth;	// largura do frame
	private int spriteHeight;	// altura do frame
	
	private int x;				// posição X do objeto na tela
	private int y;				// posição Y do objeto na tela
	
	
	public SpriteAnimated(Bitmap bitmap, int x, int y, int width, int height, int fps, int frameCount) {
		this.bitmap = bitmap;
		this.x = x;
		this.y = y;
		currentFrame = 0;
		frameNr = frameCount;
		spriteWidth = bitmap.getWidth() / frameCount;//somente exato
		spriteHeight = bitmap.getHeight();//somente exato
		sourceRect = new Rect(0, 0, spriteWidth, spriteHeight);//Rect classe de Java
		framePeriod = 1000 / fps;
		frameTicker = 0l;
	}
	
	
	public Bitmap getBitmap() {
		return bitmap;
	}
	public void setBitmap(Bitmap bitmap) {
		this.bitmap = bitmap;
	}
	public Rect getSourceRect() {
		return sourceRect;
	}
	public void setSourceRect(Rect sourceRect) {
		this.sourceRect = sourceRect;
	}
	public int getFrameNr() {
		return frameNr;
	}
	public void setFrameNr(int frameNr) {
		this.frameNr = frameNr;
	}
	public int getCurrentFrame() {
		return currentFrame;
	}
	public void setCurrentFrame(int currentFrame) {
		this.currentFrame = currentFrame;
	}
	public int getFramePeriod() {
		return framePeriod;
	}
	public void setFramePeriod(int framePeriod) {
		this.framePeriod = framePeriod;
	}
	public int getSpriteWidth() {
		return spriteWidth;
	}
	public void setSpriteWidth(int spriteWidth) {
		this.spriteWidth = spriteWidth;
	}
	public int getSpriteHeight() {
		return spriteHeight;
	}
	public void setSpriteHeight(int spriteHeight) {
		this.spriteHeight = spriteHeight;
	}
	public int getX() {
		return x;
	}
	public void setX(int x) {
		this.x = x;
	}
	public int getY() {
		return y;
	}
	public void setY(int y) {
		this.y = y;
	}
		
	public void update(long gameTime)
	{
		if (gameTime > frameTicker + framePeriod)//se passar do tempo de atualizar
		{
			frameTicker = gameTime;
			currentFrame++;//aumenta o frame
			if (currentFrame >= frameNr)//se passar da quantidade
			{
				currentFrame = 0;//reseta o frame
			}
		}
		//Aqui a animação acontece!
		this.sourceRect.left = currentFrame * spriteWidth;
		this.sourceRect.right = this.sourceRect.left + spriteWidth;
	}
	
	public void draw(Canvas canvas) {
		
		Rect destRect = new Rect(getX(), getY(), getX() + spriteWidth, getY() + spriteHeight);
		
		canvas.drawBitmap(bitmap, sourceRect, destRect, null);
		
	}
}
