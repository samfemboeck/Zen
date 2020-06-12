using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Util;

namespace Zen.Components
{
    public class SpriteRenderer : Component, IDrawable
    {
        public Color Color = Color.White;
        public Vector2 PositionOffset = Vector2.Zero;
        public RectangleF TargetRectangle;
        Sprite _sprite;
        
        public Material Material;
        Material IDrawable.Material { get => Material ?? Material.Default; }

        Transform _transform;

        public override void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public SpriteRenderer(Sprite sprite, float? width = null, float? height = null)
        {
            SetSprite(sprite, width, height);
        }

        public SpriteRenderer() {}

        protected void SetSprite(Sprite sprite, float? width = null, float? height = null)
        {
            _sprite = sprite;
            TargetRectangle = new RectangleF(0, 0, width ?? sprite.UvRect.Width, height ?? sprite.UvRect.Height);
        }

        public virtual void Draw()
        {
            Core.Batcher.PushQuad(_sprite.Texture, _sprite.UvRectNormalized, TargetRectangle, _transform.Position + PositionOffset, _transform.Origin, _transform.Rotation, _transform.Scale, _transform.Flip, Color);
        }
    }
}