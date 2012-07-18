/**
 * @file Texture.java
 * @brief Implementação da classe que manipula texturas do opengl.
 */

/* Pacote */
package xna.android;

/* Importa as libraries */
import javax.microedition.khronos.opengles.GL10;

import android.graphics.Bitmap;
import android.opengl.GLUtils;

/**
 * @brief Guarda uma textura do OpenGL.
 */
public class Texture
{
	/* Vars privadas */
	private int Width;
	private int Height;
	private int OGLTextureID;
	
	/**
	 * @brief Construtor da textura.
	 * @param [in] gl - Objeto do opengl.
	 * @param [in] bitmap - Bitmap usado para a criação da textura.
	 */
	protected Texture(GL10 gl, Bitmap bitmap)
	{
		/* Cria a var ponteiro */
		int[] pointer = { 0 };
		
		/* Cria uma textura do opengl */
		gl.glGenTextures(1, pointer, 0);
		
		/* Define ela como a atual. */
		gl.glBindTexture(GL10.GL_TEXTURE_2D, pointer[0]);
		
		/* Define os parametros da textura */
		gl.glTexParameterf(GL10.GL_TEXTURE_2D, GL10.GL_TEXTURE_MIN_FILTER, GL10.GL_LINEAR);
		gl.glTexParameterf(GL10.GL_TEXTURE_2D, GL10.GL_TEXTURE_MAG_FILTER, GL10.GL_LINEAR);
		gl.glTexParameterf(GL10.GL_TEXTURE_2D, GL10.GL_TEXTURE_WRAP_S, GL10.GL_CLAMP_TO_EDGE);
		gl.glTexParameterf(GL10.GL_TEXTURE_2D, GL10.GL_TEXTURE_WRAP_T, GL10.GL_CLAMP_TO_EDGE);
		
		/* Define os pixels da textura */
		GLUtils.texImage2D(GL10.GL_TEXTURE_2D, 0, bitmap, 0);
		
		/* Grava as variaveis */
		OGLTextureID = pointer[0];
		Width = bitmap.getWidth();
		Height = bitmap.getHeight();
	};
	
	/**
	 * Deleta a textura.
	 * @param [in] gl - Objeto do opengl.
	 */
	protected void delete(GL10 gl)
	{
		/* Desvincula a textura. */
		gl.glBindTexture(GL10.GL_TEXTURE_2D, 0);
		
		/* Cria a var ponteiro */
		int[] pointer = { OGLTextureID };
		
		/* Deleta a textura */
		gl.glDeleteTextures(1, pointer, 0);
		
		/* Limpa as variaveis. */
		OGLTextureID = 0;
		Width        = 0;
		Height       = 0;
	};
	
	/**
	 * @brief Pega a largura da textura.
	 * @return Largura da textura.
	 */
	public int getWidth() { return Width; };
	
	/**
	 * @brief Pega a altura da textura.
	 * @return Altura da textura.
	 */
	public int getHeight() { return Height; };
	
	/**
	 * @brief Pega a ID da textura do OPENGL.
	 * @return ID da textura.
	 */
	protected int getOGLTextureID() { return OGLTextureID; };
};