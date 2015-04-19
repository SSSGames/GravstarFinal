using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
namespace Gravstar
{
 public class Exit
 {
  public SpriteUV sprite;
  public TextureInfo textureInfo;
  public Bounds2 bounds;
  public bool isColliding = false;
  private Vector2 min, max;
  private Bounds2 box;
  
  
  //Public functionsx
  public Exit ()
  {
   textureInfo = new TextureInfo("/Application/textures/other/exit.png");
 //  sprite   = new SpriteUV(textureInfo);
 //  sprite.Quad.S = textureInfo.TextureSizef;
 //  bounds = sprite.Quad.Bounds2();
 //  sprite.Position = new Vector2(300f, 300f);
   //Add to the current scene.x
  // AppMain.gameScene.AddChild(sprite);

  }
  
  public Bounds2 getBoundingBox(){
   min.X  = sprite.Position.X + (textureInfo.TextureSizef.X/2);
      min.Y  = sprite.Position.Y + (textureInfo.TextureSizef.Y/2);
   max.X  = sprite.Position.X + (textureInfo.TextureSizef.X/2);
   max.Y  = sprite.Position.Y + (textureInfo.TextureSizef.Y/2);
      box.Min  = min;   
   box.Max  = max;
   
   return box;
  }
  
  public void nextLevel(){
   //AppMain.gameScene.RemoveChild(sprite, true);  
   Console.WriteLine("COLLIDEEEEE");
   AppMain.gameStart = false;
   
   AppMain.levelManager.SetLevel(0);
   AppMain.LoadLevel(AppMain.gameObjectManager);

    
  }
  
  public static void clearLevel ()
  {

  }
  
  public void collide(){
   if(isColliding == false){
    this.isColliding = true;
    nextLevel();
   }
  }

 }
}