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
    public static Color Default => new Color(OpenTK.Mathematics.Color4.Cornflowerblue);
    
    public static Color White => new Color();
    public static Color Black => new Color(0, 0, 0);
    public static Color Red => new Color(1, 0, 0);
    public static Color Green => new Color(0, 1, 0);
    public static Color Blue => new Color(0, 0, 1);
    
    #endregion
    
    /// <summary>
    /// Converts a color defined in HSVA to a color defined with RGBA values
    /// </summary>
    /// <param name="h"></param>
    /// <param name="s"></param>
    /// <param name="v"></param>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public static Color FromHSV(float h = 1, float s = 1, float v = 1, float alpha = 1)
    {
        Color4<Hsva> c = new Color4<Hsva>(h, s, v, alpha);
        Color4<Rgba> result = c.ToRgba();
        return new Color(result.X, result.Y, result.Z, alpha);
    }
    
    /// <summary>
    /// The openTK color value in RGBA
    /// </summary>
    public Color4<Rgba> Color4 { get; private set; }

    public float R
    {
        get { return Color4.X; }
        set { Color4 = new Color4<Rgba>(value, Color4.Y, Color4.Z, Color4.W); }
    }
    public float G
    {
        get { return Color4.Y; }
        set { Color4 = new Color4<Rgba>(Color4.X, value, Color4.Z, Color4.W); }
    }
    public float B
    {
        get { return Color4.Z; }
        set { Color4 = new Color4<Rgba>(Color4.X, Color4.Y, value, Color4.W); }
    }
    public float Alpha
    {
        get { return Color4.W; }
        set { Color4 = new Color4<Rgba>(Color4.X, Color4.Y, Color4.Z, value); }
    }
    
    public Color(float r = 1, float g = 1, float b = 1, float alpha = 1)
    {
        Color4 = new Color4<Rgba>(r, g, b, alpha);
    }
    public Color(Color4<Rgba> color4)
    {
        Color4 = color4;
    }
}