using OpenTK.Graphics.OpenGL;

namespace GameTK.Graphics;

public static class Graphics
{
    #region Clear Screen
    
    /// <summary>
    /// Default color to clear the screen buffer
    /// </summary>
    public static Color ClearColor = Color.Default;

    /// <summary>
    /// Clear the screen with a given color
    /// </summary>
    /// <param name="color"></param>
    public static void ClearScreen(Color color)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.ClearColor(color.Color4);
    }

    /// <summary>
    /// Clear the screen with the default clear color
    /// </summary>
    public static void ClearScreen()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.ClearColor(ClearColor.Color4);
    }
    
    #endregion
    
    #region Shaders
    
    #endregion
    
    #region Draw Textures
    
    #endregion

    #region Render SpriteBatches

    #endregion
}