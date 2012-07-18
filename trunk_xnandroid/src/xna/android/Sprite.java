/**
 * @file Sprite.java
 * @brief Implementação da classe que desenha as texturas.
 */

/* Pacote */
package xna.android;

/* Importa as libraries */
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.FloatBuffer;

import javax.microedition.khronos.opengles.GL10;

import android.graphics.Rect;

/**
 * @brief Classe usada para desenhar uma imagem.
 */
public final class Sprite
{
	/* Variaveis publicas */
	public int		x;			/**< Posição X. */
	public int 		y;			/**< Posição Y. */
	public int 		z;			/**< Posição Z. */
	public int 		ox;			/**< Origem X.	*/
	public int 		oy;			/**< Origem Y. 	*/
	public float 	zoom_x;		/**< Zoom X.   	*/
	public float 	zoom_y;		/**< Zoom Y.	*/
	public int 		angle;		/**< Angulo.	*/
	public int 		opacity;	/**< Opacidade.	*/
	
	/* Variaveis privadas */
	private Texture texture;
	private Rect 	src_rect;      
	
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
		
		/* Define as coordenadas da texura. */
		float texCoord[] = {
				(float) src_rect.left  / (float) texture.getWidth(), (float) src_rect.top     / (float) texture.getHeight(),
				(float) src_rect.right / (float) texture.getWidth(), (float) src_rect.top     / (float) texture.getHeight(),
				(float) src_rect.left  / (float) texture.getWidth(), (float) src_rect.bottom  / (float) texture.getHeight(),
				(float) src_rect.right / (float) texture.getWidth(), (float) src_rect.bottom  / (float) texture.getHeight()
		};
		
		/* Cria os buffers */
		FloatBuffer vBuff = createFloatBuffer(vertexes);
		FloatBuffer tBuff = createFloatBuffer(texCoord);
		
		/* Associa os buffers ao opengl */
		gl.glVertexPointer(2, GL10.GL_FLOAT, 0, vBuff);
		gl.glTexCoordPointer(2, GL10.GL_FLOAT, 0, tBuff);

		/* Habilita o desenho de vertices e de texturas */
		gl.glEnableClientState(GL10.GL_VERTEX_ARRAY);
		gl.glEnableClientState(GL10.GL_TEXTURE_COORD_ARRAY);

		/* Desenha o sprite */
		gl.glDrawArrays(GL10.GL_TRIANGLE_STRIP, 0, 4);

		/* Desabilita o desenho de vertices e texturas */
		gl.glDisableClientState(GL10.GL_VERTEX_ARRAY);	
		gl.glDisableClientState(GL10.GL_TEXTURE_COORD_ARRAY);
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
	
}
