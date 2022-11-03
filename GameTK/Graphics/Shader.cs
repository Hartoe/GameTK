using OpenTK.Graphics.OpenGL4;

namespace GameTK.Graphics;

/// <summary>
/// A Simple Shader class that creates an OpenGL program to handle vertex and fragment shaders
/// </summary>
public class Shader
{
    /// <summary>
    /// The ProgramHandle from which to call this shader
    /// </summary>
    public readonly int Handle;

    private Dictionary<string, int> _uniformLocations;

    public Shader(string vertexPath, string fragmentPath)
    {
        // Read the vertex shader .glsl file
        var vertexSource = File.ReadAllText(vertexPath);
        // Create a new OpenGL Shader Object
        var vertexShader = GL.CreateShader(ShaderType.VertexShader);
        
        // Couple the shader handle with the source and compile
        GL.ShaderSource(vertexShader, vertexSource);
        CompileShader(vertexShader);

        // Read the fragment shader .glsl file
        var fragmentSource = File.ReadAllText(fragmentPath);
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

}