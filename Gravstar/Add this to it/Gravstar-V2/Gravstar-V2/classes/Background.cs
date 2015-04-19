using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace GravstarV2
{
	public class Background
	{	
		private TextureInfo		textureInfo;
		private TextureInfo[]	textures;
		//-------------------------------------------
		public SpriteUV background;
		public SpriteUV pauseScreen;
		//-------------------------------------------
		public bool pauseScreenShowing;
		//--------------------------------------------------------------------------------------
		public Background(Scene scene)
		{
			SetTextureArray();
			
			textureInfo = new TextureInfo();
			
			textureInfo = textures[0];
			background			= new SpriteUV(textureInfo);
			background.Quad.S	= textureInfo.TextureSizef;
			
			textureInfo = textures[2];
			pauseScreen			= new SpriteUV(textureInfo);
			pauseScreen.Quad.S	= textureInfo.TextureSizef;
			
			pauseScreenShowing = false;
			
			//Add to the current scene.
			scene.AddChild(background, 1);
			scene.AddChild(pauseScreen, 3);
		}	
		//--------------------------------------------------------------------------------------
		public void SetTextureArray()
		{			
			textures = new TextureInfo[4];
			textures[0]	= new TextureInfo("/Application/Textures/background/menuBackground.jpg");
			textures[1]	= new TextureInfo("/Application/Textures/background/levelBackground.jpg");
			textures[2]	= new TextureInfo("/Application/Textures/background/pauseBackground.png");		
			textures[3]	= new TextureInfo("/Application/Textures/background/gameOverBackground.jpg");	
		}
		//--------------------------------------------------------------------------------------
		public void Update(Scene scene, int textureValue)
		{
			if (textureValue == 2)
			{
				pauseScreenShowing = true;
			}
			else
			{
				pauseScreenShowing = false;
					
				textureInfo	= textures[textureValue];
			
				background.TextureInfo = textureInfo;
			}
			
			background.Draw();
			pauseScreen.Draw();
		}
		//--------------------------------------------------------------------------------------
		public void Dispose(Scene scene)
		{
			textureInfo.Dispose();
			
			scene.RemoveChild(background, true);
			scene.RemoveChild(pauseScreen, true);
		}
		//--------------------------------------------------------------------------------------
	}
}

