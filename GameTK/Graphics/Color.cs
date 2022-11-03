using OpenTK.Mathematics;

namespace GameTK.Graphics;

/// <summary>
/// Wrapper class so the solution using this library doesn't have to deal with openTK colors
/// </summary>
public class Color
{
    #region Colors

    /// <summary>
    /// The default clear color
    /// </summary>
    public static Color Default => new Color(Color4.CornflowerBlue);
    
    public static Color White => new Color();
    public static Color Black => new Color(0, 0, 0);
    public static Color Red => new Color(1, 0, 0);
    public static Color Green => new Color(0, 1, 0);
    public static Color Blue => new Color(0, 0, 1);
    
    #endregion

    /// <summary>
    /// The openTK color value in RGBA
    /// </summary>
    public Color4 Color4 { get; private set; }

    public float R
    {
        get { return Color4.R; }
        set { Color4 = new Color4(value, Color4.G, Color4.B, Color4.A); }
    }
    public float G
    {
        get { return Color4.G; }
        set { Color4 = new Color4(Color4.R, value, Color4.B, Color4.A); }
    }
    public float B
    {
        get { return Color4.B; }
        set { Color4 = new Color4(Color4.R, Color4.G, value, Color4.A); }
    }
    public float Alpha
    {
        get { return Color4.A; }
        set { Color4 = new Color4(Color4.R, Color4.G, Color4.B, value); }
    }
    
    public Color(float r = 1, float g = 1, float b = 1, float alpha = 1)
    {
        Color4 = new Color4(r, g, b, alpha);
    }
    public Color(Color4 color4)
    {
        Color4 = color4;
    }
}