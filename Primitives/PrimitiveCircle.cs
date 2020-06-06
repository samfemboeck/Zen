using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zen
{

    public class PrimitiveCircle : PrimitiveShape
    {
        int _numRadialSegments;
        protected override int NumPrimitives => _numRadialSegments;

        public PrimitiveCircle(GraphicsDevice device, Vector2 center, float radius, int numRadialSegments, Color color) : base(device)
        {
            SetCircle(center, radius, numRadialSegments, color);
        }

        public void SetCircle(Vector2 center, float radius, int numRadialSegments, Color color)
        {
            _numRadialSegments = numRadialSegments;
            float angleBetweenSegments = MathHelper.ToRadians(360 / (float)numRadialSegments);

            Vector3[] vertices = new Vector3[numRadialSegments + 1];
            Vector3 center3 = new Vector3(center, 0);
            vertices[0] = center3;

            Vector3 radialSegmentVertex = new Vector3(Vector2.Zero + Vector2.UnitY * radius, 0);
            vertices[1] = radialSegmentVertex + center3;

            for (int i = 1; i < numRadialSegments; i++)
            {
                Matrix rotation = Matrix.CreateRotationZ(-angleBetweenSegments * i);
                vertices[1 + i] = Vector3.Transform(radialSegmentVertex, rotation) + center3;
            }

            short[] indices = new short[3 * numRadialSegments];

            for (int i = 0; i < numRadialSegments; i++)
            {
                indices[3 * i] = 0;
                indices[3 * i + 1] = (short)(i + 1);
                indices[3 * i + 2] = (short)(i + 2);
            }

            indices[indices.Length - 1] = 1;

            Color[] colors = Enumerable.Repeat<Color>(Color.Red, vertices.Length).ToArray();

            SetVertexPositionColor(colors, vertices, indices);
        }
    }
}