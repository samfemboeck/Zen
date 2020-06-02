using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Graphics;

namespace Zen.EC
{
    public class SpriteRenderer : Component, IDrawable
    {
        public Vector2 Origin;
        protected Sprite _sprite;
        public Color Color = Color.White;
        public float LayerDepth;
        
        public Sprite Sprite
		{
			get => _sprite;
			set => SetSprite(value);
		}

        public Material Material { get; set; } = Material.Default;

        public SpriteRenderer()
		{ }

        public SpriteEffects SpriteEffects = SpriteEffects.None;

        public SpriteRenderer(Sprite sprite) => SetSprite(sprite);

        public void SetSprite(Sprite sprite)
        {
            _sprite = sprite;
            Origin = _sprite.Origin;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite.Texture2D, Entity.Transform.Position, _sprite.SourceRect, Color, Entity.Transform.Rotation, Origin, Entity.Transform.Scale, SpriteEffects, LayerDepth);
        }
    }
}