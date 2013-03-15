///-----------------------------------------------------------------------------
/// Image Viewer class for going to the gallery
///-----------------------------------------------------------------------------

function imageViewer::onAdd(%this, %scenegraph)
{
   %this.UseInputEvents = true;
   %this.bGrabbed = false;
}

function imageViewer::onTouchDown(%this, %modifier, %worldPosition, %clicks)
{
   %this.previousPosition = %worldPosition;
   %this.bGrabbed = true;
}

function imageViewer::onTouchDragged(%this, %modifier, %worldPosition, %clicks)
{
   echo("move the image");
   if (%this.bGrabbed)
   {
      // get the current position
      %this.currentPosition = %worldPosition;
      // calculate the delta, how far you dragged
      %delta = VectorSub(%this.currentPosition, %this.previousPosition);
      // Change the images location based on delta.      
      %this.setPosition(%this.Position.x + %delta.x, %this.Position.y + %delta.y);
      // make sure the image stays on screen.
      %area = %this.getArea();
      %size = %this.size;
      %cameraArea = SceneWindow.getCameraArea();
      %cameraSize = SceneWindow.getCameraSize();
      if (%this.size.x >= %cameraSize.x)
      {
         if (%area.x > %cameraArea.x)         
            %this.Position.x = %cameraArea.x + (%this.size.x/2);
         else if (%area.z < %cameraArea.z)
            %this.Position.x = %cameraArea.z - (%this.size.x/2);
         
      }
      else
      {
         if (%area.x < %cameraArea.x)
            %this.Position.x = %cameraArea.x + (%this.size.x/2);
         else if (%area.z > %cameraArea.z)
            %this.Position.x = %cameraArea.z - (%this.size.x/2);
         
      }
      if (%size.y >= %cameraSize.y)
      {
         if (%area.y > %cameraArea.y)
            %this.Position.y = %cameraArea.y + (%this.size.y/2);
         else if (%area.w < %cameraArea.w)
            %this.Position.y = %cameraArea.w - (%this.size.y/2);
      }
      else
      {
         if (%area.y < %cameraArea.y)
            %this.Position.y = %cameraArea.y + (%this.size.y/2);
         else if (%area.w > %cameraArea.w)
            %this.Position.y = %cameraArea.w - (%this.size.y/2);
      }
      
      
      %this.previousPosition = %this.currentPosition;
   }
}

function imageViewer::onTouchUp(%this, %modifier, %worldPosition, %clicks)
{
   %this.bGrabbed = false;   
}
