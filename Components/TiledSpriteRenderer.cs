using Microsoft.Xna.Framework;

namespace Zen.Components
{
    public class TiledSpriteRenderer : SpriteRenderer, IDrawable
    {
        Material IDrawable.Material { get => Material.LinearWrap; }
        Rectangle _sourceRectangle;
        Vector2 _localOffset;

        public TiledSpriteRenderer(Sprite sprite, Rectangle sourceRectangle, Vector2 localOffset) : base(sprite)
        {
            _sourceRectangle = sourceRectangle;
            _localOffset = localOffset;
        }

        public override void Draw()
        {
            Core.Batcher.Draw(Sprite, Transform.Position + _localOffset, _sourceRectangle, Color, Transform.Rotation, Origin, Transform.Scale, SpriteEffects, LayerDepth);
        }
    }
}