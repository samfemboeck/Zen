using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Util;

namespace Zen
{
    public class Sprite
    {
        public Texture2D Texture2D;
		public Rectangle SourceRect;
		public RectangleF Uvs;
		public Vector2 Center;
		public Vector2 Origin;

        public Sprite(Texture2D texture, Rectangle sourceRect, Vector2 origin)
		{
			Texture2D = texture;
			SourceRect = sourceRect;
			Center = new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f);
			Origin = origin;

			var inverseTexW = 1.0f / Texture2D.Width;
			var inverseTexH = 1.0f / Texture2D.Height;

			Uvs.X = sourceRect.X * inverseTexW;
			Uvs.Y = sourceRect.Y * inverseTexH;
			Uvs.Width = sourceRect.Width * inverseTexW;
			Uvs.Height = sourceRect.Height * inverseTexH;
		}

		public static implicit operator Texture2D(Sprite tex) => tex.Texture2D;
    }
}