package xna.android;

import br.com.R;
import android.graphics.Bitmap;
import android.graphics.Canvas;

/* Importa as libraries */
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.FloatBuffer;

import javax.microedition.khronos.opengles.GL10;

import android.graphics.Rect;
import android.opengl.GLSurfaceView;

public class Sprite
{
	
	private static final String TAG = Sprite.class.getSimpleName();

	private Bitmap bitmap;		// imagem
	private int x;				// coord X
	private int y;				// Y
	private boolean touched;	// para o toque
	private Speed speed;
	private boolean visible;
	
	
	/* Variaveis privadas */
	private Texture texture;
	private Rect 	src_rect;     
	
	private SoundManager mSoundManager;
	
	/* Bloco de inicialização */
	{
		x        = 0;
		y        = 0;
		z        = 0;
		ox       = 0;
		oy       = 0;
		zoom_x   = 1.0f;
		zoom_y   = 1.0f;
		angle 	 = 0;
		opacity  = 255;
		texture  = null;
		src_rect = new Rect(0, 0, 0, 0);
	}
	
	/**
	 * @brief Construtor padrão.
	 */
	public Sprite() 
	{
	}
	
	/**
	 * @brief Cria um sprite usando uma textura.
	 * @param [in] other_texture - null ou uma textura pega usando a classe Cache
	 */
	public Sprite(Texture other_texture) 
	{
		setTexture(other_texture);
	}
	
	/**
	 * @brief Define a textura do sprite.
	 * @param [in] other_texture - null ou uma textura pega usando a classe Cache
	 */
	public final void setTexture(Texture other_texture)
	{
		texture = other_texture;
		if (texture != null)
			src_rect.set(0, 0, texture.getWidth(), texture.getHeight());
	}
	
	/**
	 * @brief Define um recorte do sprite.
	 * @param [in] x - Posição X.
	 * @param [in] y - Posição Y.
	 * @param [in] w - Largura.
	 * @param [in] h - Altura.
	 */
	public final void setRect(int x, int y, int w, int h)
	{
		src_rect.set(x, y, x + w, y + h);
	}
	

	/**
	 * @brief Desenha o sprite.
	 * @param [in] gl - Parametro para um objeto GL10
	 * 
	 * Esta função desenha o sprite .
	 */
	public final void draw(GL10 gl)
	{
		/* Não tem textura */
		if (texture == null)
			return;
		
		/* Assegura a existência de um src_rect*/
		if (src_rect == null)
			src_rect = new Rect(0, 0, texture.getWidth(), texture.getHeight());
		
		/* Define a textura atual */
		gl.glBindTexture(GL10.GL_TEXTURE_2D, texture.getOGLTextureID());
		
		/* Limpa a matriz de transformação */
		gl.glLoadIdentity();
		
		/* Define os parametros */
		gl.glTranslatef(x, y, z);
		gl.glRotatef(angle, 0, 0, 1);
		gl.glScalef(zoom_x, zoom_y, 1);
		gl.glTranslatef(-ox, -oy, 0);
		gl.glColor4f(1, 1, 1, opacity / 255.0f);
		
		/* Pega a largura do sprite baseado na rect */
		int w = src_rect.right - src_rect.left;
		int h = src_rect.bottom - src_rect.top;
		
		/* Define as coordenadas do sprite. */
		float vertexes[] = {
				0.0f, 0.0f,
				w, 0.0f,
				0.0f, h,
				w, h
		};
	}
	
	 /**
	 * @brief Função estática para criar um buffer.
	 * @param [in] data - Array usada para criar o buffer.
	 * @return O buffer criado.
	 */
	private final static FloatBuffer createFloatBuffer(float data[])
	{
		ByteBuffer vbb = ByteBuffer.allocateDirect(data.length * 4);
		vbb.order(ByteOrder.nativeOrder());
		FloatBuffer outBuffer = vbb.asFloatBuffer();
		outBuffer.put(data).position(0);
		return outBuffer;
	}

	
	public Sprite(Bitmap bitmap, int x, int y, SoundManager sm)
	{
		this.bitmap = bitmap;
		this.x = x;
		this.y = y;
		speed = new Speed(2.0f, 2.0f);
		visible = true;
		this.mSoundManager = sm;

	     mSoundManager.addSound("plim", R.raw.sound);	     
	}
	
	public Sprite(Bitmap bitmap, int x, int y)
	{
		this.bitmap = bitmap;
		this.x = x;
		this.y = y;
		speed = new Speed(2.0f, 2.0f);
		visible = true;
     
	}

	public Bitmap getBitmap()
	{
		return bitmap;
	}
	
	public void setBitmap(Bitmap bitmap)
	{
		this.bitmap = bitmap;
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
	
	//para o toque
	
	public boolean isTouched()
	{
		return touched;
	}

	public void setTouched(boolean touched)
	{
		this.touched = touched;
	}

	public boolean isVisible() {
		return visible;
	}

	public void setVisible(boolean visible) {
		this.visible = visible;
	}

	public Speed getSpeed() {
		return speed;
	}

	public void setSpeed(Speed speed) {
		this.speed = speed;
	}
	
	public void handleActionDown(int eventX, int eventY)
	{
//		if ((eventX >= (x - bitmap.getWidth() / 2))
//				&& (eventX <= (x + bitmap.getWidth() / 2))
//				&& (eventY >= (y - bitmap.getHeight() / 2))
//				&& (eventY <= (y + bitmap.getHeight() / 2)))
		if((eventX > x)
			&&(eventX < x + bitmap.getWidth())
			&&(eventY > y)
			&&(eventY < y + bitmap.getHeight()))
		{
			// tocado
			setTouched(true);
		} 
		else 
		{
			setTouched(false);
		}
	}
	
	//para o desenho
		public void draw(Canvas canvas)
		{
			if(visible)
			canvas.drawBitmap(bitmap, x , y , null);
		}
		
		public boolean intersects(int x, int y, int width, int height)
		{
			if(
					(this.x+this.bitmap.getWidth()<x)//fora pela esquerda
					||
					(this.x>x+width)//fora pela direita
					||
					(this.y+this.bitmap.getHeight()<y)//fora por cima
					||
					(this.y>y+height)//fora por baixo
					)
					{
						return false;
					}
					else
					{
						return true;
					}
		}
		
		public void update(int windowWidth, int windowHeight)
		{
			x+=speed.getXv()*speed.getxDirection();
			y+=speed.getYv()*speed.getyDirection();
			
			if(x<0)
			{
				//Log.d(TAG, "bati no canto esquerdo");
				speed.toggleXDirection();
			}
			
			if(x>windowWidth)
			{
				//Log.d(TAG, "bati no canto direita");
				speed.toggleXDirection();
			}
			
			if(y<0)
			{
				//Log.d(TAG, "bati no canto esquerdo");
				speed.toggleYDirection();
			}
			
			if(y>windowHeight)
			{
				//Log.d(TAG, "bati no canto esquerdo");
				speed.toggleYDirection();
			}
		}		
}