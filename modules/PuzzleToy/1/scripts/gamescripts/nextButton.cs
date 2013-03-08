///-----------------------------------------------------------------------------
/// next Button class for proceeding to the next level
///-----------------------------------------------------------------------------

/// onTouchDown callback.  This also fires on left mouse down.
function nextButton::onTouchDown(%this, %modifier, %worldPosition, %clicks)
{   
   /// Set the image for the button so it looks pressed
   %this.setImage(%this.downImage);
   
   // If sound is enabled, play the select option sound
   if (PuzzleToy.soundEnabled)
      alxPlay("PuzzleToy:SelectOptionSound");
      
   // if we aren't complete, let's save the current level
   if (PuzzleToy.nextBoard !$= "LevelComplete" && PuzzleToy.nextBoard !$= "mainMenu")
   {
      PuzzleToy.savedata.currentLevel = PuzzleToy.nextBoard;      
      // This isn't used yet, but we will update our high score here
      PuzzleToy.savedata.highScore = 0;      
      // write the save file
      PuzzleToy.saveUserData();
      
   }   
   
   // Since our scenes only store the simple name of the level, call setSelectedLevel
   // so it can create the full filename.
   PuzzleToy.setSelectedLevel(PuzzleToy.nextBoard);   
   
}