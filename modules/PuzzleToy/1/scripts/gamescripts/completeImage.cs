function CompleteImage::onAdd(%this)
{
   %completeImageName = "PuzzleToy:LevelComplete" @ PuzzleToy.currentLevelNumber;
   %this.setImage(%completeImageName);
}