package xna.android;

import android.opengl.GLSurfaceView.Renderer;
import javax.microedition.khronos.opengles.GL10;
import javax.microedition.khronos.egl.EGLConfig;

import android.opengl.GLU;

public final class Video implements Renderer
{
	
	public Sprite s = null;
	
	public GL10 test;
	
	public final void onSurfaceCreated(GL10 gl, EGLConfig config)
	{
		/* Inicializa o opengl */
		gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
		
		/* Habilita o blend com alpha */
		gl.glEnable(GL10.GL_TEXTURE_2D);
		gl.glEnable(GL10.GL_BLEND);
		gl.glBlendFunc(GL10.GL_SRC_ALPHA, GL10.GL_ONE_MINUS_SRC_ALPHA);
		
		/* Define outros parametros */
		gl.glShadeModel(GL10.GL_SMOOTH);
		gl.glHint(GL10.GL_PERSPECTIVE_CORRECTION_HINT, GL10.GL_NICEST);
		
		s = new Sprite();
		//s.setTexture(Cache.load(gl, R.drawable.));
		//s.setRect(32, 32, 32, 32);
		
		test = gl;
	}
	
	public final void onDrawFrame(GL10 gl)
	{
		/* Limpa o buffer */
		gl.glClear(GL10.GL_COLOR_BUFFER_BIT);
		
		/* Define a matriz */
		gl.glMatrixMode(GL10.GL_MODELVIEW);
		gl.glLoadIdentity();
		
		if ((test == gl) && (s != null))
		{
			s.angle += 5;
			s.draw(gl);
		}
	}
	
	public final void onSurfaceChanged(GL10 gl, int width, int height)
	{
		/* Define o viewport */
		gl.glViewport(0, 0, width, height);
		
		/* Define a matriz de projeção */
		gl.glMatrixMode(GL10.GL_PROJECTION);
		gl.glLoadIdentity();
		GLU.gluOrtho2D(gl, 0.0f, width, height, 0.0f);
		
		/* Limpa a matriz de modelo. */
		gl.glMatrixMode(GL10.GL_MODELVIEW);
		gl.glLoadIdentity();
	}
	
}
