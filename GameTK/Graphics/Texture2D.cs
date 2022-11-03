using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using StbImageSharp;

namespace GameTK.Graphics;

public class Texture2D
{
    /// <summary>
    /// Global flag to check if scaling textures should preserve their pixalated edges
    /// </summary>
    public static bool HardEdge = false;
    
    /// <summary>
    /// Handle Pointer to the texture
    /// </summary>
    public readonly int Handle;

    public readonly int Width;
    public readonly int Height;

    public Vector2 Origin;
    public Vector2 Scale;
    
    public Texture2D(int handle, ImageResult image)
    {
        Handle = handle;
        Width = image.Width;
        Height = image.Height;
        Origin = Vector2.Zero;
        Scale = Vector2.One;

        // Bind the texture handle
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);
        
        // Generate the texture
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

        // Set the texture blend mode on scaling
        if (HardEdge)
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        }
        else
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
        
        // Set the texture wrap mode
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        
        // Generate a MipMap
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
    }

    /// <summary>
    /// Call this before using a texture
    /// </summary>
    /// <param name="unit"></param>
    public void Use(TextureUnit unit)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }

}