///-----------------------------------------------------------------------------
/// Gallery Button class for going to the gallery
///-----------------------------------------------------------------------------

/// onTouchDown callback for our galleryButton. This also fires when 
/// on left mouse button down.
function galleryButton::onTouchDown(%this, %modifier, %worldPosition, %clicks)
{   
   /// Set the image for the button so it looks pressed
   %this.setImage(%this.downImage);
   
   // If sound is enabled, play the select option sound
   if (PixelPainter.soundEnabled)
      alxPlay("PixelPainter:SelectOptionSound");
      
   // Call our custom loadLevel function to schedule the main menu scene to be loaded
   // after first scheduling the loading screen to load.   
   PixelPainter.loadLevel( "./levels/gallery.scene.taml", true );  
   
   
}