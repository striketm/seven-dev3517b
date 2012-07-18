/**
 * 
 */
package xna.android;

import java.text.DecimalFormat;

import android.graphics.Canvas;
import android.util.Log;
import android.view.SurfaceHolder;

public class MainThread extends Thread {
	
	private static final String TAG = MainThread.class.getSimpleName();
		 
	// fps desejado
	private final static int 	MAX_FPS = 50;//

	// maximo numero de frames a serem pulados
	private final static int	MAX_FRAME_SKIPS = 5;

	// o periodo do quadro
	private final static int	FRAME_PERIOD = 1000 / MAX_FPS;	
		
    private DecimalFormat df = new DecimalFormat("0.##");  
	
	private final static int 	STAT_INTERVAL = 1000; 
	
	private final static int	FPS_HISTORY_NR = 10;
	
	private long statusIntervalTimer	= 0l;
	
	private long totalFramesSkipped			= 0l;
	
	private long framesSkippedPerStatCycle 	= 0l;

	private int frameCountPerStatCycle = 0;
	
	private long totalFrameCount = 0l;
	
	private double 	fpsStore[];
	
	private long 	statsCount = 0;
	
	private double 	averageFps = 0.0;

	private SurfaceHolder surfaceHolder;
	
	private Game1 gamePanel;

	private boolean running;
	
	public void setRunning(boolean running) {
		this.running = running;
	}

	public MainThread(SurfaceHolder surfaceHolder, Game1 gamePanel) {
		super();
		this.surfaceHolder = surfaceHolder;
		this.gamePanel = gamePanel;
		

		  /*gamePanel e surfaceHolder declaradas como variavel e passadas as instancias como parametro
		   * ambas sao necessarias (nao apenas o panel) pq precisa-se travar (lock) a tela pra desenho, 
		   * e pra isso precisa do holder
		   * mudar o construtor, conferir*/
	}

	@Override
	public void run() {
		
		initTimingElements();
		
		//Canvas importado, é a imagem
		Canvas spriteBatch;
		
		Log.d(TAG, "loop");

		long beginTime;		// hora de inicio
		long timeDiff;		// tempo de execucao
		int sleepTime;		// ms para dormir (<0 caso nao)
		int framesSkipped;	// numeros de quadros a escapar 

		sleepTime = 0;

		
		while (running) {
			spriteBatch = null;
			try {
				spriteBatch = this.surfaceHolder.lockCanvas();
				synchronized (surfaceHolder) {
					beginTime = System.currentTimeMillis();
					framesSkipped = 0;	
					this.gamePanel.Update();
					this.gamePanel.Draw(spriteBatch);		
					timeDiff = System.currentTimeMillis() - beginTime;
					
					sleepTime = (int)(FRAME_PERIOD - timeDiff);
					
					if (sleepTime > 0) {
						
						try {
							
							Thread.sleep(sleepTime);	
						} catch (InterruptedException e) {}
					}
					
					while (sleepTime < 0 && framesSkipped < MAX_FRAME_SKIPS) {
						
						this.gamePanel.Update(); 
						sleepTime += FRAME_PERIOD;	
						framesSkipped++;
					}
					
					framesSkippedPerStatCycle += framesSkipped;
					storeStats();
				}
			} finally {
				if (spriteBatch != null) {
					surfaceHolder.unlockCanvasAndPost(spriteBatch);
				}
			}	
		}
	}

	private void storeStats() {
		frameCountPerStatCycle++;
		totalFrameCount++;
		
		statusIntervalTimer += FRAME_PERIOD;
		
		if (statusIntervalTimer >= STAT_INTERVAL) {
			
			double actualFps = (double)(frameCountPerStatCycle / (STAT_INTERVAL / 1000));
			
			fpsStore[(int) statsCount % FPS_HISTORY_NR] = actualFps;
			
			statsCount++;
			
			double totalFps = 0.0;
			
			for (int i = 0; i < FPS_HISTORY_NR; i++) {
				totalFps += fpsStore[i];
			}
			
			if (statsCount < FPS_HISTORY_NR) {
				
				averageFps = totalFps / statsCount;
			} else {
				averageFps = totalFps / FPS_HISTORY_NR;
			}
			totalFramesSkipped += framesSkippedPerStatCycle;
			framesSkippedPerStatCycle = 0;
			statusIntervalTimer = 0;
			frameCountPerStatCycle = 0;
		}
	}

	private void initTimingElements() {
		fpsStore = new double[FPS_HISTORY_NR];
		for (int i = 0; i < FPS_HISTORY_NR; i++) {
			fpsStore[i] = 0.0;
		}
	}

}
