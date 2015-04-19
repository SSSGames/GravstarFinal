using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Gravstar
{
	public class BackgroundManager
	{	
		private TextureInfo		textureInfo;
		private TextureInfo		parallaxInfo;
		private TextureInfo[]	textures;
		private TextureInfo[]	parallaxTextures;
		
		public SpriteUV background;
		public SpriteUV parallax;
		
		public int		level;
		public float	yVelocity;
		public float	pwidth;
		public float	width;
		public bool		stop;
		public Vector2 parallaxPos;
		

		//Public functions.
		public BackgroundManager (Scene scene)
		{
			SetTextureArray();
			SetParallaxArray();
			
			textureInfo = new TextureInfo();
			parallaxInfo = new TextureInfo();
			textureInfo = textures[0];
			parallaxInfo = parallaxTextures[0];
			
			stop = false;
			yVelocity = -1.0f;
			
			background			= new SpriteUV(textureInfo);
			parallax			= new SpriteUV(parallaxInfo);
			parallaxPos			= new Vector2(0.0f,0.0f);
			parallaxPos.X		= 0.0f;
			parallaxPos.Y		= 0.0f;
			background.Quad.S	= textureInfo.TextureSizef;
			parallax.Quad.S		= parallaxInfo.TextureSizef;
			
			//Get background bounds.
			Bounds2 b = background.Quad.Bounds2();
			Bounds2 p = parallax.Quad.Bounds2 ();
			
			pwidth 	  = p.Point10.X;
			width     = b.Point10.X;
			
			//Add to the current scene.
			scene.AddChild(background);
			scene.AddChild(parallax);
			
		}	
		
		public void SetTextureArray()
		{
			textures = new TextureInfo[4];
			textures[0]	= new TextureInfo("/Application/textures/background/menuBackground.jpg");
			textures[1]	= new TextureInfo("/Application/textures/background/New Background.jpg");
			textures[2]	= new TextureInfo("/Application/textures/background/pauseBackground.png");
			
		}
		
		public void SetParallaxArray()
		{
			parallaxTextures = new TextureInfo[2];
			parallaxTextures[0] = new TextureInfo("/Application/textures/background/Paralax Background.png");
			parallaxTextures[1] = new TextureInfo("/Application/textures/background/Clear Parallax.png");
			
		}

		public void Update(Scene scene,int level)
		{
			if (level == 0)
			{
				textureInfo	= textures[0];
				parallaxInfo = parallaxTextures[1];
				
				parallax.TextureInfo = parallaxInfo;
				parallax.Draw();
				background.TextureInfo = textureInfo;
				background.Draw();
			}
			else if (level == 14)
			{
				textureInfo	= textures[2];
				parallaxInfo = parallaxTextures[1];
				
				parallax.TextureInfo = parallaxInfo;
				parallax.Draw();				
				background.TextureInfo = textureInfo;
				background.Draw();
								
			}	
			else
			{
			//	Console.WriteLine (parallaxPos.X);
				
				textureInfo	= textures[1];
				parallaxInfo = parallaxTextures[0];
				
				parallax.TextureInfo = parallaxInfo;
				background.TextureInfo = textureInfo;
				background.Draw();
							
				parallaxPos.X -= 0.1f;
				parallax.Position = parallaxPos;
				parallax.Draw();
				
				if (parallaxPos.X < -1580.0f)
				{
				parallaxPos.X = 940.0f;	
				}
				

				
			}
		}
	
		public void Dispose()
		{
			textureInfo.Dispose();
			parallaxInfo.Dispose();
		}
	}
}

