using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zen
{
    public class PrimitiveCircleBorder : PrimitiveShape
    {
        short _numRadialSegments;
        protected override int NumPrimitives => _numRadialSegments * 2;

        public PrimitiveCircleBorder(GraphicsDevice device, Vector2 center, float radius, short numRadialSegments, float borderSize, Color color) : base(device)
        {
            SetCircle(center, radius, numRadialSegments, borderSize, color);
        }

        public void SetCircle(Vector2 center, float radius, short numRadialSegments, float borderSize, Color color)
        {
            _numRadialSegments = numRadialSegments;
            float angleBetweenSegments = MathHelper.ToRadians(360 / (float)numRadialSegments);

            Vector3[] vertices = new Vector3[2 * (numRadialSegments)];
            Vector3 center3 = new Vector3(center, 0);

            Vector3 radialSegmentVertex = new Vector3(Vector2.Zero + Vector2.UnitY * radius, 0);
            Vector3 radialSegmentVertexLower = radialSegmentVertex - new Vector3(0, borderSize, 0);
            vertices[0] = radialSegmentVertex + center3;
            vertices[numRadialSegments] = radialSegmentVertexLower + center3;

            for (int i = 1; i < numRadialSegments; i++)
            {
                Matrix rotation = Matrix.CreateRotationZ(-angleBetweenSegments * i);
                vertices[i] = Vector3.Transform(radialSegmentVertex, rotation) + center3;
                vertices[numRadialSegments + i] = Vector3.Transform(radialSegmentVertexLower, rotation) + center3;
            }

            short[] indices = new short[3 * 2 * numRadialSegments];

            for (short i = 0; i < numRadialSegments; i++)
            {
                indices[6 * i] = i;
                indices[6 * i + 1] = (short)(numRadialSegments + i);
                indices[6 * i + 2] = (short)(numRadialSegments + i + 1);
                
                indices[6 * i + 3] = i;
                indices[6 * i + 4] = (short)(numRadialSegments + i + 1);
                indices[6 * i + 5] = (short)(i + 1);
            }

            indices[indices.Length - 1] = 0;
            indices[indices.Length - 2] = numRadialSegments;
            indices[indices.Length - 4] = numRadialSegments;

            Color[] colors = Enumerable.Repeat<Color>(Color.Red, vertices.Length).ToArray();

            SetVertexPositionColor(colors, vertices, indices);
        }
    }
}