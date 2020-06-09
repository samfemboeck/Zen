using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Util;

namespace Zen
{
    public class Sprite
    {
        public Texture2D Texture2D;
		public RectangleF UvRect;
		public Vector2 Center;
		public Vector2 Origin;
		public float Width;
		public float Height;
		public bool FlipHorizontally;
		public bool FlipVertically;

        public Sprite(Texture2D texture, RectangleF uvRect, Vector2? origin = null, float? width = null, float? height = null)
		{
			Texture2D = texture;
			UvRect = uvRect;
			Center = new Vector2(uvRect.Width * 0.5f, uvRect.Height * 0.5f);
			Origin = origin ?? new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);

			var inverseTexW = 1.0f / Texture2D.Width;
			var inverseTexH = 1.0f / Texture2D.Height;

			UvRect.X = uvRect.X * inverseTexW;
			UvRect.Y = uvRect.Y * inverseTexH;
			UvRect.Width = uvRect.Width * inverseTexW;
			UvRect.Height = uvRect.Height * inverseTexH;

			Width = width ?? UvRect.Width * texture.Width;
			Height = height ?? UvRect.Height * texture.Height;
		}

		public Sprite(Texture2D texture) : this(texture, new RectangleF(0, 0, texture.Width, texture.Height), new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), texture.Width, texture.Height) {}

		public static implicit operator Texture2D(Sprite tex) => tex.Texture2D;
    }
}