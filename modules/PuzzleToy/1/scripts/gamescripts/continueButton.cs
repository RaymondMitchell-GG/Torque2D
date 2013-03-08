///-----------------------------------------------------------------------------
/// continueButton class for the title screen.
///-----------------------------------------------------------------------------

//------------------------------------------------------------------------------
/// onTouchDown callback for our main menu start button.
function continueButton::onTouchDown(%this, %modifier, %worldPosition, %clicks)
{
   // Change the image to the down image so it looks pressed visually
   %this.setImage("PuzzleToy:PlayBtnDown", 0);
   // If the sound is enabled, play the select option sound
   if (PuzzleToy.soundEnabled)
      alxPlay("PuzzleToy:SelectOptionSound");
      
   // Call our custom loadLevel function to schedule the load.
   // This is so we can load the loading scene then the intended scene
   PuzzleToy.currentBoard = PuzzleToy.currentLevelNumber @ "-1";   
   PuzzleToy.loadLevel( "./levels/GamePlay.scene.taml", true );
     
}