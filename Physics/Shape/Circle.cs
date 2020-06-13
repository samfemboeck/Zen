using Microsoft.Xna.Framework;
using Zen.Util;

namespace Zen
{
    public class Circle
    {
        public Vector2 GlobalCenter;
        public Vector2 LocalCenter;

        public float GlobalRadius;
        public float LocalRadius;

        public RectangleF BroadphaseBounds;

        Transform transform;

        public Circle(Vector2 center, float radius, Transform transform)
        {
            LocalCenter = center;
            GlobalCenter = Vector2.Transform(center, transform.TransformMatrix);
            LocalRadius = radius;
            GlobalRadius = radius * transform.Scale;
            this.transform = transform;
            BroadphaseBounds = GetBroadphaseBounds();
        }

        public void Update(float scale)
        {
            GlobalRadius = LocalRadius * scale;
            GlobalCenter = Vector2.Transform(LocalCenter, transform.TransformMatrix);
            BroadphaseBounds = GetBroadphaseBounds();
        }

        RectangleF GetBroadphaseBounds() => new RectangleF(GlobalCenter.X - GlobalRadius, GlobalCenter.Y - GlobalRadius, 2 * GlobalRadius, 2 * GlobalRadius);

        public bool IntersectsCircle(Circle other)
        {
            Vector2 direction = other.GlobalCenter - GlobalCenter;
            return direction.Length() - other.GlobalRadius <= GlobalRadius;
        }

        public bool EncasesVertices(Vector2[] vertices)
        {
            foreach (Vector2 vertex in vertices)
            {
                Vector2 direction = vertex - GlobalCenter;
                if (direction.Length() > GlobalRadius)
                    return false;
            }

            return true;
        }

        public bool EncasesCircle(Circle other) => Vector2.Distance(other.GlobalCenter, GlobalCenter) + other.GlobalRadius <= GlobalRadius;
    }
}