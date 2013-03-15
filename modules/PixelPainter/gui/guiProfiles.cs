$platformFontType = ($platform $= "windows") ? "lucida console" : "monaco";
$platformFontSize = ($platform $= "ios") ? 18 : 12;

//-----------------------------------------------------------------------------

new GuiCursor(DefaultCursor)
{
    hotSpot = "4 4";
    renderOffset = "0 0";
    bitmapName = "^PixelPainter/gui/images/defaultCursor";
};

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

//-----------------------------------------------------------------------------

if (!isObject(GuiTextEditProfile)) new GuiControlProfile (GuiTextEditProfile)
{
    fontSize = $platformFontSize;
    opaque = false;
    fillColor = "232 240 248 255";
    fillColorHL = "251 170 0 255";
    fillColorNA = "127 127 127 52";
    border = -2;
    bitmap = "^PixelPainter/gui/images/textEdit.png";
    borderColor = "40 40 40 10";
    fontColor = "27 59 95 255";
    fontColorHL = "232 240 248 255";
    fontColorNA = "0 0 0 52";
    fontColorSEL = "0 0 0 255";
    textOffset = "5 2";
    autoSizeWidth = false;
    autoSizeHeight = false;
    tab = false;
    canKeyFocus = true;
    returnTab = true;
};

//-----------------------------------------------------------------------------

if (!isObject(GuiButtonProfile)) new GuiControlProfile (GuiButtonProfile)
{
    opaque = true;
    border = -1;
    fontColor = "white";
    fontColorHL = "229 229 229 255";
    fixedExtent = true;
    justify = "center";
    canKeyFocus = false;
    fontType = $platformFontType;
    bitmap = "^PixelPainter/gui/images/smallButtonContainer";
};

//-----------------------------------------------------------------------------

if (!isObject(BlueButtonProfile)) new GuiControlProfile (BlueButtonProfile : GuiButtonProfile)
{
    fontSize = $platformFontSize;
    fontColor = "255 255 255 255";
    fontColorHL = "255 255 255 255";
    bitmap = "^PixelPainter/gui/images/blueButton.png";
};

//-----------------------------------------------------------------------------

if(!isObject(GuiConsoleProfile)) new GuiControlProfile (GuiConsoleProfile)
{
    fontType = $platformFontType;
    fontSize = $platformFontSize * 1.1;
    fontColor = White;
    fontColorHL = LightSlateGray;
    fontColorNA = Red;
    fontColors[6] = "100 100 100";
    fontColors[7] = "100 100 0";
    fontColors[8] = "0 0 100";
    fontColors[9] = "0 100 0";
};

//-----------------------------------------------------------------------------

if(!isObject(GuiConsoleTextEditProfile)) new GuiControlProfile (GuiConsoleTextEditProfile : GuiTextEditProfile)
{
    fontType = $platformFontType;
    fontSize = $platformFontSize * 1.1;
};

//-----------------------------------------------------------------------------

if(!isObject(GuiScrollProfile)) new GuiControlProfile (GuiScrollProfile)
{
    opaque = true;
    fillColor = "255 255 255";
    border = 1;
    borderThickness = 2;
    bitmap = "^PixelPainter/gui/images/scrollBar.png";
    hasBitmapArray = true;
};

//-----------------------------------------------------------------------------

if(!isObject(ConsoleScrollProfile)) new GuiControlProfile( ConsoleScrollProfile : GuiScrollProfile )
{
    opaque = true;
    fillColor = "0 0 0 120";
    border = 3;
    borderThickness = 0;
    borderColor = "0 0 0";
};

//-----------------------------------------------------------------------------

if(!isObject(GuiToolboxProfile)) new GuiControlProfile( GuiToolboxProfile : GuiScrollProfile )
{
    opaque = true;
    fillColor = "255 255 255 220";
    border = 3;
    borderThickness = 0;
    borderColor = "0 0 0";
};