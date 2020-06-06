using System;
using Microsoft.Xna.Framework;

namespace Zen
{
    public static class RectangleEx
    {
		public static void Union(ref Rectangle value1, ref Rectangle value2, out Rectangle result)
		{
			result.X = Math.Min(value1.X, value2.X);
			result.Y = Math.Min(value1.Y, value2.Y);
			result.Width = Math.Max(value1.Right, value2.Right) - result.X;
			result.Height = Math.Max(value1.Bottom, value2.Bottom) - result.Y;
		}

		public static void Union(ref Rectangle first, ref Point point, out Rectangle result)
		{
			var rect = new Rectangle(point.X, point.Y, 0, 0);
			Union(ref first, ref rect, out result);
		}
    }
}