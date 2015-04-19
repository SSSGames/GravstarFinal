using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Audio;

namespace GravstarV2
{
	public class AudioManager
	{
		//System.Collections.Generic.Dictionary<int, Bgm> AudioDictionary;
		
		private BgmPlayer	BGMPlayer;
		private Bgm[]		BGM;
		
		//private SoundPlayer	SFXPlayer;
  		//private Sound[]		SFX;

		public AudioManager()
		{
			//AudioDictionary = new System.Collections.Generic.Dictionary<int, Bgm>();
			
			SetBGMArray();
			SetSFXArray();
			
			BGMPlayer = BGM[0].CreatePlayer();
			
			BGMPlayer.Loop = true;
		}
		
		public void SetBGMArray()
		{
			BGM = new Bgm[4];
			BGM[0]	= new Bgm("/Application/sounds/bgm/Space Fighter Loop.mp3");
			BGM[1]	= new Bgm("/Application/sounds/bgm/The Complex.mp3");
			BGM[2]	= new Bgm("/Application/sounds/bgm/Airport Lounge.mp3");
			BGM[3]	= new Bgm("/Application/sounds/bgm/Dark Fog.mp3");
		}		
		public void SetSFXArray()
		{
		}
		
		public void SetBGM(int level)
		{	
		//if(BGMPlayer != null)
			//{
			
			//BGMPlayer.Dispose();
			////	BGMPlayer.Dispose();
			////}
			////
			//if(level <= 4)
			//{
			//	BGMPlayer = BGM[0].CreatePlayer();
			//}
			//else if(level == 5 || level == 6)
			//{
			//	BGMPlayer = BGM[1].CreatePlayer();
			//}
			//else if(level == 14)
			//{
			//	BGMPlayer = BGM[2].CreatePlayer();
			//}
			//else if(level == 15)
			//{
			//	BGMPlayer = BGM[3].CreatePlayer();
			//}
			////else if(level == 5 || level == 6)
			////{
			////	if(!AudioDictionary.ContainsKey(level))
			////	{
			////		//BGMPlayer = new Bgm("/Application/sounds/bgm/The Complex.mp3").CreatePlayer();
			////	}
			////}	
			////else if(level == 7)
			////{
			////	if(!AudioDictionary.ContainsKey(level))
			////	{
			////		//BGMPlayer = new Bgm("/Application/sounds/bgm/Cephalopod.mp3").CreatePlayer();
			////	}
			////}
			////else if(level == 8 || level == 9)
			////{
			////	if(!AudioDictionary.ContainsKey(level))
			////	{
			////		//BGMPlayer = new Bgm("/Application/sounds/bgm/Rocket.mp3").CreatePlayer();
			////	}
			////}
			////else if(level == 10)
			////{
			////	if(!AudioDictionary.ContainsKey(level))
			////	{
			////		//BGMPlayer = new Bgm("/Application/sounds/bgm/In a Heartbeat.mp3").CreatePlayer();
			////	}
			////}
			////else if(level == 11)
			////{
			////	if(!AudioDictionary.ContainsKey(level))
			////	{
			////		//BGMPlayer = new Bgm("/Application/sounds/bgm/Controlled Chaos NP.mp3").CreatePlayer();
			////	}
			////}
			////else if(level == 12)
			////{
			////	if(!AudioDictionary.ContainsKey(level))
			////	{
			////		//BGMPlayer = new Bgm("/Application/sounds/bgm/Controlled Chaos.mp3").CreatePlayer();
			////	}
			////}
			////else if(level == 13)
			////{
			////	if(!AudioDictionary.ContainsKey(level))
			////	{
			////		//BGMPlayer = new Bgm("/Application/sounds/bgm/Volatile Reaction.mp3").CreatePlayer();
			////	}
			////}
			////else if(level == 14)
			////{
			////	if(!AudioDictionary.ContainsKey(level))
			////	{
			////		//BGMPlayer = new Bgm("/Application/sounds/bgm/Airport Lounge.mp3").CreatePlayer();
			////	}
			////}
			////else
			////{
			////	if(!AudioDictionary.ContainsKey(level))
			////	{
			////		//BGMPlayer = new Bgm("/Application/sounds/bgm/Ouroboros.mp3").CreatePlayer();
			////	}
			////}
			//
			//BGMPlayer.Play();
		}
		
		//public void SetSFX(int effect)
		//{
		//	SFXPlayer.Dispose();
		//	
		//	SFXPlayer = SFX[effect].CreatePlayer();
		//	
		//	PlaySFX();
		//}

		//
		//public void StopBGM()
		//{
		//	BGMPlayer.Stop();
		//	
		//	BGMPlaying = false;
		//}
		// 
		//public void PlaySFX()
		//{
		//	SFXPlayer.Play();
		//	
		//	SFXPlaying = true;
		//}
		//
		//public void StopSFX()
		//{
		//	SFXPlayer.Stop();
		//	
		//	SFXPlaying = false;
		//}
		//
		//public void Dispose()
		//{
		//	BGMPlayer.Dispose();
		//}
	}
}
