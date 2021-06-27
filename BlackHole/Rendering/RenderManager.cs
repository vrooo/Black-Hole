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
        private const string TEXTURE_COLORS = TEXTURE_PATH_PREFIX + "Colors/";
        private const string TEXTURE_MILKYWAY1 = TEXTURE_PATH_PREFIX + "MilkyWay1/";
        private const string TEXTURE_MILKYWAY2 = TEXTURE_PATH_PREFIX + "MilkyWay2/";
        // tex names: 0 - px, 1 - nx, 2 - py, 3 - ny, 4 - pz, 5 - nz

        private Shader shader;
        private CubeMap cubeColors, cubeMilkyWay1, cubeMilkyWay2;
        public Camera Camera { get; }

        public RenderManager(Camera camera)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            //GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.Blend);
            GL.Disable(EnableCap.CullFace);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.DstAlpha);

            shader = new Shader(SHADER_VERT_BASIC, SHADER_FRAG_BASIC);
            shader.Use();
            Camera = camera;

            cubeColors = new CubeMap(TEXTURE_COLORS);
            cubeMilkyWay1 = new CubeMap(TEXTURE_MILKYWAY1);
            cubeMilkyWay2 = new CubeMap(TEXTURE_MILKYWAY2);
        }

        public void SetupRender(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            GL.Clear(ClearBufferMask.ColorBufferBit |
                     ClearBufferMask.DepthBufferBit);
            var invView = Camera.GetInvViewMatrix();
            shader.BindMatrix(invView, "invViewMatrix");
            shader.BindVector3(invView.ExtractTranslation(), "camPos");
            shader.BindFloat((float)width / height, "aspectRatio");
        }

        public void UseCubeColors()
        {
            cubeColors.Use();
            GL.Disable(EnableCap.TextureCubeMapSeamless);
            shader.BindInt(0, "cubeMap");
        }

        public void UseCubeMilkyWay1()
        {
            cubeMilkyWay1.Use();
            GL.Enable(EnableCap.TextureCubeMapSeamless);
            shader.BindInt(0, "cubeMap");
        }

        public void UseCubeMilkyWay2()
        {
            cubeMilkyWay2.Use();
            GL.Enable(EnableCap.TextureCubeMapSeamless);
            shader.BindInt(0, "cubeMap");
        }

        public void Render(Transform transform)
        {
            //SetupTransform(transform.Color, transform.GetModelMatrix());
            transform.Render();
        }

        public void SetupTransform(Vector4 color, Matrix4 model)
        {
            shader.BindVector4(color, "color");
            shader.BindMatrix(model, "modelMatrix");
        }

        public void BindFloat(float obj, string name)
        {
            shader.BindFloat(obj, name);
        }

        public void BindInt(int obj, string name)
        {
            shader.BindInt(obj, name);
        }
    }
}
