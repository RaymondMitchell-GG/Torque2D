///-----------------------------------------------------------------------------
/// next Level Button class for proceeding to the next level
///-----------------------------------------------------------------------------

/// onTouchDown callback.  This also fires on left mouse down.
function nextLevelButton::onTouchDown(%this, %modifier, %worldPosition, %clicks)
{   
   /// Set the image for the button so it looks pressed
   %this.setImage(%this.downImage);
   
   // If sound is enabled, play the select option sound
   if (PuzzleToy.soundEnabled)
      alxPlay("PuzzleToy:SelectOptionSound");      
   
   if (PuzzleToy.savedata.highestComplete < PuzzleToy.currentLevelNumber)
   {
      // update our saved highest complete
      PuzzleToy.savedata.highestComplete = PuzzleToy.currentLevelNumber;
   }
   // Since our scenes only store the simple name of the level, call setSelectedLevel
   // so it can create the full filename.
   if (PuzzleToy.currentLevelNumber < 6)
   {
      PuzzleToy.currentLevelNumber++;
      PuzzleToy.nextBoard = PuzzleToy.currentLevelNumber @ "-1";
      PuzzleToy.savedata.currentLevel = PuzzleToy.nextBoard;    
   }
   else
   {
      PuzzleToy.currentLevelNumber = 1;
      PuzzleToy.nextBoard = "mainMenu";
      PuzzleToy.savedata.currentLevel = "1-1";
   }
   // update the current level in our save data.    
   PuzzleToy.savedata.currentLevelNumber = PuzzleToy.currentLevelNumber;
      
   %savedata = PuzzleToy.savedata;
   // save our savedata
   PuzzleToy.saveUserData();
      
   PuzzleToy.setSelectedLevel(PuzzleToy.nextBoard);   
   
}