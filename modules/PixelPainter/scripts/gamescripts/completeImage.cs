function CompleteImage::onAdd(%this)
{
   %completeImageName = "PixelPainter:LevelComplete" @ PixelPainter.currentLevelNumber;
   %this.setImage(%completeImageName);
}