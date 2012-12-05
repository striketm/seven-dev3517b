using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using game.util;
using game.util.scene;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace game.util.character
{
    class Character : GameElement {
    
        enum ActionState { IDLE, WALKING, RUNNING, ATTACK1, ATTACK2, ATTACK3, ATTACK4, ATTACK5, DAMAGED }
	
	    enum DirectionState { RIGHT, LEFT }
	
	    enum JumpState { JUMPING, FALLING, IS_GROUND }
	
	    enum DamageState { NO_DAMAGE, DAMAGED }
	
	    ActionState action;
	
	    DirectionState direction;
	
	    JumpState jump;
	
	    DamageState damageState;
	
	    List<AnimationSprites> animationList;
	
	    AnimationSprites aIdle, aWalking, aRunning, aAttack1, aAttack2, aAttack3, 
	    aAttack4, aAttack5, aJumping, aDamaged;
	
	    List<String> aCollisionElementBySide_Tag;

	    List<String> aCollisionElementBySide_Type;
	
	    List<String> aCollisionElementByFall_Tag;

	    List<String> aCollisionElementByFall_Type;
	
	
	    int INITIAL_VELOCITY_JUMP = -18;
	
	    int MAX_VELOCITY_FALL = 12;
	
	    int JumpShift;
	
	    int MAX_FRAME_DAMAGE = 15;
	
	    int countFrameDamage;
	
	    bool enableFall = true;
	
	
	
	public Character(ContentManager content, int x, int y, int w, int h)
	{
		aIdle = new AnimationSprites(content, x, y, w, h);
		
		aWalking = new AnimationSprites(content, x, y, w, h);
		
		aRunning = new AnimationSprites(content, x, y, w, h);
		
		aAttack1 = new AnimationSprites(content, x, y, w, h);
		
		aAttack2 = new AnimationSprites(content, x, y, w, h);
		
		aAttack3 = new AnimationSprites(content, x, y, w, h);
		
		aAttack4 = new AnimationSprites(content, x, y, w, h);
		
		aAttack5 = new AnimationSprites(content, x, y, w, h);
						
		aJumping = new AnimationSprites(content, x, y, w, h);
		
		aDamaged = new AnimationSprites(content, x, y, w, h);
		
		SetBounds(x, y, w, h);
		
		animationList = new List<AnimationSprites>();
		
		animationList.Add(aAttack1);
		animationList.Add(aAttack2);
		animationList.Add(aAttack3);
		animationList.Add(aAttack4);
		animationList.Add(aAttack5);
		animationList.Add(aIdle);
		animationList.Add(aWalking);
		animationList.Add(aRunning);
		animationList.Add(aJumping);
		animationList.Add(aDamaged);
		
		
		aCollisionElementBySide_Tag =  new List<String>();
		aCollisionElementBySide_Type = new List<String>();
		
		aCollisionElementByFall_Tag =  new List<String>();
		aCollisionElementByFall_Type = new List<String>();
		
		direction = DirectionState.RIGHT;
		jump = JumpState.IS_GROUND;
		damageState = DamageState.NO_DAMAGE;
		
		
	}
	
	
	public void AddNewSpriteIdle(string pathName)
	{
		aIdle.Add(pathName);
	}
	
	public void AddNewSpriteWalking(string pathName)
	{
		aWalking.Add(pathName);
	}
	
	public void AddNewSpriteRunning(string pathName)
	{
		aRunning.Add(pathName);
	}
	
	public void AddNewSpriteAttack1(string pathName)
	{
		aAttack1.Add(pathName);
	}
	
	public void AddNewSpriteAttack2(string pathName)
	{
		aAttack2.Add(pathName);
	}
	
	public void AddNewSpriteAttack3(string pathName)
	{
		aAttack3.Add(pathName);
	}
	
	
	public void AddNewSpriteAttack4(string pathName)
	{
		aAttack4.Add(pathName);
	}
	
	public void AddNewSpriteAttack5(string pathName)
	{
		aAttack5.Add(pathName);
	}
	
	public void AddNewSpriteJumping(string pathName)
	{
		aJumping.Add(pathName);
	}
	
	public void AddNewSpriteDamage(string pathName)
	{
		aDamaged.Add(pathName);
	}
	
	
	public void Idle(int frames, bool loop  )
	{
	    action = ActionState.IDLE;	
		aIdle.Start(frames, loop);
	}
	
	
	public void WalkingToRight(int frames, bool loop )
	{
		if((direction != DirectionState.RIGHT) || (action != ActionState.WALKING))
		{
		  direction = DirectionState.RIGHT;
 		  action = ActionState.WALKING;
		  aWalking.Start(frames, loop);
		  
		}
		
	}
	

	public void WalkingToLeft(int frames, bool loop )
	{
		if((direction != DirectionState.LEFT) || (action != ActionState.WALKING))
		{
		  direction = DirectionState.LEFT;
 		  action = ActionState.WALKING;
		  aWalking.Start(frames, loop);
		  
		}
		
	}
	
	public void RunningToRight(int frames, bool loop )
	{
		if((direction != DirectionState.RIGHT) || (action != ActionState.RUNNING))
		{
		  direction = DirectionState.RIGHT;
 		  action = ActionState.RUNNING;
		  aRunning.Start(frames, loop);
		  
		}
		
	}
	
	public void RunningToLeft(int frames, bool loop )
	{
		if((direction != DirectionState.LEFT) || (action != ActionState.RUNNING))
		{
		  direction = DirectionState.LEFT;
 		  action = ActionState.RUNNING;
		  aRunning.Start(frames, loop);
		  
		}
		
	}
	
	public void Attack1(int frames) {
		
		action = ActionState.ATTACK1;
		aAttack1.Start(frames, false);
		
	}
	
	
    public void Attack2(int frames) {
		
		action = ActionState.ATTACK2;
		aAttack2.Start(frames, false);
		
	}
    
    public void Attack3(int frames) {
		
		action = ActionState.ATTACK3;
		aAttack3.Start(frames, false);
		
	}
    
    
    public void Attack4(int frames) {
		
		action = ActionState.ATTACK4;
		aAttack4.Start(frames, false);
		
	}
    
    
    public void Attack5(int frames) {
		
		action = ActionState.ATTACK5;
		aAttack5.Start(frames, false);
		
	}
    
    public void Jump(int frames, bool loop)
    {
    	if(jump == JumpState.IS_GROUND)
    	{
    		jump = JumpState.JUMPING;
    		JumpShift = INITIAL_VELOCITY_JUMP;    		
    		aJumping.Start(frames, loop);
    	}
    }
    
	

	
	public override void Draw(SpriteBatch spriteBatch) {
		
		if(damageState == DamageState.DAMAGED) {
			  
			
			countFrameDamage++;
			
			if(countFrameDamage != MAX_FRAME_DAMAGE)
			{
			   if((countFrameDamage % 2) == 0)
				  return;
			}
			else 
		    {
			  countFrameDamage = 0;
			  damageState = DamageState.NO_DAMAGE;
		    }
		}
		
		

		if(jump == JumpState.IS_GROUND)
		{
			if(action == ActionState.IDLE)
			{
				if(direction == DirectionState.RIGHT)
					aIdle.Draw(spriteBatch);
				else
					aIdle.Draw(spriteBatch,true);
			}
			else if(action == ActionState.WALKING)
			{
				if(direction == DirectionState.RIGHT)
					aWalking.Draw(spriteBatch);
				else
					aWalking.Draw(spriteBatch,true);
			}
			else if(action == ActionState.RUNNING)
			{
				if(direction == DirectionState.RIGHT)
					aRunning.Draw(spriteBatch);
				else
					aRunning.Draw(spriteBatch,true);
			}
			else if(action == ActionState.ATTACK1)
			{
				if(direction == DirectionState.RIGHT)
					aAttack1.Draw(spriteBatch);
				else
					aAttack1.Draw(spriteBatch,true);
			}
			
			else if(action == ActionState.ATTACK2)
			{
				if(direction == DirectionState.RIGHT)
					aAttack2.Draw(spriteBatch);
				else
					aAttack2.Draw(spriteBatch,true);
			}
			
			else if(action == ActionState.ATTACK3)
			{
				if(direction == DirectionState.RIGHT)
					aAttack3.Draw(spriteBatch);
				else
					aAttack3.Draw(spriteBatch,true);
			}
			
			else if(action == ActionState.ATTACK4)
			{
				if(direction == DirectionState.RIGHT)
					aAttack4.Draw(spriteBatch);
				else
					aAttack4.Draw(spriteBatch,true);
			}
			
			else if(action == ActionState.ATTACK5)
			{
				if(direction == DirectionState.RIGHT)
					aAttack5.Draw(spriteBatch);
				else
					aAttack5.Draw(spriteBatch,true);
			}
			else if(action == ActionState.DAMAGED)
			{
				if(direction == DirectionState.RIGHT)
					aDamaged.Draw(spriteBatch);
				else
                    aDamaged.Draw(spriteBatch, true);
			}
			
		} else {
			
			if(direction == DirectionState.RIGHT)
				aJumping.Draw(spriteBatch);
			else
				aJumping.Draw(spriteBatch,true);
			
		}

	}
	
	
	public void Update(Scene scene) {
		
		
		bool isGround = false;
		
		
		
		if(action == ActionState.ATTACK1)
		{
			if(!aAttack1.IsPlaying())
			{
				action = ActionState.IDLE;
			}
		} 
		else if(action == ActionState.ATTACK2)
		{
			if(!aAttack2.IsPlaying())
			{
				action = ActionState.IDLE;
			}
		} 
		
		else if(action == ActionState.ATTACK3)
		{
			if(!aAttack3.IsPlaying())
			{
				action = ActionState.IDLE;
			}
		}
		
		else if(action == ActionState.ATTACK4)
		{
			if(!aAttack4.IsPlaying())
			{
				action = ActionState.IDLE;
			}
		}
		
		else if(action == ActionState.ATTACK5)
		{
			if(!aAttack5.IsPlaying())
			{
				action = ActionState.IDLE;
			}
		}
		
		else if(action == ActionState.DAMAGED)
		{
			if(!aDamaged.IsPlaying())
			{
				action = ActionState.IDLE;
			}
		}
		   
		if(!enableFall)
			return; //Sai, não será processado nenhuma queda
		   
		   if(jump == JumpState.JUMPING) {
			   
			   
			   this.MoveByY(JumpShift);
			   
			 
			   JumpShift++;
			   
			   if(JumpShift == 0) {
				  jump = JumpState.FALLING;
				   
			   }
		   } else if(jump == JumpState.FALLING) {
			   
	           this.MoveByY(JumpShift);
	           
	           JumpShift++;
	           
	           if(JumpShift > MAX_VELOCITY_FALL)
	        	   JumpShift--;
			   
			  
			   
			   //Processa todos os elementos da tela para ver se colidiu com o chao
			   foreach(GameElement element in scene.Elements()) {
				   
				   bool colidiu = false;
				   
				   if( (IsCollisionElementByFall(element)) || (IsCollisionElementBySide(element)))  {
				      
					   
					   //Checa a colisao entre os objetos
					   if(Collision.Check(this, element)) {
						   
						  
						   
						   if ( (this.GetY() + this.GetHeight()) <= (element.GetY() + 15)) {
							   
							   jump = JumpState.IS_GROUND;
							   
							   
							   
							   this.SetY(element.GetY() - (this.GetHeight()));
							   
							   colidiu = true;
							   
						   }
						   
					   }
				   
				   }
				   
				   if(colidiu)
				     break;
				   
			   }
			   
			   
		   } else if(jump == JumpState.IS_GROUND) {
			   
			   foreach(GameElement element in scene.Elements()) {
				   
				   if( (IsCollisionElementByFall(element)) || (IsCollisionElementBySide(element)))  {
					   
					  
					   if(   
							   
							   
							   ((this.GetY() + (this.GetHeight())) == (element.GetY())) &&
								 
							    ((this.GetX() + (this.GetWidth())) >= element.GetX() ) && 
								
								((this.GetX()) <= (element.GetX() + element.GetWidth() ) ) ) {
						   
						   isGround = true;
						   
						   break;
					   }
					   
					   
				   }
				   
				   
			   }
			   
			   if(!isGround)
			   {
				   jump = JumpState.FALLING;
				   JumpShift = 0;
				  
			   }
			   
		   }

		
		
	}
	
	public bool IsAttacking() {
		return ((action == ActionState.ATTACK1) || (action == ActionState.ATTACK2) ||
				(action == ActionState.ATTACK3) || (action == ActionState.ATTACK4) ||
				(action == ActionState.ATTACK5));
	}
	
	public bool IsDamaged()
	{
		return (damageState == DamageState.DAMAGED);
	}
	
	public void SufferDamage(int frames) {
		countFrameDamage = 0;
		damageState = DamageState.DAMAGED;
		if(aDamaged.GetCount() > 0) {
			
		   action = ActionState.DAMAGED;	
		   aDamaged.Start(frames, false);
		
		}
	}
	
	public void SetMaxFramesDamage(int max_frames)
	{
		MAX_FRAME_DAMAGE = max_frames;
	}
	
	
	public void AddCollisionElementOfFallByTag(String tag) {
		aCollisionElementByFall_Tag.Add(tag);
	}
	
	public void AddCollisionElementOfSideByTag(String tag) {
		aCollisionElementBySide_Tag.Add(tag);
	}
	
	
	public void AddCollisionElementOfFallByType(String type) {
		aCollisionElementByFall_Type.Add(type);
	}
	
	public void AddCollisionElementOfSideByType(String type) {
		aCollisionElementBySide_Type.Add(type);
	}
	
	
	private bool IsCollisionElementByFall(GameElement element) {
		
		bool isElement = false;
		
		foreach(String type in aCollisionElementByFall_Type)
		{
			
			if(element.GetType().ToString()  == type ) {
				isElement = true;
			    break;
			}
		}
			
		if(isElement)
			return true;
		else
		
		{
			
			foreach(String tag in aCollisionElementByFall_Tag)
			{
				if(element.GetTag() == tag) {
					isElement = true;
					
				    break;	
				}
			}
			
			if(isElement)
				return true;
							
		}
		
		return isElement;
		
	}
	
	
  private bool IsCollisionElementBySide(GameElement element) {
		
		bool isElement = false;
		
		foreach(String type in aCollisionElementBySide_Type)
		{
			
			if(element.GetType().ToString() == type)
				isElement = true;
		}
			
		if(isElement)
			return true;
		else
		
		{
			
			foreach(String tag in aCollisionElementBySide_Tag)
			{
				if(element.GetTag() == tag)
					isElement = true;
			}
			
			if(isElement)
				return true;
			
				
		}
		
		return isElement;
		
	}
	
    
  public void MoveByX(int value) {
	 
	 
	 base.MoveByX(value);
	 
	 foreach(AnimationSprites a in animationList)
		 a.MoveByX(value);
	 
  }
 
  public void MoveByY(int value) {
	 
	 
	 base.MoveByY(value);
	 
	 foreach(AnimationSprites a in animationList)
		 a.MoveByY(value);
	 
  }
  
   public void SetX(int value) {
		 
		 
		 base.SetX(value);
		 
		 foreach(AnimationSprites a in animationList)
			 a.SetX(value);
		 
   }
   
   public void SetY(int value) {
		 
		 
		 base.SetY(value);
		 
		 foreach(AnimationSprites a in animationList)
			 a.SetY(value);
		 
   }
   
   public void SetWidth(int value) {
		 
		 
		 base.SetWidth(value);
		 
		 foreach(AnimationSprites a in animationList)
			 a.SetWidth(value);
		 
  }
   
   public void SetHeight(int value) {
		 
		 
		 base.SetHeight(value);
		 
		 foreach(AnimationSprites a in animationList)
			 a.SetHeight(value);
		 
  }
   
   
   public bool CollisionBySide(Scene scene)
   {
	   bool anyCollision = false;
	   
	   foreach(GameElement e in scene.Elements())
	   {
		   if(IsCollisionElementBySide(e))
		   {
			 if(Collision.Check(this, e))
			 {
			   anyCollision = true;
			   break;
			 }
		   }
	   }
	   
	   return anyCollision;
   }

   public void SetEnableFall(bool fall)
   {
	   enableFall = fall;
   }
   
   public void TurnToLeft()
   {
	   direction = DirectionState.LEFT;
   }
   
   public void TurnToRight()
   {
	   direction = DirectionState.RIGHT;
   }

    }
}
