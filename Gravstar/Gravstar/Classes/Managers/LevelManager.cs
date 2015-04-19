using System;

using Sce.PlayStation.Core;

namespace Gravstar
{
	public class LevelManager
	{	
		public int	level;
		public int	changeTo;
		
		/*
		LEVEL 0 = MENU SCREEN
		LEVEL 1 = TUTORIAL SCREEN
		LEVEL 2 = OPTIONS SCREEN
		LEVEL 3 = HIGH SCORES SCREEN
		LEVEL 4 = CONFIRMATION SCREEN
		LEVEL 5 = GAME LEVEL ALPHA 1
		LEVEL 6 = GAME LEVEL ALPHA 2 
		LEVEL 7 = GAME LEVEL ALPHA BOSS
		LEVEL 8 = GAME LEVEL BETA 1 
		LEVEL 9 = GAME LEVEL BETA 2
		LEVEL 10 = GAME LEVEL BETA BOSS
		LEVEL 11 = GAME LEVEL GAMMA 1 
		LEVEL 12 = GAME LEVEL GAMMA 2 
		LEVEL 13 = GAME LEVEL GAMMA BOSS
		LEVEL 14 = PAUSE SCREEN
		LEVEL 15 = GAME OVER SCREEN
		LEVEL 16 = VICTORY SCREEN
		*/
		
		public LevelManager()
		{				
			level = 0;
		}
		
		public void SetLevel(int changeTo)
		{
			level = changeTo;
			
			if (level != 15)
			{
				AppMain.audioManager.SetBGM(level);
			}
			else
			{
				AppMain.audioManager.StopBGM();
			}
		}
	}
}

