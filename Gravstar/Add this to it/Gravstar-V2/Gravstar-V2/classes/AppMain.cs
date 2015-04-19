using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.HighLevel.Physics2D;
	
namespace GravstarV2
{
	public class AppMain
	{
		//--------------------------------------------------------------------------------------
		private const int GAME_STATE_TITLE	 			= 1;
		private const int GAME_STATE_TUTORIAL			= 2;
		private const int GAME_STATE_OPTIONS	 		= 3;
		private const int GAME_STATE_HIGH_SCORES 		= 4;
		private const int GAME_STATE_CONFIRMATION		= 5;
		private const int GAME_STATE_LEVEL_ALPHA_1		= 6;
		private const int GAME_STATE_LEVEL_ALPHA_2		= 7;
		private const int GAME_STATE_LEVEL_ALPHA_BOSS	= 8;
		private const int GAME_STATE_LEVEL_BETA_1		= 9;
		private const int GAME_STATE_LEVEL_BETA_2		= 10;
		private const int GAME_STATE_LEVEL_BETA_BOSS	= 11;
		private const int GAME_STATE_LEVEL_GAMMA_1		= 12;
		private const int GAME_STATE_LEVEL_GAMMA_2		= 13;
		private const int GAME_STATE_LEVEL_GAMMA_BOSS	= 14;
		private const int GAME_STATE_PAUSE				= 15;
		private const int GAME_STATE_GAME_OVER			= 16;
		private const int GAME_STATE_VICTORY			= 17;	
		//-------------------------------------------
		public static Sce.PlayStation.HighLevel.GameEngine2D.Scene 	gameScene;
		public static Sce.PlayStation.HighLevel.UI.Scene 			uiScene;
		//-------------------------------------------
		public static GamePadData	gamePadData;
		public static AudioManager	audioManager;
		public static GameObject	gameObject;
		public static Background	background;
		public static Hud			hud;
		public static Panel			panel;
		public static Timer			timer;
		public static Player		player;
		//-------------------------------------------
		public static int currentGameState;
		public static int playerHealth;
		public static int playerArmour;
		//-------------------------------------------
		public static float	previousTime; 
		public static float	currentTime;
		public static float	deltaTime;
		//-------------------------------------------
		public static bool isPressed;
		public static bool playerAlive;
		public static bool quitGame;
		public static bool gameStart;
		//--------------------------------------------------------------------------------------
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
			
			background.Dispose(gameScene);
			
			Director.Terminate();
		}
		//--------------------------------------------------------------------------------------
		public static void Initialize ()
		{	
			//Set up director and UI System.
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			//Set game scene.
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			
			//Set the ui scene.
			uiScene = new Sce.PlayStation.HighLevel.UI.Scene();
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
				
			uiScene.RootWidget.AddChildLast(panel);
			UISystem.SetScene(uiScene);
											
			//Create the background manager.
			background = new Background(gameScene);
			
			//Load audio manager.
			audioManager = new AudioManager();
			
			switchGameState(GAME_STATE_TITLE);
			
			playerAlive = true;
			gameStart = false;
			
			audioManager.SetBGM(1);
			
			//Run the scene.
			Director.Instance.RunWithScene(gameScene, true);
		}
		//--------------------------------------------------------------------------------------
		public static void loadLevel ()
		{
			//Create the player.
			player = new Player(gameScene, new Vector2(0, 0));
			//Create the game objects.
			gameObject = new GameObject(player, gameScene, new Vector2(480, 472));
			//Set the ui scene.
			hud = new Hud(gameScene);			
		}
		//--------------------------------------------------------------------------------------
		public static void switchGameState (int newState)
		{
			currentGameState = newState;
		}
		//--------------------------------------------------------------------------------------
		public static void runGame ()
		{
			switch (currentGameState)
			{	
				case GAME_STATE_TITLE:
					gameStateTitle();
					break;			
				case GAME_STATE_TUTORIAL:
					//gameStateTutorial();
					break;
				case GAME_STATE_OPTIONS:
					//gameStateOptions();
					break;			
				case GAME_STATE_HIGH_SCORES:
					//gameStateHighScores();
					break;		
				case GAME_STATE_CONFIRMATION:
					//gameStateConfirmation();
					break;		
				case GAME_STATE_LEVEL_ALPHA_1:
					gameStateLevelAlpha1();
					break;
				case GAME_STATE_LEVEL_ALPHA_2:
					//gameStateLevelAlpha2();
					break;
				case GAME_STATE_LEVEL_ALPHA_BOSS:
					//gameStateLevelAlphaBoss();
					break;
				case GAME_STATE_LEVEL_BETA_1:
					//gameStatelevelBeta1();
					break;
				case GAME_STATE_LEVEL_BETA_2:
					//gameStateLevelBeta2();
					break;
				case GAME_STATE_LEVEL_BETA_BOSS:
					//gameStateLevelBetaBoss();
					break;
				case GAME_STATE_LEVEL_GAMMA_1:
					//gameStateLevelGamma1();
					break;
				case GAME_STATE_LEVEL_GAMMA_2:
					//gameStateLevelGamma2();
					break;
				case GAME_STATE_LEVEL_GAMMA_BOSS:
					//gameStateLevelGammaBoss();
					break;
				case GAME_STATE_PAUSE:
					gameStatePause();
					break;
				case GAME_STATE_GAME_OVER:
					gameStateGameOver();
					break;
				case GAME_STATE_VICTORY:
					//gameStateVictory();
					break;
			}
		}
		//--------------------------------------------------------------------------------------
		public static void Update ()
		{	
			currentTime = (float)timer.Milliseconds();
			deltaTime = currentTime - previousTime;
			previousTime = currentTime;
			
			runGame();
			
			//Get gamepad input.
			gamePadData = GamePad.GetData(0);
		}
		//-------------------------------------------
		public static void gameStateTitle()
		{
			//background update.
			background.Update(gameScene, 0);
			
			//audio update.
			audioManager.SetBGM(0);
			
			if ((gamePadData.Buttons & GamePadButtons.Start) != 0 && !isPressed)
    		{
				switchGameState(GAME_STATE_LEVEL_ALPHA_1);
				isPressed = true;	
				gameStart = true;
				
				loadLevel();
    		} 
		}
		//-------------------------------------------
		public static void gameStateLevelAlpha1()
		{
			//Change health and armour values.
			playerHealth = player.health;
			playerArmour = player.armour;
			
			playerAlive = player.isAlive;
			
			//Player update.
			player.Update(gameScene);

			//background update.
			background.Update(gameScene, 1);
			
			//audio update.
			audioManager.SetBGM(5);
			
			//game objects update.
			gameObject.Update(gameScene, gamePadData);
			
			//hud update.
			hud.Update(gamePadData, playerHealth, playerArmour);
			
			if ((gamePadData.Buttons & GamePadButtons.Start) != 0 && !isPressed)
    		{
				switchGameState(GAME_STATE_PAUSE);
				isPressed = true;	
				System.Threading.Thread.Sleep(200);
    		} 
			if ((gamePadData.Buttons & GamePadButtons.Start) == 0)
    		{		
				isPressed = false;
    		}		
			
			if (playerAlive == false)
			{
				switchGameState(GAME_STATE_GAME_OVER);
			}
		}
		//-------------------------------------------
		public static void gameStatePause()
		{
			//background update.
			background.Update(gameScene, 2);
			
			//audio update.
			audioManager.SetBGM(14);
			
			if ((gamePadData.Buttons & GamePadButtons.Start) != 0 && !isPressed)
    		{
				switchGameState(GAME_STATE_LEVEL_ALPHA_1);
				isPressed = true;	
				System.Threading.Thread.Sleep(200);
    		} 
			if ((gamePadData.Buttons & GamePadButtons.Start) == 0)
    		{		
				isPressed = false;
    		}		
		}
		//-------------------------------------------
		public static void gameStateGameOver()
		{
			//background update.
			background.Update(gameScene, 3);
			
			//player dispose.
			player.Dispose(gameScene);
			
			//hud dispose.
			hud.Dispose(gameScene);
			
			//game object dispose.
			gameObject.Dispose(gameScene);
			
			//audio update.
			audioManager.SetBGM(15);
		}
		//-------------------------------------------
	}
	//--------------------------------------------------------------------------------------
}