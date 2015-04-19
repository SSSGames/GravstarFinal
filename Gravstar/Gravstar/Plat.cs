using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
namespace Gravstar
{
 public class Plat
 {
//bounding box
  public Rectangle boundPlat;


  public SpriteUV platSprite;
  public  TextureInfo textureInfo;
  public Bounds2 bounds;
  public bool isColliding = false;
  private Vector2 min, max;
  private Bounds2 box;
  public Vector2 position;
  private GameObjectManager gameObjectManager;
  //Public functionsx
  public Plat (GameObjectManager gom, Vector2 position)
  {
   textureInfo = new TextureInfo("/Application/textures/other/box.png");
			this.position = position;
			this.gameObjectManager = gom;
   //sprite   = new SpriteUV(textureInfo);
   //sprite.Quad.S = textureInfo.TextureSizef;
   //bounds = sprite.Quad.Bounds2();
   //sprite.Position = position;
   //Add to the current scene.x
   //AppMain.gameScene.AddChild(sprite);
  }
  
  public Bounds2 getBoundingBox(){
   min.X  = platSprite.Position.X;
      min.Y  = platSprite.Position.Y;
   max.X  = platSprite.Position.X + (textureInfo.TextureSizef.X);
   max.Y  = platSprite.Position.Y + (textureInfo.TextureSizef.Y);
      box.Min  = min;   
   	  box.Max  = max;
   
   return box;
  }

  public void Update()
  {
			
			boundPlat = new Rectangle((gameObjectManager.Origin.X + platSprite.Position.X), (gameObjectManager.Origin.Y + platSprite.Position.Y), platSprite.TextureInfo.Texture.Width, platSprite.TextureInfo.Texture.Height);	
			
  }
  
  public void nextLevel(){
   //AppMain.gameScene.RemoveChild(sprite, true);  
   Console.WriteLine("COLLIDEEEEE");
   AppMain.gameStart = false;
   
   AppMain.levelManager.SetLevel(0);
   AppMain.LoadLevel(AppMain.gameObjectManager);

    
  }
  
  public void collide(){
   if(isColliding == false){
    this.isColliding = true;
    nextLevel();
   }
  }

 }
}