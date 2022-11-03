using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GameTK.Graphics;

/// <summary>
/// A Simple Shader class that creates an OpenGL program to handle vertex and fragment shaders
/// </summary>
public class Shader
{
    public static Shader Default
        => new Shader(
            "#version 330 core\n" +
                      "in vec3 aPosition;\n" +
                      "out vec4 vertexColor;\n" +
                      "void main(void)\n" +
                      "{\n" +
                      "gl_Position = vec4(aPosition, 1.0);\n" +
                      "vertexColor = vec4(1.0, 1.0, 1.0, 1.0);\n" +
                      "}",
            "#version 330 core\n" +
            "out vec4 outputColor;\n" +
            "in vec4 vertexColor;\n" +
            "void main()\n" +
            "{\n" +
            "outputColor = vertexColor;\n" +
            "}");

    /// <summary>
    /// The ProgramHandle from which to call this shader
    /// </summary>
    public readonly int Handle;

    /// <summary>
    /// Dictionary to cache the active uniforms of the shader
    /// </summary>
    private Dictionary<string, int> _uniformLocations;

    public Shader(string vertexSource, string fragmentSource)
    {
        // Create a new OpenGL Shader Object
        var vertexShader = GL.CreateShader(ShaderType.VertexShader);
        
        // Couple the shader handle with the source and compile
        GL.ShaderSource(vertexShader, vertexSource);
        CompileShader(vertexShader);

        // Create a new OpenGL Shader Object
        var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        
        // Couple the shader handle with the source and compile
        GL.ShaderSource(fragmentShader, fragmentSource);
        CompileShader(fragmentShader);

        // Create a new OpenGL Program Object
        Handle = GL.CreateProgram();
        
        // Attach the shaders to the program
        GL.AttachShader(Handle, vertexShader);
        GL.AttachShader(Handle, fragmentShader);

        // Link the two shader programs together
        LinkProgram(Handle);
        
        // Detach and dispose the shaders
        GL.DetachShader(Handle, vertexShader);
        GL.DetachShader(Handle, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
        
        // Cache the uniforms in the shader to lower wait time
        CacheUniforms();
    }

    /// <summary>
    /// Tries to compile a given Shader Handle
    /// </summary>
    /// <param name="shader"></param>
    /// <exception cref="Exception"></exception>
    private static void CompileShader(int shader)
    {
        // Try to compile the shader
        GL.CompileShader(shader);
        
        // Check for compilation errors
        GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
        if (code != (int)All.True)
        {
            GL.GetShaderInfoLog(shader, out var infoLog);
            throw new Exception($"Error occured whilst compiling Shader({shader}).\n\n{infoLog}");
        }
    }

    /// <summary>
    /// Tries to link a given Program Handle
    /// </summary>
    /// <param name="program"></param>
    /// <exception cref="Exception"></exception>
    private static void LinkProgram(int program)
    {
        // Try to link the program
        GL.LinkProgram(program);
        
        // Check for linking errors
        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
        if (code != (int)All.True)
        {
            GL.GetProgramInfoLog(program, out var infoLog);
            throw new Exception($"Error occured whilst linking Program({program}).\n\n{infoLog}");
        }
    }

    /// <summary>
    /// Reads and caches each active uniform in the shader
    /// </summary>
    private void CacheUniforms()
    {
        // Get the number of active uniforms in the shader
        GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numOfUniforms);
        
        // Initialize the dictionary
        _uniformLocations = new Dictionary<string, int>();

        for (int i = 0; i < numOfUniforms; i++)
        {
            // Get uniform name
            var key = GL.GetActiveUniform(Handle, i, out _, out _);

            // Get uniform location
            var location = GL.GetUniformLocation(Handle, key);

            // Save to the dictionary
            _uniformLocations.Add(key, location);
        }

    }
    
    /// <summary>
    /// Wrapper function to use the shader
    /// </summary>
    public void Use()
    {
        GL.UseProgram(Handle);
    }

    /// <summary>
    /// Returns the location pointer to an attribute with the given name
    /// </summary>
    /// <param name="attribName"></param>
    /// <returns></returns>
    public int GetAttribLocation(string attribName)
    {
        return GL.GetAttribLocation(Handle, attribName);
    }

    #region Uniform Setters

    public void SetInt(string name, int data)
    {
        GL.UseProgram(Handle);
        GL.Uniform1(_uniformLocations[name], data);
    }

    public void SetFloat(string name, float data)
    {
        GL.UseProgram(Handle);
        GL.Uniform1(_uniformLocations[name], data);
    }

    public void SetMatrix4(string name, Matrix4 data)
    {
        GL.UseProgram(Handle);
        GL.UniformMatrix4(_uniformLocations[name], true, ref data);
    }

    public void SetVector2(string name, Vector2 data)
    {
        GL.UseProgram(Handle);
        GL.Uniform2(_uniformLocations[name], data);
    }
    
    public void SetVector3(string name, Vector3 data)
    {
        GL.UseProgram(Handle);
        GL.Uniform3(_uniformLocations[name], data);
    }

    public void SetVector4(string name, Vector4 data)
    {
        GL.UseProgram(Handle);
        GL.Uniform4(_uniformLocations[name], data);
    }

    #endregion
    
}