///-----------------------------------------------------------------------------
/// Main Menu Button class for exiting to the main menu
///-----------------------------------------------------------------------------

/// onTouchDown callback for our mainMenuButton. This also fires when 
/// on left mouse button down.
function mainMenuButton::onTouchDown(%this, %modifier, %worldPosition, %clicks)
{   
   /// Set the image for the button so it looks pressed
   %this.setImage(%this.downImage);   
   
}

function mainMenuButton::onTouchUp(%this)
{
   %this.setImage(%this.upImage);
   
   // If sound is enabled, play the select option sound
   if (PixelPainter.soundEnabled)
      alxPlay("PixelPainter:SelectOptionSound");
      
   // Call our custom loadLevel function to schedule the main menu scene to be loaded
   // after first scheduling the loading screen to load.   
   PixelPainter.loadLevel( "./levels/mainMenu.scene.taml", true );
}