using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Gravstar
{
  public class SpritesheetScene : Scene
  {
    private SpriteTile _currentSprite;
    private Texture2D _texture;
    private TextureInfo _ti;
	
		
	private List<int> leftSpriteMovement = new List<int>();
	private List<int> rightSpriteMovement = new List<int>();
    
    private float elapsedTime = 0.0f;

    public SpritesheetScene ()
    {
			
      _texture = new Texture2D("/Application/PlayerSprite.png",false);
      _ti = new TextureInfo(_texture,new Vector2i(1,8));
      _currentSprite = new SpriteTile(_ti,new Vector2i(0,1));
      _currentSprite.Pivot = new Vector2(0.5f,0.5f);
      _currentSprite.Position = new Vector2(0.0f,0.0f);
      _currentSprite.Scale = new Vector2(3.0f,3.0f);
      this.AddChild(_currentSprite);
			
		_currentSprite.TileIndex2D = new Vector2i(0,5);
		// CGenerate sprite movement
		leftSpriteMovement.Add(7);
		leftSpriteMovement.Add(5);
		
		rightSpriteMovement.Add(6);
		rightSpriteMovement.Add(4);
			
	 
      
      Scheduler.Instance.ScheduleUpdateForTarget(this,1,false);
			
		
	}
	
   public override void Update (float dt)
    {
			if(Input2.GamePad0.Left.Down)
			{
				
				elapsedTime += dt;
			    if(elapsedTime >= 0.2f)
			    {
					Console.WriteLine("Left has been held for 0.2seconds");
					
					foreach(int spritePosition in leftSpriteMovement)
					{
						if(_currentSprite.TileIndex2D.Y != spritePosition)
						{
							_currentSprite.TileIndex2D.Y = spritePosition;
							elapsedTime = 0;
							return;
						}
					}
				}
			}
					
				if(Input2.GamePad0.Right.Down)
				{
				
					elapsedTime += dt;
			   		if(elapsedTime >= 0.2f)
			    {
					Console.WriteLine("Right has been held for 0.2seconds");
					
					foreach(int spritePosition in rightSpriteMovement)
					{
						if(_currentSprite.TileIndex2D.Y != spritePosition)
						{
							_currentSprite.TileIndex2D.Y = spritePosition;
							elapsedTime = 0;
							return;
						}
					}
					
				}
			  }
		
			
			if(Input2.GamePad0.Up.Down)
			{
				_currentSprite.TileIndex2D = new Vector2i(0,2);
			}
		
    } 
  }
}


