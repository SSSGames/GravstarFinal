using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace GravstarV2
{
	public class Player
	{
		private TextureInfo		textureInfo;
		private TextureInfo[]	textures;
		private GamePadData		gamePadData;
		private Timer			timer;
			
		public Bounds2 			bounds;
		public Vector2 			playerRec;
		public Vector2			playerPos;
		public SpriteTile		player;
		public Vector2i[]		tileIndex;
		
		public int		health;
		public int		armour;
		public int 		facingDirection;
		public float	previousTime; 
		public float	currentTime;
		public float	elapsedTime;
		public float	xVelocity;
		public float	yVelocity;
		public bool		mayJumpAgain;
		public bool 	canBeHit;
		public bool		onGround;
		public bool		startOn;
		public bool 	isPressed;
		public bool		isAlive;
		/*
		DIRECTIONS:
		LEFT			= 0
		DIAGONAL-LEFT	= 1
		UP				= 2
		DIAGONAL-RIGHT	= 3
		RIGHT			= 4
		*/
			
		public Player (Scene scene, Vector2 playerPosition)
		{
			SetSpriteArray();
			
			textureInfo = new TextureInfo();
			textureInfo = textures[3];
			
			timer = new Timer();
			previousTime = (float)timer.Milliseconds();
			
			player = new SpriteTile(textureInfo);
			playerRec = new Vector2(64 ,64);
			player.Quad.S = playerRec;
			player.CenterSprite(TRS.Local.Center);
			playerPos = playerPosition;
			
			isAlive = true;
			bounds = new Bounds2();
			health = 100;
			armour = 100;
			mayJumpAgain = true;
			onGround = true;
			
			facingDirection = 4;
			
			facingDirection = 4;
			
			scene.AddChild(player, 2);
		}
		
		public void SetSpriteArray()
		{
			textures = new TextureInfo[6];
			textures[0]		= new TextureInfo("/Application/textures/player/sprites/playerSheet.jpg");
			textures[1]		= new TextureInfo("/Application/textures/player/sprites/playerSheet.jpg");
			textures[2]		= new TextureInfo("/Application/textures/player/sprites/playerSheet.jpg");
			textures[3]		= new TextureInfo("/Application/textures/player/sprites/playerSheet.jpg");	
			textures[4]		= new TextureInfo("/Application/textures/player/sprites/playerSheet.jpg");
			textures[5]		= new TextureInfo("/Application/textures/player/sprites/playerSheet.jpg");
		}
		
		public void Update(Scene gameScene)
		{
        	//Get gamepad input.
			gamePadData = GamePad.GetData(0);
			currentTime = (float)timer.Milliseconds();
			elapsedTime = currentTime - previousTime;
			previousTime = currentTime;

			//Check if player is on ground.
        	if (onGround)
			{
				if (facingDirection < 2)
				{
					textureInfo	= textures[0];
					player.TextureInfo = textureInfo;
				}
				if (facingDirection > 2)
				{
					textureInfo	= textures[1];
					player.TextureInfo = textureInfo;
				}
				
				yVelocity = 0;
				
				xVelocity *= 0.9f;
				
				//Apply friction.
        		if ((gamePadData.Buttons & GamePadButtons.Left) == 0)
				{
					xVelocity *= 0.65f;
        		}
				
				//Check if cross is pressed.
				if ((gamePadData.Buttons & GamePadButtons.Cross) != 0)
				{
            		//Check if player is able to jump.
					if (mayJumpAgain)
					{
                		yVelocity = 11.0f;
                		mayJumpAgain = false;
					}
        		}
        		else
				{
					mayJumpAgain = true;
        		}
        	}	
			
			if(health < 0)
			{
				health = 0;	
			}
			
			if(armour < 0)
			{
				armour = 0;	
			}
			
			if(health > 100)
			{
				health = 100;	
			}
			
			if(armour > 100)
			{
				armour = 100;	
			}
			
			if ((gamePadData.Buttons & GamePadButtons.Start) != 0 && !isPressed)
        	{
				isPressed = true;
				System.Threading.Thread.Sleep(100);
        	} 
			if ((gamePadData.Buttons & GamePadButtons.Start) == 0)
        	{
				isPressed = false;
        	}				
			//Left movement.
        	if ((gamePadData.Buttons & GamePadButtons.Left) != 0)
        	{
				xVelocity = -4.0f;
				//Aim Left
				facingDirection = 0;
				health += 1;
				armour += 2;
        	}
			
			//Right movement.
        	if ((gamePadData.Buttons & GamePadButtons.Right) != 0)
        	{
				xVelocity = 4.0f;
				//Aim Right
				facingDirection = 4;
				health -= 1;
				armour -= 2;
        	}
				
			//Slow down player if button is held.
			if ((gamePadData.Buttons & GamePadButtons.Cross) != 0 & !onGround && yVelocity > 0)
			{
        		yVelocity += 0.1f;
			}
			
			//Check if player is off the ground.
			if (!onGround)
			{
				//Player loses vertical speed tue to gravity.
				yVelocity -= 0.5f;
				
				if ((gamePadData.Buttons & GamePadButtons.Left) == 0 && (gamePadData.Buttons & GamePadButtons.Right) == 0)
				{
					xVelocity *= 0.975f;
				}
				
				if (facingDirection < 2)
				{
					textureInfo	= textures[4];
					player.TextureInfo = textureInfo;
				}
				if (facingDirection > 2)
				{
					textureInfo	= textures[5];
					player.TextureInfo = textureInfo;
				}
			}

			//Player shouldn't fall too fast. [Terminal Velocity]
			if (yVelocity < -5.0f)
			{
        		yVelocity = -5.0f;
			}
			
			//Check if player is on the ground.
            if (yVelocity != 0.0f)
			{
				onGround = false;
    		}
			
			//Check if player has hit the ground.
			if (playerPos.Y < 0.0f)
			{
				playerPos.Y = 0.0f;
				onGround = true;
			}
						
			//Update player position.
    		playerPos.Y += yVelocity;
			playerPos.X += xVelocity;
			
			//Check if player has hit the wall.
			if (playerPos.X > Director.Instance.GL.Context.GetViewport().Width - player.Quad.S.X)
			{
				playerPos.X = Director.Instance.GL.Context.GetViewport().Width - player.Quad.S.X;
			}
			else if (playerPos.X < 0.0f)
			{
				playerPos.X = 0.0f;
			}
			
			if(health == 0)	
			{
				//isAlive = false;
			}
			
			player.Position = playerPos;
			player.Draw();
		}
		
		public void Dispose(Scene scene)
		{
			textureInfo.Dispose();
			
			scene.RemoveChild(player, true);
		}
	}
}

