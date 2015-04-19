//implementing scenes
//if count = 1,2 or 3
//why ~main

using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

	
namespace Gravstar
{
	public class AppMain
	{
		public static Sce.PlayStation.HighLevel.GameEngine2D.Scene 		gameScene;
		public static Sce.PlayStation.HighLevel.UI.Scene 				uiScene;
		
		public static GamePadData		gamePadData;
		public static BackgroundManager	backgroundManager;
		public static AudioManager		audioManager;
		public static GameObjectManager	gameObjectManager;
		public static LevelManager		levelManager;
		public static UIManager			uIManager;
		public static Panel				panel;
		public static Timer				timer;
		public static Player			player;
		public static List<Plat>        platforms;
 		public static Exit     exit;
	
		
		public static int	playerHealth;
		public static int	level;
		public static float	previousTime; 
		public static float	currentTime;
		public static float	elapsedTime;
		public static float	accumulatedDeltaTime;
		public static bool	isPressed;
		public static bool	justDied;
		public static bool	quitGame;
		public static bool	gameStart;
		
		public static void Main (string[] args)
		{					
			timer = new Timer();
			previousTime = (float)timer.Milliseconds();
			
			Initialize();
			
			//Game loop
			quitGame = false;
			while (!quitGame) 
			{
				Update();
				
				Director.Instance.Update();
				Director.Instance.Render();
				UISystem.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
			
			backgroundManager.Dispose();
			
			Director.Terminate();
		}

		public static void Initialize ()
		{
			//Set up director and UISystem.
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			
			//Set the ui scene.
			uiScene = new Sce.PlayStation.HighLevel.UI.Scene();
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
			
				
			uiScene.RootWidget.AddChildLast(panel);
			UISystem.SetScene(uiScene);

			justDied = true;
			gameStart = false;
			
			//Create the game objects.
			gameObjectManager = new GameObjectManager(new Vector2(480,562));
			
			LoadLevel(gameObjectManager);
			
			level = levelManager.level;
			audioManager.SetBGM(level);
			
			
			//Run the scene.
			Director.Instance.RunWithScene(gameScene, true);
		}
		
		public static void Update()
		{
			level = levelManager.level;
			
			currentTime = (float)timer.Milliseconds();
			elapsedTime = currentTime - previousTime;
			previousTime = currentTime;

			accumulatedDeltaTime += elapsedTime;
			
			//Get gamepad input.
			gamePadData = GamePad.GetData(0);
			
			
			if(level == 0)
			{
				backgroundManager.Update(gameScene, levelManager.level);
				
				if ((gamePadData.Buttons & GamePadButtons.Start) != 0 && !isPressed)
        		{
					AppMain.levelManager.SetLevel(5);
					isPressed = true;	
					gameStart = true;
					
					LoadLevel(gameObjectManager);
        		} 
			}

		
			if(level == 5)
			{
				//Player update
				player.Update(gameScene);
				int health = player.health;
				
				//backgroundManager update
				backgroundManager.Update(gameScene, levelManager.level);
				
				//Update GameObject
				gameObjectManager.Update(gameScene, gamePadData);
				for(int i = 0; i < platforms.Count; i++)
				{
					platforms[i].Update();	
				}
				for(int i = 0; i < platforms.Count; i++)
				{
					DoBoxesIntersect(player.Bounding, platforms[i].boundPlat);
				}
				
				CheckCollision();
			}
			
			if(level == 14)
			{
				if ((gamePadData.Buttons & GamePadButtons.Start) != 0 && !isPressed)
        		{
					AppMain.levelManager.SetLevel(5);
					isPressed = true;	
					System.Threading.Thread.Sleep(100);
        		} 
				if ((gamePadData.Buttons & GamePadButtons.Start) == 0)
        		{		
					isPressed = false;
        		}		
			}
			
			if (level == 15)
			{
				if(justDied)
				{
					AppMain.audioManager.SetSFX(3);
					System.Threading.Thread.Sleep(1000);
					AppMain.audioManager.SetSFX(4);
					System.Threading.Thread.Sleep(2500);
					AppMain.audioManager.SetBGM(level);	
					justDied = false;
				}
				
				if (!backgroundManager.stop)
				{
					backgroundManager.Update(gameScene, level);
				}
				else
				{
					System.Threading.Thread.Sleep(5000);
						
					quitGame = true;
				}

			}
		}
		
	public static void CheckCollision()
  {
   checkExitCollision();
   checkPlatformCollision();
  }

	
	public static void DoBoxesIntersect(Rectangle a, Rectangle b)
		{
			if (a.X < b.X + b.Width && 
			    a.X + a.Width > b.X && 
			    a.Y < b.Y + b.Height && 
			    a.Height + a.Y > b.Y)
			{
				// Toggle al gravity, hit test options
			}
			else
			{
 				Console.WriteLine("No Collision");	
			}
		}
		
   public static void checkPlatformCollision(){   
   Bounds2 b1;
   Bounds2 b2;
   
   for (var i = 0; i < platforms.Count; i++) {
    b1 = player.getBoundingBox ();
    b2 = platforms [i].getBoundingBox ();
    

    
    bool b = b1.Overlaps(b2);
    if (b == true) { 
		Console.WriteLine("Hittest");
     platforms[i].collide ();
    }
    
   }

  }
  
  public static void checkExitCollision(){
   Bounds2 b1;
   Bounds2 b2;
   b1 = player.getBoundingBox ();
   b2 = exit.getBoundingBox();
   
   bool b = b1.Overlaps (b2);
   if (b == true) {      
    exit.collide ();
    player.collide();
   }
  }
		 public static void spawnPlatforms(GameObjectManager gom){
		   	platforms.Add (new Plat(gom, new Vector2(-170, -270)));
		  	platforms.Add (new Plat(gom, new Vector2(-120, -270)));
			platforms.Add (new Plat(gom, new Vector2(-70, -270)));
			platforms.Add (new Plat(gom, new Vector2(-70, -220)));
			platforms.Add (new Plat(gom, new Vector2(-70, -170)));
			platforms.Add (new Plat(gom, new Vector2(-70, -320)));
			platforms.Add (new Plat(gom, new Vector2(100, -250)));
			platforms.Add (new Plat(gom, new Vector2(150, -250)));
			platforms.Add (new Plat(gom, new Vector2(250, -100)));
			platforms.Add (new Plat(gom, new Vector2(200, -100)));
			platforms.Add (new Plat(gom, new Vector2(250, -150)));
			platforms.Add (new Plat(gom, new Vector2(100, -20)));
			platforms.Add (new Plat(gom, new Vector2(-80, -20)));
			platforms.Add (new Plat(gom, new Vector2(-130, -20)));
			platforms.Add (new Plat(gom, new Vector2(-130, -70)));
			platforms.Add (new Plat(gom, new Vector2(-180, -20)));
			
  }
		
		public static void LoadLevel(GameObjectManager gom)
		{	
			if (!gameStart)
			{
				//Load level manager
				levelManager = new LevelManager();
			
				//Load audio manager
				audioManager = new AudioManager();
				
				//Create the backgroundManager.
				backgroundManager = new BackgroundManager(gameScene);
			}
			else
			{				
				
				
				 exit = new Exit();
				gameObjectManager.addExit(exit);
				 platforms = new List<Plat> ();
    			 spawnPlatforms (gom);
				for(var i = 0; i < platforms.Count; i++){
					gameObjectManager.addPlatforms(platforms[i]); 
					//Console.WriteLine(platforms[i]);
				}
				
				gameObjectManager.addRing(gameScene);
				//Create the player.
				player = new Player(gameObjectManager, gameScene, new Vector2(Director.Instance.GL.Context.GetViewport().Width / 2,170.0f));//Director.Instance.GL.Context.GetViewport().Height - 50));
				
				gameObjectManager.SetPlayer(player);			
				
				//Create the user interface.
				uIManager = new UIManager(gameScene, new Vector2(20, 20));
				
				
				

	}
}
	}
}
		
		//		//This method checks to see if the Sprite is going to move into an area that does
//        //not contain all Gray pixels. If the move amount would cause a movement into a non-gray
//        //pixel, then a collision has occurred.
//		private static bool CollisionOccurred(int playerPos)
//        {
//            //Calculate the Position of the Car and create the collision Texture. This texture will contain
//            //all of the pixels that are directly underneath the sprite currently on the Track image.
//            float aXPosition = (float)(-player / 2 + mCarPosition.X + aMove * Math.Cos(mCarRotation));
//            float aYPosition = (float)(-mCarHeight / 2 + mCarPosition.Y + aMove * Math.Sin(mCarRotation));
//            Texture2D aCollisionCheck = CreateCollisionTexture(aXPosition, aYPosition);
// 
//            //Use GetData to fill in an array with all of the Colors of the Pixels in the area of the Collision Texture
//            int aPixels = mCarWidth * mCarHeight;
//            Color[] myColors = new Color[aPixels];
//            aCollisionCheck.GetData<Color>(0, new Rectangle((int)(aCollisionCheck.Width / 2 - mCarWidth / 2), 
//                (int)(aCollisionCheck.Height / 2 - mCarHeight / 2), mCarWidth, mCarHeight), myColors, 0, aPixels);
// 
//            //Cycle through all of the colors in the Array and see if any of them
//            //are not Gray. If one of them isn't Gray, then the Car is heading off the road
//            //and a Collision has occurred
//            bool aCollision = false;
//            foreach (Color aColor in myColors)
//            {
//                //If one of the pixels in that area is not Gray, then the sprite is moving
//                //off the allowed movement area
//                if (aColor != Color.Gray)
//                {
//                    aCollision = true;
//                    break;
//                }
//            }
// 
//            return aCollision;
//        }
//		
//	}
//}
// //Code for getting the pixel data
//private static bool ColourCollision()
//{
//Image ring = new Image("/Application/textures/other/ring.png")
//ring.Decode();
//Byte[]byteData=ImageBox.ToBuffer();
//Byte[]alphaChannelData = newByte[byteData.Length/4];
//
//if(byteData.Length>0)
//{
//	for(int i = 0-; if<alphaChanneldata.Length; i++)
//	{
//		alphaChannelData[i] = byteData[(i*4)+3]	
//	}
//		
//		
//}
//	
//	return true;
//}
//
//
//public static bool GetBoundsColour(Texture2D CollisionMap)
//{
//	Color[] data = new Color[Bounds.Height * Bounds.Width]
//	CollisionMap.GetData<Color>(0, Bounds, data, 0, data.length)
//		
//		for (int i = 0; i < data.length; i++)
//	{
//		if(data[i] == Color.Red)
//		return true;
//	}
//	return false;
//}
//
////if (pixel.alpha = !0)
////{
////	Collision = true;	
