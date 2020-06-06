using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zen.Components
{
    public class SpriteRenderer : Component, IDrawable
    {
        public Color Color = Color.White;
        public float LayerDepth;
        Material IDrawable.Material { get => Material.Default; }
        public SpriteEffects SpriteEffects { get; protected set; }
        public Vector2 Origin;
        Sprite _sprite;

        public Sprite Sprite 
        { 
            get => _sprite;
            protected set
            {
                _sprite = value;
                Origin = value.Origin;
            }
        }

		public bool FlipX
		{
			get => (SpriteEffects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally;
			set => SpriteEffects = value
				? (SpriteEffects | SpriteEffects.FlipHorizontally)
				: (SpriteEffects & ~SpriteEffects.FlipHorizontally);
		}

		public bool FlipY
		{
			get => (SpriteEffects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically;
			set => SpriteEffects = value
				? (SpriteEffects | SpriteEffects.FlipVertically)
				: (SpriteEffects & ~SpriteEffects.FlipVertically);
		}

        public SpriteRenderer(Sprite sprite)
        {
            Sprite = sprite;
        }

        public SpriteRenderer() {}

        public virtual void Draw()
        {
            Core.Batcher.Draw(Sprite, Entity.Transform.Position, Color, Entity.Transform.Rotation, Origin, Entity.Transform.Scale, SpriteEffects, LayerDepth);
        }
    }
}