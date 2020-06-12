using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Util;

namespace Zen
{
    public class Sprite
    {
        public Texture2D Texture;
		public RectangleF UvRect;
		public RectangleF UvRectNormalized;

        public Sprite(Texture2D texture, RectangleF? uvRect = null)
		{
			Texture = texture;
			UvRect = uvRect ?? new RectangleF(0, 0, texture.Width, texture.Height);
			UvRectNormalized = new RectangleF(UvRect.X / texture.Width, UvRect.Y / texture.Height, UvRect.Width / texture.Width, UvRect.Height / texture.Height);
		}
    }
}