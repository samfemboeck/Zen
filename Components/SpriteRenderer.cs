using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Util;

namespace Zen.Components
{
    public class SpriteRenderer : Component, IDrawable, ITransformObserver
    {
        public Color Color = Color.White;
        Material IDrawable.Material { get => Material.Default; }
        public SpriteEffects SpriteEffects { get; protected set; }
        public Vector2 LocalOffset;
        public Vertex4 Vertices;
        Vertex4 _baseVertices;
        Sprite _sprite;

        public Sprite Sprite
        {
            get => _sprite;
            protected set
            {
                _sprite = value;
                _baseVertices.LeftTop = Vector3.Zero;
                _baseVertices.RightTop = new Vector3(value.Width, 0, 0);
                _baseVertices.RightBottom = new Vector3(value.Width, value.Height, 0);
                _baseVertices.LeftBottom = new Vector3(0, value.Height, 0);
            }
        }

        public override void PreMount()
        {
            GetComponent<Transform>().Origin = _sprite.Origin;
        }

        public SpriteRenderer(Sprite sprite)
        {
            Sprite = sprite;
        }

        public SpriteRenderer() {}

        public virtual void Draw()
        {
            Core.Batcher.PushQuad(Sprite.Texture2D, Sprite.UvRect, Vertices, Color);
        }

        void TransformVertices(Matrix transformMatrix)
        {
            Vertices.LeftTop = Vector3.Transform(_baseVertices.LeftTop, transformMatrix);
            Vertices.RightTop = Vector3.Transform(_baseVertices.RightTop, transformMatrix);
            Vertices.RightBottom = Vector3.Transform(_baseVertices.RightBottom, transformMatrix);
            Vertices.LeftBottom = Vector3.Transform(_baseVertices.LeftBottom, transformMatrix);
        }

        public void TransformChanged(Matrix transformMatrix) => TransformVertices(transformMatrix);
    }
}