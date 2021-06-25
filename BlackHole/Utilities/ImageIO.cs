using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Generic;

namespace BlackHole
{
    public static class ImageIO
    {
        public static (byte[] pixels, int width, int height) LoadImageBytes(string path)
        {
            Image<Rgba32> img = Image.Load<Rgba32>(path);
            img.TryGetSinglePixelSpan(out System.Span<Rgba32> span);
            var tempPixels = span.ToArray();

            var pixels = new List<byte>();
            foreach (Rgba32 p in tempPixels)
            {
                pixels.Add(p.R);
                pixels.Add(p.G);
                pixels.Add(p.B);
                pixels.Add(p.A);
            }
            return (pixels.ToArray(), img.Width, img.Height);
        }
    }
}
