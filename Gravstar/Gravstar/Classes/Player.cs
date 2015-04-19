using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Gravstar
{
	public class Player
	{
		private TextureInfo		textureInfo;
		private TextureInfo[]	textures;
		private GamePadData		gamePadData;
		private Timer			timer;
		private GameObjectManager gameObjectManager;
			
		public Bounds2 			bounds;
		public Vector2 			playerRec;
		public Vector2			playerPos;
		public SpriteUV		    player;
		public Rectangle        Bounding;
		public Vector2i[]		tileIndex;
		public BulletManager[]	bullet;
				
		public int		health;
		public int 		facingDirection;
		public int 		ammo;
		public float	previousTime; 
		public float	currentTime;
		public float	elapsedTime;
		public float	xVelocity;
		public float	yVelocity;
		public float	coolTime;
		public float	shootCoolTime;
		public bool		mayJumpAgain;
		public bool 	aiming;
		public bool 	canBeHit;
		public bool		onGround;
		public bool		startOn;
		public bool 	isPressed;
		public bool		isAlive;
		public bool		canShoot;
		public bool[] 	bulletActive;
		private float increasedAmount = 0.0f;
  private bool goDown;
  private bool isJumping = false;
  private float goingUp = 250.0f;
  private Vector2 min, max;
  private Bounds2 box;
  public bool isColliding = false;
		
		/*
		DIRECTIONS:
		LEFT			= 0
		DIAGONAL-LEFT	= 1
		UP				= 2
		DIAGONAL-RIGHT	= 3
		RIGHT			= 4
		*/
			
		public Player (GameObjectManager gameObjectManager, Scene scene, Vector2 playerPosition)
		{
			this.gameObjectManager = gameObjectManager;
			
			SetSpriteArray();
			
			textureInfo = new TextureInfo();
			textureInfo = textures[3];
			
			timer = new Timer();
			previousTime = (float)timer.Milliseconds();
			
			player = new SpriteUV(textureInfo);
			playerRec = new Vector2(64 ,64);
			player.Quad.S = playerRec;
			playerPos = playerPosition;
						
			isAlive = true;
			bounds = new Bounds2();
			health = 10;
			ammo = 50;
			mayJumpAgain = true;
			onGround = true;
			
			bulletActive = new bool[20];
			bullet = new BulletManager[20];
			
			facingDirection = 4;
			
			for (int i = 0; i < 20; i++)
			{
				bulletActive[i] = false;
				bullet[i] = new BulletManager(scene, new Vector2(-100.0f,-100.0f), 0); 
			}

			facingDirection = 4;
			aiming = false;
			canShoot = true;
			
			scene.AddChild(player);
		}
		
		public void SetSpriteArray()
		{
			textures = new TextureInfo[4];
			textures[0]		= new TextureInfo("/Application/textures/player/sprites/SpriteStandingGun.png");
			textures[1]		= new TextureInfo("/Application/textures/player/sprites/RunRightGun.png");
			textures[2]		= new TextureInfo("/Application/textures/player/sprites/SpriteStandingLeft.png");
			textures[3]		= new TextureInfo("/Application/textures/player/sprites/RunLeftGun.png");	
//			textures[0]			= new TextureInfo("/Application/textures/player/Test.png");
//			textures[1]			= new TextureInfo("/Application/textures/player/Test.png");
//			textures[2]			= new TextureInfo("/Application/textures/player/Test.png");
//			textures[3]			= new TextureInfo("/Application/textures/player/Test.png");
			
			
			
		}
		
		 public void jump(){
   if(goDown == false){
    if(increasedAmount >= goingUp){   
     goDown = true;
    }
    else{
     playerPos.Y += 10.0f;
     increasedAmount += 10.0f;
    }
   }
   else{
    playerPos.Y += 1.0f;
       
    if(onGround == true){
     isJumping = false; 
    }
   }
  }
  
  public void collide(){
   if(isColliding == false){
    this.isColliding = true;
    AppMain.gameScene.RemoveChild(player, true);  
   }
  }
public Bounds2 getBoundingBox(){
   min.X  = player.Position.X;
      min.Y  = player.Position.Y;
   max.X  = player.Position.X + (textureInfo.TextureSizef.X);
   max.Y  = player.Position.Y + (textureInfo.TextureSizef.Y);
      box.Min  = min;   
   box.Max  = max;
   
   return box;
  }
	
		//----------------------------------------------------
		public void Update(Scene gameScene)
		{
        	//Get gamepad input.
			gamePadData = GamePad.GetData(0);
			currentTime = (float)timer.Milliseconds();
			elapsedTime = currentTime - previousTime;
			previousTime = currentTime;
			coolTime+= elapsedTime;
			shootCoolTime += elapsedTime;

			if(health < 0)
			{
				health = 0;	
			}		
			
			//Check if player is on ground.
        	//if (onGround)
			//{
			//				
			//	if (facingDirection > 2)
			//	{
			//		textureInfo	= textures[1];
			//		player.TextureInfo = textureInfo;
			//	}
			//	else if (facingDirection < 2)
			//	{
			//		textureInfo = textures[3];
			//		player.TextureInfo = textureInfo;	
			//	}
			//	
			//	yVelocity = 0;
			//	
			//	xVelocity *= 0.9f;
			//	
			//	//Apply friction.
        	//	if ((gamePadData.Buttons & GamePadButtons.Left) == 0)
			//	{
			//		xVelocity *= 0.65f;
        	//	}
			//	
			//	//Check if cross is pressed.
			//	if ((gamePadData.Buttons & GamePadButtons.Cross) != 0)
			//	{
            //		//Check if player is able to jump.
			//		if (mayJumpAgain)
			//		{
            //    		yVelocity = 11.0f;
            //    		mayJumpAgain = false;
			//		}
        	//	}
        	//	else
			//	{
			//		mayJumpAgain = true;
        	//	}
        	//}	
				
			if (!canShoot)
			{
				if (shootCoolTime >= 300)
				{
					shootCoolTime = 0.0f;
					canShoot = true;
				}
			}
			
			if(health < 0)
			{
				health = 0;	
			}
			if((gamePadData.Buttons & GamePadButtons.Cross) != 0)
   {
    if(isJumping == false){
     goDown = false;
     increasedAmount = 0.0f;
     isJumping = true; 
    }
   }
			  if(isJumping == true){
    jump();
   }
			
			
			for (int i = 0; i < 20; i++)
			{
				if (bulletActive[i] == true)
				{
					bullet[i].Update();
					if(bullet[i].bulletPosition.X < -10 || bullet[i].bulletPosition.X > 970 || bullet[i].bulletPosition.Y < -10 || bullet[i].bulletPosition.Y > 554)
					{
						bullet[i].bulletPosition = new Vector2(-100.0f, -100.0f);
						bulletActive[i] = false;
					}
				}
			}		
			
			//Shooting.
        	if ((gamePadData.Buttons & GamePadButtons.Square) != 0)
        	{
        		//shoot;
				if (canShoot)
				{
					if (ammo > 0)
					{
						ammo --;
						bool bulletNotActive = false;
						int checkCount = 0;
						do
						{
							if (bulletActive[checkCount] == false)
							{
								bullet[checkCount].bulletDirection = facingDirection;
								bullet[checkCount].bulletPosition = new Vector2(playerPos.X + 28,playerPos.Y + 32);
								bulletActive[checkCount] = true;
								bulletNotActive = true;
								canShoot = false;
							}
							checkCount++;
						} 
						
						while(bulletNotActive == false);
					}

				}
        	} 
			
			if ((gamePadData.Buttons & GamePadButtons.Start) != 0 && !isPressed)
        	{
				AppMain.levelManager.SetLevel(14);
				isPressed = true;
				System.Threading.Thread.Sleep(100);
        	} 
			if ((gamePadData.Buttons & GamePadButtons.Start) == 0)
        	{
				isPressed = false;
        	}				
			if(!isAlive)
			{
				AppMain.levelManager.SetLevel(15);
			}
			
			//Left movement.
        	if ((gamePadData.Buttons & GamePadButtons.Left) != 0)
        	{
				playerPos.X += -4.0f;
				//Aim Left
				facingDirection = 0;
        	}
			
			//Right movement.
        	if ((gamePadData.Buttons & GamePadButtons.Right) != 0)
        	{
				playerPos.X += 4.0f;
				//Aim Right
				facingDirection = 4;
        	}
			
			if ((gamePadData.Buttons & GamePadButtons.R) != 0)
			{
				//Stop moving to aim
				aiming = true;
			}
			
			if (aiming)
			{
				if ((gamePadData.Buttons & GamePadButtons.R) == 0)
				{
					//Stop aiming
					aiming = false;
				}
				if ((gamePadData.Buttons & GamePadButtons.Up) != 0)
				{
					//Aim up
					facingDirection = 2;
				}
				if (((gamePadData.Buttons & GamePadButtons.Up) != 0) & ((gamePadData.Buttons & GamePadButtons.Right) != 0))
				{
					//Aim up-right
					facingDirection = 3;
				}
				if (((gamePadData.Buttons & GamePadButtons.Up) != 0) & ((gamePadData.Buttons & GamePadButtons.Left) != 0))
				{
					//Aim up-left
					facingDirection = 1;
				}
				if ((gamePadData.Buttons & GamePadButtons.Left) != 0 & !((gamePadData.Buttons & GamePadButtons.Up) != 0))
        		{
					//Aim Left
					facingDirection = 0;
        		}
        		if ((gamePadData.Buttons & GamePadButtons.Right) != 0 & !((gamePadData.Buttons & GamePadButtons.Up) != 0))
        		{
					//Aim Right
					facingDirection = 4;
        		}
			}
				
			////Slow down player if button is held.
			//if ((gamePadData.Buttons & GamePadButtons.Cross) != 0 & !onGround && yVelocity > 0)
			//{
        	//	yVelocity += 0.1f;
			//}
			//
			////Check if player is off the ground.
			//if (!onGround)
			//{
			//	//Player loses vertical speed tue to gravity.
			//	yVelocity -= 0.5f;
			//	
			//	if ((gamePadData.Buttons & GamePadButtons.Left) == 0 && (gamePadData.Buttons & GamePadButtons.Right) == 0)
			//	{
			//		xVelocity *= 0.975f;
			//	}
			//	
			//	if (facingDirection < 2)
			//	{
			//		textureInfo	= textures[0];
			//		player.TextureInfo = textureInfo;
			//	}
			//	if (facingDirection > 2)
			//	{
			//		textureInfo	= textures[2];
			//		player.TextureInfo = textureInfo;
			//	}
			//}

			////Player shouldn't fall too fast. [Terminal Velocity]
			//if (yVelocity < -5.0f)
			//{
        	//	yVelocity = -5.0f;
			//}
			//	
			////Check if player is on the ground.
            //if (yVelocity != 0.0f)
			//{
			//	onGround = false;
    		//}
			//
			////Check if player has hit the ground.
			//if (playerPos.Y < 0.0f)
			//{
			//	playerPos.Y = 0.0f;
			//	onGround = true;
			//}
						
			//Update player position.
    		//playerPos.Y += yVelocity;
			//playerPos.X += xVelocity;
			//
			////Check if player has hit the wall.
			//if (playerPos.X > Director.Instance.GL.Context.GetViewport().Width - player.Quad.S.X)
			//{
			//	playerPos.X = Director.Instance.GL.Context.GetViewport().Width - player.Quad.S.X;
			//}
			//else if (playerPos.X < 0.0f)
			//{
			//	playerPos.X = 0.0f;
			//}
			
			if(health == 0)	
			{
				isAlive = false;
			}
			
			player.Position = playerPos;
			Bounding = new Rectangle(player.Position.X, player.Position.Y, player.TextureInfo.Texture.Width, player.TextureInfo.Texture.Height);
			
			
			player.Draw();
		}
	}
}

