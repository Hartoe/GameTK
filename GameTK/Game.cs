using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GameTK;

/// <summary>
/// Entry class that contains the game world and the Update/Render loop
/// </summary>
public abstract class Game
{
    /// <summary>
    /// The title shown on top of the window
    /// </summary>
    protected string WindowTitle { get; set; }
    /// <summary>
    /// The initial width in pixels that the window has
    /// </summary>
    protected int InitialWindowWidth { get; set; }
    /// <summary>
    /// The initial height in pixels that the window has
    /// </summary>
    protected int InitialWindowHeight { get; set; }

    /// <summary>
    /// Default window settings used to initialized the game window
    /// </summary>
    private GameWindowSettings _gameWindowSettings = GameWindowSettings.Default;
    /// <summary>
    /// Native window settings used to initialized the game window.
    /// Uses the values given in WindowTitle, InitialWindowWidth, and InitialWindowHeight
    /// </summary>
    private NativeWindowSettings _nativeWindowSettings;

    /// <summary>
    /// Entry class that contains the game world and the Update/Render loop
    /// </summary>
    /// <param name="windowTitle">Title of the window</param>
    /// <param name="windowWidth">Initial width of the window in pixels</param>
    /// <param name="windowHeight">Initial height of the window in pixels</param>
    public Game(string windowTitle, int windowWidth, int windowHeight)
    {
        // Set the values of the initial values
        WindowTitle = windowTitle;
        InitialWindowWidth = windowWidth;
        InitialWindowHeight = windowHeight;
        
        // Initialize the nativeWindowSettings
        _nativeWindowSettings = new NativeWindowSettings()
        {
            Size = new Vector2i(InitialWindowWidth, InitialWindowHeight),
            Title = WindowTitle,
            Flags = ContextFlags.ForwardCompatible
        };
    }

    /// <summary>
    /// The Run method initializes starting values, loads content, and starts the Update/Render loop 
    /// </summary>
    public void Run()
    {
        // Calls the initializer of the game
        Initialize();
        
        // Creates the game window that will be used for the game
        using GameWindow gameWindow = new GameWindow(_gameWindowSettings, _nativeWindowSettings);
        
        // Setup gameTime variable to keep track of how long the game has been running
        GameTime gameTime = new();
        
        // Link the methods of the Game class to the game window events
        gameWindow.Load += LoadContent;
        gameWindow.Unload += UnloadContent;
        gameWindow.UpdateFrame += args =>
        {
            double time = args.Time;
            gameTime.ElapsedGameTime = TimeSpan.FromMilliseconds(time);
            gameTime.TotalGameTime += TimeSpan.FromMilliseconds(time);
            Update(gameTime);
        };
        gameWindow.RenderFrame += args =>
        {
            Render(gameTime);
            // Swap buffers here so we don't have to remember to add it at the end of the render method
            gameWindow.SwapBuffers();
        };
        
        // Run the game window Update/Render loop
        gameWindow.Run();
    }

    /// <summary>
    /// This method initializes any values and objects that need to be created before entering the Update/Render loop
    /// </summary>
    protected abstract void Initialize();
    /// <summary>
    /// Loads content before entering the Update/Render loop, so to not slow it down
    /// </summary>
    protected abstract void LoadContent();
    /// <summary>
    /// Updates the Handlers, SceneManager, and the GameWorld within
    /// </summary>
    /// <param name="gameTime"></param>
    protected abstract void Update(GameTime gameTime);
    /// <summary>
    /// Renders all game objects to the screen
    /// </summary>
    /// <param name="gameTime"></param>
    protected abstract void Render(GameTime gameTime);
    /// <summary>
    /// Unloads content after the Update/Render loop has ended
    /// </summary>
    protected abstract void UnloadContent();

}