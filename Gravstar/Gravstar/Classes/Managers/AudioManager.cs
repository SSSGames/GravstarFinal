using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Audio;

namespace Gravstar
{
	public class AudioManager
	{
		System.Collections.Generic.Dictionary<int, Bgm> AudioDictionary;
		private BgmPlayer	BGMPlayer;
		private SoundPlayer	SFXPlayer;
  		private Sound[]		SFX;
				
		public bool	BGMPlaying;
		public bool	SFXPlaying;
		
		public AudioManager()
		{
			AudioDictionary = new System.Collections.Generic.Dictionary<int, Bgm>();
			
			SetSFXArray();
			
			//BGMPlayer =	BGM[0].CreatePlayer();
			//SFXPlayer = SFX[0].CreatePlayer();
			
			//BGMPlayer.Loop = true;
			//SFXPlayer.Loop = false;
		}
		
		public void SetSFXArray()
		{
			SFX = new Sound[5];
			SFX[0] = new Sound("/Application/sounds/sfx/shoot.wav");
			SFX[1] = new Sound("/Application/sounds/sfx/impact.wav");
			SFX[2] = new Sound("/Application/sounds/sfx/grunt.wav");
			SFX[3] = new Sound("/Application/sounds/sfx/damn.wav");
			SFX[4] = new Sound("/Application/sounds/sfx/fail.wav");
		}
		
		public void SetBGM(int level)
		{	
			if(BGMPlayer != null)
			{
				BGMPlayer.Stop();
				BGMPlayer.Dispose();
			}
			
			if(level <= 4)
			{
				if(!AudioDictionary.ContainsKey(level))
				{
					BGMPlayer = new Bgm("/Application/sounds/bgm/Space Fighter Loop.mp3").CreatePlayer();
				}
			}
			else if(level == 5 || level == 6)
			{
				if(!AudioDictionary.ContainsKey(level))
				{
					BGMPlayer = new Bgm("/Application/sounds/bgm/The Complex.mp3").CreatePlayer();
				}
			}	
			else if(level == 7)
			{
				if(!AudioDictionary.ContainsKey(level))
				{
					BGMPlayer = new Bgm("/Application/sounds/bgm/Cephalopod.mp3").CreatePlayer();
				}
			}
			else if(level == 8 || level == 9)
			{
				if(!AudioDictionary.ContainsKey(level))
				{
					BGMPlayer = new Bgm("/Application/sounds/bgm/Rocket.mp3").CreatePlayer();
				}
			}
			else if(level == 10)
			{
				if(!AudioDictionary.ContainsKey(level))
				{
					BGMPlayer = new Bgm("/Application/sounds/bgm/In a Heartbeat.mp3").CreatePlayer();
				}
			}
			else if(level == 11)
			{
				if(!AudioDictionary.ContainsKey(level))
				{
					BGMPlayer = new Bgm("/Application/sounds/bgm/Controlled Chaos NP.mp3").CreatePlayer();
				}
			}
			else if(level == 12)
			{
				if(!AudioDictionary.ContainsKey(level))
				{
					BGMPlayer = new Bgm("/Application/sounds/bgm/Controlled Chaos.mp3").CreatePlayer();
				}
			}
			else if(level == 13)
			{
				if(!AudioDictionary.ContainsKey(level))
				{
					BGMPlayer = new Bgm("/Application/sounds/bgm/Volatile Reaction.mp3").CreatePlayer();
				}
			}
			else if(level == 14)
			{
				if(!AudioDictionary.ContainsKey(level))
				{
					BGMPlayer = new Bgm("/Application/sounds/bgm/Airport Lounge.mp3").CreatePlayer();
				}
			}
			else if(level == 15)
			{
				if(!AudioDictionary.ContainsKey(level))
				{
					BGMPlayer = new Bgm("/Application/sounds/bgm/Dark Fog.mp3").CreatePlayer();
				}
			}
			else
			{
				if(!AudioDictionary.ContainsKey(level))
				{
					BGMPlayer = new Bgm("/Application/sounds/bgm/Ouroboros.mp3").CreatePlayer();
				}
			}
			
			PlayBGM();
		}
		
		public void SetSFX(int effect)
		{
			SFXPlayer.Dispose();
			
			SFXPlayer = SFX[effect].CreatePlayer();
			
			PlaySFX();
		}
		
		public void PlayBGM()
		{		
			BGMPlayer.Play();
			
			BGMPlaying = true;
		}
		
		public void StopBGM()
		{
			BGMPlayer.Stop();
			
			BGMPlaying = false;
		}
		 
		public void PlaySFX()
		{
			SFXPlayer.Play();
			
			SFXPlaying = true;
		}
		
		public void StopSFX()
		{
			SFXPlayer.Stop();
			
			SFXPlaying = false;
		}

		public void Dispose()
		{
			BGMPlayer.Dispose();
		}
	}
}
