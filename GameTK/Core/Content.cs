using GameTK.Graphics;
using OpenTK.Graphics.OpenGL;
using GL = OpenTK.Graphics.OpenGL4.GL;
using TextureTarget = OpenTK.Graphics.OpenGL4.TextureTarget;
using TextureUnit = OpenTK.Graphics.OpenGL4.TextureUnit;
using StbImageSharp;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using PixelInternalFormat = OpenTK.Graphics.OpenGL4.PixelInternalFormat;
using PixelType = OpenTK.Graphics.OpenGL4.PixelType;

namespace GameTK.Content;

public static class Content
{
    /// <summary>
    /// Loads in a new shader object from the given paths
    /// </summary>
    /// <param name="vertexPath"></param>
    /// <param name="fragmentPath"></param>
    /// <returns></returns>
    public static Shader LoadShader(string vertexPath, string fragmentPath)
    {
        return new Shader(File.ReadAllText(vertexPath), File.ReadAllText(fragmentPath));
    }

    /// <summary>
    /// Loads in a new Texture2D object from a given path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Texture2D LoadTexture2D(string path)
    {
        // Generate handle
        int handle = GL.GenTexture();

        // Flip image to correct origin problem
        StbImage.stbi_set_flip_vertically_on_load(1);

        // Load in image data
        ImageResult image;
        using (Stream stream = File.OpenRead(path))
        {
            image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        }

        // Return Texture2D
        return new Texture2D(handle, image);
    }
}