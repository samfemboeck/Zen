using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zen.Graphics
{
    public class Sprite
    {
        public Texture2D Texture2D;

        public readonly Rectangle SourceRect;

        public readonly Vector2 Center;

        public Vector2 Origin;

        public Sprite(Texture2D texture, Rectangle sourceRect, Vector2 origin)
        {
            Texture2D = texture;
            SourceRect = sourceRect;
            Center = new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f);
            Origin = origin;
        }

        public Sprite(Texture2D texture, Rectangle sourceRect) : this(texture, sourceRect, new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f))
        { }

        public Sprite(Texture2D texture) : this(texture, new Rectangle(0, 0, texture.Width, texture.Height))
        { }

        public Sprite(Texture2D texture, int x, int y, int width, int height) : this(texture,
            new Rectangle(x, y, width, height))
        { }
        public Sprite(Texture2D texture, float x, float y, float width, float height) : this(texture, (int)x,
            (int)y, (int)width, (int)height)
        { }
    }
}