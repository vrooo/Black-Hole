using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace BlackHole
{
    public class CubeMap
    {
        private static TextureTarget[] targets = { 
            TextureTarget.TextureCubeMapPositiveX,
            TextureTarget.TextureCubeMapNegativeX,
            TextureTarget.TextureCubeMapPositiveY,
            TextureTarget.TextureCubeMapNegativeY,
            TextureTarget.TextureCubeMapPositiveZ,
            TextureTarget.TextureCubeMapNegativeZ
        };
        private int tex;

        public CubeMap(string folder, TextureUnit unit = TextureUnit.Texture0)
        {
            tex = GL.GenTexture();
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.TextureCubeMap, tex);
            for (int i = 0; i < 6; i++)
            {
                var (pixels, width, height) = ImageIO.LoadImageBytes($"{folder}{i}.png");
                GL.TexImage2D(targets[i], 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
            }
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        }

        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.TextureCubeMap, tex);
        }
    }
}
