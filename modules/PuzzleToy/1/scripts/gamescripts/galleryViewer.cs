///-----------------------------------------------------------------------------
/// Gallery Viewer class for managing the gallery images.
///-----------------------------------------------------------------------------

///-----------------------------------------------------------------------------
/// Initialize the gallery image positions.
///-----------------------------------------------------------------------------
function galleryViewer::onAdd(%this)
{      
   %this.imageSize = "56 70";
   // iterate and load up images.
   for (%i = 1; %i <= PuzzleToy.NumberOfLevels; %i++)
   {  
      %spriteobject = new Sprite();    
      %this.GallerySprites[%i] = %spriteobject;
                  
      if(%i <= PuzzleToy.savedata.highestComplete)
      {
         %imagename = "PuzzleToy:LevelComplete" @ %i;
         %spriteobject.setImage(%imagename);
         %spriteobject.bUnLocked = true;
      }
      else
      {
         %spriteobject.setImage(%this.LockedImage);
         %spriteobject.bUnLocked = false;       
      }
      %spriteobject.addToScene(Gallery);
   }
   // set the locations and the first image selected
   %this.selectedImage = 1;
   %this.initialize();
   
}

///-----------------------------------------------------------------------------
/// Initialize the gallery image positions.
///-----------------------------------------------------------------------------
function galleryViewer::initialize(%this)
{
   %startingx = (%this.selectedImage - 1) * (- %this.imageSize.x * 0.5);
   for (%i = 1; %i <= PuzzleToy.NumberOfLevels; %i++)
   {
      %sprite = %this.GallerySprites[%i];
      %sprite.setPosition(%startingx + ((%i - 1) * (%this.imageSize.x * 0.5)), %this.getPosition().y);
      %sprite.setSize(%this.imageSize.x * 0.5, %this.imageSize.y * 0.5);
      %sprite.setSceneLayer(2);
   }
   %this.GallerySprites[%this.selectedImage].setSceneLayer(1);
   %this.GallerySprites[%this.selectedImage].setSize(%this.imageSize.x, %this.imageSize.y);
}

///-----------------------------------------------------------------------------
/// Update the image positions and sizes based on the selected image.
///-----------------------------------------------------------------------------
function galleryViewer::updateSelected(%this)
{
   %startingx = (%this.selectedImage - 1) * (- %this.imageSize.x * 0.5);
   for (%i = 1; %i <= PuzzleToy.NumberOfLevels; %i++)
   {
      %sprite = %this.GallerySprites[%i];
      %targetlocation = %startingx + ((%i - 1) * (%this.imageSize.x * 0.5)) SPC %this.getPosition().y;
      %spritelocationx = %sprite.getPosition().x;
      %speed = mAbs(%spritelocationx - %targetlocation.x);
      %sprite.cancelMoveTo(true);                 
      if (%speed > 0)
         %sprite.moveTo(%targetlocation, %speed * 4, true, true);

      // %sprite.setPosition(%startingx + ((%i - 1) * (%this.getSize().x * 0.5)), %this.getPosition().y);      
      %sprite.setSize(%this.imageSize.x * 0.5, %this.imageSize.y * 0.5);
      %sprite.setSceneLayer(5);
   }   
   %this.GallerySprites[%this.selectedImage].setSceneLayer(4);
   %this.GallerySprites[%this.selectedImage].setSize(%this.imageSize.x, %this.imageSize.y);
}

///-----------------------------------------------------------------------------
/// onTouch down callback.
///-----------------------------------------------------------------------------
function galleryViewer::onTouchDown(%this, %modifier, %worldPosition, %clicks)
{
   %this.bDragged = false;
   %this.previousPosition = %worldPosition;
   %this.currentDelta = "0 0";   
}
///-----------------------------------------------------------------------------
/// onTouchUp callback.
/// If we haven't dragged then open the selected image in the ImageViewer.
///-----------------------------------------------------------------------------
function galleryViewer::onTouchUp(%this, %modifier, %worldPosition, %clicks)
{
   if (!%this.bDragged && %this.GallerySprites[%this.selectedImage].bUnLocked)
   {
      echo("create an image viewer TBD");  
   }
}

///-----------------------------------------------------------------------------
/// onTouchDragged callback
/// This is how we change the selected image
///-----------------------------------------------------------------------------
function galleryViewer::onTouchDragged(%this, %modifier, %worldPosition, %clicks)
{
   // if we are paused then return
   if (SandboxScene.getScenePause())
      return;
      
  
   // get the drag delta by using the previousPosition and the current worldPosition.
   %delta = VectorSub(%this.previousPosition, %worldPosition);
      
   // Add the new delta to the overall currentDelta values.
   %this.currentDelta = VectorAdd(%this.currentDelta, %delta);   
   
   // If our total delta is high enough in the X direction
   if (mAbs(%this.currentDelta.x) > 20)
   {
      %this.bDragged = true;
      // If the current delta is positive, select left, otherwise select right
      if (%this.currentDelta.x > 0)
      {
         // change selection right
         %this.selectedImage++;
         if (%this.selectedImage > PuzzleToy.NumberOfLevels)
            %this.selectedImage = PuzzleToy.NumberOfLevels;
         else           
            %this.updateSelected();         
      }
      else
      {
         // change selection left
         %this.selectedImage--;
         if (%this.selectedImage < 1)
            %this.selectedImage = 1;
         else
            %this.updateSelected();
      }
      // Now that we selected, reset the currentDeltas.
      %this.currentDelta = "0 0";
      
   }
   // Track the new position as the previous position for later calculations.
   %this.previousPosition = %worldPosition;
   
   
}