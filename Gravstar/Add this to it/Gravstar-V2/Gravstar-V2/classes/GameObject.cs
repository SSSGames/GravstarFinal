using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace GravstarV2
{
	public class GameObject
	{
		private TextureInfo		textureInfo;
		private TextureInfo[]	textures;
		private Player player;
		private Vector2 origin;
		private Vector2 ringRec;
						
		public SpriteUV ring;
		public Vector4 BoundingSphere;
		public Bounds2 bounds;
		
		public GameObject (Player player, Scene scene, Vector2 ringPosition)
		{
			SetTextureArray();
			
			this.player = player;
			
			textureInfo = new TextureInfo();
			textureInfo = textures[0];
			
			ring		= new SpriteUV(textureInfo);
			ringRec 	= new Vector2(826 ,826);
			ring.Quad.S	= ringRec;
			ring.CenterSprite(TRS.Local.Center);
			
			ring.Position = ringPosition;
			bounds = new Bounds2();
			
			origin.X = ring.Position.X;
			origin.Y = ring.Position.Y;
			
			//Add to the current scene.
			scene.AddChild(ring, 2);
		}	
		
		public void Update(Scene gameScene, GamePadData gpd)
		{
			if((gpd.Buttons & GamePadButtons.L) !=0)
			{
				ring.Rotate(0.005f);
			}
			if((gpd.Buttons & GamePadButtons.R) !=0)
			{
				ring.Rotate(-0.005f);	
			}

			ringCollision();
		}
		
		public void SetTextureArray()
		{
			textures 	= new TextureInfo[1];
			textures[0]	= new TextureInfo("/Application/textures/game objects/ringtest.png");	
		}
				
		public void ringCollision()
		{
			float radius = 361.0f; 	// 380		//(float)(textures[0].TextureSizef.X/2)-165.0f;  //150 is control variable
			Vector2 vecRingCentreToPlayer = player.playerPos-origin;
			//Console.WriteLine ("Vector = "+vecRingCentreToPlayer);	
			if (vecRingCentreToPlayer.Length() < radius)
			{
				//Console.WriteLine ("Inside ring!");
				player.onGround = false;
				//return player.playerPos;								
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
		
		public void Dispose(Scene scene)
		{
			textureInfo.Dispose();
			
			scene.RemoveChild(ring, true);
		}
	}
}