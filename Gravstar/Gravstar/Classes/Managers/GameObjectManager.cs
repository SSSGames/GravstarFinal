using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;


namespace Gravstar
{
	public class GameObjectManager
	{
		private TextureInfo		textureInfo;
		private TextureInfo[]	textures;
		private Vector2 origin;

		public Vector2 Origin {
			get {
				return this.origin;
			}
			
		}

		private Bounds2 box;
		private Vector2 ringRec;
		private Vector2 min, max;
		private Player player;
		
		
		//Obstacle List ---------------
		
		private Vector2 ObstacleRect;
		private Vector2 mObstaclePos;
		
		//-----------------------------
						
		public SpriteUV ring;
		public SpriteUV Obstacle;
		public Vector4 BoundingSphere;
		public Bounds2 bounds;
		

		
		public GameObjectManager (Vector2 ringPosition)
		{
			SetTextureArray();
									
			textureInfo = new TextureInfo();
			textureInfo = textures[0];
						
			//-----------------------------------------------------------Obstacle Stuff------------------------------------------------------------
		//	Obstacle = new SpriteUV(textureInfo);
						
		//	ObstacleRect = new Vector2(100, 100); //Size of Box
		//	Obstacle.Quad.S	= ObstacleRect;
						
			mObstaclePos = new Vector2(0, -200); // Location on screen
			
		//	Obstacle.CenterSprite (TRS.Local.Center);
			
			
		//	ring.AddChild(Obstacle);
			
			
			//----------------------------------------------------------------------------------------------------------------------------------------
			
			ring		= new SpriteUV(textureInfo);
			ringRec = new Vector2(886 ,886);
			ring.Quad.S	= ringRec;
			ring.CenterSprite (TRS.Local.Center);
			
			origin.X = textures[0].TextureSizef.X/2;
			origin.Y = textures[0].TextureSizef.Y/2;
			
			ring.Position = ringPosition;
			bounds = new Bounds2();
		
			//Add to the current scene.
					

		}	
		public void addRing(Scene scene){
			scene.AddChild(ring);	
		}
		public void SetPlayer(Player P)
		{
			this.player = P;	
		}
		
		public void addExit(Exit e){
			e.sprite   = new SpriteUV(e.textureInfo);
		   e.sprite.Quad.S = e.textureInfo.TextureSizef;
		   e.bounds = e.sprite.Quad.Bounds2();
			e.sprite.Quad.S = new Vector2(70, 90); //size of exit
			e.sprite.Position = new Vector2(-170, -200);
			e.sprite.CenterSprite (TRS.Local.Center);
			ring.AddChild(e.sprite);
		}
		
		public void addPlatforms(Plat p){
	   
	   
	   p.platSprite   = new SpriteUV(p.textureInfo);
	   p.platSprite.Quad.S = p.textureInfo.TextureSizef;
	  // p.bounds = p.platSprite.Quad.Bounds2();
//	   p.sprite.Position = position;
			
			ObstacleRect = new Vector2(50, 50); //Size of Box
			p.platSprite.Quad.S	= ObstacleRect;
			mObstaclePos = p.position; // Location on screen
			p.platSprite.Position = mObstaclePos;
			p.platSprite.CenterSprite (TRS.Local.Center);
			
			ring.AddChild(p.platSprite);
			
		}
		
		public void Update(Scene gameScene, GamePadData gpd)
		{
			if((gpd.Buttons & GamePadButtons.L) !=0)
			{
				ring.Rotate (0.005f);	
			}
			if((gpd.Buttons & GamePadButtons.R) !=0)
			{
				ring.Rotate (-0.005f);	
			}
			
			
			ringCollision();
		}
		
		public void SetTextureArray()
			
		{
			textures 	= new TextureInfo[2];
			textures[0]	= new TextureInfo("/Application/textures/other/newRing.png");
			textures[1] = new TextureInfo("/Application/textures/other/box.png");
		}
		
		public Bounds2 getBoundingBox(){
			min.X  = ring.Position.X;
		    min.Y  = ring.Position.Y;
			max.X  = ring.Position.X + (textureInfo.TextureSizef.X);
			max.Y  = ring.Position.Y + (textureInfo.TextureSizef.Y);
		    box.Min  = min;   
			box.Max  = max;
			return box;
			
			
		}
		
				
	public void ringCollision()
		{
			origin.X = Director.Instance.GL.Context.GetViewport().Width*0.5f - 24.0f;////textures[0].TextureSizef.X/2;
			origin.Y = Director.Instance.GL.Context.GetViewport().Height;////textures[0].TextureSizef.Y/2
				
						
			float   radius = 380.0f; 	// 380		//(float)(textures[0].TextureSizef.X/2)-165.0f;  //150 is control variable
			Vector2 vecRingCentreToPlayer = player.playerPos-origin;
			
			if (vecRingCentreToPlayer.Length() < radius)
			{
				//Console.WriteLine ("Inside ring!");
				//Console.WriteLine (vecRingCentreToPlayer);
				player.onGround = false;
				//return player.playerPos;
				
				player.playerPos.Y -= 5.0f;
										
			}
			else 
			{
				//Console.WriteLine ("Outside of ring!");
				//Console.WriteLine (vecRingCentreToPlayer);
				player.onGround = true;
				
				
				Vector2 unitVector = vecRingCentreToPlayer.Normalize();
				Vector2 newPlayerPosition = unitVector * radius;
				player.playerPos = origin + newPlayerPosition;
				
				
			}
			
	   }
		
	}
}