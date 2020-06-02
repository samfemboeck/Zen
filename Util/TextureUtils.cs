using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace Zen.Textures
{
    /// <summary>
    /// various utilties for creating textures on the fly. These can be pretty heavy on the CPU so it's best to not try to use them every frame.
    /// </summary>
    public static class TextureUtils
    {
        public enum EdgeDetectionFilter
        {
            Sobel,
            Scharr,
            FiveTap
        }

#if FNA
		/// <summary>
		/// loads a Texture2D and premultiplies the alpha
		/// </summary>
		public static Texture2D TextureFromStreamPreMultiplied(Stream stream)
		{
			if (stream.CanSeek && stream.Position == stream.Length)
				stream.Seek(0, SeekOrigin.Begin);

			Texture2D.TextureDataFromStreamEXT(stream, out int width, out int height, out var pixels);
			PremultiplyAlpha(pixels);

			var result = new Texture2D(Core.GraphicsDevice, width, height);
			result.SetData(pixels);
			return result;
		}
#else
        /// <summary>
        /// loads a Texture2D and premultiplies the alpha
        /// </summary>
        public static Texture2D TextureFromStreamPreMultiplied(Stream stream)
        {
            var texture = Texture2D.FromStream(Core.GraphicsDevice, stream);

            var pixels = new byte[texture.Width * texture.Height * 4];
            texture.GetData(pixels);
            PremultiplyAlpha(pixels);
            texture.SetData(pixels);

            return texture;
        }
#endif

        static unsafe void PremultiplyAlpha(byte[] pixels)
        {
            fixed (byte* b = &pixels[0])
            {
                for (var i = 0; i < pixels.Length; i += 4)
                {
                    if (b[i + 3] != 255)
                    {
                        var alpha = b[i + 3] / 255f;
                        b[i + 0] = (byte)(b[i + 0] * alpha);
                        b[i + 1] = (byte)(b[i + 1] * alpha);
                        b[i + 2] = (byte)(b[i + 2] * alpha);
                    }
                }
            }
        }
    }
}