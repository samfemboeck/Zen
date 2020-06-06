using Microsoft.Xna.Framework;
using Zen.Components;
using Zen.Util;

namespace Zen
{
    public class Collider : Component, ITransformListener
    {
        public Vector2[] Vertices;
        bool _boundsInitialized;
        public RectangleF Bounds => new RectangleF(0, 0, 100, 200);

        void SetVertices(RectangleF bounds)
        {
            Vertices = new Vector2[4];
            Vertices[0] = new Vector2(bounds.Left, bounds.Top);
            Vertices[1] = new Vector2(bounds.Left, bounds.Bottom);
            Vertices[2] = new Vector2(bounds.Right, bounds.Bottom);
            Vertices[3] = new Vector2(bounds.Right, bounds.Top);
        }

        public Collider(RectangleF bounds)
        {
            SetVertices(bounds);
            _boundsInitialized = true;
        }

        public Collider() { }

        public override void Mount()
        {
            if (!_boundsInitialized)
            {
                var renderer = GetComponent<SpriteRenderer>();
                System.Diagnostics.Debug.Assert(renderer != null, "No renderer found for default Collider");
                SetVertices(new RectangleF(Transform.Position.X - renderer.Origin.X, Transform.Position.Y - renderer.Origin.Y, renderer.Sprite.SourceRect.Width, renderer.Sprite.SourceRect.Height));
                _boundsInitialized = true;
            }

            UpdateVertices();

            Physics.RegisterCollider(this);
        }

        public void TransformUpdated()
        {
            UpdateVertices();
        }

        void UpdateVertices()
        {
            foreach (Vector2 vertex in Vertices)
                Vector2.Transform(vertex, Transform.Matrix);
        }
    }
}