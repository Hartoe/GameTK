using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Drawing;
using PrimitiveType = OpenTK.Graphics.OpenGL.PrimitiveType;

namespace GameTK.Graphics;

public static class Graphics
{
    #region Clear Screen
    
    /// <summary>
    /// Default color to clear the screen buffer
    /// </summary>
    public static Color ClearColor = Color.CornflowerBlue;

    /// <summary>
    /// Clear the screen with a given color
    /// </summary>
    /// <param name="color"></param>
    public static void ClearScreen(Color color)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.ClearColor(color);
    }

    /// <summary>
    /// Clear the screen with the default clear color
    /// </summary>
    public static void ClearScreen()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.ClearColor(ClearColor);
    }
    
    #endregion
    
    #region Shaders
    
    #endregion
    
    #region Draw Textures

    public static void RenderTexture2D(Vector2 position, Texture2D texture)
    {
        // Create quad on which to draw the texture
        
        
        // Translate positions
        
        // Activate Shader
        
        // Activate Texture
        
        // Unbind?
    }
    
    #endregion

    #region Render SpriteBatches

    #endregion
}