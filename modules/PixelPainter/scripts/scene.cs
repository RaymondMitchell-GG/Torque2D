function createScene()
{
    // Destroy the scene if it already exists.
    if ( isObject(PixelScene) )
        destroyScene();
    
    // Create the scene.
    new Scene(PixelScene);
    
}

function destroyScene()
{
    // Finish if no scene available.
    if ( !isObject(PixelScene) )
        return;

    // Delete the scene.
    PixelScene.delete();
}

function setCustomScene(%scene)
{   
   // Sanity!
    if ( !isObject(%scene) )
    {
        error( "Cannot set PixelScene to use an invalid Scene." );
        return;
    }
   
    // Destroy the existing scene.  
    destroyScene();

    // The Sandbox needs the scene to be named this.
    %scene.setName( "PixelScene" );    
    
    // Set the scene to the window.
    setSceneToWindow();
}

function setSceneToWindow()
{
    // Sanity!
    if ( !isObject(PixelScene) )
    {
        error( "Cannot set Sandbox Scene to Window as the Scene is invalid." );
        return;
    }
    
     // Set scene to window.
    PixelWindow.setScene( PixelScene );

    // Set camera to a canonical state.
    %allBits = "0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31";
    PixelWindow.stopCameraMove();
    PixelWindow.dismount();
    PixelWindow.setViewLimitOff();
    PixelWindow.setRenderGroups( %allBits );
    PixelWindow.setRenderLayers( %allBits );
    PixelWindow.setObjectInputEventGroupFilter( %allBits );
    PixelWindow.setObjectInputEventLayerFilter( %allBits );
    PixelWindow.setLockMouse( true );
    PixelWindow.setCameraPosition( 0, 0 );
    PixelWindow.setCameraSize( 100, 75 );
    PixelWindow.setCameraZoom( 1 );
    PixelWindow.setCameraAngle( 0 );        

}