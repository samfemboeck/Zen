using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zen.Components
{
    public class SpriteRenderer : Component, Zen.IDrawable
    {
        public Color Color = Color.White;
        public float LayerDepth;
        public Sprite Sprite { get; protected set; }
        SpriteEffects _spriteEffects;

		public bool FlipX
		{
			get => (_spriteEffects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally;
			set => _spriteEffects = value
				? (_spriteEffects | SpriteEffects.FlipHorizontally)
				: (_spriteEffects & ~SpriteEffects.FlipHorizontally);
		}

		public bool FlipY
		{
			get => (_spriteEffects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically;
			set => _spriteEffects = value
				? (_spriteEffects | SpriteEffects.FlipVertically)
				: (_spriteEffects & ~SpriteEffects.FlipVertically);
		}

        public SpriteRenderer(Sprite sprite)
        {
            Sprite = sprite;
        }

        public SpriteRenderer() {}

        public void Draw()
        {
            Core.Batcher.Draw(Sprite, Entity.Transform.Position, Color, Entity.Transform.Rotation, Sprite.Origin, Entity.Transform.Scale, _spriteEffects, LayerDepth);
        }
    }
}