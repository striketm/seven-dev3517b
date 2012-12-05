package br.com.supermegabros;

import game.util.GameObject;
import android.content.Context;
import android.graphics.drawable.AnimationDrawable;
import android.util.AttributeSet;
import android.widget.AbsoluteLayout;
import android.widget.ImageView;

public class Megaman extends ImageView {
	
	enum Status {PARADO, CORRENDO};
	Status sts;
	
	enum Sentido {DIREITA, ESQUERDA};
	Sentido sentido;
	
	
	enum StatusSolo {SUBINDO, DESCENDO, NO_CHAO};
	
	StatusSolo stsSolo;
	
	AnimationDrawable animation;
	
	int countPulo;

	public Megaman(Context context, AttributeSet attrs) {
		super(context, attrs);
		
		sts = Status.PARADO;
		sentido = Sentido.DIREITA;
		stsSolo = StatusSolo.NO_CHAO;
		
		
	}
	
	
	public void MoverParaDireita() {
		
		
		sts = Status.CORRENDO;
		
		if(sentido != Sentido.DIREITA) {
			
			sentido = Sentido.DIREITA;	
			
		}
		
		setImageResource(R.drawable.megaman_correndo_direita);
		animation = (AnimationDrawable) getDrawable();
		
		animation.start();
		
	}
	
	
   public void MoverParaEsquerda() {
	   
	   sts = Status.CORRENDO;
		
		if(sentido != Sentido.ESQUERDA) {
			
			sentido = Sentido.ESQUERDA;
			
			
		}
		
		setImageResource(R.drawable.megaman_correndo_esquerda);
		animation = (AnimationDrawable) getDrawable();
		
		animation.start();
		
	}
   
   
   public void Parar() {
	   
	   sts = Status.PARADO;
	   
	   if(sentido == Sentido.DIREITA )
	   {
		 
		   setImageResource(R.drawable.megaman_parado_direita);
	   } else {
		   
		  
		   setImageResource(R.drawable.megaman_parado_esquerda);
	   }
	   
	   
   }
   
   public void Pular() {
	   
	   if(stsSolo == StatusSolo.NO_CHAO) {
		   stsSolo = StatusSolo.SUBINDO;
		   countPulo = 0;
	   }
   }
   
   
   public void Processar(AbsoluteLayout tela) {
	   
	   boolean pisouEmAlgo  = false;
	   
	   if(stsSolo == StatusSolo.SUBINDO) {
			countPulo++;
			GameObject.MoveByY(this, -10);
			if(countPulo==12) {
				stsSolo = StatusSolo.DESCENDO;
				
			}
		} else if(stsSolo == StatusSolo.DESCENDO) {
			//Desde até se colidir com algum sólido
			GameObject.MoveByY(this, 10);
			
			
			for(int i = 0; i < tela.getChildCount() ; i++) {
				//Checa colisao com o Chao
				if((tela.getChildAt(i) instanceof Bloco)) {
					
					if(GameObject.CheckCollision(this,tela.getChildAt(i))) {
						
						if((  this.getTop() + this.getHeight()  ) <= (tela.getChildAt(i).getTop() + 15)) {
						
						stsSolo = StatusSolo.NO_CHAO;
						
						
						GameObject.MoveToY(this, tela.getChildAt(i).getTop() - this.getHeight() );
						
						break;
						
						}
						
					}
					
					
					
				}
			}
			
			
		}
		
		else if(stsSolo == StatusSolo.NO_CHAO) {
			
			
			
			for(int i = 0; i < tela.getChildCount() ; i++) {
				//Checa colisao com o Chao
				if((tela.getChildAt(i) instanceof Bloco)) {
					
				
				  
				  //Está pisando em algo ?
				  
				  if(   ((this.getTop() + (this.getHeight())) == (tela.getChildAt(i).getTop())) &&
					 
					    ((this.getLeft() + this.getWidth()) >= tela.getChildAt(i).getLeft() ) && 
						
						((this.getLeft() - this.getWidth()) <= (tela.getChildAt(i).getLeft() + tela.getChildAt(i).getWidth() ) ) )
					 {
					  
					  pisouEmAlgo = true;
					  break;
					  
				  }
				  
				}
				
			}
			
			if(!pisouEmAlgo) {
				
				stsSolo = StatusSolo.DESCENDO;
				
			}
							
		}
		
	}

	   
   }
   
   
   
   
   
   
   
   


