///-----------------------------------------------------------------------------
/// next Level Button class for proceeding to the next level
///-----------------------------------------------------------------------------

/// onTouchDown callback.  This also fires on left mouse down.
function nextLevelButton::onTouchDown(%this, %modifier, %worldPosition, %clicks)
{   
   /// Set the image for the button so it looks pressed
   %this.setImage(%this.downImage);
   
   // If sound is enabled, play the select option sound
   if (PixelPainter.soundEnabled)
      alxPlay("PixelPainter:SelectOptionSound");      
   
   if (PixelPainter.savedata.highestComplete < PixelPainter.currentLevelNumber)
   {
      // update our saved highest complete
      PixelPainter.savedata.highestComplete = PixelPainter.currentLevelNumber;
   }
   // Since our scenes only store the simple name of the level, call setSelectedLevel
   // so it can create the full filename.
   if (PixelPainter.currentLevelNumber < 6)
   {
      PixelPainter.currentLevelNumber++;
      PixelPainter.nextBoard = PixelPainter.currentLevelNumber @ "-1";
      PixelPainter.savedata.currentLevel = PixelPainter.nextBoard;    
   }
   else
   {
      PixelPainter.currentLevelNumber = 1;
      PixelPainter.nextBoard = "mainMenu";
      PixelPainter.savedata.currentLevel = "1-1";
   }
   // update the current level in our save data.    
   PixelPainter.savedata.currentLevelNumber = PixelPainter.currentLevelNumber;
      
   %savedata = PixelPainter.savedata;
   // save our savedata
   PixelPainter.saveUserData();
      
   PixelPainter.setSelectedLevel(PixelPainter.nextBoard);   
   
}