function createSceneWindow()
{
    // Sanity!
    if ( !isObject(PixelWindow) )
    {
        // Create the scene window.
        new SceneWindow(PixelWindow);

       // Set Gui profile. While this line is not necessary, you usually would specify a .Profile for most       
       //GuiControls. If you omit the following line, the program will still run as it uses                
       //GuiDefaultProfile by default

        PixelWindow.Profile = GuiDefaultProfile;

        // Push the window.
        Canvas.setContent( PixelWindow );                     
    }

    //Note that the SceneWindow's camera defaults to the following values, you could omit them entirely and       
    //obtain the same result.
    PixelWindow.Profile = PixelWindowProfile;
    PixelWindow.setCameraPosition( 0, 0 );
    PixelWindow.setCameraSize( 100, 75 );
    PixelWindow.setCameraZoom( 1 );
    PixelWindow.setCameraAngle( 0 );    
}

function destroySceneWindow()
{
    // Finish if no window available.
    if ( !isObject(mySceneWindow) )
        return;
    
    // Delete the window.
    mySceneWindow.delete();
}