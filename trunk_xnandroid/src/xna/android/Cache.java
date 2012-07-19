/**
 * @file Cache.java
 * @brief Implementação da classe que guarda as texturas.
 */

/* Pacote */
package xna.android;

/* Importa as libraries */
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import javax.microedition.khronos.opengles.GL10;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;

/**
 * @brief Classe cache para carregamento de imagems.
 */
public final class Cache
{
	/* Hash interna */
	private static Map<Integer, Texture> OGLTextureMap = new HashMap<Integer, Texture>();
	
	/**
	 * @brief Limpa o cache.
	 * @param [in] gl - Objeto da classe GL10.
	 */
	public final static void clear(GL10 gl)
	{
		/* Iterator */
		Iterator<Texture> iterator = OGLTextureMap.values().iterator();
		
		/* Desvincula a textura */
		gl.glBindTexture(GL10.GL_TEXTURE_2D, 0);
		
		/* Para cada textura */
        while(iterator. hasNext())
        {        
        	/* Pega a textura e deleta */
        	Texture tex = (Texture) iterator.next();
        	if (tex != null)
        		tex.delete(gl);
        }
        
        /* Limpa a hash */
        OGLTextureMap.clear();
	}	
	
	/**
	 * @brief Carrega uma textura.
	 * @param [in] gl - Objeto da classe GL10.
	 * @param [in] id - ID do resource da textura.
	 * @return Um objeto da classe textura.
	 */
	public final static Texture load(GL10 gl, int id)
	{
		/* Cria o objeto a partir da id */
		Integer resourceID = new Integer(id);
		
		/* Verifica se já existe na hash */
		Texture tex = OGLTextureMap.get(resourceID);
		if (tex != null)
			return tex;
		
		/* Bitmap */
		Bitmap bitmap = null;
		
		/* Tenta carregar o bitmap */
		try
		{
			bitmap = BitmapFactory.decodeResource(Program.activity.getResources(), id);//Main
		}
		catch (Exception var)
		{
			bitmap = null;
		}
		
		/* Não conseguiu carregar o bitmap. */
		if (bitmap == null)
			return null;
		
		/* Cria a textura */
		tex = new Texture(gl, bitmap);
		
		/* Deleta o bitmap */
		bitmap.recycle();
		bitmap = null;
		
		/* Grava ela no map */
		OGLTextureMap.put(resourceID, tex);
		
		/* Retorna a textura */
		return tex;
	}
	
	/**
	 * @brief Pega uma textura se já foi carregada.
	 * @param [in] id - ID do resource da textura.
	 * @return Um objeto da classe textura.
	 */
	public final static Texture find(int id)
	{
		/* Cria o objeto a partir da id */
		Integer resourceID = new Integer(id);
		
		/* Verifica se já existe na hash */
		return OGLTextureMap.get(resourceID);
	}
	
}
