using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL.Compatibility;

namespace GameTK.Graphics;

/// <summary>
/// A Simple Shader class that creates an OpenGL program to handle vertex and fragment shaders
/// </summary>
public class Shader
{
    /// <summary>
    /// The ProgramHandle from which to call this shader
    /// </summary>
    public readonly ProgramHandle Handle;

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
    }

    /// <summary>
    /// Tries to compile a given Shader Handle
    /// </summary>
    /// <param name="shader"></param>
    /// <exception cref="Exception"></exception>
    private static void CompileShader(ShaderHandle shader)
    {
        // Try to compile the shader
        GL.CompileShader(shader);
        
        // Check for compilation errors
        var code = 0;
        GL.GetShaderi(shader, ShaderParameterName.CompileStatus, ref code);
        if (code != (int)All.True)
        {
            string infoLog = "";
            GL.GetShaderInfoLog(shader, out infoLog);
            throw new Exception($"Error occured whilst compiling Shader({shader}).\n\n{infoLog}");
        }
    }

    /// <summary>
    /// Tries to link a given Program Handle
    /// </summary>
    /// <param name="program"></param>
    /// <exception cref="Exception"></exception>
    private static void LinkProgram(ProgramHandle program)
    {
        // Try to link the program
        GL.LinkProgram(program);
        
        // Check for linking errors
        var code = 0;
        GL.GetProgrami(program, ProgramPropertyARB.LinkStatus, ref code);
        if (code != (int)All.True)
        {
            string infoLog = "";
            GL.GetProgramInfoLog(program, out infoLog);
            throw new Exception($"Error occured whilst linking Program({program}).\n\n{infoLog}");
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