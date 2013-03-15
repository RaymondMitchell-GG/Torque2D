///-----------------------------------------------------------------------------
/// startButton class for the title screen.
///-----------------------------------------------------------------------------

//------------------------------------------------------------------------------
/// onTouchDown callback for our main menu start button.
function startButton::onTouchDown(%this, %modifier, %worldPosition, %clicks)
{
   // Change the image to the down image so it looks pressed visually
   %this.setImage(%this.downImage);
   // If the sound is enabled, play the select option sound
   if (PixelPainter.soundEnabled)
      alxPlay("PixelPainter:SelectOptionSound");
      
   // Since we are starting a new game, set our current level to 1 and our current board to 1-1.
   PixelPainter.currentLevelNumber = 1;   
   PixelPainter.currentBoard = "1-1";   
   // Call our custom loadLevel function to schedule the load.
   // This is so we can load the loading scene then the intended scene
   PixelPainter.loadLevel( "./levels/GamePlay.scene.taml", true );
     
}



