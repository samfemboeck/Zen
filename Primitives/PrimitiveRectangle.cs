using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.Util;

namespace Zen
{
    public class PrimitiveRectangle : PrimitiveShape
    {
        readonly short[] Indices = { 0, 2, 1, 0, 3, 2 };

        protected override int NumPrimitives => 2;

        public void SetRectangle(RectangleF rect, Color color)
        {
            Vector3[] vertices = new Vector3[] { new Vector3(rect.Left, rect.Top, 0), new Vector3(rect.Right, rect.Top, 0), new Vector3(rect.Right, rect.Bottom, 0), new Vector3(rect.Left, rect.Bottom, 0) };
            SetVertexPositionColor(new Color[] { color, color, color, color }, vertices, Indices);
        }

        public PrimitiveRectangle(GraphicsDevice device, RectangleF rect, Color color) : base(device)
        {
            SetRectangle(rect, color);
        }
    }
}