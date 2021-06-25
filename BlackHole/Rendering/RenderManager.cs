using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace BlackHole
{
    public class RenderManager
    {
        private const string SHADER_PATH_PREFIX = "../../Shaders/";
        private const string SHADER_VERT_BASIC = SHADER_PATH_PREFIX + "basic.vert";
        private const string SHADER_FRAG_BASIC = SHADER_PATH_PREFIX + "basic.frag";

        private const string TEXTURE_PATH_PREFIX = "../../Textures/";
        private const string TEXTURE_TEST = TEXTURE_PATH_PREFIX + "Test/";
        // tex names: 0 - px, 1 - nx, 2 - py, 3 - ny, 4 - pz, 5 - nz

        private Shader shader;
        private CubeMap cubeTest;
        public Camera Camera { get; }

        public RenderManager(Camera camera)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            //GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.TextureCubeMapSeamless);
            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.Blend);
            GL.Disable(EnableCap.CullFace);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.DstAlpha);

            shader = new Shader(SHADER_VERT_BASIC, SHADER_FRAG_BASIC);
            shader.Use();
            Camera = camera;

            cubeTest = new CubeMap(TEXTURE_TEST);
        }

        public void SetupRender(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            GL.Clear(ClearBufferMask.ColorBufferBit |
                     ClearBufferMask.DepthBufferBit);
            shader.BindMatrix(Camera.GetViewMatrix(), "viewMatrix");
            shader.BindMatrix(Camera.GetProjectionMatrix(width, height), "projMatrix");
        }

        public void UseCubeTest()
        {
            cubeTest.Use();
            shader.BindInt(0, "cubeMap");
        }

        public void Render(Transform transform)
        {
            SetupTransform(transform.Color, transform.GetModelMatrix());
            transform.Render();
        }

        public void SetupTransform(Vector4 color, Matrix4 model)
        {
            shader.BindVector4(color, "color");
            shader.BindMatrix(model, "modelMatrix");
        }
    }
}
