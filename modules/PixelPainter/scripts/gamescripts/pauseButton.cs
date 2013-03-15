function pauseButton::onTouchDown(%this)
{
   /// Set the image for the button so it looks pressed
   %this.setImage(%this.downImage);   
   
}

function pauseButton::onTouchUp(%this)
{
   %this.setImage(%this.upImage);
   
   // If sound is enabled, play the select option sound
   if (PixelPainter.soundEnabled)
      alxPlay("PixelPainter:SelectOptionSound");
   // toggle pause   
   TogglePauseScene();
}