using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;

namespace GravstarV2
{
	public class Hud
	{
		private TextureInfo		textureInfo;
		private TextureInfo[]	textures;
		private Vector2[]		vectors;
				
		public SpriteUV healthAndArmour;
		public SpriteUV healthBar;
		public SpriteUV armourBar;
		public SpriteUV healthDamage;
		public SpriteUV armourDamage;
		public float healthChange;
		public float armourChange;
		
		public Hud (Scene scene)
		{	
			SetTextureArray();
			SetVectorArray();
			
			textureInfo = new TextureInfo();
			
			textureInfo = textures[0];
			healthAndArmour			 = new SpriteUV(textureInfo);
			healthAndArmour.Quad.S   = vectors[5];
			healthAndArmour.Position = vectors[0];
			
			textureInfo = textures[1];
			healthBar			= new SpriteUV(textureInfo);
			healthBar.Quad.S	= vectors[6];
			healthBar.Position  = vectors[1];
			
			textureInfo = textures[2];
			armourBar			= new SpriteUV(textureInfo);
			armourBar.Quad.S	= vectors[7];
			armourBar.Position  = vectors[2];
			
			textureInfo = textures[3];
			healthDamage		= new SpriteUV(textureInfo);
			healthDamage.Quad.S	= vectors[8];
			healthDamage.Position  = vectors[3];
			
			textureInfo = textures[4];
			armourDamage		= new SpriteUV(textureInfo);
			armourDamage.Quad.S	= vectors[9];
			armourDamage.Position  = vectors[4];
		
			//Add to the current scene.
			scene.AddChild(healthDamage, 2);
			scene.AddChild(armourDamage, 2);
			scene.AddChild(healthBar, 2);
			scene.AddChild(armourBar, 2);
			scene.AddChild(healthAndArmour, 2);
		}
		
		public void Update(GamePadData gpd, int health, int armour)
		{
			healthChange = health * 2.3f;
			armourChange = armour * 2.3f;
			healthBar.Quad.S = new Vector2(healthChange, healthBar.TextureInfo.Texture.Height);
			armourBar.Quad.S = new Vector2(armourChange, armourBar.TextureInfo.Texture.Height);
		}
		
		public void SetTextureArray()
		{
			textures 	= new TextureInfo[5];
			textures[0] = new TextureInfo("/Application/textures/hud/healthAndArmourFrame.png");
			textures[1] = new TextureInfo("/Application/textures/hud/healthBar.png");
			textures[2] = new TextureInfo("/Application/textures/hud/armourBar.png");
			textures[3] = new TextureInfo("/Application/textures/hud/healthDamageBar.png");
			textures[4] = new TextureInfo("/Application/textures/hud/armourDamageBar.png");
		}
		
		public void SetVectorArray()
		{
			vectors 	= new Vector2[10];
			vectors[0]  = new Vector2(5,-5);
			vectors[1]  = new Vector2(17,37);
			vectors[2]  = new Vector2(17,74);
			vectors[3]  = new Vector2(17,37);
			vectors[4]  = new Vector2(17,74);
			vectors[5]  = new Vector2(252,135);
			vectors[6]  = new Vector2(230,34);
			vectors[7]  = new Vector2(230,24);
			vectors[8]  = new Vector2(230,34);
			vectors[9]  = new Vector2(230,24);
		}
		
		public void Dispose(Scene scene)
		{
			textureInfo.Dispose();
			
			scene.RemoveChild(healthDamage, true);
			scene.RemoveChild(armourDamage, true);
			scene.RemoveChild(healthBar, true);
			scene.RemoveChild(armourBar, true);
			scene.RemoveChild(healthAndArmour, true);
		}
	}
}


//ROTATE CODE
//https://msdn.microsoft.com/en-us/library/bb203869.aspx
//priteTexture = Content.Load<Texture2D>("ship");