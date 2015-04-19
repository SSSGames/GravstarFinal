using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Gravstar
{
	public class UIManager
	{
		private TextureInfo		textureInfo;
		private TextureInfo[]	textures;
		private Vector2 origin;
				
		public SpriteUV healthAndArmour;
		
		public UIManager (Scene scene, Vector2 healthPosition)
		{
			SetTextureArray();
			
			textureInfo = new TextureInfo();
			textureInfo = textures[0];
			
			healthAndArmour			= new SpriteUV(textureInfo);
			healthAndArmour.Quad.S	= textureInfo.TextureSizef;
			
			origin.X = textures[0].TextureSizef.X/2;
			origin.Y = textures[0].TextureSizef.Y/2;
			
			healthAndArmour.Position = healthPosition;
		
			//Add to the current scene.
			scene.AddChild(healthAndArmour);
		}
		
		public void SetTextureArray()
		{
			textures 	= new TextureInfo[1];
			textures[0]	= new TextureInfo("/Application/textures/UI/healthAndArmour.png");	
		}
	}
}


//ROTATE CODE
//https://msdn.microsoft.com/en-us/library/bb203869.aspx
//priteTexture = Content.Load<Texture2D>("ship");