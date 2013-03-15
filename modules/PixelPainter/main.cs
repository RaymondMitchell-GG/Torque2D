//-----------------------------------------------------------------------------
// Puzzle Toy Demo
//-----------------------------------------------------------------------------

///-----------------------------------------------------------------------------
/// Function used to create the toy.
///-----------------------------------------------------------------------------
function PixelPainter::create( %this )
{   
   // load up my game specific game scripts first   
   exec("./scripts/scene.cs");
   exec("./scripts/scenewindow.cs");
   exec("./gui/guiProfiles.cs");
   exec("./scripts/gamescripts/gameBoard.cs");   
   exec("./scripts/gamescripts/gamePiece.cs");
   exec("./scripts/gamescripts/soundButton.cs");   
   exec("./scripts/gamescripts/startButton.cs");
   exec("./scripts/gamescripts/continueButton.cs");
   exec("./scripts/gamescripts/galleryButton.cs");
   exec("./scripts/gamescripts/nextButton.cs");
   exec("./scripts/gamescripts/mainMenuButton.cs");
   exec("./scripts/gamescripts/pauseButton.cs");
   exec("./scripts/gamescripts/timeBar.cs");
   exec("./scripts/gamescripts/nextLevelButton.cs");
   exec("./scripts/gamescripts/completeImage.cs");
   exec("./scripts/gamescripts/galleryViewer.cs");
   exec("./scripts/gamescripts/imageViewer.cs");
   
   // First we need a scene window.  create it.
   createSceneWindow();
   
   %enabledebug = true; // isDebugBuild();
   // if this is a debug build enable the console
   if (%enabledebug)
   {
      // load the console script
      exec("./scripts/console.cs");
      // create the console and bind tilde to toggle it.
      // Load and configure the console.      
      PixelPainter.add( TamlRead("./gui/ConsoleDialog.gui.taml") );
      GlobalActionMap.bind( keyboard, "ctrl tilde", toggleConsole );
   }
   // When we create the toy, we want to check for a save file.
   // set up our path and filename for save data.   
   PixelPainter.savedirectory = getUserDataDirectory() @ "/puzzletoy";
   PixelPainter.savepath = PixelPainter.savedirectory @ "/PixelPainter.savedata";
   // load user data
   PixelPainter.loadUserData();
                    
   // This is a tab separated list of color strings so that we can use a color index
   // to store a game pieces color and then use this list to find out it's color string
   // We do this using getWord(PixelPainter::colors, index)
   PixelPainter.colors =  "Gray" TAB "White" TAB "Red" TAB "Blue" TAB "Yellow" TAB 
                     "Green" TAB "Orange" TAB "Purple" TAB "Grey" TAB "Bucket" TAB "Eraser";
   
   // default the current level to the main menu scene.
   PixelPainter.currentLevel = "./levels/mainMenu.scene.taml";   
   PixelPainter.currentLevelNumber = PixelPainter.savedata.currentLevelNumber;
   // Set some global variables
   // this will be false until the options, level time etc are set.
   PixelPainter.bGameInit = false;	
	PixelPainter.soundEnabled = true;	
	PixelPainter.LevelTime = 360000;	
	// number of levels
	PixelPainter.NumberOfLevels = 6;
	
	
   // now my toy options are initialized, so set bToyInit to true.
   PixelPainter.bGameInit = true;   
   
   // I moved all the script loading up the main menu and such in the reset function 
   // so I don't have to write it twice.
   // I call it here to start everything up
   PixelPainter.reset();      
}

function PixelPainter::saveUserData(%this)
{
   %userdata = PixelPainter.savedata;
   if (isObject(%userdata))
   {
      if (!isDirectory(PixelPainter.savedirectory))
         createPath(PixelPainter.savedirectory);      
      
      TamlWrite(%userdata, PixelPainter.savepath, binary);
   }
   else
   {      
      %this.createUserData();
   }   
}

function PixelPainter::loadUserData(%this)
{
   if (isDirectory(PixelPainter.savedirectory) && isFile(PixelPainter.savepath))
   {
      PixelPainter.savedata = TamlRead(PixelPainter.savepath, binary);      
   }
   else
   {
      PixelPainter.createUserData();
   }
   
}

function PixelPainter::createUserData(%this)
{
   // if the save directory doesn't exist, create it.
   if (!isDirectory(PixelPainter.savedirectory))
   {
      createPath(PixelPainter.savedirectory);
   }
   
   // setup default player data
   %savedata = new ScriptObject();
   %savedata.currentLevelNumber = 1;
   %savedata.currentLevel = 1-1;
   %savedata.highestComplete = 0;
   %savedata.highScore = 0;   
   
   // set the current save data so we can access it
   PixelPainter.savedata = %savedata;
   // write the default save data
   TamlWrite(%savedata, PixelPainter.savepath, binary);
}

///-----------------------------------------------------------------------------
/// set the selected level
/// Param %value The base level name without the path or ".scene.taml" added
/// This is specifically for our toolbox control don't call this elsewhere.
///-----------------------------------------------------------------------------
function PixelPainter::setSelectedLevel( %this, %value )
{
   // Only set if we have already initialized the toy.
   // this is to keep from resetting when the toolbox options are added.
   if (PixelPainter.bGameInit)
   {
      // set the current Board
      PixelPainter.currentBoard = %value;
      // Create the filename string.
      %levelToLoad = "./levels/" @ %value @ ".scene.taml";
      // this isn't the main menu, then we want gameplay      
      if (%value !$= "mainMenu" && %value !$= "LevelComplete")
      {                 
         PixelPainter.currentLevelNumber = getSubStr(%value, 0, 1);
         %levelToLoad = "./levels/GamePlay.scene.taml";
      } 
      // Load the level and show a loading screen.
      PixelPainter.loadLevel( %levelToLoad, true );
   }
}

///-----------------------------------------------------------------------------
/// set the time before you fail a level
/// param %value - time in seconds
/// This will be converted to milliseconds
///-----------------------------------------------------------------------------
function PixelPainter::setLevelTime( %this, %value )
{
   // Only set if we have already initialized the toy.
   // this is to keep from resetting when the toolbox options are added.
   if (PixelPainter.bGameInit)
   {
      // Set the time allowed to complete a level
      PixelPainter.LevelTime = %value * 60000;
   }
}
///-----------------------------------------------------------------------------
/// Get the level list
/// returns a comma separated list of level names
///-----------------------------------------------------------------------------
function getLevelList()
{
   // create the comma separated list   
   %list = "1-1,1-2,1-3,1-4,2-1,2-2,2-3,2-4,3-1,3-2,3-3,3-4,3-5,3-6,4-1,4-2,4-3,4-4,4-5,4-6,5-1,5-2,5-3,5-4,5-5,5-6,5-7,5-8,5-9,6-1,6-2,6-3,6-4,6-5,6-7,6-8,6-9,6-10,6-11,6-12,mainMenu";
   // return the created list
   return %list;
}

///-----------------------------------------------------------------------------
/// destroy function for this toy
///-----------------------------------------------------------------------------
function PixelPainter::destroy( %this )
{   
   // Before I do anything, I want to cancel any level load events so we don't 
   // try to load a level after destroying the toy.
   %this.cancelLoadEvents();
      
   // destroy my sandbox scene??
   PixelScene.delete();
   // Stop any sounds that are playing.
   alxStopAll();
   // Since I changed these, I am setting them back to their defaults
   PixelWindow.setUseWindowInputEvents( true );
   PixelWindow.setUseObjectInputEvents( false ); 
}

///-----------------------------------------------------------------------------
/// Cancel any pending load events
/// This function is to keep from having unwanted load events
///-----------------------------------------------------------------------------
function PixelPainter::cancelLoadEvents( %this )
{
   // If we don't have a pending load event, just return
   if (!isEventPending( %this.loadEventId ))
      return;
   // Otherwise, we should cancel the load event
   cancel(%this.loadEventId);
   // Now lets clear out the load event Id so we won't get a false positive next time
   %this.loadEventId = "";
   
}

///-----------------------------------------------------------------------------
/// Reset the toy
///-----------------------------------------------------------------------------
function PixelPainter::reset( %this )
{
   // Since we use this to start and reset our toy, we should make sure there
   // aren't any pending load events scheduled.
   %this.cancelLoadEvents();
   // Stop any sounds that may be playing
   alxStopAll();
   // Use our custom loadLevel function so we can schedule main menu to be loaded.
   // Loading is true to show the loading screen first.
   PixelPainter.loadLevel( "./levels/mainMenu.scene.taml", true );

   
}

///-----------------------------------------------------------------------------
/// Load the currently set level
/// This should not be called directly.  Call PixelPainter:loadLevel instead so
/// it can cycle to the loading screen, then the level you want.
///-----------------------------------------------------------------------------
function loadLevel(%this)
{
   // Any time we load a level we should stop any playing sounds.
   alxStopAll();   
   // if a gameboard exists, we need to clean it up
   if (isObject(gBoard))
   {
      gBoard.cleanUp();
   }
   // Set our scene by reading in the level file value stored in currentLevel
   setCustomScene(TamlRead(PixelPainter.currentLevel));
   
   // If we are loading the loading scene, schedule the next level to load with
   // loading as false.
   if (PixelPainter.loading)
   {
      // set loading to false since we already loaded the loading scene.
      PixelPainter.loading = false;
      // Schedule the level we intend to load next
      PixelPainter.loadLevel(PixelPainter.nextLevel, false);
   }
   else
   {
      // play this levels background music if the sound is enabled.
      if (PixelPainter.soundEnabled)
         alxPlay(PixelScene.MusicAsset);
      
   }
   // Set up our window input so we can play the game correctly.
   PixelWindow.setUseWindowInputEvents( false);
   // We want our objects to be able to get input events so set this to true.
   PixelWindow.setUseObjectInputEvents( true );
   // We don't want gravity, so I want to make sure our gravity is set to (0,0).
   PixelScene.setGravity(0, 0);
   // Since we may be coming from other toys, and my scene size might be different,
   // I set the window position, size, and zoom here.
   PixelWindow.setCameraPosition( 0, 0);
   PixelWindow.setCameraSize( 160, 90 );
   PixelWindow.setCameraZoom( 1 );
   
   
}

function TogglePauseScene()
{
   if (!isObject(PauseScene))
   {
      %pausescene = TamlRead("./levels/PauseScreen.scene.taml");      
   }
   
   if (PixelScene.getScenePause())
   {
      PixelScene.setScenePause(false);
      PixelPainter.Paused = false;
      PixelWindow.setScene(PixelScene);
   }
   else
   {
      PixelScene.setScenePause(true);
      PixelPainter.Paused = true;
      PixelWindow.setScene(PauseScene);
   }
   
   
}

///-----------------------------------------------------------------------------
/// Load the level specified.
/// Param %levelName - the name of the level, not the file name.
/// Param %loading - bool for if we should transition to the loading screen
/// be displayed first.
///-----------------------------------------------------------------------------
function PixelPainter::loadLevel( %this, %levelName, %loading )
{
   // There shoudln't be any load events at this point, so clear out the load event id.
   %this.loadEventId = "";
   // Set our global bool to know whether or not we should load the loading scene.
   PixelPainter.loading = %loading;
   
   // Should we show the loading scene, or load the specified level?
   if (PixelPainter.loading)
   {
      // set the current level to the loading scene so we can schedule the load.
      PixelPainter.currentLevel = "./levels/LoadLevel.scene.taml";
      // Since we aren't loading the supplied %levelName, we need to store it in 
      // $nextLevel so we can load it after the loading scene.
      PixelPainter.nextLevel = %levelName;
      // Schedule the load and store the schedule event Id so we can cancel it if needed.
      %this.loadEventId = schedule(500, 0, "loadLevel" );
   }
   else
   {
      // Set the current level to the supplied level so we can schedule the load.
      PixelPainter.currentLevel = %levelName;
      // Schedule the load and store the schedule event Id so we can cancel it if needed.
      %this.loadEventId = schedule(1000, 0, "loadLevel" );
   }

}

