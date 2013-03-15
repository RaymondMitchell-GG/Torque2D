$platformFontType = ($platform $= "windows") ? "lucida console" : "monaco";
$platformFontSize = ($platform $= "ios") ? 18 : 12;

//---------------------------------------------------------------------------------------------
// GuiDefaultProfile is a special profile that all other profiles inherit defaults from. It
// must exist.
//---------------------------------------------------------------------------------------------
if(!isObject(GuiDefaultProfile)) new GuiControlProfile (GuiDefaultProfile)
{
    tab = false;
    canKeyFocus = false;
    hasBitmapArray = false;
    mouseOverSelected = false;

    // fill color
    opaque = false;
    fillColor = "211 211 211";
    fillColorHL = "244 244 244";
    fillColorNA = "244 244 244";

    // border color
    border = 0;
    borderColor   = "100 100 100 255";
    borderColorHL = "128 128 128";
    borderColorNA = "64 64 64";

    // font
    fontType = $platformFontType;
    fontSize = $platformFontSize;

    fontColor = "0 0 0";
    fontColorHL = "32 100 100";
    fontColorNA = "0 0 0";
    fontColorSEL= "10 10 10";

    // bitmap information
    bitmap = "^PixelPainter/gui/images/window.png";
    bitmapBase = "";
    textOffset = "0 0";

    // used by guiTextControl
    modal = true;
    justify = "left";
    autoSizeWidth = false;
    autoSizeHeight = false;
    returnTab = false;
    numbersOnly = false;
    cursorColor = "0 0 0 255";

    // sounds
    soundButtonDown = $ButtonSound.fileName;
    soundButtonOver = "";
};

//-----------------------------------------------------------------------------

if(!isObject(PixelWindowProfile)) new GuiControlProfile (PixelWindowProfile : GuiDefaultProfile)
{
    // fill color
    opaque = false;
    fillColor = "0 0 0 92";

    // font
    fontType = $platformFontType;
    fontSize = $platformFontSize;
    fontColor = "255 255 255 255";
};